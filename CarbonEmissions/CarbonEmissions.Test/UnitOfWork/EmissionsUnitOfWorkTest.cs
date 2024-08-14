using Moq;
using CarbonEmissions.Backend.Repositories;
using CarbonEmissions.Backend.UnitOfWork;
using CarbonEmissions.Shared.Dtos;
using CarbonEmissions.Shared.Entities;
using CarbonEmissions.Shared.Responses;
using CarbonEmissions.Backend.Repositories.Implementations;
using CarbonEmissions.Backend.Repositories.Interfaces;
using CarbonEmissions.Backend.UnitOfWork.Implementations;


namespace CarbonEmissions.Test.UnitOfWork
{
    [TestClass]
    public class EmissionsUnitOfWorkTest
    {
        private Mock<IGenericRepository<Emission>> _repositoryUnitMock = null!;
        private Mock<IEmissionsRepository> _mockEmissionsRepository = null!;
        private EmissionsUnitOfWork _unitOfWork = null!;

        [TestInitialize]
        public void Setup()
        {
            _repositoryUnitMock = new Mock<IGenericRepository<Emission>>();
            _mockEmissionsRepository = new Mock<IEmissionsRepository>();
            _unitOfWork = new EmissionsUnitOfWork(_repositoryUnitMock.Object, _mockEmissionsRepository.Object);
        }

        [TestMethod]
        public async Task Get_Async_CallsRepositoryAndReturnResult()
        {
            // Arrange
            var pagination = new PaginationDTO();
            var expectedActionResponse = new ActionResponse<IEnumerable<Emission>> { Result = new List<Emission>() };
            _mockEmissionsRepository.Setup(x => x.GetAsync()).ReturnsAsync(expectedActionResponse);

            // Act
            var result = await _unitOfWork.GetAsync();

            // Assert
            Assert.AreEqual(expectedActionResponse, result);
            _mockEmissionsRepository.Verify(x => x.GetAsync(), Times.Once);

        }
    }
}
