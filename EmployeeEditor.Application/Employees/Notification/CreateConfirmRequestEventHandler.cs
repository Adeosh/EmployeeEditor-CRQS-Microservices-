using MediatR;
using Microsoft.Extensions.Logging;

namespace EmployeeEditor.Application.Employees.Notification
{
    internal sealed class CreateConfirmRequestEventHandler : INotificationHandler<EmployeeCreatedEvent>
    {
        private readonly ILogger<CreateConfirmRequestEventHandler> _logger;

        public CreateConfirmRequestEventHandler(ILogger<CreateConfirmRequestEventHandler> logger)
        {
            _logger = logger;
        }

        public async Task Handle(EmployeeCreatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting to create employee request {@EmployeeId}", notification.Id);
        }
    }
}
