using System;
using Fractions;

namespace FractionCalculator
{
	/// <summary>
	/// Used to parse command line arguments into an understandable form.  Since we will linearly process the arguments using a linked list.  That lets us
	/// do an operation across 3 nodes, then replace them with a new node with 
	/// </summary>
	public static class Parser
	{
		/// <summary>
		/// Fractional numbers > 1 come in on command line like 3_1/4, simply remove the _ here. 
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		internal static string CleanFraction(string input) => input.Replace("_", " ");

		/// <summary>
		/// Build an array of MathNodes which either have a fraction, or an operator in them
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public static MathNode[] Parse(string[] input)
		{
			int elements = input.Length;
			MathNode[] operations = new MathNode[elements];
			for (int i = 0; i < input.Length; i++)
			{
				//Is it an operator>
				if (AllowedOperators.IsAnOperator(input[i]))
				{
					operations[i] = new MathNode(input[i]);
				}
				//Try to handle it as a fraction
				else
				{
					string c = CleanFraction(input[i]);
					try
					{
						if (c.Contains(" "))
						{
							//Fractions library only takes care of the 1/2 part from 3 1/2
							string[] split = c.Split(' ');
							Fraction f1 = Fraction.FromString(split[0]);
							Fraction f2 = Fraction.FromString(split[1]);
							operations[i] = new MathNode(f1.Add(f2));
						}
						else
						{
							//Whole number or simple fraction
							Fraction f = Fraction.FromString(c);
							operations[i] = new MathNode(f);
						}						
					}
					catch (Exception e)
					{
						//Do some logging with e
						Console.WriteLine($"Invalid fractional value {input[i]}. " + Environment.NewLine + e.Message + Environment.NewLine + e.StackTrace);
						//Don't throw e or you may lose stack info, rethrow original error, probably a parse error from test values like 'bob'
						throw;
					}
				}
			}
			return operations;
		}
	}
}
