using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using ClassesToSerialize;

namespace BinarySerialization
{
    public class Program
    {
        public static void Main(string[] args)
        {
            
            Department department = new Department
            {
                DepartmentName = "TestDepartment",
                Employees = new List<Employee>(){new Employee{ EmployeeName = "Test Name" }}
            };

            #pragma warning disable SYSLIB0011
            var formatter = new BinaryFormatter();
            using var fsWriter = new FileStream("department.bin", FileMode.Create);
            formatter.Serialize(fsWriter, department);

            using var fsReader = new FileStream("department.bin", FileMode.Open);
            var dept = formatter.Deserialize(fsReader) as Department;
            if(dept != null) Console.WriteLine(dept);

        }
    }
    
}