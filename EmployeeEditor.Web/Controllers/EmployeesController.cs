using EmployeeEditor.Application.Abstractions;
using EmployeeEditor.Application.Abstractions.Data;
using EmployeeEditor.Domain.Models.Employee;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeEditor.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IUnitOfWork _unitOfWork;

        public EmployeesController(
            IEmployeeService employeeService, 
            IUnitOfWork unitOfWork)
        {
            _employeeService = employeeService;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<Employee>>> GetAllEmployees()
        {
            var employees = await _employeeService.GetAllEmployeeAsync();
            return Ok(employees);
        }

        [HttpPost("Create")]
        public async Task<ActionResult<Guid>> CreateEmployee(Employee employee)
        {
            var employeeId = await _employeeService.CreateEmployeeAsync(employee);
            await _unitOfWork.SaveChangesAsync();
            return Ok(employeeId);
        }

        [HttpPut("Update")]
        public async Task<ActionResult<Employee>> UpdateEmployee(Employee employee)
        {
            var updatedEmployee = await _employeeService.UpdateEmployeeAsync(employee);
            await _unitOfWork.SaveChangesAsync();
            return Ok(updatedEmployee);
        }


        [HttpDelete("Delete")]
        public async Task<ActionResult<bool>> DeleteEmployee(Guid id)
        {
            var result = await _employeeService.DeleteEmployeeAsync(id);

            if (result)
            {
                return NoContent();
            }

            await _unitOfWork.SaveChangesAsync();

            return NotFound();
        }
    }
}
