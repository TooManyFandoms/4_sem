using System;
using System.Collections.Generic;
using System.Collections;
using RaGlib.Core;

namespace RaGlib {

	public class TSymbol: Symbol
	{
		public bool outSymbol = false;
		public TSymbol() : base() {}
		public TSymbol(string s, int production, int symbolPosition): base(s, production, symbolPosition) { }
		public TSymbol(string s, List<Symbol> attr): base(s, attr) { }
		public TSymbol(string value): base(value) { }
		public static implicit operator TSymbol(string str) => new TSymbol(str);
		public override string ToString() => this != Epsilon ? this.symbol : "e";
	}

	public class TProduction
    {
        public TSymbol LHS { set; get; } = null;
        public List<TSymbol> RHS { set; get; }
        public static int Count = 0; 
        public int Id;

        public TProduction(TSymbol LHS, List<TSymbol> RHS)
        {
            Count++;
            Id = Count;
            this.LHS = LHS;
            this.RHS = RHS;
        }
    }
	
	public class TGrammar: AGrammar
	{
		public List<TSymbol> Tout = null;
		public TGrammar() {}
		public List<TSymbol> T = null;
        public List<TSymbol> V = null;
		public List<TProduction> P = null;
		public TSymbol S0 = null;

		public TGrammar(List<TSymbol> Tin, List<TSymbol> Tout, List<TSymbol> V, string S0)
		{
			for (int i = 0; i < Tout.Count; i++)
			{
				Tout[i].outSymbol = true;
			}
			this.T = Tin;
			this.Tout = Tout;
			this.V = V;
			this.S0 = new TSymbol(S0);
			this.P = new List<TProduction>();
		}
		
		public override void Inference() {}
		
		public bool wrongCount(List<TSymbol> RHSin, List<TSymbol> RHSout)
		{
			return RHSin.Count == RHSout.Count;
		}
		
		public bool isSymbol(TSymbol LHS, List<TSymbol> RHSin, List<TSymbol> RHSout)
		{
			if (!V.Contains(LHS)) return false;
			for (int i = 0; i < RHSin.Count; i++)
			{
				if (!V.Contains(RHSin[i]) && !T.Contains(RHSin[i]))
				{
					return false;
				}
				if (!V.Contains(RHSout[i]) && !Tout.Contains(RHSout[i]))
				{
					return false;
				}
			}
			return true;
		}

		public void AddRule(TSymbol LHS, List<TSymbol> RHSin, List<TSymbol> RHSout)
		{
			if (!wrongCount(RHSin, RHSout))
			{
				Console.WriteLine("Wrong count of terminsls");
				return;
			}
			if (!isSymbol(LHS, RHSin, RHSout))
			{
				Console.WriteLine("Wrong symbol");
				return;
			}
			List<TSymbol> RHS = new List<TSymbol>();
			for (int i = 0; i < RHSin.Count; i++)
			{
				if (V.Contains(RHSin[i]))
				{
					RHS.Add(RHSin[i]);
				} 
				else
				{
					RHS.Add(RHSin[i]);
					RHSout[i].outSymbol = true;
					RHS.Add(RHSout[i]);
				}
			}
			P.Add(new TProduction(LHS, RHS));
		}

		public void printProductions()
		{
			Console.WriteLine("Translation grammar:");
			for (int i = 0; i < P.Count; i++)
			{
				Console.Write(i + 1 + ": " + P[i].LHS + " -> ");
				for (int j = 0; j < P[i].RHS.Count; j++)
				{
					if (V.Contains(P[i].RHS[j]))
					{
						Console.Write(P[i].RHS[j] + " ");
					}
					else
					{
						if (!P[i].RHS[j].outSymbol)
						{
							Console.Write("{" + P[i].RHS[j] + "} ");
						}
						else
						{
							Console.Write("<" + P[i].RHS[j] + "> ");
						
						}
					}
				}
				Console.Write("\n");
			}
		}
	}

	public class Rule
	{
		public int type = 0;
		public List<TSymbol> LHS = null;
		public List<List<TSymbol>> RHS = null;
		public Rule(int type, List<TSymbol> LHS, List<List<TSymbol>> RHS)
		{
			this.type = type;
			this.LHS = LHS;
			this.RHS = RHS;
		}
		public void printRule()
		{
			if (type == 1)
			{
				Console.Write("ф(");
				for (int i = 0; i < LHS.Count; i++)
				{
					Console.Write(LHS[i].ToString() + " ");
				}
				Console.Write(") = (");
				for (int i = 0; i < RHS.Count;i++)
				{
					Console.Write("[");
					for (int j = 0; j < RHS[i].Count;j++)
					{
						if (RHS[i][j].outSymbol)
						{
							Console.Write("<" + RHS[i][j] + "> ");
						}
						else
						{
							Console.Write(RHS[i][j] + " ");
						}
					}
					Console.Write("] ");
				}
				Console.Write(")\n");
			}
			else 
			{
				Console.Write("ф*(");
				for (int i = 0; i < LHS.Count; i++)
				{
					if (!LHS[i].outSymbol)
					{
						Console.Write(LHS[i].ToString() + " ");
					}
					else
					{
						Console.Write("<" + LHS[i].ToString() + "> ");
					}
				}
				Console.Write(") = (");
				for (int i = 0; i < RHS.Count;i++)
				{
					Console.Write("[");
					for (int j = 0; j < RHS[i].Count;j++)
					{
						if (RHS[i][j].outSymbol)
						{
							Console.Write("<" + RHS[i][j] + "> ");
						}
						else
						{
							Console.Write(RHS[i][j] + " ");
						}
					}
					Console.Write("] ");
				}
				Console.Write(")\n");
			}
		}	
	}

	public class Configuration
	{
		public List<Rule> rules = new List<Rule>();
		public TSymbol state = "s0";
		public List<TSymbol> input = null;
		public List<TSymbol> stack = null;
		public List<TSymbol> output = null;
		
		public Configuration(TSymbol S0, List<TSymbol> input)
		{
			this.input = input;
			stack = new List<TSymbol>() {"h0", S0};
			output = new List<TSymbol>();
		}

		public void addRule(int type, List<TSymbol> LHS, List<List<TSymbol>> RHS)
		{
			rules.Add(new Rule(type, LHS, RHS));
		}
		
		public void printRules()
		{
			Console.WriteLine("Retail converter configuration rules:");
			for (int i = 0; i < rules.Count; i++)
			{
				Console.Write(i + 1 + ": ");
				rules[i].printRule();
			}
		}
		public void execute(List<TSymbol> inputStr, TGrammar grammar)
		{
			List<TSymbol> inputStack = inputStr;
			inputStack.Add("h0");
			inputStack.Reverse();
			TSymbol curSymbol = inputStack[inputStack.Count - 1];
			inputStack.RemoveAt(inputStack.Count - 1);
			Console.WriteLine("Trigger timing:");
			int iter = 1;
			while (curSymbol != "h0") 
			{
				if (curSymbol == "ε")
				{
					for (int i = 0; i < rules.Count; i++)
					{
						if (rules[i].LHS[2] == stack[stack.Count - 1] && rules[i].LHS[1] == curSymbol)
						{
							output.Add(stack[stack.Count - 1]);
							stack.RemoveAt(stack.Count - 1);
							break;
						}
					}
				}
				Console.Write(iter + ": (s0, {");
				for (int i = inputStack.Count - 1; i > 0; i--)
				{
					Console.Write(inputStack[i] + " ");
				}
				Console.Write("}, {");
				for (int j = 0; j < stack.Count; j++)
				{
					if (stack[j].outSymbol)
					{
						Console.Write("<" + stack[j] + "> ");
					}
					else
					{
						Console.Write(stack[j] + " ");
					}
				}
				Console.Write("}, {");
				for (int i = 0; i < output.Count; i++)
				{
					if (output[i].outSymbol)
					{
						Console.Write("<" + output[i] + "> ");
					}
					else
					{
						Console.Write(output[i] + " ");
					}
				}
				Console.Write("})\n");

				if (curSymbol == "ε")
				{
					curSymbol = "h0";
				}

				if (grammar.V.Contains(stack[stack.Count - 1]))
				{
					for (int i = 0; i < rules.Count; i++)
					{
						if (rules[i].LHS[2] == stack[stack.Count - 1])
						{
							stack.RemoveAt(stack.Count - 1);
							for (int j = 0; j < rules[i].RHS[1].Count; j++)
							{
								//Console.WriteLine(rules[i].RHS[1][j]);
								stack.Add(rules[i].RHS[1][j]);
							}
							curSymbol = inputStack[inputStack.Count - 1];
							inputStack.RemoveAt(inputStack.Count - 1);							
							break;
						}
					}
				}
				else if (stack[stack.Count - 1].outSymbol) 
				{
					for (int i = 0; i < rules.Count; i++)
					{
						if (rules[i].LHS[2] == stack[stack.Count - 1] && rules[i].LHS[1] == curSymbol)
						{
							output.Add(stack[stack.Count - 1]);
							stack.RemoveAt(stack.Count - 1);
							break;
						}
					}
				}
				else
				{
					for (int i = 0; i < rules.Count; i++)
					{
						if (rules[i].LHS[2] == curSymbol && rules[i].LHS[1] == curSymbol)
						{
							stack.RemoveAt(stack.Count - 1);
							curSymbol = inputStack[inputStack.Count - 1];
							inputStack.RemoveAt(inputStack.Count - 1);
							break;
						}
					}
				}
				iter++;
			}
			Console.WriteLine("Output:");
			for (int i = 0; i < output.Count; i++)
			{
				Console.Write(output[i]);
			}
			Console.Write("\n");
		}
	}

	public class KP
	{
		List<TSymbol> inputStr = null;
		TGrammar grammar = null;
		Configuration configs = null;
		public KP() { 
			inputStr = new List<TSymbol>();
			inputStr.Add(new TSymbol("{P’| P' = ∅} = {A → αRβ ∈ P"));
			inputStr.Add(new TSymbol("|"));
			inputStr.Add(new TSymbol("α = ε, β = ε, R ∈ Vi"));
			inputStr.Add(new TSymbol("} => {"));
			inputStr.Add(new TSymbol("{"));
			inputStr.Add(new TSymbol("R ∈ Vi| R → Yp ∈ P, Y ∉ Vi"));
			inputStr.Add(new TSymbol("} => {"));
			inputStr.Add(new TSymbol("P’ = P’ ∪ {A → Yp}"));
			inputStr.Add(new TSymbol("}"));
			inputStr.Add(new TSymbol("}{"));
			inputStr.Add(new TSymbol("P’ = P’ ∪ {A → αRβ ∈ P}"));
			inputStr.Add(new TSymbol("}"));
			
			Console.WriteLine("Input:");
			for (int i = 0; i < inputStr.Count; i++)
			{
				Console.Write(inputStr[i]);
			}
			Console.WriteLine("\n");

			grammar = new TGrammar(
				new List<TSymbol>()
				{
                    "{P’| P' = ∅} = {A → αRβ ∈ P",
                    "|",
                    "α = ε, β = ε, R ∈ Vi",
                    "} => {",
					"{",
                    "R ∈ Vi| R → Yp ∈ P, Y ∉ Vi",
                    "} => {",
                    "P’ = P’ ∪ {A → Yp}",
					"}",
					"}{",
                    "P’ = P’ ∪ {A → αRβ ∈ P}",
					"}",
				},
				new List<TSymbol>() 
				{
                    "P' = ∅\nforeach(A → αRβ ∈ P)\n\t",
					"\n\t\t",
					"\nend",
                    "if(α = ε, β = ε, R ∈ Vi)",
					"\n\t\t\t\t",
                    "foreach(R ∈ Vi)\n\t\t\tif(R → Yp ∈ P, Y ∉ Vi)",
                    "P’ = P’ ∪ {A → Yp}\n\t\t\tend\n\t\tend\n\telse",
                    "P’ = P’ ∪ {A → αRβ ∈ P}\n\tend",
				},
				new List<TSymbol>() {"S", "A", "B", "C", "D", "E"},
				"S"
			);
			grammar.AddRule(
				"S", 
				new List<TSymbol>() { "{P’| P' = ∅} = {A → αRβ ∈ P", "|", "A", "} => {", "B", "}{", "C", "}" }, 
				new List<TSymbol>() { "P' = ∅\nforeach(A → αRβ ∈ P)\n\t", "A", "\n\t\t", "B", "\n\t\t", "C", "\nend" });
			grammar.AddRule(
				"A", 
				new List<TSymbol>() { "α = ε, β = ε, R ∈ Vi" }, 
				new List<TSymbol>() { "if(α = ε, β = ε, R ∈ Vi)" });
			grammar.AddRule(
				"B",
				new List<TSymbol>() {"{", "D", "} => {", "E", "}" },
				new List<TSymbol>() {"D", "\n\t\t\t\t", "E"});
			grammar.AddRule(
				"D",
				new List<TSymbol>() { "R ∈ Vi| R → Yp ∈ P, Y ∉ Vi" },
				new List<TSymbol>() { "foreach(R ∈ Vi)\n\t\t\tif(R → Yp ∈ P, Y ∉ Vi)" });
			grammar.AddRule(
				"E",
				new List<TSymbol>() { "P’ = P’ ∪ {A → Yp}" },
				new List<TSymbol>() { "P’ = P’ ∪ {A → Yp}\n\t\t\tend\n\t\tend\n\telse" });
			grammar.AddRule(
				"C",
				new List<TSymbol>() { "P’ = P’ ∪ {A → αRβ ∈ P}" },
				new List<TSymbol>() { "P’ = P’ ∪ {A → αRβ ∈ P}\n\tend" });
			grammar.printProductions();
			
			// здесь можно сделать лексер с вводом конструктора множеств, вместо статического поля
			//for (int i = 0; i < input.Length; i++)
			//{
			//	//Console.WriteLine(input[i]);
			//	inputStr.Add(new TSymbol(input[i].ToString()));
			//}
			//inputStr.Add(new TSymbol("ε"));

			configs = new Configuration(grammar.S0, inputStr);
			for (int i = 0; i < grammar.P.Count; i++)
			{
				if (grammar.T.Contains(grammar.P[i].RHS[0]) && !grammar.P[i].RHS[0].outSymbol)
				{
					List<TSymbol> configLHS = new List<TSymbol>() {"s0", grammar.P[i].RHS[0], grammar.P[i].LHS};
					List<TSymbol> configRHS1 = new List<TSymbol>() {"s0"};
					List<TSymbol> configRHS2 = new List<TSymbol>();
					for (int j = grammar.P[i].RHS.Count - 1; j > 0; j--)
					{
						configRHS2.Add(grammar.P[i].RHS[j]);
					}
					List<TSymbol> configRHS3 = new List<TSymbol>() { "$" };
					List<List<TSymbol>> configRHS = new List<List<TSymbol>>() {configRHS1, configRHS2, configRHS3};
					configs.addRule(1, configLHS, configRHS);
				}
			}
			for (int i = 0; i < grammar.P.Count; i++)
			{
				for (int j = 0; j < grammar.P[i].RHS.Count; j++)
				{
					if (grammar.P[i].RHS[j].outSymbol)
					{
						TSymbol configLHS2 = findSymbolIn1("ε", grammar.P[i].RHS, j);
						if (configLHS2 == "ε")
						{
							configLHS2 = findSymbolIn2("ε", grammar.P[i].LHS);
						}
						List<TSymbol> configLHS = new List<TSymbol>() { "s0", configLHS2, grammar.P[i].RHS[j]};
						List<TSymbol> configRHS1 = new List<TSymbol>() { "s0" };
						List<TSymbol> configRHS2 = new List<TSymbol>() { "$" };
						List<TSymbol> configRHS3 = new List<TSymbol>() { grammar.P[i].RHS[j] };
						List<List<TSymbol>> configRHS = new List<List<TSymbol>> { configRHS1, configRHS2, configRHS3 };
						configs.addRule(5, configLHS, configRHS);
					}
				}
			}
			for (int i = 0; i < grammar.T.Count; i++)
			{
				List<TSymbol> configLHS = new List<TSymbol>() { "s0", grammar.T[i], grammar.T[i] };
				List<TSymbol> configRHS1 = new List<TSymbol>() { "s0" };
				List<TSymbol> configRHS2 = new List<TSymbol>() { "$" };
				List<List<TSymbol>> configRHS = new List<List<TSymbol>> { configRHS1, configRHS2, configRHS2 };
				configs.addRule(4, configLHS, configRHS);
			}
			List<TSymbol> c6LHS = new List<TSymbol>() { "s0", "ε", "h0"};
			List<List<TSymbol>> c6RHS = new List<List<TSymbol>>() 
			{ 
				new List<TSymbol>() { "s1" }, 
				new List<TSymbol>() { "$" }, 
				new List<TSymbol>() { "$" }
			};
			configs.addRule(6, c6LHS, c6RHS);
			configs.printRules();
		}

		public TSymbol findSymbolIn1(TSymbol symbol, List<TSymbol> RHS, int pos)
		{
			if (symbol != "ε")
			{
				return symbol;
			}
			for (int i = pos; i < RHS.Count; i++)
			{
				if (grammar.V.Contains(RHS[i]))
				{
					for (int j = 0; j < grammar.P.Count; j++)
					{
						if (grammar.P[j].LHS == RHS[i])
						{
							symbol = findSymbolIn1(symbol, grammar.P[j].RHS, 0);
						}
					}
					if (symbol != "ε")
					{
						break;
					}
				}
				else if (grammar.T.Contains(RHS[i]) && !RHS[i].outSymbol)
				{
					symbol = RHS[i];
				}
			}
			return symbol;
		}

		public TSymbol findSymbolIn2(TSymbol symbol, TSymbol LHS)
		{
			if (symbol != "ε")
			{
				return symbol;
			}
			for (int i = 0; i < grammar.P.Count; i++)
			{
				for (int j = 0; j < grammar.P[i].RHS.Count; j++)
				{
					if (grammar.P[i].RHS[j] == LHS)
					{
						return findSymbolIn1("ε", grammar.P[i].RHS, j + 1);
					}
				}
			}
			return symbol;
		}

		public void execute()
		{
			configs.execute(inputStr, grammar);
		}
	}
}