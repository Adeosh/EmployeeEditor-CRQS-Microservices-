using EmployeeEditor.Application.Abstractions.Microservices;
using EmployeeEditor.Application.Employees.Create;
using EmployeeEditor.Application.Employees.Delete;
using EmployeeEditor.Application.Employees.Get.GetAll;
using EmployeeEditor.Application.Employees.Get.GetByDepartment;
using EmployeeEditor.Application.Employees.Get.GetByEmail;
using EmployeeEditor.Application.Employees.Get.GetById;
using EmployeeEditor.Application.Employees.Update;
using EmployeeEditor.Domain.Models.Employee;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeEditor.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesCQRSController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ICacheService _cacheService;

        public EmployeesCQRSController(IMediator mediator, ICacheService cacheService)
        {
            _mediator = mediator;
            _cacheService = cacheService;
        }

        [HttpGet("GetAllEmployees")]
        public async Task<ActionResult> GetAllEmployees()
        {
            await _cacheService.RemoveData("employee");

            var cacheData = await _cacheService.GetCacheData<List<Employee>>("employee");

            if (cacheData != null)
            {
                return Ok(cacheData);
            }

            var employees = await _mediator.Send(new GetAllEmployeesQuery());

            await _cacheService.SetCacheData("employee", employees, DateTimeOffset.Now.AddHours(3.0));

            return Ok(employees);
        }

        [HttpGet("GetEmployeeById{id}")]
        public async Task<ActionResult> GetEmployeeById(Guid id)
        {
            var employee = await _mediator.Send(new GetEmployeeByIdQuery(id));
            return Ok(employee);
        }

        [HttpGet("GetEmployeeByEmail{email}")]
        public async Task<ActionResult> GetEmployeeByEmail(string email)
        {
            var employee = await _mediator.Send(new GetEmployeeByEmailQuery(email));
            return Ok(employee);
        }

        [HttpGet("GetEmployeesByDepartment{department}")]
        public async Task<ActionResult> GetEmployeesByDepartment(string department)
        {
            var employee = await _mediator.Send(new GetEmployeesByDepartmentQuery(department));
            return Ok(employee);
        }

        [HttpPost("CreateEmployee")]
        public async Task<IActionResult> CreateEmployee(
            CreateEmployeeRequest request,
            IValidator<CreateEmployeeCommand> validator)
        {
            var command = new CreateEmployeeCommand(
                request.FirstName,
                request.MiddleName,
                request.LastName,
                request.Age,
                request.Email,
                request.Department,
                request.Salary,
                request.IsActive,
                request.EmploymentDate,
                request.CreatedAt);

            var result = await validator.ValidateAsync(command);

            if (!result.IsValid)
            {
                return ValidationProblem(new ValidationProblemDetails(result.ToDictionary()));
            }

            await _mediator.Send(command);

            return Ok(command);
        }

        [HttpPut("UpdateEmployee{id}")]
        public async Task<IActionResult> UpdateEmployee(Guid id, [FromBody] UpdateEmployeeRequest request)
        {
            try
            {
                var command = new UpdateEmployeeCommand(
                    id,
                    request.FirstName,
                    request.MiddleName,
                    request.LastName,
                    request.Age,
                    request.Email,
                    request.Department,
                    request.Salary,
                    request.IsActive,
                    request.UpdatedAt);

                await _mediator.Send(command);

                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("DeleteEmployee{id}")]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            try
            {
                await _mediator.Send(new DeleteEmployeeCommand(id));

                await _cacheService.RemoveData($"employees{id}");

                await _cacheService.SetCacheData($"deleted_employee_{id}", id, DateTimeOffset.Now.AddMinutes(30));

                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
