using CarbonEmissions.Backend.Repositories.Interfaces;
using CarbonEmissions.Backend.UnitOfWork.Implementations;
using CarbonEmissions.Shared.Responses;
using Moq;
namespace CarbonEmissions.Test.UnitOfWork
{
    [TestClass]
    public class GenericUnitOfWorkTest
    {
        private Mock<IGenericRepository<object>> _mockRepository = null!;
        private GenericUnitOfWork<object> _unitOfWork = null!;
        private object _testModel = null!;
        private int _testId;


        [TestInitialize]
        public void Initialize()
        {
            _mockRepository = new Mock<IGenericRepository<object>>();
            _unitOfWork = new GenericUnitOfWork<object>(_mockRepository.Object);
            _testModel = new object();
            _testId = 1;

        }

        [TestMethod]
        public async Task AddAsync_Success()
        {
            _mockRepository.Setup(x => x.AddAsync(It.IsAny<object>()))
                .ReturnsAsync(new ActionResponse<object> { Result = _testModel });

            var result = await _unitOfWork.UpdateAsync(_testModel);

            Assert.IsNotNull(result);
            Assert.AreEqual(_testModel, result.Result);
        }

        [TestMethod]
        public async Task DeleteAsync_Success()
        {
            _mockRepository.Setup(x => x.DeleteAsync(It.IsAny<int>()))
                .ReturnsAsync(new ActionResponse<object> { Result = _testModel });

            var result = await _unitOfWork.DeleteAsync(_testId);

            Assert.IsNotNull(result);
            Assert.AreEqual(_testModel, result.Result);
        }

        [TestMethod]
        public async Task GetAsync_Id_Success()
        {
            _mockRepository.Setup(x => x.GetAsync(It.IsAny<int>()))
                .ReturnsAsync(new ActionResponse<object> { Result = _testModel });

            var result = await _unitOfWork.GetAsync(_testId);

            Assert.IsNotNull(result);
            Assert.AreEqual(_testModel, result.Result);
        }

        [TestMethod]
        public async Task GetAsync_Success()
        {
            _mockRepository.Setup(x => x.GetAsync())
                .ReturnsAsync(new ActionResponse<IEnumerable<object>> { Result = new List<object> { _testModel } });

            var result = await _unitOfWork.GetAsync();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task UpdateAsync_Success()
        {
            _mockRepository.Setup(x => x.UpdateAsync(It.IsAny<object>()))
                .ReturnsAsync(new ActionResponse<object> { Result = _testModel });

            var result = await _unitOfWork.UpdateAsync(_testModel);

            Assert.IsNotNull(result);
            Assert.AreEqual(_testModel, result.Result);
        }
    }
}
