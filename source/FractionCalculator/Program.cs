﻿using System;
using FractionCalculator;
using Fractions;

/*
the next step, we ask that you tackle the below coding problem. 

Coding Challenge

Write a command line program in the language of your choice that will take operations on fractions as an input and produce a fractional result.
Legal operators shall be *, /, +, - (multiply, divide, add, subtract)
Operands and operators shall be separated by one or more spaces
Mixed numbers will be represented by whole_numerator/denominator. e.g. "3_1/4"
Improper fractions and whole numbers are also allowed as operands 
Example run:
? 1/2 * 3_3/4
= 1_7/8

? 2_3/8 + 9/8
= 3_1/2
*/

namespace FractionCaclulator
{
	public class Program
	{
		public static void Main(string[] args)
		{
			MathNode[] nodes = Parser.Parse(args);
			Fraction result = MathRunner.Calculate(nodes);
			Console.WriteLine("= " + result.ToConsoleOutput());
		}
	}
}
