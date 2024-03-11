using EmployeeEditor.Application.Abstractions;
using EmployeeEditor.Application.Abstractions.Data;
using EmployeeEditor.Domain.Models.Employee;
using MediatR;
using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("EmployeeEditor.Tests")]

namespace EmployeeEditor.Application.Employees.Delete
{
    internal class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteEmployeeCommandHandler(IEmployeeRepository employeeRepository, IUnitOfWork unitOfWork)
        {
            _employeeRepository = employeeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(request.Id);
            if (employee == null)
            {
                throw new EmployeeNotFoundException(request.Id);
            }

            _employeeRepository.RemoveEmployee(employee);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
