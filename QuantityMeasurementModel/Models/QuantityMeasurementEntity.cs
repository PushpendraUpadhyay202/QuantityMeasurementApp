using System.ComponentModel.DataAnnotations;
namespace QuantityMeasurementModel.Models
{
    public class QuantityMeasurementEntity
    {
        [Key]
        public int Id { get; set; }
        public double Value { get; set; }
        public string Unit { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
    }
}