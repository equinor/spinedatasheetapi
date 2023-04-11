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
                TagNo = "Tag001",
                Area = "Area 1",
                Category = "",
                Description = "Flow Transmitter Coriolis",
                Dicipline = "Instrumentation",
                SupplierOfferedProduct = new()
                {
                    Manufacturer = "KROHNE",
                    ModelNumber = "OPTIMASS 7400C/EX-I H15",
                    EquipmentManufacturerSerialNumber = "G19000000332s964",
                    MinimumAmbientTemperature = null,
                    MaximumAmbientTemperature = null,
                    PressureRetainingBoltMaterial = "IS109",
                    PressureRetainingNutMaterial = "IS109",
                    SILRating = "2",
                    IngressProtection = "IP67",
                    ExProtection = "Exd",
                    HazardousAreaClassificationStandard = "IEC 60079",
                    ExplosionHazardClassification = "Zone 1",
                    ExplosionGroup = "IIC",
                    TemperatureClass = "T6",
                    BodyElementSensorManufacturerModelNumber = "PTIMASS 1400C S40",
                    BodyMaterial = "SS316L",
                    CoriolisTubeMaterial = "Duplex UNS 31803 ASTM A789",
                    CoriolisTubeType = "straight",
                    FlangeMaterial = "Duplex UNS 31803 ASTM A789",
                    LinerMaterial = null,
                    CoilCoverMaterial = null,
                    JunctionBoxMaterial = null,
                    ElectrodeType = null,
                    ElectrodeMaterial = null,
                    MeterMinimumConductivity = null,
                    GroundingRing = true,
                    GroundingRingMaterial = "Copper",
                    LiningProtector = "Polyethylene",
                    BodySize = 3,
                    EndConnectionSize = 3,
                    EndConnectionFlangeType = "ASME B16.5 RF",
                    EndConnectionFlangeRating = "CI-150",
                    CoriolisOuterCasingMaterial = "SS316L",
                    CoriolisOuterCasingBurstPressure = null,
                    RuptureDiscBurstPressure = null,
                    ProcessSecondaryContainment = "fully rated",
                    TransmitterModelNumber = "MFC 400 C Ex",
                    TransmitterEnclosureMaterial = "SS316L",
                    TransmitterMounting = "Integrated",
                    TransmitterDisplay = "integral LCD display",
                    TransmitterConnectingCables = "no",
                    TransmitterConnectingCableLength = null,
                    TransmitterConnectingCableQuantity = null,
                    SupplyVoltage = "24 V DC loop power",
                    ExternalPowerVoltage = null,
                    IsolatedOutputs = "not applicable",
                    CableEntry = "M20 x 1.5",
                    CableTermination = "Screwed",
                    CommunicationProtocol = "4-20mA HART",
                    ProtocolVersion = "Version 7",
                    FailSafeDirection = "fail low (NAMUR)",
                    CalibrationBespoke = null,
                    MeasurementRangeMinimum = 0,
                    MeasurementRangeMaximum = 24900,
                    SpanAndZeroAdjustment = "both",
                    LowerRangeLimit = null,
                    UpperRangeLimit = null,
                    Accuracy = "+/- 0.1",
                    Repeatability = 0.05,
                    StepResponse = null,
                    LongTermDrift = null,
                    LongTermStability = "+/- 0.015",
                    Vibration = null,
                    WeatherEnclosure = "no",
                    MountingBracket = "yes",
                    MountingBracketMaterial = "316 SS",
                    ElectricalSurgeProtector = "no",
                    Sunshade = "no",
                    CodeRequirement = 0,
                    ConformityAssessmentSystemLevel = 0,
                    TagNumber = null,
                    ServiceDescription = null,
                    ProjectCountry = null,
                    ProjectRegion = null,
                    PlantEnvironmentalLocation = null,
                    PIDNumber = null,
                    LineOrEquipmentNumber = null,
                    BaseConditions = null,
                    BaseTemperature = null,
                    BasePressure = null,
                    CoatingDurability = null,
                    UpstreamHighSidePipeSchedule = null,
                    UpstreamHighSideLineSize = null,
                    UpstreamHighSideLineEquipmentRating = null,
                    UpstreamHighSideLineConnectionType = null,
                    UpstreamHighSideConnectionOrientation = null,
                    UpstreamHighSideMaterialType = null,
                    DesignPressureMaximum = null,
                    DesignPressureMinimum = null,
                    DesignTemperatureMaximum = null,
                    DesignTemperatureMinimum = null,
                    SourServiceSpecification = null,
                    ProcessFluids = null,
                    ProcessFluidState = null,
                    ServiceDescription2 = null,
                    ProcessFluidCorrosiveCompounds = null,
                    ProcessFluidErosionPossibility = null,
                    ProcessFluidConductivity = null,
                    ProcessFluidSpecificHeatRatio = null,
                    ProcessVacuumPossibility = null,
                    ProcessFluidVapourPressure = null,
                    MinimumOperatingVolumetricFlow = null,
                    MinimumOperatingVelocity = null,
                    MinimumOperatingTemperature = null,
                    MinimumOperatingPressure = null,
                    NormalOperatingVolumetricFlow = null,
                    NormalOperatingVelocity = null,
                    NormalOperatingTemperature = null,
                    NormalOperatingPressure = null,
                    NormalOperatingLiquidSpecificGravity = null,
                    NormalOperatingLiquidViscosity = null,
                    MaximumOperatingVolumetricFlow = null,
                    MaximumOperatingVelocity = null,
                    MaximumOperatingTemperature = null,
                    MaximumOperatingPressure = null,
                    MaximumOperatingLiquidSpecificGravity = null,
                    MaximumOperatingLiquidViscosity = null,
                    MaximumOperatingVapourMolecularWeight = null,
                    MaximumOperatingVapourCompressibilityFactor = null,
                    MaximumOperatingVapourActualDensity = null,
                    MaximumOperatingVapourViscosity = null,
                    MaximumRecoverablePressureDrop = null,
                    MaximumUnrecoverablePressureLoss = "0.7",
                },
                PurchaserRequirement = new()
                {
                    Manufacturer = "KROHNE",
                    ModelNumber = null,
                    EquipmentManufacturerSerialNumber = "NO",
                    MinimumAmbientTemperature = 25,
                    MaximumAmbientTemperature = -8,
                    PressureRetainingBoltMaterial = null,
                    PressureRetainingNutMaterial = null,
                    SILRating = "not applicable",
                    IngressProtection = "IP44",
                    ExProtection = "Exia",
                    HazardousAreaClassificationStandard = "IEC 60079",
                    ExplosionHazardClassification = "Zone 2",
                    ExplosionGroup = "IIC",
                    TemperatureClass = "T3",
                    BodyElementSensorManufacturerModelNumber = null,
                    BodyMaterial = "SS316L",
                    CoriolisTubeMaterial = null,
                    CoriolisTubeType = null,
                    FlangeMaterial = null,
                    LinerMaterial = null,
                    CoilCoverMaterial = null,
                    JunctionBoxMaterial = null,
                    ElectrodeType = null,
                    ElectrodeMaterial = null,
                    MeterMinimumConductivity = null,
                    GroundingRing = true,
                    GroundingRingMaterial = "Copper",
                    LiningProtector = "Polyethylene",
                    BodySize = null,
                    EndConnectionSize = null,
                    EndConnectionFlangeType = null,
                    EndConnectionFlangeRating = null,
                    CoriolisOuterCasingMaterial = "SS316L",
                    CoriolisOuterCasingBurstPressure = null,
                    RuptureDiscBurstPressure = null,
                    ProcessSecondaryContainment = "fully rated",
                    TransmitterModelNumber = null,
                    TransmitterEnclosureMaterial = null,
                    TransmitterMounting = "Integrated",
                    TransmitterDisplay = "Integral LCD display",
                    TransmitterConnectingCables = null,
                    TransmitterConnectingCableLength = null,
                    TransmitterConnectingCableQuantity = null,
                    SupplyVoltage = "24 V DC loop power",
                    ExternalPowerVoltage = null,
                    IsolatedOutputs = "not applicable",
                    CableEntry = "M20 x 1.5",
                    CableTermination = "Spring load",
                    CommunicationProtocol = "4-20mA HART",
                    ProtocolVersion = "Version 7",
                    FailSafeDirection = "fail low (NAMUR)",
                    CalibrationBespoke = null,
                    MeasurementRangeMinimum = 0,
                    MeasurementRangeMaximum = 24000,
                    SpanAndZeroAdjustment = "both",
                    LowerRangeLimit = null,
                    UpperRangeLimit = null,
                    Accuracy = "+/- 1",
                    Repeatability = null,
                    StepResponse = null,
                    LongTermDrift = null,
                    LongTermStability = null,
                    Vibration = null,
                    WeatherEnclosure = "no",
                    MountingBracket = "no",
                    MountingBracketMaterial = null,
                    ElectricalSurgeProtector = "no",
                    Sunshade = "no",
                    CodeRequirement = 0,
                    ConformityAssessmentSystemLevel = 0,
                    TagNumber = null,
                    ServiceDescription = null,
                    ProjectCountry = "NO",
                    ProjectRegion = "Norwegian Continental Shelf",
                    PlantEnvironmentalLocation = "offshore",
                    PIDNumber = "C-156-KR-P-XB-62010-01",
                    LineOrEquipmentNumber = "L-62L00003A-0300OF-AD120",
                    BaseConditions = "normal",
                    BaseTemperature = 20,
                    BasePressure = 1,
                    CoatingDurability = "high",
                    UpstreamHighSidePipeSchedule = "20",
                    UpstreamHighSideLineSize = "3",
                    UpstreamHighSideLineEquipmentRating = "Cl-150",
                    UpstreamHighSideLineConnectionType = "RF",
                    UpstreamHighSideConnectionOrientation = "Vertical",
                    UpstreamHighSideMaterialType = "Duplex SS",
                    DesignPressureMaximum = 22,
                    DesignPressureMinimum = -3,
                    DesignTemperatureMaximum = 50,
                    DesignTemperatureMinimum = -10,
                    SourServiceSpecification = "not applicable",
                    ProcessFluids = "Diesel",
                    ProcessFluidState = "liquid",
                    ServiceDescription2 = "Diesel Filter Coalescer Package Outlet",
                    ProcessFluidCorrosiveCompounds = "None",
                    ProcessFluidErosionPossibility = "no",
                    ProcessFluidConductivity = null,
                    ProcessFluidSpecificHeatRatio = 830,
                    ProcessVacuumPossibility = false,
                    ProcessFluidVapourPressure = "N/A",
                    MinimumOperatingVolumetricFlow = 0,
                    MinimumOperatingVelocity = 0,
                    MinimumOperatingTemperature = -3,
                    MinimumOperatingPressure = 1,
                    NormalOperatingVolumetricFlow = 14110,
                    NormalOperatingVelocity = 0.9,
                    NormalOperatingTemperature = 20,
                    NormalOperatingPressure = 14.5,
                    NormalOperatingLiquidSpecificGravity = 0.83,
                    NormalOperatingLiquidViscosity = 3.4,
                    MaximumOperatingVolumetricFlow = "24000",
                    MaximumOperatingVelocity = "N/A",
                    MaximumOperatingTemperature = "N/A",
                    MaximumOperatingPressure = "N/A",
                    MaximumOperatingLiquidSpecificGravity = "N/A",
                    MaximumOperatingLiquidViscosity = "N/A",
                    MaximumOperatingVapourMolecularWeight = "N/A",
                    MaximumOperatingVapourCompressibilityFactor = "N/A",
                    MaximumOperatingVapourActualDensity = "N/A",
                    MaximumOperatingVapourViscosity = "N/A",
                    MaximumRecoverablePressureDrop = "N/A",
                    MaximumUnrecoverablePressureLoss = "1",
                },
            },
            new()
            {
                Id = new Guid("0db99855-f5e4-40dd-b4c3-da201ee89ff9"),
                ProjectId = default,
                TagNo = "TAG-456",
                Area = "Area 1",
                Category = "Category 2",
                Description = "Description 2",
                Dicipline = "Dicipline 2",
                SupplierOfferedProduct = new()
                {

                },
                PurchaserRequirement = new()
                {

                },
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
