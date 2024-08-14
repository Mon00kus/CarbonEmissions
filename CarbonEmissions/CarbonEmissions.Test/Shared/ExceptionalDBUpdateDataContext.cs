using CarbonEmissions.Backend.Data;
using Microsoft.EntityFrameworkCore;

namespace CarbonEmissions.Tests.Shared
{
    public class ExceptionalDBUpdateDataContext : DataContext
    {
        public ExceptionalDBUpdateDataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            throw new DbUpdateException("Test Exception");
        }
    }
}
