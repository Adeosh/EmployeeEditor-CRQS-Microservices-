using EmployeeEditor.Application.Abstractions;
using EmployeeEditor.Domain.Models.Employee;
using MediatR;

namespace EmployeeEditor.Application.Employees.Get.GetByEmail
{
    internal class GetEmployeeByEmailQueryHandler : IRequestHandler<GetEmployeeByEmailQuery, Employee>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public GetEmployeeByEmailQueryHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<Employee> Handle(GetEmployeeByEmailQuery request, CancellationToken cancellationToken) =>
            await _employeeRepository.GetEmployeeByEmailAsync(request.Email);
    }
}
