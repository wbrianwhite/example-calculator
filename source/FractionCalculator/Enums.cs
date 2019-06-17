using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FractionCalculator
{
	public static class Enums
	{
		/// <summary>
		/// Used in MathNode to indicate if the math node is a Fraction or Operation
		/// </summary>
		public enum NodeType
		{
			Fraction = 0,
			Operation
		}
	}
}
