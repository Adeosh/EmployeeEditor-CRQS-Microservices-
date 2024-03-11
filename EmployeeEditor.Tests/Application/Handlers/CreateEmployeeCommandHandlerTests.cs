using EmployeeEditor.Application.Abstractions.Data;
using EmployeeEditor.Application.Abstractions;
using EmployeeEditor.Application.Employees.Create;
using MediatR;
using Moq;

namespace EmployeeEditor.Tests.Application.Handlers
{
    public class CreateEmployeeCommandHandlerTests
    {
        private readonly Mock<IEmployeeRepository> _mockEmployeeRepository;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IPublisher> _mockPublisher;
        private readonly CreateEmployeeCommandHandler _handler;

        public CreateEmployeeCommandHandlerTests()
        {
            _mockEmployeeRepository = new();
            _mockUnitOfWork = new();
            _mockPublisher = new();
            _handler = new CreateEmployeeCommandHandler(
                _mockEmployeeRepository.Object,
                _mockUnitOfWork.Object,
                _mockPublisher.Object);
        }

        [Fact]
        public async Task Handle_ValidCommand_CreatesEmployeeAndPublishesEvent()
        {
            // Arrange
            var firstName = "Marry";
            var middleName = "Alfred";
            var lastName = "Poppins";
            var age = 35;
            var email = "poppy774@example.com";
            var department = "Engineering";
            var salary = 34000;
            var isActive = true;

            var createEmployeeCommand = new CreateEmployeeCommand(
                firstName, middleName, lastName, age, email, department, salary, isActive, DateTime.UtcNow, DateTime.UtcNow);

            _mockUnitOfWork.Setup(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(1));

            // Act
            await _handler.Handle(createEmployeeCommand, CancellationToken.None);

            // Assert
            _mockEmployeeRepository.Verify(repo => repo.AddEmployee(It.Is<Employee>(e =>
                e.FirstName == firstName &&
                e.MiddleName == middleName &&
                e.LastName == lastName &&
                e.Email.Value == email &&
                e.Department == department &&
                e.Salary == salary &&
                e.IsActive == isActive
            )), Times.Once);

            _mockUnitOfWork.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
