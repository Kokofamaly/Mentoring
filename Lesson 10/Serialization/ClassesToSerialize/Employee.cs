using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json.Serialization;



namespace ClassesToSerialize
{

    [Serializable]
    public class Employee
    {
        [JsonPropertyName("employee_name")]
        [XmlAttribute]
        public string? EmployeeName { get; set; }
    }

}
