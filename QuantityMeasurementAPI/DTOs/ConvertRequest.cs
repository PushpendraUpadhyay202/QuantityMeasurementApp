using QuantityMeasurementModel.DTOs;

public class ConvertRequest
{
    public QuantityDTO Input{get;set;}
    public string TargetUnit { get; set; } = string.Empty;
}