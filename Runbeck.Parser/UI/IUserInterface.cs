#region Usings

using System;

#endregion

namespace Runbeck.Parser.UI
{
    public interface IUserInterface
    {
        void WriteLine(string s);
        string ReadLine();
        ConsoleKey ReadKey();
    }
}