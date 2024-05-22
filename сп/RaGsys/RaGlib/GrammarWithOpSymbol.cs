using System;
using System.Collections.Generic;
using System.Collections;
using RaGlib.Core;

namespace RaGlib
{
    public class GrammarWithOpSymbol : Grammar
    {
        public List<Symbol_Operation> Top = null;
        public GrammarWithOpSymbol(List<Symbol> T, List<Symbol_Operation> Top, List<Symbol> V, string S0)
        {
            this.T = T;
            this.V = V;
            this.Top = Top;
            this.S0 = new Symbol(S0);
            this.P = new List<Production>();
        }

        public FSAutomateWithOpSymbols Transform()
        {
            var Q = this.V;
            Q.Add(new Symbol("qf"));
            var q0 = this.S0;
            var F = new List<Symbol>();
            //Конструируем множество заключительных состояний
            foreach (var p in this.P)
            {
                //Если начальный символ переходит в конечную цепочку,
                //то в множество F добавляется начальный символ S0 и состояние qf
                // F = {S0, qf}
                if (p.LHS.symbol.Contains("S0") && p.RHS.Contains(new Symbol("e")))
                {
                    F = new List<Symbol> { p.LHS, new Symbol("qf") };
                    break;
                }
                //Иначе F = {qf} множество F(конечных состояний) будет состоять из одного состояния qf
                else if (p.LHS.symbol.Contains("S0"))
                {
                    F = new List<Symbol> { new Symbol("qf") };
                    break;
                }
            }

            //Конструируем конечный автомат
            FSAutomateWithOpSymbols KA = new FSAutomateWithOpSymbols(Q, this.T, this.Top, F, q0.symbol);
            bool flag = true;

            foreach (var p in this.P)
            {
                //Если существует правило порождения,
                //в котором из начального символа существует переход в пустую цепочку,
                //то создаем правило (S0, "e", "qf")
                if (flag && p.LHS.symbol.Contains("S0") && p.RHS.Contains(new Symbol("e")))
                {
                    KA.AddRule(p.LHS.symbol, "e", "qf");
                    flag = false;
                }
                //Проходим по всем входным символам
                foreach (var t in this.T)
                {
                    if (OpSymbolReturn(p.RHS) != null)
                    {
                        if (p.RHS.Contains(t) && this.Top.Contains(new Symbol_Operation(p.RHS[1].ToString())))
                            KA.AddRule(p.LHS.symbol, t.symbol, OpSymbol(p.RHS), "qf");

                        else if (p.RHS.Contains(t) && !this.Top.Contains(new Symbol_Operation(p.RHS[1].ToString())))
                            KA.AddRule(p.LHS.symbol, t.symbol, OpSymbol(p.RHS), NoTerminal(p.RHS));
                    }
                    else
                    {

                        //Если справа есть символ и этот символ терминал,
                        //то добавляем правило (Нетерминал -> (Терминал,  "qf"))
                        if (p.RHS.Contains(t) && NoTermReturn(p.RHS) == null)
                            KA.AddRule(p.LHS.symbol, t.symbol, "qf");
                        //Если справа есть символ и этот символ нетерминал,
                        //то добавляем правило (Нетерминал -> (Терминал, Нетерминал))
                        else if (p.RHS.Contains(t) && NoTermReturn(p.RHS) != null)
                            KA.AddRule(p.LHS.symbol, t.symbol, NoTerminal(p.RHS));
                    }
                }
            }
            return KA;
        }

        private List<Symbol> OSSymbolContains(List<Symbol> array)
        {
            var NoTerm = new List<Symbol>();
            bool flag = true; // added
            foreach (var s in array)
                if (this.V.Contains(s))
                {
                    flag = false; // added
                    NoTerm.Add(s);
                }
            if (flag)
                return null; // added
            else
                return NoTerm;
        }

        private List<Symbol> NoTermReturn(List<Symbol> array)
        {
            var NoTerm = new List<Symbol>();
            bool flag = true; // added
            foreach (var s in array)
                if (this.V.Contains(s))
                {
                    flag = false; // added
                    NoTerm.Add(s);
                }
            if (flag)
                return null; // added
            else
                return NoTerm;
        }

        private string NoTerminal(List<Symbol> array)
        {
            var NoTermin = "";
            foreach (var s in array)
            {
                if (this.V.Contains(s))
                    NoTermin = s.symbol;
            }
            return NoTermin;
        }

        private string OpSymbol(List<Symbol> array)
        {
            var NoTermin = "";
            foreach (var s in array)
            {
                if (this.Top.Contains(new Symbol_Operation(s.symbol)))
                    NoTermin = s.symbol;
            }
            return NoTermin;
        }
        private List<Symbol_Operation> OpSymbolReturn(List<Symbol> array)
        {
            var OpSymbol = new List<Symbol_Operation>();
            bool flag = true; // added
            foreach (var s in array)
                if (this.Top.Contains(new Symbol_Operation(s.symbol)))
                {
                    flag = false; // added
                    OpSymbol.Add(new Symbol_Operation(s.symbol));
                }
            if (flag)
                return null; // added
            else
                return OpSymbol;
        }

        // Терминальные символы из массива
        private List<Symbol> TermReturn(List<Symbol> A)
        {
            var Term = new List<Symbol>();
            bool flag = true;
            foreach (var t in this.T)
                if (A.Contains(t))
                {
                    flag = false;
                    Term.Add(t);
                }
            if (flag)
                return null;
            else
                return Term;
        }
    }

}