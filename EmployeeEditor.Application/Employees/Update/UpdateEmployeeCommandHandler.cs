using EmployeeEditor.Application.Abstractions;
using EmployeeEditor.Application.Abstractions.Data;
using EmployeeEditor.Domain.Models.Employee;
using MediatR;

namespace EmployeeEditor.Application.Employees.Update
{
    internal class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateEmployeeCommandHandler(IEmployeeRepository employeeRepository, IUnitOfWork unitOfWork)
        {
            _employeeRepository = employeeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(request.Id);

            if (employee == null)
            {
                throw new EmployeeNotFoundException(request.Id);
            }

            employee.Update(
                request.FirstName,
                request.MiddleName,
                request.LastName,
                request.Age,
                Email.Create(request.Email)!,
                request.Department,
                request.Salary,
                request.IsActive,
                request.UpdatedAt ?? DateTime.UtcNow);

            _employeeRepository.UpdateEmployee(employee);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
