using QuantityMeasurementModel.DTOs;
using QuantityMeasurementModel.Models;

namespace QuantityMeasurementServices.Interfaces
{
    public interface IQuantityMeasurementService
    {
        bool Compare(QuantityDTO first, QuantityDTO second, string? userId = null);
        QuantityDTO Convert(QuantityDTO source, string targetUnit);
        QuantityDTO Add(QuantityDTO first, QuantityDTO second, string targetUnit, string? userId = null);
        QuantityDTO Subtract(QuantityDTO first, QuantityDTO second, string targetUnit, string? userId = null);
        double Divide(QuantityDTO first, QuantityDTO second, string? userId = null);
        HistoryResponse GetFullHistory();
        HistoryResponse GetHistoryByUser(string userId);
        void DeleteAllRecords();
        void DeleteRecordsByUser(string userId);
    }
}