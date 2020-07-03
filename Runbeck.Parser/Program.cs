#region Usings

using Microsoft.Extensions.DependencyInjection;
using Runbeck.Parser.Output;
using Runbeck.Parser.UI;

#endregion

namespace Runbeck.Parser
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //setup our DI
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IApp, App>()
                .AddSingleton<IUserInterface, ConsoleUserInterface>()
                .AddSingleton<IOutputWriter, FileWriter>()
                .BuildServiceProvider();

            var app = serviceProvider.GetService<IApp>();

            app.Run();
        }
    }
}