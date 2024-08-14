using CarbonEmissions.Backend.Data;
using CarbonEmissions.Backend.Repositories.Implementations;
using CarbonEmissions.Shared.Entities;
using CarbonEmissions.Shared.Enums;
using Microsoft.EntityFrameworkCore;

namespace CarbonEmissions.Test.Repositories
{
    [TestClass]
    public class EmissionsRepositoriesTest
    {
        private DataContext _context = null!;
        private EmissionsRepository _repository = null!;

        public void Setup()
        {
            var options = new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            _context = new DataContext(options);
            _repository = new EmissionsRepository(_context);

            _context.Emissions.AddRange(new List<Emission>
            {
               new Emission {Id = 1, Description="Exxon mobile llc", Quantity= (decimal)8896.26, EmissionDate= DateTime.Now, EmissionType= EmissionType.DirectEmissions, CompanyId= 2 },
               new Emission {Id = 2, Description="Texaco Compamies llc", Quantity= (decimal)8896.76, EmissionDate= DateTime.Now, EmissionType= EmissionType.DirectEmissions, CompanyId= 1 },
               new Emission {Id = 3, Description="Haliburton XXX", Quantity= (decimal)3226.92, EmissionDate= DateTime.Now, EmissionType= EmissionType.DirectEmissions, CompanyId= 2 }
            });

            _context.SaveChangesAsync();
        }

        [TestCleanup]
        public void CleanUp()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [TestMethod]
        public async Task GetAsync_ReturnsFilteredEmissions()
        {
            // Arrange            

            // Act
            var response = await _repository.GetAsync();

            // Assert
            Assert.IsTrue(response.WasSuccess);
            var emissions = response.Result!.ToList();
            Assert.AreEqual(1, emissions.Count);
            Assert.AreEqual("Books", emissions.First().Description);
        }

    }
}
