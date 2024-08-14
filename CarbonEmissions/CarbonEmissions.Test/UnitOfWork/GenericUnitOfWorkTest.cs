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
//        private PaginationDTO _paginationDTO = null!;

        [TestInitialize]
        public void Initialize()
        {
            _mockRepository = new Mock<IGenericRepository<object>>();
            _unitOfWork = new GenericUnitOfWork<object>(_mockRepository.Object);
            _testModel = new object();
            _testId = 1;
            //_paginationDTO = new PaginationDTO();
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

    }
}
