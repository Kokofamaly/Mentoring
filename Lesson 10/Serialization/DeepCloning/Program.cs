using System.Text;
using System.Text.Json;

namespace DeepCloning
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var department = new Department
            {
                DepartmentName = "IT",
                Employees = new(){  new Employee{EmployeeName = "Vadim"}    }    
            };

            var cloneDepartment = department.DeepClone();
            var cloneEmployee = department.Employees[0].DeepClone();

            cloneEmployee.EmployeeName = "Fred";
            cloneDepartment.DepartmentName = "Design";

            if(cloneDepartment.Employees.Any()) cloneDepartment.Employees.Clear();
            cloneDepartment.Employees.Add(cloneEmployee);

            Console.WriteLine(department);
            Console.WriteLine(cloneDepartment);
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
        public override string ToString()
        {
            var sb = new StringBuilder($"{DepartmentName}\nEmployees: \n");
            foreach(var e in Employees)
            {
                sb.Append($"{e.EmployeeName}\n");
            }
            return sb.ToString();
        }
    }
    public static class DeepCloner
    {
        public static T DeepClone<T>(this T source)
        {
            ArgumentNullException.ThrowIfNull(source);

            var json = JsonSerializer.Serialize(source);
            return JsonSerializer.Deserialize<T>(json);
        }
    }
}