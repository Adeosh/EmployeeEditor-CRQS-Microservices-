using EmployeeEditor.Application.Abstractions.Data;
using EmployeeEditor.Application.Abstractions;
using EmployeeEditor.Application.Employees.Update;
using Moq;

namespace EmployeeEditor.Tests.Application.Handlers
{
    public class UpdateEmployeeCommandHandlerTests
    {
        private readonly Mock<IEmployeeRepository> _mockEmployeeRepository;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly UpdateEmployeeCommandHandler _handler;
        private readonly Employee _existingEmployee;

        public UpdateEmployeeCommandHandlerTests()
        {
            _mockEmployeeRepository = new();
            _mockUnitOfWork = new();
            _handler = new UpdateEmployeeCommandHandler(_mockEmployeeRepository.Object, _mockUnitOfWork.Object);

            _existingEmployee = new Employee(
                "James",
                "Gardner",
                "Smith",
                30,
                Email.Create("dereck.smith@example.com"),
                "IT",
                50000,
                true,
                DateTime.UtcNow,
                DateTime.UtcNow
            );
        }

        [Fact]
        public async Task Handle_ExistingEmployee_UpdatesEmployeeAndSavesChanges()
        {
            // Arrange
            var updateCommand = new UpdateEmployeeCommand(
                _existingEmployee.Id,
                "John",
                "Rasty",
                "Coleman",
                28,
                "crj11@example.com",
                "Marketing",
                12000,
                true,
                DateTime.UtcNow
            );

            _mockEmployeeRepository.Setup(repo => repo.GetEmployeeByIdAsync(updateCommand.Id))
                .ReturnsAsync(_existingEmployee);

            // Act
            await _handler.Handle(updateCommand, CancellationToken.None);

            // Assert
            _mockEmployeeRepository.Verify(repo => repo.GetEmployeeByIdAsync(updateCommand.Id), Times.Once);
            _mockEmployeeRepository.Verify(repo => repo.UpdateEmployee(_existingEmployee), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}

