using System;
using Fractions;

namespace FractionCalculator
{
	/// <summary>
	/// We want to be about to output 9/2 as 8_1/2
	/// </summary>
	public static class FractionExtensions
	{
		public static string ToConsoleOutput(this Fraction f)
		{
			decimal val = f.ToDecimal();
			string sval = val.ToString();
			if (sval.Contains("."))
			{
				string[] pieces = sval.Split('.');
				int whole = int.Parse(pieces[0]);
				decimal partial = decimal.Parse("." + pieces[1]);
				Fraction fpartial = Fraction.FromDecimal(partial);
				return $"{whole}_{fpartial}";
			}
			else
			{
				return f.ToString();
			}
		}
	}
}
