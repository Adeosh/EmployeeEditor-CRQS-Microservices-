using EmployeeEditor.Application.Abstractions.Data;
using EmployeeEditor.Application.Abstractions;
using EmployeeEditor.Application.Employees.Delete;
using EmployeeEditor.Domain.Models.Employee;
using Moq;

namespace EmployeeEditor.Tests.Application.Handlers
{
    public class DeleteEmployeeCommandHandlerTests
    {
        private readonly Mock<IEmployeeRepository> _mockEmployeeRepository;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly DeleteEmployeeCommandHandler _handler;

        public DeleteEmployeeCommandHandlerTests()
        {
            _mockEmployeeRepository = new();
            _mockUnitOfWork = new();
            _handler = new DeleteEmployeeCommandHandler(
                _mockEmployeeRepository.Object,
                _mockUnitOfWork.Object);
        }

        [Fact]
        public async Task Handle_ExistingEmployee_DeletesAndSaves()
        {
            // Arrange
            var existingEmployeeId = Guid.NewGuid();
            var existingEmployee = new Employee(
                "Alex",
                "William",
                "Cooper",
                45,
                Email.Create("coo5e9@example.com"),
                "HR",
                20650,
                true,
                DateTime.UtcNow,
                DateTime.UtcNow
            );

            _mockEmployeeRepository.Setup(repo => repo.GetEmployeeByIdAsync(existingEmployeeId))
                .ReturnsAsync(existingEmployee);
            _mockUnitOfWork.Setup(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(1));

            // Act
            await _handler.Handle(new DeleteEmployeeCommand(existingEmployeeId), CancellationToken.None);

            // Assert
            _mockEmployeeRepository.Verify(repo => repo.GetEmployeeByIdAsync(existingEmployeeId), Times.Once);
            _mockEmployeeRepository.Verify(repo => repo.RemoveEmployee(existingEmployee), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_NonexistentEmployee_ThrowsNotFoundException()
        {
            // Arrange
            var nonExistentEmployeeId = Guid.NewGuid();

            _mockEmployeeRepository.Setup(repo => repo.GetEmployeeByIdAsync(nonExistentEmployeeId))
                .ReturnsAsync(() => null);

            // Act & Assert
            await Assert.ThrowsAsync<EmployeeNotFoundException>(async () =>
                await _handler.Handle(new DeleteEmployeeCommand(nonExistentEmployeeId), CancellationToken.None));

            _mockEmployeeRepository.Verify(repo => repo.GetEmployeeByIdAsync(nonExistentEmployeeId), Times.Once);
            _mockEmployeeRepository.Verify(repo => repo.RemoveEmployee(It.IsAny<Employee>()), Times.Never);
            _mockUnitOfWork.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}
