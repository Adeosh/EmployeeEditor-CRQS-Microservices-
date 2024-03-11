using System.Reflection;

namespace EmployeeEditor.Tests
{
    public abstract class BaseTest
    {
        protected static readonly Assembly DomainAssambly = typeof(Employee).Assembly;
    }
}
