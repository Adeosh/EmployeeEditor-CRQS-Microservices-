using EmployeeEditor.Application.Abstractions;
using EmployeeEditor.Domain.Models.Employee;
using MediatR;

namespace EmployeeEditor.Application.Employees.Get.GetById
{
    internal class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, Employee>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public GetEmployeeByIdQueryHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<Employee> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken) =>
            await _employeeRepository.GetEmployeeByIdAsync(request.Id);
    }
}
