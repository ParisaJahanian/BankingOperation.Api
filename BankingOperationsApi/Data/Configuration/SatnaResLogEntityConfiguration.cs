using BankingOperationsApi.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankingOperationsApi.Data.Configuration
{
    public class SatnaResLogEntityConfiguration : IEntityTypeConfiguration<SatnaResLog>
    {
        public void Configure(EntityTypeBuilder<SatnaResLog> builder)
        {
            builder.ToTable("SatnaTransfer_LOG_RES");
            builder.HasKey(entity => entity.Id);
            builder.HasIndex(entity => entity.Id).IsUnique(true);
            builder.HasIndex(entity => entity.ReqLogId).IsUnique(true);
            builder.Property(entity => entity.Id).ValueGeneratedOnAdd();
            builder.Property(entity => entity.ResCode).IsRequired();
            builder.Property(entity => entity.PublicReqId).IsRequired();
            builder.Property(entity => entity.HTTPStatusCode).IsRequired();
            builder.Property(entity => entity.ReqLogId).IsRequired();
            builder.Property(entity => entity.JsonRes).IsRequired();
            builder.HasOne(entity => entity.SatnaReqLog).WithMany(entity => entity.SatnaResLogs)
                .HasForeignKey(p => p.ReqLogId);
        }
    }
}
