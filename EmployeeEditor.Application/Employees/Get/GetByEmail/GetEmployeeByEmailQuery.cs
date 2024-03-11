using EmployeeEditor.Domain.Models.Employee;
using MediatR;

namespace EmployeeEditor.Application.Employees.Get.GetByEmail
{
    public record GetEmployeeByEmailQuery(string Email) : IRequest<Employee>;
}
