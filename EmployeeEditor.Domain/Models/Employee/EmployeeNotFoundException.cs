namespace EmployeeEditor.Domain.Models.Employee
{
    public sealed class EmployeeNotFoundException : Exception
    {
        public EmployeeNotFoundException(Guid id)
            : base($"The employee with the Id = {id} was not found")
        {
            
        }
    }
}
