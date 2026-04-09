using System;

namespace Task2
{
    public class NumberParser : INumberParser
    {
        /*
        Вопрос по логике метода Parse: Что должен выдавать метод, если он, к примеру, получает строку --123?
        FormatException или число 123?
        */
        public int Parse(string stringValue)
        {

            if(stringValue == null) 
                throw new ArgumentNullException();

            stringValue = stringValue.Trim();

            if(string.IsNullOrEmpty(stringValue)) 
                throw new FormatException();
            
            int result = 0;
            int sign = 1;

            if(stringValue.StartsWith("-")) 
            {
                sign = -1;
                stringValue = stringValue.TrimStart('-');
            }
            else if (stringValue.StartsWith("+"))
            {
                stringValue = stringValue.TrimStart('+');
            }

            foreach(char c in stringValue)
            {
                if(!char.IsDigit(c)) 
                    throw new FormatException();

                if((result * sign) > ((int.MaxValue - (c - '0')) / 10) || (result * sign) < ((int.MinValue + (c - '0')) / 10))
                { 
                    throw new OverflowException();
                }
                result = result * 10 + (c - '0');
            }

            return result * sign;

        }
    }
}