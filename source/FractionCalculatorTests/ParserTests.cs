using Xunit;
using FractionCalculator;
using Fractions;

namespace FractionCalculatorTests
{
    public class ParserTests
    {
        [Fact]
        public void Parse3()
        {
			string[] values = new string[] { "3_1/2", "*", "1_1/2" };
			MathNode[] parsed = Parser.Parse(values);
			Assert.Equal(3, parsed.Length);
			Assert.Equal(OperatorConsts.MULTIPLY, parsed[1].Operator);
			Fraction f1 = Fraction.FromString("7/2");
			Assert.Equal(f1, parsed[0].Value);
			Fraction f2 = Fraction.FromString("3/2");
			Assert.Equal(f2, parsed[2].Value);
		}

		/// <summary>
		/// 4 should probably not be valid, there will be a trailing operator or number.
		/// For now parser doesn't validate, and it is rejected at runtime by calculator
		/// </summary>
		[Fact]
		public void Parse4()
		{
			string[] values = new string[] { "3_1/2", "*", "1_1/2", "+" };
			MathNode[] parsed = Parser.Parse(values);
			Assert.Equal(4, parsed.Length);
			Assert.Equal(OperatorConsts.MULTIPLY, parsed[1].Operator);
			Fraction f1 = Fraction.FromString("7/2");
			Assert.Equal(f1, parsed[0].Value);
			Fraction f2 = Fraction.FromString("3/2");
			Assert.Equal(f2, parsed[2].Value);
			Assert.Equal(OperatorConsts.PLUS, parsed[3].Operator);
		}
    }
}
