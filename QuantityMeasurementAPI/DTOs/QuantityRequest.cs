using QuantityMeasurementModel.DTOs;

namespace QuantityMeasurementAPI.DTOs
{
    public class QuantityRequest
    {
        public QuantityDTO First { get; set; }
        public QuantityDTO Second { get; set; }
        public string TargetUnit { get; set; }
    }
}