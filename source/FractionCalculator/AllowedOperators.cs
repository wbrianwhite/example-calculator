namespace FractionCalculator
{
	/// <summary>
	/// Controls which OperatorConsts are allowed
	/// </summary>
	public static class AllowedOperators
	{
		/// <summary>
		/// Returns true for PLUS, MINUS, MULTIPLY, DIVIDE, false otherwise
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		static public bool IsAnOperator(string input)
		{
			return (input == OperatorConsts.PLUS || input == OperatorConsts.MINUS || input == OperatorConsts.MULTIPLY || input == OperatorConsts.DIVIDE);
		}

	}
}
