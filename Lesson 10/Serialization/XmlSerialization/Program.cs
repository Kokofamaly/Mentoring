using System.Text;
using System.Xml.Serialization;
using ClassesToSerialize;


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

}