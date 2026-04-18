using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using QuantityMeasurementModel.Models;

namespace QuantityMeasurementData
{
    public class QuantityDbContext : DbContext
    {
        public QuantityDbContext(DbContextOptions<QuantityDbContext> options)
            : base(options) { }

        public DbSet<QuantityMeasurementEntity> Quantities { get; set; }
        public DbSet<OperationHistoryEntity> Operations { get; set; }
        public DbSet<UserEntity> Users { get; set; }
    }
}