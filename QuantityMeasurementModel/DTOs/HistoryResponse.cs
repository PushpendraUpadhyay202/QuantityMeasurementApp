using QuantityMeasurementModel.Models;
public class HistoryResponse
{
    public List<QuantityMeasurementEntity> Quantities { get; set; }
    public List<OperationHistoryEntity> Operations { get; set; }
}