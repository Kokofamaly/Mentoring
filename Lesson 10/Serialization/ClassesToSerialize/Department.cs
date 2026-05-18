using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json.Serialization;




namespace ClassesToSerialize
{

    [Serializable]
    public class Department
    {
        [JsonPropertyName("department_name")]
        [XmlElement("DeptName")]
        public string? DepartmentName { get; set; }
        [JsonPropertyName("employees")]
        [XmlArray("Employees")]
        [XmlArrayItem("Employee")]
        public List<Employee> Employees { get; set; } = new List<Employee>();
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