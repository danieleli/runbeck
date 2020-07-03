using System;

namespace Runbeck.Parser.UI
{
    public interface IUserInterface
    {
        void WriteLine(string s);
        string ReadLine();
        ConsoleKey ReadKey();
    }
}