using System;
using Calculator.Library;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Calculator.Tests
{
    [TestClass]
    public class EquationTests
    {
        [TestMethod]
        public void Result_WithValidValues()
        {
            var input = "((15 / (7 - (1 + 1))) * 3) - (2 + (1 + 1))";
            var equation = new Equation(input);

            var result = equation.Result;

            Assert.AreEqual(5, result);
        }

        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException))]
        public void Result_DivideByZero()
        {
            var input = "15/0";
            var equation = new Equation(input);

            var inputEquationResult = equation.Result;
        }

        [TestMethod]
        public void OperatorTest()
        {
            var eq1 = new Equation("1+3");
            var eq2 = new Equation("5-3");
            var result = eq1 * eq2;

            Assert.AreEqual(8, result);
        }

        [TestMethod]
        public void OverrideToStringTest()
        {
            var e1 = new Equation("1+4");
            var e2 = new Equation("1+7");

            Assert.AreEqual("1+4-1+7=-3",$"{e1}-{e2}={e1 - e2}");
        }
    }
}
