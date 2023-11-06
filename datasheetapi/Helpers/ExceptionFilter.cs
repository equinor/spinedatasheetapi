using datasheetapi.Exceptions;

using Microsoft.AspNetCore.Mvc.Filters;

namespace datasheetapi.Helpers;
public class ExceptionFilter : IActionFilter, IOrderedFilter
{
    public int Order => int.MaxValue - 10;

    private readonly ILogger<ExceptionFilter> _logger;

    public ExceptionFilter(ILogger<ExceptionFilter> logger)
    {
        _logger = logger;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        // Do nothing while executing.
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {

        if (context.Exception is NotFoundException notFoundException)
        {
            context.Result = new ObjectResult(AssembleErrorDto(notFoundException.Value))
            {
                StatusCode = StatusCodes.Status404NotFound
            };
            context.ExceptionHandled = true;
        }
        else if (context.Exception is BadRequestException badRequestException)
        {
            context.Result = new ObjectResult(AssembleErrorDto(badRequestException.Value))
            {
                StatusCode = StatusCodes.Status400BadRequest
            };
            context.ExceptionHandled = true;
        }
        else if (context.Exception is ConflictException conflictException)
        {
            context.Result = new ObjectResult(AssembleErrorDto(conflictException.Value))
            {
                StatusCode = StatusCodes.Status409Conflict
            };
            context.ExceptionHandled = true;
        }
        else if (context.Exception is Exception exception)
        {
            _logger.LogError(exception, "Error while preforming the action");

            context.Result = new ObjectResult(AssembleErrorDto("An error occurred while processing your request."))
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
            context.ExceptionHandled = true;
        }
    }

    private static ErrorDto AssembleErrorDto(object message)
    {
        return new()
        {
            Message = message,
        };
    }
}
