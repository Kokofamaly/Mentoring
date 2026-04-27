using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace MyBinarySerialization
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var employee = new Employee
            {
                Name = "Vadim",
                Age = 23
            };

            var formatter = new BinaryFormatter();

            using (var fs = new FileStream("employee.bin", FileMode.Create))
            {
                formatter.Serialize(fs, employee);
            }

            using (var fs = new FileStream("employee.bin", FileMode.Open))
            {
                var result = formatter.Deserialize(fs) as Employee;
                if(result != null) Console.WriteLine(result);
            }
        }
    }

    [Serializable]
    public class Employee : ISerializable
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public Employee(){}
        protected Employee(SerializationInfo info, StreamingContext context)
        {
            Name = info.GetString("name");
            Age = info.GetInt32("age");
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("name", Name);
            info.AddValue("age", Age);
        }

        public override string ToString()
        {
            return $"{Name} – {Age}";
        }
    }
}