using Microsoft.EntityFrameworkCore;
using CarbonEmissions.Backend.Data;
using CarbonEmissions.Backend.Repositories.Implementations;
using CarbonEmissions.Shared.Dtos;
using CarbonEmissions.Shared.Entities;
using CarbonEmissions.Test.Shared;
using CarbonEmissions.Shared.Enums;

namespace CarbonEmissions.Test.Shared
{
    [TestClass]
    public class GenericRepositoryTests
    {
        private DataContext _context = null!;
        private DbContextOptions<DataContext> _options = null!;
        private GenericRepository<Emission> _repository = null!;

        [TestInitialize]
        public void Initialize()
        {
            _options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new DataContext(_options);
            _repository = new GenericRepository<Emission>(_context);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [TestMethod]
        public async Task AddAsync_ShouldAddEntity()
        {
            // Arrange
            var testEntity = new Emission { Id = 1, Description = "Exxon mobile llc", Quantity = (decimal)8896.26, EmissionDate = DateTime.Now, EmissionType = EmissionType.DirectEmissions, CompanyId = 2 };            

            // Act
            var response = await _repository.AddAsync(testEntity);

            // Assert
            Assert.IsTrue(response.WasSuccess);
            Assert.IsNotNull(response.Result);
            Assert.AreEqual("Exxon mobile llc", response.Result.Description);
        }

    }
}
