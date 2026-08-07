using System.Text.Json;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.Json.Serialization;

namespace JsonSerialization
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Department department = new Department
            {
                DepartmentName = "TestDepartment",
                Employees = new(){new Employee{ EmployeeName = "Test Name" }}
            };
            var jsonSerialized = JsonSerializer.Serialize(department, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText("department.json", jsonSerialized);

            var read = File.ReadAllText("department.json");
            var result = JsonSerializer.Deserialize<Department>(read);

            Console.WriteLine(result?.ToString());
        }
    }

    public class Employee
    {
        [JsonPropertyName("employee_name")]
        public string? EmployeeName { get; set; }
    }

    public class Department
    {
        [JsonPropertyName("department_name")]
        public string? DepartmentName { get; set; }
        [JsonPropertyName("employees")]
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
}