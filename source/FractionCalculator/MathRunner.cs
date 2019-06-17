using System;
using System.Collections.Generic;
using System.Diagnostics;
using Fractions;

namespace FractionCalculator
{
	/// <summary>
	/// Handles a map/reduce style multi pass over MathNode[] executing one operator at a time
	/// </summary>
	public static class MathRunner
	{
		/// <summary>
		/// Runs Divide, Multiply, Plus, and Minus on the input array.  Finally left with a one node array holding our result in its Value
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public static Fraction Calculate(MathNode[] input)
		{
#if DEBUG
			Console.WriteLine($"Calculate starting with {input.Length} long array");
#endif
			MathNode[] afterDivide = ProcessOperator(input, OperatorConsts.DIVIDE);
			MathNode[] afterMultiply = ProcessOperator(afterDivide, OperatorConsts.MULTIPLY);
			MathNode[] afterPlus = ProcessOperator(afterMultiply, OperatorConsts.PLUS);
			MathNode[] afterMinus = ProcessOperator(afterPlus, OperatorConsts.MINUS);
			//At this point you should have a one length array
			if (afterMinus.Length > 1)
			{
				throw new InvalidOperationException($"array was {afterMinus.Length} nodes long after running all 4 operator passes");
			}
			if (afterMinus[0].NodeType != Enums.NodeType.Fraction)
			{
				throw new InvalidOperationException($"array was left with {afterMinus[0].NodeType} node type and {afterMinus[0].Operator} operator");
			}
			return (Fraction)afterMinus[0].Value;
		}

		/// <summary>
		/// To process a 3 * 7 we need to be able to go one down and one up in index from current index
		/// </summary>
		/// <param name="operatorIndex"></param>
		/// <param name="sourceArraySize"></param>
		/// <returns></returns>
		internal static bool OperatorHasLeftAndRightMovesAvailable(int operatorIndex, int sourceArraySize)
		{
			if (operatorIndex == 0)
			{
				//we can't go down one.  We could throw an exception here for invalid input string like: " * 3 2"
				return false;
			}
			//-1 for highest array index one below array length
			//-1 because we need to be able to go up one.
			// We could throw an exception here for invalid input string like: "3 * *"
			if (operatorIndex > (sourceArraySize - 1 - 1))
			{
				return false;
			}
			return true;
		}

		/// <summary>
		/// Runs through an array of MathNodes executing the supplied operator only.  Not perfect... i think multiplication and division are same priority
		/// </summary>
		/// <param name="input"></param>
		/// <param name="op"></param>
		/// <returns></returns>
		internal static MathNode[] ProcessOperator(MathNode[] input, string op)
		{
			int elements = input.Length;
			var ret = new List<MathNode>();
			for (var i = 0; i < input.Length; i++)
			{
				if (input[i].NodeType == Enums.NodeType.Operation && input[i].Operator == op)
				{
					Fraction? temp = null;
					if (OperatorHasLeftAndRightMovesAvailable(i, input.Length))
					{
						//Make sure an operator is surrounded by numbers only
						if (input[i - 1].NodeType != Enums.NodeType.Fraction || input[i + 1].NodeType != Enums.NodeType.Fraction)
						{
							throw new InvalidOperationException($"Tried to process an operator not surrounded by 2 numbers. i {i}, node types: {input[i - 1].NodeType}, {input[i].NodeType}, {input[i + 1].NodeType}");
						}
						Fraction last = (Fraction) input[i - 1].Value;
						Fraction next = (Fraction) input[i + 1].Value;
						if (last == null || next == null)
						{
							throw new InvalidOperationException($"Tried to process a null number node. i {i}, last: {last}, next: {next}");
						}
						switch (op)
						{
							case OperatorConsts.DIVIDE:
								//Need to test for divide by 0,
								if (next == 0)
								{
									throw new InvalidOperationException($"About to divide by 0 {last}/{next}");
								}
								else
								{
									temp = last.Divide(next);
									break;
								}
							case OperatorConsts.MULTIPLY:
								temp = last.Multiply(next);
								break;
							case OperatorConsts.PLUS:
								temp = last.Add(next);
								break;
							case OperatorConsts.MINUS:
								temp = last.Subtract(next);
								break;
						}
						//TODO: there is a better way to do this, can't think of it at the moment.  We already added the previous node to the ret list.  Yank it out.  
						//Add this number.  And then, skip i ahead 1 because that way we skip over the processing of the next numeric node which will be a no-op, and 
						//add an entry in the ret list
						ret.RemoveAt(ret.FindLastIndex(x => x.NodeType == Enums.NodeType.Fraction && x.Value == last));
						ret.Add(new MathNode((Fraction)temp));
						i++;
					}
					else
					{
						//Would use some normal logging...
						Debug.WriteLine($"On index {i}, input is {input.Length} long, node type {input[i].NodeType}, operator? {input[i].Operator}, number? {input[i].Value}");
						throw new InvalidOperationException($"Operator with no available next or previous fraction node. On index {i}, input is {input.Length}");
					}


				}
				else
				{
					ret.Add(input[i]);
				}
			}

			return ret.ToArray();
		}
	}

}
