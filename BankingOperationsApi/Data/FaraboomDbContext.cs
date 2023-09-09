using BankingOperationsApi.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankingOperationsApi.Data
{
    public class FaraboomDbContext : DbContext
    {
        public DbSet<SatnaReqLog> SatnaReqLogs { get; set; }
        public DbSet<SatnaResLog> SatnaResLogs { get; set; }
        public DbSet<PayaReqLog> PayaReqLogs { get; set; }
        public DbSet<PayaResLog> PayaResLogs { get; set; }
        public DbSet<AccessTokenEntity> AccessTokens { get; set; }
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
