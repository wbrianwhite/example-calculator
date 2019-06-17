using System;
using Fractions;

namespace FractionCalculator
{
	/// <summary>
	/// Node represents one input argument, either a Fraction number or an operator
	/// </summary>
	public class MathNode
	{
		public Enums.NodeType NodeType { get; set; }
		public Fraction? Value { get; set; }
		public string Operator { get; set; }

		/// <summary>
		/// Create a MathNode to hold a fraction
		/// </summary>
		/// <param name="input"></param>
		public MathNode(Fraction input)
		{
			Value = input;
			NodeType = Enums.NodeType.Fraction;
		}

		/// <summary>
		/// Create a MathNode to hold an operator
		/// </summary>
		/// <param name="input">Must be +, -, /, *</param>
		/// <exception cref="InvalidOperationException">When supplied a string other than the OperatorConstants values in AllowedOperators.IsAnOperator</exception>
		public MathNode(string input)
		{
			if (AllowedOperators.IsAnOperator(input))
			{
				Operator = input;
				NodeType = Enums.NodeType.Operation;
			}
			else
			{
				throw new InvalidOperationException($"{nameof(input)} is not in the list of allowed operators (* / + -)");
			}
		}		
	}
}
