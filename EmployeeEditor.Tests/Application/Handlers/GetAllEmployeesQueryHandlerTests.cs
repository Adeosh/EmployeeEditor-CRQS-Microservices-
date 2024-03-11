using EmployeeEditor.Application.Abstractions;
using EmployeeEditor.Application.Employees.Get.GetAll;
using EmployeeEditor.Domain.Models.Employee;
using Moq;

namespace EmployeeEditor.Tests.Application.Handlers
{
    public class GetAllEmployeesQueryHandlerTests
    {
        private readonly Mock<IEmployeeRepository> _mockEmployeeRepository;
        private readonly GetAllEmployeesQueryHandler _handler;

        public GetAllEmployeesQueryHandlerTests()
        {
            _mockEmployeeRepository = new();
            _handler = new GetAllEmployeesQueryHandler(_mockEmployeeRepository.Object);
        }

        [Fact]
        public async Task Handle_ReturnsAllEmployeesFromRepository()
        {
            // Arrange
            var expectedEmployees = new List<Employee>()
            {
                new Employee("James", "Gardner", "Smith", 30, Email.Create("dereck.smith@example.com"),
                    "IT", 50000, true, DateTime.UtcNow, DateTime.UtcNow),
                new Employee("Jason", "Born", "Johnson", 44, Email.Create("born007@example.com"),
                    "Marketing", 40500, true, DateTime.UtcNow, DateTime.UtcNow),
            };

            _mockEmployeeRepository.Setup(repo => repo.GetAllEmployeesAsync())
                .Returns(Task.FromResult(expectedEmployees));

            // Act
            var result = await _handler.Handle(new GetAllEmployeesQuery(), CancellationToken.None);

            // Assert
            _mockEmployeeRepository.Verify(repo => repo.GetAllEmployeesAsync(), Times.Once);
            Assert.Equal(expectedEmployees, result);
        }
    }
}
