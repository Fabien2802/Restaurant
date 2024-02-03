using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Shared.Domain;

namespace Restaurant.Client.Configurations.Enitities
{
    public class TableSeedConfiguration : IEntityTypeConfiguration<Table>
    {
        public void Configure(EntityTypeBuilder<Table> builder)
        {
            builder.HasData(
                new Table
                {
                    TableID = 1,
                    TableCapacity = 2,
                }
                );
        }
    }
}
