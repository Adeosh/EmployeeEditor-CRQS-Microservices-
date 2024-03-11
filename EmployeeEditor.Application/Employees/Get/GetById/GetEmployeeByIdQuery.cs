using EmployeeEditor.Domain.Models.Employee;
using MediatR;

namespace EmployeeEditor.Application.Employees.Get.GetById
{
    public record GetEmployeeByIdQuery(Guid Id) : IRequest<Employee>;
}
