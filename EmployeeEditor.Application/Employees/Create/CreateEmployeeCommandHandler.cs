using EmployeeEditor.Application.Abstractions.Data;
using EmployeeEditor.Application.Abstractions;
using EmployeeEditor.Domain.Models.Employee;
using MediatR;
using EmployeeEditor.Application.Employees.Notification;

namespace EmployeeEditor.Application.Employees.Create
{
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPublisher _publisher;

        public CreateEmployeeCommandHandler(
            IEmployeeRepository employeeRepository,
            IUnitOfWork unitOfWork,
            IPublisher publisher)
        {
            _employeeRepository = employeeRepository;
            _unitOfWork = unitOfWork;
            _publisher = publisher;
        }

        public async Task Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = new Employee(
                request.FirstName,
                request.MiddleName,
                request.LastName,
                request.Age,
                Email.Create(request.Email)!,
                request.Department,
                request.Salary,
                request.IsActive,
                request.EmploymentDate ?? DateTime.UtcNow,
                request.CreatedAt ?? DateTime.UtcNow);

            _employeeRepository.AddEmployee(employee);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            await _publisher.Publish(new EmployeeCreatedEvent(employee.Id), cancellationToken);
        }
    }
}
