using CarbonEmissions.Backend.Controllers;
using CarbonEmissions.Backend.UnitOfWork.Interfaces;
using CarbonEmissions.Shared.Dtos;
using CarbonEmissions.Shared.Entities;
using CarbonEmissions.Shared.Responses;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CarbonEmissions.Test.Controllers
{
    [TestClass]
    public class EmissionsControllerTests
    {
        private Mock<IGenericUnitOfWork<Emission>> _mockGenericUnitOfWork = null!;
        private Mock<IEmissionsUnitOfWork> _mockEmissionsUnitOfWork = null!;
        private EmissionsController _controller = null!;
        [TestInitialize]
        public void Setup()
        {
            _mockGenericUnitOfWork = new Mock<IGenericUnitOfWork<Emission>>();
            _mockEmissionsUnitOfWork = new Mock<IEmissionsUnitOfWork>();
            _controller = new EmissionsController(_mockGenericUnitOfWork.Object, _mockEmissionsUnitOfWork.Object);
        }
        [TestMethod]
        public async Task GetAsync_ReturnsOkObjectResult_WhenWasSuccessIsTrue()
        {
            // Arrange
            var pagination = new PaginationDTO();
            var response = new ActionResponse<IEnumerable<Emission>> { WasSuccess = true };
            _mockEmissionsUnitOfWork.Setup(x => x.GetAsync()).ReturnsAsync(response);

            // Act
            var result = await _controller.GetAsync();

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.AreEqual(response.Result, okResult!.Value);
            _mockEmissionsUnitOfWork.Verify(x => x.GetAsync(), Times.Once());
        }
    }
}
