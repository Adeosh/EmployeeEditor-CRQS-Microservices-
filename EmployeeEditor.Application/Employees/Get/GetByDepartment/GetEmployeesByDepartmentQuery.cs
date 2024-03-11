using EmployeeEditor.Domain.Models.Employee;
using MediatR;

namespace EmployeeEditor.Application.Employees.Get.GetByDepartment
{
    public record GetEmployeesByDepartmentQuery(string Department) : IRequest<IEnumerable<Employee>>;
}
