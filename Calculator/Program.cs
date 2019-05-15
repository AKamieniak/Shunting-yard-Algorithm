using Calculator.ConsoleProject.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Calculator.ConsoleProject
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddTransient<IUserInterface, UserInterface>()
                .BuildServiceProvider();                      

            var control = serviceProvider.GetService<IUserInterface>();
            control.ShowInterface();
        }
    }
}
