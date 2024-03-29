namespace datasheetapi.Models;

public class InstrumentPurchaserRequirement
{
    public int? CodeRequirement { get; set; }
    public int? ConformityAssessmentSystemLevel { get; set; }
    public string? TagNumber { get; set; }
    public string? ServiceDescription { get; set; }
    public string? EquipmentManufacturerSerialNumber { get; set; }
    public string? ProjectCountry { get; set; }
    public string? ProjectRegion { get; set; }
    public string? PlantEnvironmentalLocation { get; set; }
    public string? PIDNumber { get; set; }
    public string? LineOrEquipmentNumber { get; set; }
    public int? MinimumAmbientTemperature { get; set; }
    public int? MaximumAmbientTemperature { get; set; }
    public string? BaseConditions { get; set; }
    public int? BaseTemperature { get; set; }
    public int? BasePressure { get; set; }
    public string? CoatingDurability { get; set; }
    public string? SILRating { get; set; }
    public string? IngressProtection { get; set; }
    public string? ExProtection { get; set; }
    public string? HazardousAreaClassificationStandard { get; set; }
    public string? ExplosionHazardClassification { get; set; }
    public string? ExplosionGroup { get; set; }
    public string? TemperatureClass { get; set; }
    public string? UpstreamHighSidePipeSchedule { get; set; }
    public string? UpstreamHighSideLineSize { get; set; }
    public string? UpstreamHighSideLineEquipmentRating { get; set; }
    public string? UpstreamHighSideLineConnectionType { get; set; }
    public string? UpstreamHighSideConnectionOrientation { get; set; }
    public string? UpstreamHighSideMaterialType { get; set; }
    public int? DesignPressureMaximum { get; set; }
    public int? DesignPressureMinimum { get; set; }
    public int? DesignTemperatureMaximum { get; set; }
    public int? DesignTemperatureMinimum { get; set; }
    public string? SourServiceSpecification { get; set; }
    public string? ProcessFluids { get; set; }
    public string? ProcessFluidState { get; set; }
    public string? ServiceDescription2 { get; set; }
    public string? ProcessFluidCorrosiveCompounds { get; set; }
    public string? ProcessFluidErosionPossibility { get; set; }
    public int? ProcessFluidConductivity { get; set; }
    public double? ProcessFluidSpecificHeatRatio { get; set; }
    public bool? ProcessVacuumPossibility { get; set; }
    public string? ProcessFluidVapourPressure { get; set; }
    public int? MinimumOperatingVolumetricFlow { get; set; }
    public double? MinimumOperatingVelocity { get; set; }
    public string? MinimumOperatingTemperature { get; set; }
    public int? MinimumOperatingPressure { get; set; }
    public int? NormalOperatingVolumetricFlow { get; set; }
    public double? NormalOperatingVelocity { get; set; }
    public int? NormalOperatingTemperature { get; set; }
    public double? NormalOperatingPressure { get; set; }
    public double? NormalOperatingLiquidSpecificGravity { get; set; }
    public double? NormalOperatingLiquidViscosity { get; set; }
    public string? MaximumOperatingVolumetricFlow { get; set; }
    public string? MaximumOperatingVelocity { get; set; }
    public string? MaximumOperatingTemperature { get; set; }
    public string? MaximumOperatingPressure { get; set; }
    public string? MaximumOperatingLiquidSpecificGravity { get; set; }
    public string? MaximumOperatingLiquidViscosity { get; set; }
    public string? MaximumOperatingVapourMolecularWeight { get; set; }
    public string? MaximumOperatingVapourCompressibilityFactor { get; set; }
    public string? MaximumOperatingVapourActualDensity { get; set; }
    public string? MaximumOperatingVapourViscosity { get; set; }
    public string? MaximumRecoverablePressureDrop { get; set; }
    public string? MaximumUnrecoverablePressureLoss { get; set; }
    public string? BodyMaterial { get; set; }
    public string? TransmitterDisplay { get; set; }
    public double? TransmitterConnectingCableLength { get; set; }
    public double? ExternalPowerVoltage { get; set; }
    public string? CableEntry { get; set; }
    public string? CableTermination { get; set; }
    public string? CommunicationProtocol { get; set; }
    public string? ProtocolVersion { get; set; }
    public string? FailSafeDirection { get; set; }
    public string? CalibrationBespoke { get; set; }
    public double? MeasurementRangeMinimum { get; set; }
    public double? MeasurementRangeMaximum { get; set; }
    public string? SpanAndZeroAdjustment { get; set; }
    public string? Accuracy { get; set; }
    public string? Repeatability { get; set; }
    public double? StepResponse { get; set; }
    public double? LongTermDrift { get; set; }
    public string? LongTermStability { get; set; }
    public string? Vibration { get; set; }
    public string? WeatherEnclosure { get; set; }
    public string? MountingBracket { get; set; }
    public string? MountingBracketMaterial { get; set; }
    public string? ElectricalSurgeProtector { get; set; }
    public string? Sunshade { get; set; }
    public string? Manufacturer { get; set; }
    public string? ModelNumber { get; set; }
    public string? PressureRetainingBoltMaterial { get; set; }
    public string? PressureRetainingNutMaterial { get; set; }
    public string? BodyElementSensorManufacturerModelNumber { get; set; }
    public string? CoriolisTubeMaterial { get; set; }
    public string? CoriolisTubeType { get; set; }
    public string? FlangeMaterial { get; set; }
    public string? LinerMaterial { get; set; }
    public string? CoilCoverMaterial { get; set; }
    public string? JunctionBoxMaterial { get; set; }
    public string? ElectrodeType { get; set; }
    public string? ElectrodeMaterial { get; set; }
    public double? MeterMinimumConductivity { get; set; }
    public bool? GroundingRing { get; set; }
    public string? GroundingRingMaterial { get; set; }
    public string? LiningProtector { get; set; }
    public double? BodySize { get; set; }
    public double? EndConnectionSize { get; set; }
    public string? EndConnectionFlangeType { get; set; }
    public string? EndConnectionFlangeRating { get; set; }
    public string? TransmitterModelNumber { get; set; }
    public string? TransmitterEnclosureMaterial { get; set; }
    public string? TransmitterMounting { get; set; }
    public string? TransmitterConnectingCables { get; set; }
    public int? TransmitterConnectingCableQuantity { get; set; }
    public string? SupplyVoltage { get; set; }
    public string? IsolatedOutputs { get; set; }
    public double? LowerRangeLimit { get; set; }
    public double? UpperRangeLimit { get; set; }
    public string? CoriolisOuterCasingMaterial { get; set; }
    public string? CoriolisOuterCasingBurstPressure { get; set; }
    public string? RuptureDiscBurstPressure { get; set; }
    public string? ProcessSecondaryContainment { get; set; }
    public string? PipeClassSheet { get; set; }
    public string? SetAlarmPoint { get; set; }
    public string? PONumber { get; set; }
    public string? Type { get; set; }
    public string? OperatingTemperatureLimits { get; set; }
    public string? OperatingPressureLimit { get; set; }
    public string? PressureLossAtFullRange { get; set; }
    public string? Mounting { get; set; }
    public double? Weight { get; set; }
    public string? CalibratedRange { get; set; }
    public string? Characteristics { get; set; }
    public string? Linearity { get; set; }
    public string? MinMaxRangeLimit { get; set; }
    public double? BodyNominalSize { get; set; }
    public string? ProcessConnectionSizeType { get; set; }
    public double? PressureRating { get; set; }
    public double? FaceToFaceDimension { get; set; }
    public double? NumberOfTubeRuns { get; set; }
    public double? TubeInnerDiameter { get; set; }
    public string? MaterialTube { get; set; }
    public string? MaterialFlangeConnection { get; set; }
    public string? MaterialTubeCover { get; set; }
    public string? BodyEnclosureProtection { get; set; }
    public string? BodyProtectiveCoatingColor { get; set; }
    public string? TransmitterCableConnection { get; set; }
    public string? TransmitterDimension { get; set; }
    public string? TransmitterEnclosureProtection { get; set; }
    public string? TransmitterExClassification { get; set; }
    public string? TransmitterProtectiveCoating { get; set; }
    public string? TransmitterConsumption { get; set; }
    public string? TransmitterLoadLimitation { get; set; }
    public string? TransmitterFlowRange { get; set; }
    public double? LineNominalSize { get; set; }
    public double? LineInnerDiameter { get; set; }
    public string? LineMaterial { get; set; }
    public string? FlangeStandardOrCode { get; set; }
    public double? FlangeSize { get; set; }
    public string? FlangePressureClass { get; set; }
    public string? FlangeFacing { get; set; }
    public string? PipingDesignTemperature { get; set; }
    public string? PipingDesignPressure { get; set; }
    public string? Fluid { get; set; }
    public string? Phase { get; set; }
    public string? CorrosiveCompounds { get; set; }
    public double? MaximumPressureLoss { get; set; }
    public double? MinimumFlowRate { get; set; }
    public double? MinimumInletPressure { get; set; }
    public string? MinimumDensityAtTAndP { get; set; }
    public string? MinimumViscosityAtT { get; set; }
    public string? MinimumOperatingVapourMolecularWeight { get; set; }
    public string? MinimumVapourCompressFactor { get; set; }
    public string? MinimumVapourSpecificHeatRatio { get; set; }
    public double? MaximumFlowRate { get; set; }
    public double? MaximumInletPressure { get; set; }
    public string? MaximumDensityAtTAndP { get; set; }
    public string? MaximumViscosityAtT { get; set; }
    public string? MaximumVapourCompressFactor { get; set; }
    public string? MaximumVapourSpecificHeatRatio { get; set; }
    public string? MaxDistanceMeterTrans { get; set; }
    public string? TransmitterIndicator { get; set; }
    public string? TransmitterOutputSignal { get; set; }
    public string? LineEquipmentNumber { get; set; }
    public string? PAndID { get; set; }
    public string? MinimumOperatingMassFlow { get; set; }
    public string? MinimumOperatingLiquidSpecificGravity { get; set; }
    public string? MinimumOperatingLiquidViscosity { get; set; }
    public string? MinimumOperatingVapourActualDensity { get; set; }
    public string? MinimumOperatingVapourViscosity { get; set; }
    public string? NormalOperatingMassFlow { get; set; }
    public string? NormalOperatingVapourCompressibilityFactor { get; set; }
    public string? NormalOperatingVapourActualDensity { get; set; }
    public string? NormalOperatingVapourViscosity { get; set; }
    public string? MaximumOperatingMassFlow { get; set; }
    public string? NormalOperatingVapourMolecularWeight { get; set; }
}
