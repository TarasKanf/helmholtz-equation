using System;
using System.IO;
using System.Text;

namespace SocialNetwork.UI.Console.InputOutput
{
    internal class ConsoleService : IInputOutputSevice
    {
        private const string Star = "*";
        private const string Bb = "\b \b";

        public TextWriter Out => System.Console.Out;

        public TextReader In => System.Console.In;

        /// <summary>
        ///     Allows to enter a hidden password and read it.
        /// </summary>
        /// <returns></returns>
        public string ReadPassword()
        {
            var password = new StringBuilder();
            ConsoleKeyInfo key;
            do
            {
                key = System.Console.ReadKey(true);

                if (key.Key != ConsoleKey.Backspace 
                    && key.Key != ConsoleKey.Enter)
                {
                    password.Append(key.KeyChar);
                    Out.Write(Star);
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace 
                        && password.Length > 0)
                    {
                        password.Remove(password.Length - 1, 1);
                        Out.Write(Bb);
                    }
                }
            }
            while (key.Key != ConsoleKey.Enter);

            Out.WriteLine();

            return password.ToString();
        }

        public void SetBackgroundColor(ConsoleColor color)
        {
            System.Console.BackgroundColor = color;
        }

        public void SetForegroundColor(ConsoleColor color)
        {
            System.Console.ForegroundColor = color;
        }

        public void ResetForgroundColor()
        {
            System.Console.ResetColor();
        }
    }
}