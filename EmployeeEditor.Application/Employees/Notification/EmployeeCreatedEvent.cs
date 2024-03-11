using MediatR;

namespace EmployeeEditor.Application.Employees.Notification
{
    public record EmployeeCreatedEvent(Guid Id) : INotification;
}
