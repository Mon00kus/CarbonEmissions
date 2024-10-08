﻿using CarbonEmissions.Backend.Data;
using Microsoft.EntityFrameworkCore;

namespace CarbonEmissions.Tests.Shared
{
    public class ExceptionalDBUpdateDataContextWithInnerException : DataContext
    {
        public ExceptionalDBUpdateDataContextWithInnerException(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var innerException = new Exception("duplicate record");
            throw new DbUpdateException("Test Exception", innerException);
        }
    }
}
