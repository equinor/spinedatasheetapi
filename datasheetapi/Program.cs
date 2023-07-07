using System.Text.Json.Serialization;

using datasheetapi.Repositories;

using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Web;

using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Host.ConfigureAppConfiguration(config =>
{
    config.AddJsonFile("appsettings.json");
});
var azureAppConfigurationConnectionString =
    builder.Configuration.GetSection("AppConfiguration").GetValue<string>("ConnectionString");
var environment = builder.Configuration.GetSection("AppConfiguration").GetValue<string>("Environment");
var appInsightTelemetryOptions = new ApplicationInsightsServiceOptions
{
    ConnectionString = builder.Configuration.GetSection("ApplicationInsights").GetValue<string>("ConnectionString")
};
Console.WriteLine("Loading configuration for: " + environment);

var configurationBuilder = new ConfigurationBuilder()
    .AddAzureAppConfiguration(options =>
    {
        options.Connect(azureAppConfigurationConnectionString).ConfigureKeyVault(x =>
                x.SetCredential(new DefaultAzureCredential(new DefaultAzureCredentialOptions
                { ExcludeSharedTokenCacheCredential = true })))
            .Select(KeyFilter.Any).Select(KeyFilter.Any, environment);
    });

var config = configurationBuilder.Build();
builder.Configuration.AddConfiguration(config);
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));


// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
builder.Services.AddEndpointsApiExplorer();

// Set up CORS
var _accessControlPolicyName = "AllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(_accessControlPolicyName,
        builder =>
        {
            builder.AllowAnyHeader();
            builder.AllowAnyMethod();
            builder.WithExposedHeaders("Location");
            builder.WithOrigins(
                "http://localhost:3000",
                "https://fusion.equinor.com",
                "https://pro-s-portal-ci.azurewebsites.net",
                "https://pro-s-portal-fqa.azurewebsites.net",
                "https://pro-s-portal-fprd.azurewebsites.net",
                "https://fusion-s-portal-ci.azurewebsites.net",
                "https://fusion-s-portal-fqa.azurewebsites.net",
                "https://fusion-s-portal-fprd.azurewebsites.net",
                "https://pr-3422.fusion-dev.net",
                "https://pr-*.fusion-dev.net"
            ).SetIsOriginAllowedToAllowWildcardSubdomains();
        });
});


builder.Services.AddFusionIntegration(options =>
{
    var fusionEnvironment = environment switch
    {
        "dev" => "CI",
        "qa" => "FQA",
        "prod" => "FPRD",
        "radix-prod" => "FPRD",
        "radix-qa" => "FQA",
        "radix-dev" => "CI",
        _ => "CI",
    };

    Console.WriteLine("Fusion environment: " + fusionEnvironment);
    options.UseServiceInformation("Spine Datasheet", fusionEnvironment);
    options.UseDefaultEndpointResolver(fusionEnvironment);
    options.UseDefaultTokenProvider(opts =>
    {
        opts.ClientId = builder.Configuration.GetSection("AzureAd").GetValue<string>("ClientId");
        opts.ClientSecret = builder.Configuration.GetSection("AzureAd").GetValue<string>("ClientSecret");
    });
    options.AddFusionRoles();
    options.ApplicationMode = true;
});

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .ReadFrom.Configuration(config)
    .Enrich.WithMachineName()
    .Enrich.WithProperty("Environment", environment)
    .Enrich.FromLogContext()
    .CreateBootstrapLogger();

builder.Services.AddApplicationInsightsTelemetry(appInsightTelemetryOptions);
builder.Services.AddScoped<ITagDataService, TagDataService>();
builder.Services.AddScoped<IContractService, ContractService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<TagDataReviewService>();
builder.Services.AddScoped<RevisionContainerReviewService>();
builder.Services.AddScoped<IFusionService, FusionService>();
builder.Services.AddScoped<IProjectService, ProjectService>();

builder.Services.AddSingleton<IFAMService, DummyFAMService>();
builder.Services.AddSingleton<IProjectRepository, DummyProjectRepository>();
builder.Services.AddSingleton<IContractRepository, DummyContractRepository>();
builder.Services.AddSingleton<ICommentRepository, DummyCommentRepository>();
builder.Services.AddSingleton<ITagDataReviewRepository, DummyTagDataReviewRepository>();
builder.Services.AddSingleton<IRevisionContainerRepository, DummyRevisionContainerRepository>();
builder.Services.AddSingleton<IRevisionContainerReviewRepository, DummyRevisionContainerReviewRepository>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<IAuthorizationHandler, ApplicationRoleAuthorizationHandler>();
builder.Services.AddSingleton<IAuthorizationPolicyProvider, ApplicationRolePolicyProvider>();
builder.Services.AddSingleton<IAzureUserCacheService, AzureUserCacheService>();
builder.Services.Configure<IConfiguration>(builder.Configuration);

//Swagger config
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Spine Datasheet Api",
        Version = "v1",
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description =
            "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer",
                },
            },
            Array.Empty<string>()
        },
    });
}
);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(_accessControlPolicyName);

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ClaimsMiddelware>();
app.UseMiddleware<RequestLoggingMiddleware>();

app.MapControllers();

app.Run();
