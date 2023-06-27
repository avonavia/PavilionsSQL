using System;
using System.Linq;

namespace PasswordLevelLibrary
{
    public class PasswordCheck
    {
        public int CheckPassword(string password)
        {
            bool hasNumbers = false;
            bool hasSymbols = false;

            string numbers = "1234567890";
            string symbols = "@/?.,!&";

            int level = 0;

            if (password.Length >= 8)
            {
                level++;
            }
            if (password.Any(Char.IsUpper) && password.Any(Char.IsLower))
            {
                level++;
            }
            for (int i = 0; i < numbers.Length; i++)
            {
                if (password.Contains(numbers[i]))
                {
                    hasNumbers = true;
                }
            }
            if (hasNumbers)
            {
                level++;
            }
            for (int i = 0; i < symbols.Length; i++)
            {
                if (password.Contains(symbols[i]))
                {
                    hasSymbols = true;
                }
            }
            if (hasSymbols)
            {
                level++;
            }
            return level;
        }
    }
}
