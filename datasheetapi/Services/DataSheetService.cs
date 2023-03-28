namespace datasheetapi.Services;

public class DatasheetService : IDatasheetService
{
    private readonly List<Datasheet> _dataSheets;

    public DatasheetService()
    {
        _dataSheets = new List<Datasheet>
        {
            new()
            {
                Id = Guid.NewGuid(),
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
                Id = Guid.NewGuid(), SupplierOfferedProduct = new()
                {
                    Manufacturer = null,
                    ModelNumber = null,
                    EquipmentManufacturerSerialNumber = null,
                    MinimumAmbientTemperature = 0,
                    MaximumAmbientTemperature = 0,
                    PressureRetainingBoltMaterial = null,
                    PressureRetainingNutMaterial = null,
                    SILRating = null,
                    IngressProtection = null,
                    ExProtection = null,
                    HazardousAreaClassificationStandard = null,
                    ExplosionHazardClassification = null,
                    ExplosionGroup = null,
                    TemperatureClass = null,
                    MaximumUnrecoverablePressureLoss = 0,
                    BodyElementSensorManufacturerModelNumber = null,
                    BodyMaterial = null,
                    FlangeMaterial = null,
                    LinerMaterial = null,
                    CoilCoverMaterial = null,
                    JunctionBoxMaterial = null,
                    ElectrodeType = null,
                    ElectrodeMaterial = null,
                    MeterMinimumConductivity = 0,
                    GroundingRing = false,
                    GroundingRingMaterial = null,
                    LiningProtector = null,
                    BodySize = 0,
                    EndConnectionSize = 0,
                    EndConnectionFlangeType = null,
                    EndConnectionFlangeRating = 0,
                    TransmitterModelNumber = null,
                    TransmitterEnclosureMaterial = null,
                    TransmitterMounting = null,
                    TransmitterDisplay = false,
                    TransmitterConnectingCables = 0,
                    TransmitterConnectingCableLength = 0,
                    TransmitterConnectingCableQuantity = 0,
                    SupplyVoltage = 0,
                    ExternalPowerVoltage = 0,
                    IsolatedOutputs = false,
                    CableEntry = null,
                    CableTermination = null,
                    CommunicationProtocol = null,
                    ProtocolVersion = null,
                    FailSafeDirection = null,
                    CalibrationBespoke = false,
                    MeasurementRangeMinimum = 0,
                    MeasurementRangeMaximum = 0,
                    SpanAndZeroAdjustment = null,
                    LowerRangeLimit = 0,
                    UpperRangeLimit = 0,
                    Accuracy = 0,
                    Repeatability = 0,
                    StepResponse = 0,
                    LongTermDrift = 0,
                    LongTermStability = 0,
                    Vibration = false,
                    WeatherEnclosure = false,
                    ElectricalSurgeProtector = false,
                    Sunshade = false,
                },
                PurchaserRequirement = new()
                {
                    CodeRequirement = 0,
                    ConformityAssessmentSystemLevel = 0,
                    TagNumber = null,
                    ServiceDescription = null,
                    EquipmentManufacturerSerialNumber = null,
                    ProjectCountry = null,
                    ProjectRegion = null,
                    PlantEnvironmentalLocation = null,
                    PIDNumber = null,
                    LineOrEquipmentNumber = null,
                    MinimumAmbientTemperature = 0,
                    MaximumAmbientTemperature = 0,
                    BaseConditions = null,
                    BaseTemperature = 0,
                    BasePressure = 0,
                    CoatingDurability = null,
                    SILRating = null,
                    IngressProtection = null,
                    ExProtection = null,
                    HazardousAreaClassificationStandard = null,
                    ExplosionHazardClassification = null,
                    ExplosionGroup = null,
                    TemperatureClass = null,
                    UpstreamHighSidePipeSchedule = null,
                    UpstreamHighSideLineSize = null,
                    UpstreamHighSideLineEquipmentRating = null,
                    UpstreamHighSideLineConnectionType = null,
                    UpstreamHighSideConnectionOrientation = null,
                    UpstreamHighSideMaterialType = null,
                    DesignPressureMaximum = 0,
                    DesignPressureMinimum = 0,
                    DesignTemperatureMaximum = 0,
                    DesignTemperatureMinimum = 0,
                    SourServiceSpecification = null,
                    ProcessFluids = null,
                    ProcessFluidState = null,
                    ServiceDescription2 = null,
                    ProcessFluidCorrosiveCompounds = null,
                    ProcessFluidErosionPossibility = null,
                    ProcessFluidConductivity = 0,
                    ProcessFluidSpecificHeatRatio = 0,
                    ProcessVacuumPossibility = false,
                    MinimumOperatingVolumetricFlow = 0,
                    MinimumOperatingVelocity = 0,
                    MinimumOperatingTemperature = 0,
                    MinimumOperatingPressure = 0,
                    NormalOperatingVolumetricFlow = 0,
                    NormalOperatingVelocity = 0,
                    NormalOperatingTemperature = 0,
                    NormalOperatingPressure = 0,
                    NormalOperatingLiquidViscosity = 0,
                    MaximumOperatingVolumetricFlow = 0,
                    MaximumOperatingVelocity = 0,
                    MaximumOperatingTemperature = 0,
                    MaximumOperatingPressure = 0,
                    MaximumOperatingLiquidViscosity = 0,
                    MaximumRecoverablePressureDrop = 0,
                    MaximumUnrecoverablePressureLoss = 0,
                    BodyMaterial = null,
                    TransmitterMounting = null,
                    TransmitterDisplay = false,
                    TransmitterConnectingCableLength = 0,
                    SupplyVoltage = 0,
                    ExternalPowerVoltage = 0,
                    CableEntry = null,
                    CableTermination = null,
                    CommunicationProtocol = null,
                    ProtocolVersion = null,
                    FailSafeDirection = null,
                    CalibrationBespoke = false,
                    MeasurementRangeMinimum = 0,
                    MeasurementRangeMaximum = 0,
                    SpanAndZeroAdjustment = null,
                    Accuracy = 0,
                    Repeatability = 0,
                    StepResponse = 0,
                    LongTermDrift = 0,
                    LongTermStability = 0,
                    Vibration = false,
                    WeatherEnclosure = false,
                    ElectricalSurgeProtector = false,
                    Sunshade = false,
                },
            },
        };
    }

    public async Task<DatasheetDto?> GetDatasheetById(Guid id)
    {
        var dataSheet = await Task.Run(() => _dataSheets.FirstOrDefault(ds => ds.Id == id));

        if (dataSheet == null)
        {
            return null;
        }

        return MapDatasheetToDto(dataSheet);
    }

    public async Task<List<DatasheetDto>> GetAllDatasheets()
    {
        var datasheets = new List<DatasheetDto>();
        foreach (var dataSheet in _dataSheets)
        {
            datasheets?.Add(MapDatasheetToDto(dataSheet));
        }

        return await Task.Run(() => datasheets);
    }

    public async Task<ActionResult<List<DatasheetDto>>> GetDatasheetsForContractor(Guid id)
    {
        await Task.Run(() => { });
        throw new NotImplementedException();
    }

    private Datasheet MapDtoToDatasheet(DatasheetDto dataSheetDto, Datasheet? dataSheet = null)
    {
        if (dataSheet == null)
        {
            dataSheet = new Datasheet();
        }

        dataSheet.Id = dataSheetDto.Id;
        dataSheet.PurchaserRequirement = dataSheetDto.PurchaserRequirement;
        dataSheet.SupplierOfferedProduct = dataSheetDto.SupplierOfferedProduct;

        return dataSheet;
    }

    private DatasheetDto MapDatasheetToDto(Datasheet dataSheet)
    {
        return new DatasheetDto
        {
            Id = dataSheet.Id,
            PurchaserRequirement = dataSheet.PurchaserRequirement,
            SupplierOfferedProduct = dataSheet.SupplierOfferedProduct,
        };
    }
}

