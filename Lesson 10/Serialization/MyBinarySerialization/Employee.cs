using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace MyBinarySerialization
{
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