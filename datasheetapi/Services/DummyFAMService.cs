using api.Services;

using datasheetapi.Models;

namespace datasheetapi.Services;

public class DummyFAMService : IDummyFAMService
{
    private readonly ILogger<DummyFAMService> _logger;

    private readonly List<Datasheet> _datasheets;

    public DummyFAMService(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<DummyFAMService>();
        _datasheets = InitializeDatasheets();
    }

    private static List<Datasheet> InitializeDatasheets()
    {
        return new List<Datasheet> {
            new()
            {
                Id = new Guid("8e3c9263-2680-494c-842f-d8bda44cea41"),
                TagNo = "FAM-001",
                Area = "Area 1",
                Category = "Category 1",
                Description = "Description 1",
                Dicipline = "Dicipline 1",
                SupplierOfferedProduct = new()
                {
                    Manufacturer = "Acme Corp",
                    ModelNumber = "1234",
                    EquipmentManufacturerSerialNumber = "5678",
                    MinimumAmbientTemperature = -10,
                    MaximumAmbientTemperature = 40,
                    PressureRetainingBoltMaterial = "Stainless Steel",
                    PressureRetainingNutMaterial = "Brass",
                    SILRating = "SIL 2",
                    IngressProtection = "IP67",
                    ExProtection = "Ex d",
                    HazardousAreaClassificationStandard = "ATEX",
                    ExplosionHazardClassification = "Zone 1",
                    ExplosionGroup = "IIA",
                    TemperatureClass = "T3",
                    MaximumUnrecoverablePressureLoss = 1.2,
                    BodyElementSensorManufacturerModelNumber = "XYZ Sensor",
                    BodyMaterial = "Carbon Steel",
                    FlangeMaterial = "Stainless Steel",
                    LinerMaterial = "PTFE",
                    CoilCoverMaterial = "Aluminum",
                    JunctionBoxMaterial = "GRP",
                    ElectrodeType = "Insertion",
                    ElectrodeMaterial = "316 Stainless Steel",
                    MeterMinimumConductivity = 50.0,
                    GroundingRing = true,
                    GroundingRingMaterial = "Copper",
                    LiningProtector = "Polyethylene",
                    BodySize = 4.0,
                    EndConnectionSize = 2.0,
                    EndConnectionFlangeType = "Raised Face",
                    EndConnectionFlangeRating = 150.0,
                    TransmitterModelNumber = "ABC Transmitter",
                    TransmitterEnclosureMaterial = "GRP",
                    TransmitterMounting = "Remote",
                    TransmitterDisplay = true,
                    TransmitterConnectingCables = 3,
                    TransmitterConnectingCableLength = 10.0,
                    TransmitterConnectingCableQuantity = 2,
                    SupplyVoltage = 24.0,
                    ExternalPowerVoltage = 220.0,
                    IsolatedOutputs = true,
                    CableEntry = "M20",
                    CableTermination = "Flying Leads",
                    CommunicationProtocol = "Modbus RTU",
                    ProtocolVersion = "1.0",
                    FailSafeDirection = "Low",
                    CalibrationBespoke = true,
                    MeasurementRangeMinimum = 0.0,
                    MeasurementRangeMaximum = 1000.0,
                    SpanAndZeroAdjustment = "Manual",
                    LowerRangeLimit = 10.0,
                    UpperRangeLimit = 990.0,
                    Accuracy = 0.5,
                    Repeatability = 0.2,
                    StepResponse = 0.1,
                    LongTermDrift = 0.1,
                    LongTermStability = 0.2,
                    Vibration = true,
                    WeatherEnclosure = true,
                    ElectricalSurgeProtector = true,
                    Sunshade = true,

                },
                PurchaserRequirement = new()
                {
                    CodeRequirement = 123,
                    ConformityAssessmentSystemLevel = 2,
                    TagNumber = "TAG-456",
                    ServiceDescription = "Service A",
                    EquipmentManufacturerSerialNumber = "EQUIP-789",
                    ProjectCountry = "USA",
                    ProjectRegion = "West",
                    PlantEnvironmentalLocation = "Zone 1",
                    PIDNumber = "PID-101",
                    LineOrEquipmentNumber = "Line 1",
                    MinimumAmbientTemperature = -10,
                    MaximumAmbientTemperature = 40,
                    BaseConditions = "Standard",
                    BaseTemperature = 25,
                    BasePressure = 100,
                    CoatingDurability = "High",
                    SILRating = "SIL 2",
                    IngressProtection = "IP65",
                    ExProtection = "Ex d",
                    HazardousAreaClassificationStandard = "NEC",
                    ExplosionHazardClassification = "Class I",
                    ExplosionGroup = "Group D",
                    TemperatureClass = "T3",
                    UpstreamHighSidePipeSchedule = "40",
                    UpstreamHighSideLineSize = "6\"",
                    UpstreamHighSideLineEquipmentRating = "ANSI 300",
                    UpstreamHighSideLineConnectionType = "Flange",
                    UpstreamHighSideConnectionOrientation = "Horizontal",
                    UpstreamHighSideMaterialType = "Carbon steel",
                    DesignPressureMaximum = 150,
                    DesignPressureMinimum = 50,
                    DesignTemperatureMaximum = 200,
                    DesignTemperatureMinimum = -20,
                    SourServiceSpecification = "NACE MR0175",
                    ProcessFluids = "Water, oil",
                    ProcessFluidState = "Liquid",
                    ServiceDescription2 = "Service B",
                    ProcessFluidCorrosiveCompounds = "H2S",
                    ProcessFluidErosionPossibility = "Low",
                    ProcessFluidConductivity = 1000,
                    ProcessFluidSpecificHeatRatio = 0.8,
                    ProcessVacuumPossibility = true,
                    MinimumOperatingVolumetricFlow = 10,
                    MinimumOperatingVelocity = 1,
                    MinimumOperatingTemperature = -5,
                    MinimumOperatingPressure = 10,
                    NormalOperatingVolumetricFlow = 20,
                    NormalOperatingVelocity = 2,
                    NormalOperatingTemperature = 20,
                    NormalOperatingPressure = 50,
                    NormalOperatingLiquidViscosity = 1,
                    MaximumOperatingVolumetricFlow = 30,
                    MaximumOperatingVelocity = 3,
                    MaximumOperatingTemperature = 60,
                    MaximumOperatingPressure = 100,
                    MaximumOperatingLiquidViscosity = 2,
                    MaximumRecoverablePressureDrop = 10,
                    MaximumUnrecoverablePressureLoss = 5,
                    BodyMaterial = "Stainless steel",
                    TransmitterMounting = "Bracket",
                    TransmitterDisplay = true,
                    TransmitterConnectingCableLength = 5.0,
                    SupplyVoltage = 24.0,
                    ExternalPowerVoltage = 120.0,
                    CableEntry = "Threaded",
                    CableTermination = "Terminal block",
                    CommunicationProtocol = "Modbus RTU",
                    ProtocolVersion = "1.0",
                    FailSafeDirection = "High",
                    CalibrationBespoke = true,
                    MeasurementRangeMinimum = 0.1,
                    MeasurementRangeMaximum = 1000.0,
                    SpanAndZeroAdjustment = "auto",
                    Accuracy = 0.01,
                    Repeatability = 0.001,
                    StepResponse = 0.5,
                    LongTermDrift = 0.005,
                    LongTermStability = 0,
                },
            },
            new()
            {
                Id = new Guid("0db99855-f5e4-40dd-b4c3-da201ee89ff9"),
                TagNo = "TAG-456",
                Area = "Area 1",
                Category = "Category 2",
                Description = "Description 2",
                Dicipline = "Dicipline 2",
                SupplierOfferedProduct = new(),
                PurchaserRequirement = new(),
            },
        };
    }

    public async Task<Datasheet?> GetDatasheet(Guid id)
    {
        return await Task.Run(() => _datasheets.Find(d => d.Id == id));
    }

    public async Task<List<Datasheet>> GetDatasheets()
    {
        return await Task.Run(() => _datasheets);
    }

    public async Task<List<Datasheet>> GetDatasheetsForProject(Guid projectId)
    {
        return await Task.Run(() => _datasheets);
    }
}
