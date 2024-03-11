using EmployeeEditor.Application.Abstractions;
using EmployeeEditor.Domain.Models.Employee;
using MediatR;

namespace EmployeeEditor.Application.Employees.Get.GetByDepartment
{
    internal class GetEmployeesByDepartmentQueryHandler : IRequestHandler<GetEmployeesByDepartmentQuery, IEnumerable<Employee>>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public GetEmployeesByDepartmentQueryHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<IEnumerable<Employee>> Handle(GetEmployeesByDepartmentQuery request, CancellationToken cancellationToken) =>
             await _employeeRepository.GetEmployeesByDepartmentAsync(request.Department);
    }
}
