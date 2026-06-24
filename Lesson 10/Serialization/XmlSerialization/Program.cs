using System.Text;
using System.Xml.Serialization;


namespace XmlSerialization
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

            var serializer = new XmlSerializer(typeof(Department));

            using (var fs = new FileStream("dept.xml", FileMode.Create))
            {
                serializer.Serialize(fs, department);
            }

            using (var fs = new FileStream("dept.xml", FileMode.Open))
            {
                var result = serializer.Deserialize(fs) as Department;
                if(result != null) Console.WriteLine(result);
            }
        }
    }

    public class Employee
    {
        [XmlAttribute]
        public string? EmployeeName { get; set; }
    }

    public class Department
    {
        [XmlElement("DeptName")]
        public string? DepartmentName { get; set; }
        [XmlArray("Employees")]
        [XmlArrayItem("Employee")]
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