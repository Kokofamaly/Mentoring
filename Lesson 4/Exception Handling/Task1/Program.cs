using System;

namespace Task1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Enter your input here:");
            string input = Console.ReadLine();
            try
            {
                PrintFirstCharacter(input);
            }
            catch(ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void PrintFirstCharacter(string input)
        {
            if(string.IsNullOrEmpty(input)) throw new ArgumentException("Input is empty string.");
            Console.WriteLine(input[0]);
        }
    }
}