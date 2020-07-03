#region Usings

using System;

#endregion

namespace Runbeck.Parser.UI
{
    public class ConsoleUserInterface : IUserInterface
    {
        public void WriteLine(string s)
        {
            Console.WriteLine(s);
        }

        public string ReadLine()
        {
            return Console.ReadLine();
        }

        public ConsoleKey ReadKey()
        {
            return Console.ReadKey().Key;
        }
    }
}