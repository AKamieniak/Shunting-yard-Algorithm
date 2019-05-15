using System.Collections.Generic;

namespace Calculator.Library
{
    public class Constants
    {
        public const char LeftBracket = '(';
        public const char RightBracket = ')';
        public const char MinusSign = '-';
        public const char PlusSign = '+';
        public const char DivisionSign = '/';
        public const char MultiplicationSign = '*';
        public const char ExpSign = '^';

        public static List<char> CharsList = new List<char>()
        {
            LeftBracket, RightBracket, MinusSign, PlusSign, DivisionSign, MultiplicationSign, ExpSign
        };

        public static Dictionary<char, int> SignsValues = new Dictionary<char, int>()
        {
            {ExpSign, 4},
            {MultiplicationSign, 3},
            {DivisionSign, 3},
            {PlusSign, 2},
            {MinusSign, 2},
            {LeftBracket, 1}
        };
    }
}
