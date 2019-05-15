using System.Collections.Generic;

namespace Calculator.ConsoleProject
{
    public class UserInput
    {
        public string Equation { get; set; }

        private readonly List<string> _signs = new List<string>()
            {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "+", "-", "*", "/", "(", ")", "^", " ","-"};

        public UserInput(string equation)
        {
            Equation = equation;
        }

        public bool StringCheck()
        {
            if (Equation.Length == 0)
            {
                return _signs.Contains(Equation);
            }
            foreach (var sign in Equation)
            {
                if (!_signs.Contains(sign.ToString()))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
