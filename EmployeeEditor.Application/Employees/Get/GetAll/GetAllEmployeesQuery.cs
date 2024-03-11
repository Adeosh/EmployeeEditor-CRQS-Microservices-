using EmployeeEditor.Domain.Models.Employee;
using MediatR;

namespace EmployeeEditor.Application.Employees.Get.GetAll
{
    public record GetAllEmployeesQuery() : IRequest<IEnumerable<Employee>>;
}
