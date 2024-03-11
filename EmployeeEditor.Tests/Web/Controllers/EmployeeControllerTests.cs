using AutoFixture;
using EmployeeEditor.Application.Abstractions;
using EmployeeEditor.Application.Abstractions.Data;
using EmployeeEditor.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace EmployeeEditor.Tests.Web.Controllers
{
    public class EmployeeControllerTests
    {
        private readonly Mock<IEmployeeService> _mockEmployeeService;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IApplicationDbContext> _context;
        private Fixture _fixture;
        private EmployeesController _employeeController;

        public EmployeeControllerTests()
        {
            _mockEmployeeService = new();
            _mockUnitOfWork = new();
            _fixture = new Fixture();
            _context = new();
        }

        [Fact]
        public async Task GetAllEmployees_ReturnsOkObjectResult()
        {
            // Arrange
            var controller = new EmployeesController(_mockEmployeeService.Object, _mockUnitOfWork.Object);

            var employees = new List<Employee>();

            _mockEmployeeService.Setup(repo => repo.GetAllEmployeeAsync()).ReturnsAsync(employees);

            // Act
            var result = await controller.GetAllEmployees();

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result.Result);
            var model = Assert.IsAssignableFrom<List<Employee>>(okObjectResult.Value);
            Assert.Equal(employees, model);
        }

        [Fact]
        public async Task CreateEmployee_ReturnsOkObjectResult()
        {
            // Arrange
            var controller = new EmployeesController(_mockEmployeeService.Object, _mockUnitOfWork.Object);

            var employee = new Employee(
                "Alex",
                "William",
                "Cooper",
                45,
                Email.Create("coo5e9@example.com"),
                "HR",
                20650,
                true,
                DateTime.UtcNow,
                DateTime.UtcNow);

            var employeeId = Guid.NewGuid();

            _mockEmployeeService.Setup(repo => repo.CreateEmployeeAsync(It.IsAny<Employee>())).ReturnsAsync(employeeId);
            _mockUnitOfWork.Setup(uow => uow.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1);

            // Act
            var result = await controller.CreateEmployee(employee);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result.Result);
            var model = Assert.IsType<Guid>(okObjectResult.Value);
            Assert.Equal(employeeId, model);
        }

        //unfinished!!!
    }
}
