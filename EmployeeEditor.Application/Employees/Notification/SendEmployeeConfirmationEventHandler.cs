using MediatR;
using Microsoft.Extensions.Logging;

namespace EmployeeEditor.Application.Employees.Notification
{
    internal sealed class SendEmployeeConfirmationEventHandler : INotificationHandler<EmployeeCreatedEvent>
    {
        private readonly ILogger<SendEmployeeConfirmationEventHandler> _logger;

        public SendEmployeeConfirmationEventHandler(ILogger<SendEmployeeConfirmationEventHandler> logger)
        {
            _logger = logger;
        }

        public async Task Handle(EmployeeCreatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Sending employee confirmation {@EmployeeId}", notification.Id);

            await Task.Delay(2000, cancellationToken);

            _logger.LogInformation("Employee confirmation sent {@EmployeeId}", notification.Id);
        }
    }
}
