
namespace BinarySerialization
{
    public class Program
    {
        public static void Main(string[] args)
        {
            
        }
    }

    public class Employee
    {
        public string? EmployeeName { get; set; }
    }

    public class Department
    {
        public string? DepartmentName { get; set; }
        public List<Employee> Employees { get; set; } = new();
    }
}