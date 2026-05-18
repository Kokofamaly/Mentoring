using System.Text.Json;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.Json.Serialization;
using ClassesToSerialize;

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
}