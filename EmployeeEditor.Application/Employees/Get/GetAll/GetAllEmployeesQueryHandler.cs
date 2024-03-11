using EmployeeEditor.Application.Abstractions;
using EmployeeEditor.Domain.Models.Employee;
using MediatR;

namespace EmployeeEditor.Application.Employees.Get.GetAll
{
    internal class GetAllEmployeesQueryHandler : IRequestHandler<GetAllEmployeesQuery, IEnumerable<Employee>>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public GetAllEmployeesQueryHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<IEnumerable<Employee>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken) =>
            await _employeeRepository.GetAllEmployeesAsync();
    }
}
