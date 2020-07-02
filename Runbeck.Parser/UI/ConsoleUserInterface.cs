namespace Runbeck.Parser.UI
{
    using System;

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
    }
}