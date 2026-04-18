using QuantityMeasurementModel.Models;

namespace QuantityMeasurementData.Interfaces
{
    public interface IQuantityRepository
    {
        // Quantity
        void SaveQuantity(QuantityMeasurementEntity entity);
        List<QuantityMeasurementEntity> GetAll();

        // Operations
        void SaveOperation(OperationHistoryEntity entity);
        List<OperationHistoryEntity> GetOperations();
        IEnumerable<OperationHistoryEntity> GetOperationsByUser(string userId);

        // Delete
        void DeleteAll();
        void DeleteByUser(string userId);
    }
}