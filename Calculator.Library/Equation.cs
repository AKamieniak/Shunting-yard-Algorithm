using System;
using System.Collections.Generic;

namespace Calculator.Library
{
    public class Equation
    {
        private List<string> equationDivide = new List<string>();
        private Queue<string> outputQueue = new Queue<string>();
        private string EquationSigns { get; set; }
        private double? _result;

        public double Result
        {
            get
            {
                if (_result.HasValue)
                {
                    return _result.Value;
                }

                return Calculate();
            }
        }

        public Equation(string equation)
        {
            EquationSigns = equation;
        }

        public static double operator +(Equation eq1, Equation eq2)
        {
            return eq1.Result + eq2.Result;
        }

        public static double operator -(Equation eq1, Equation eq2)
        {
            return eq1.Result - eq2.Result;
        }

        public static double operator /(Equation eq1, Equation eq2)
        {
            return eq1.Result / eq2.Result;
        }

        public static double operator *(Equation eq1, Equation eq2)
        {
            return eq1.Result * eq2.Result;
        }

        public override string ToString()
        {
            return EquationSigns;
        }

        private double Calculate()
        {
            StringDivider();
            ShuntingYard();
            ReverseNotation();

            return _result.Value;
        }

        private void StringDivider()
        {
            EquationSigns = EquationSigns.Replace(" ", string.Empty);
            var number = string.Empty;
            var bracketNegative = false;
            var negativeNumber = false;

            for (var i = 0; i < EquationSigns.Length; i++)
            {
                var convertable = int.TryParse(EquationSigns[i].ToString(), out var signNumber);

                if (convertable)
                {
                    if (negativeNumber)
                    {
                        number = Constants.MinusSign + signNumber.ToString();
                        negativeNumber = false;
                    }
                    else
                    {
                        number += signNumber.ToString();
                    }

                    if (i == EquationSigns.Length - 1)
                    {
                        equationDivide.Add(number);
                    }
                }
                else
                {
                    if (EquationSigns[i] == Constants.LeftBracket)
                    {
                        if (EquationSigns[i + 1] == Constants.MinusSign)
                        {
                            bracketNegative = true;
                            negativeNumber = true;
                            i = i + 1;
                            continue;
                        }
                    }

                    if (bracketNegative)
                    {
                        if (EquationSigns[i] == Constants.RightBracket)
                        {
                            bracketNegative = false;
                            equationDivide.Add(number);
                            number = "";
                            continue;
                        }
                    }

                    if (number != "")
                    {
                        equationDivide.Add(number);
                        number = "";
                    }

                    equationDivide.Add(EquationSigns[i].ToString());
                }
            }
        }

        private void ShuntingYard()
        {
            var operatorStack = new Stack<char>();

            foreach (var sign in equationDivide)
            {
                var convertable = int.TryParse(sign, out var signValue);

                if (convertable)
                {
                    outputQueue.Enqueue(signValue.ToString());
                }
                else
                {
                    if (sign.Equals(Constants.LeftBracket.ToString()))
                    {
                        operatorStack.Push(sign[0]);
                    }
                    else if (sign.Equals(Constants.RightBracket.ToString()))
                    {
                        while (operatorStack.Count != 0)
                        {
                            var op = operatorStack.Pop();

                            if (op == Constants.LeftBracket)
                            {
                                break;
                            }
                            else
                            {
                                outputQueue.Enqueue(op.ToString());
                            }
                        }
                    }
                    else
                    {
                        if (operatorStack.Count != 0)
                        {
                            var precedence = ComparePrecedences(operatorStack.Peek(), sign[0]);

                            if (precedence)
                            {
                                outputQueue.Enqueue(operatorStack.Pop().ToString());
                                operatorStack.Push(sign[0]);
                            }
                            else
                            {
                                operatorStack.Push(sign[0]);
                            }
                        }
                        else
                        {
                            operatorStack.Push(sign[0]);
                        }
                    }

                }
            }

            while (operatorStack.Count != 0)
            {
                outputQueue.Enqueue(operatorStack.Pop().ToString());
            }
        }

        private bool ComparePrecedences(char sign1, char sign2)
        {
            var value1 = Constants.SignsValues[sign1];
            var value2 = Constants.SignsValues[sign2];

            if (value1 == 4 && value2 == 4)
            {
                return false;
            }
            else if (value1 >= value2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void ReverseNotation()
        {
            var numbersStack = new Stack<double>();

            while (outputQueue.Count != 0)
            {
                var sign = outputQueue.Dequeue();

                var convertable = int.TryParse(sign, out var number);

                if (convertable)
                {
                    numbersStack.Push(number);
                }
                else
                {
                    var number1 = numbersStack.Pop();
                    var number2 = numbersStack.Pop();
                    var result = Operations(number1, number2, Convert.ToChar(sign));

                    numbersStack.Push(result);
                }
            }

            _result = numbersStack.Pop();
        }

        private double Operations(double number1, double number2, char sign)
        {
            switch (sign)
            {
                case (Constants.PlusSign):
                    return number1 + number2;
                case (Constants.MinusSign):
                    return number2 - number1;
                case (Constants.MultiplicationSign):
                    return number1 * number2;
                case (Constants.DivisionSign):
                    if (number1 == 0)
                    {
                        throw new DivideByZeroException();
                    }
                    return number2 / number1;
                case (Constants.ExpSign):
                    try
                    {
                        return Math.Pow(number2, number1);
                    }
                    catch (Exception e)
                    {
                        break;
                    }
            }
            return 0;
        }

    }
}
