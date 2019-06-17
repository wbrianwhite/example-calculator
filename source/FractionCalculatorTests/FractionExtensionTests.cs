using Xunit;
using FractionCalculator;
using Fractions;

namespace FractionCalculatorTests
{
	public class FractionExtensionTests
	{
		[Fact]
		public void FourAndAHalfTest()
		{
			Fraction f = Fraction.FromString("9/2");
			string s = f.ToConsoleOutput();
			Assert.Equal("4_1/2", s);
		}
	}
}
