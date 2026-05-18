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

    
}