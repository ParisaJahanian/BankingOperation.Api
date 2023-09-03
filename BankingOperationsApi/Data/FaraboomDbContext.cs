using BankingOperationsApi.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankingOperationsApi.Data
{
    public class FaraboomDbContext : DbContext
    {
        public DbSet<SatnaReqLog> satnaReqLogs { get; set; }
        public DbSet<SatnaResLog> satnaResLogs { get; set; }
        public DbSet<PayaReqLog> payaReqLogs { get; set; }
        public DbSet<PayaResLog> payaResLogs { get; set; }

        public FaraboomDbContext(DbContextOptions<FaraboomDbContext> dbContext) : base(dbContext)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<CarTollsBillsLog>()
            //       .Property(e => e.PayStatus)
            //       .HasConversion<string>();
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(FaraboomDbContext).Assembly);
        }
    }
}
