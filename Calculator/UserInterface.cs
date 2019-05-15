using System;
using Calculator.ConsoleProject.Interfaces;
using Calculator.Library;

namespace Calculator.ConsoleProject
{
    public class UserInterface : IUserInterface
    {
        public void ShowInterface()
        {
            while (true)
            {
                Console.WriteLine("1. Input one string of signs: numbers and operators \n" +
                                  "2. Builder equation.");
                var option = Console.ReadLine();

                switch (option)
                {
                    case ("1"):
                        Console.WriteLine("Type one string of signs:");

                        var equation = Console.ReadLine();
                        var userInput = new UserInput(equation);

                        if (!userInput.StringCheck())
                        {
                            Console.WriteLine("Typed wrong character.");
                            continue;
                        }

                        var equationObject = new Equation(equation);
                        var result = equationObject.Result;

                        Console.WriteLine("Result " + result);
                        break;

                    case ("2"):
                        var equationPart = "";

                        while (true)
                        {
                            Console.WriteLine("Type number, operator or bracket. Equation= " + equationPart);

                            var sign = Console.ReadLine();

                            if (sign.Equals("="))
                            {
                                break;
                            }

                            equationPart = equationPart + sign;
                        }

                        var userInput2 = new UserInput(equationPart);

                        if (!userInput2.StringCheck())
                        {
                            Console.WriteLine("Typed wrong character.");
                            continue;
                        }

                        var equationObject2 = new Equation(equationPart);
                        var result2 = equationObject2.Result;

                        Console.WriteLine("Result " + result2);
                        break;

                    default:
                        Console.WriteLine("Error. Type a right number. \n");
                        break;
                }
            }
        }
    }
}
