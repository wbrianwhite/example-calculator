using Xunit;
using FractionCalculator;
using Fractions;
using System;

namespace FractionCalculatorTests
{
	public class MathRunnerTests
	{
		[Fact]
		public void Parse3Divide()
		{
			string[] values = new string[] { "3", "/", "2" };
			MathNode[] parsed = Parser.Parse(values);
			Fraction result = MathRunner.Calculate(parsed);
			Fraction expected = Fraction.FromString("3/2");
			Assert.Equal(expected, result);
		}

		[Fact]
		public void Parse3Multiply()
		{
			string[] values = new string[] { "3_1/2", "*", "1_1/2" };
			MathNode[] parsed = Parser.Parse(values);
			Fraction result = MathRunner.Calculate(parsed);
			Fraction expected = Fraction.FromString("21/4");
			Assert.Equal(expected, result);
		}

		[Fact]
		public void Parse3Add()
		{
			string[] values = new string[] { "3_1/2", "+", "1_1/2" };
			MathNode[] parsed = Parser.Parse(values);
			Fraction result = MathRunner.Calculate(parsed);
			Fraction expected = Fraction.FromString("5");
			Assert.Equal(expected, result);
		}

		[Fact]
		public void Parse3Subtract()
		{
			string[] values = new string[] { "3_1/2", "-", "1_1/2" };
			MathNode[] parsed = Parser.Parse(values);
			Fraction result = MathRunner.Calculate(parsed);
			Fraction expected = Fraction.FromString("2");
			Assert.Equal(expected, result);
		}

		[Fact]
		public void DivideByZero()
		{
			string[] values = new string[] { "3", "/", "0" };
			MathNode[] parsed = Parser.Parse(values);
			Exception ex = Assert.Throws<InvalidOperationException>(() => MathRunner.Calculate(parsed));
		}

		[Fact]
		public void InvalidInputThrowsTrailingOperator()
		{
			string[] values = new string[] { "3_1/2", "+", "1_1/2", "*" };
			MathNode[] parsed = Parser.Parse(values);
			Exception ex = Assert.Throws<InvalidOperationException>(() => MathRunner.Calculate(parsed));		
		}

		[Fact]
		public void InvalidInputThrowsTrailingFraction()
		{
			string[] values = new string[] { "3_1/2", "+", "1_1/2", "1/4" };
			MathNode[] parsed = Parser.Parse(values);
			Exception ex = Assert.Throws<InvalidOperationException>(() => MathRunner.Calculate(parsed));		
		}
	}
}
