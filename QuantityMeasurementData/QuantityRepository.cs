using QuantityMeasurementModel.Models;
using QuantityMeasurementData.Interfaces;

namespace QuantityMeasurementData 
{
    public class QuantityRepository:IQuantityRepository
    {
        private readonly QuantityDbContext _context;

        public QuantityRepository(QuantityDbContext context)
        {
            _context = context;
        }

        public void SaveQuantity(QuantityMeasurementEntity entity)
        {
            _context.Quantities.Add(entity);
            _context.SaveChanges();
        }

        public void SaveOperation(OperationHistoryEntity entity)
        {
            _context.Operations.Add(entity);
            _context.SaveChanges();
        }

        public List<QuantityMeasurementEntity> GetAll()
            => _context.Quantities.ToList();

        public List<OperationHistoryEntity> GetOperations()
            => _context.Operations.ToList();

        public void DeleteAll()
        {
            _context.Operations.RemoveRange(_context.Operations);
            _context.Quantities.RemoveRange(_context.Quantities);
            _context.SaveChanges();
        }

        public IEnumerable<OperationHistoryEntity> GetOperationsByUser(string userId)
        => _context.Operations.Where(o => o.UserId == userId).ToList();

        public void DeleteByUser(string userId)
        {
            var ops = _context.Operations.Where(o => o.UserId == userId).ToList();
            _context.Operations.RemoveRange(ops);
            _context.SaveChanges();
        }
    }
}