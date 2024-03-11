using MediatR;

namespace EmployeeEditor.Application.Employees.Delete
{
    public record DeleteEmployeeCommand(Guid Id) : IRequest;
}
