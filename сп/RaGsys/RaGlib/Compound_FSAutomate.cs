using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Data;
using System.Text;
using RaGlib.Core;
using RaGlib.Automata;

namespace RaGlib {
    public class Compound_FSAutomate
    {
        public static FSAutomate Merge2(FSAutomate a, FSAutomate b) // слияние двух автоматов 
        {
            var Q = new List<Symbol>();
            // Join  
            foreach (Symbol q in a.Q) Q.Add(q);
            foreach (Symbol q in b.Q) Q.Add(q);

            var E = new List<Symbol>();
            foreach (Symbol q in a.Sigma) E.Add(q);
            foreach (Symbol q in b.Sigma) if (!E.Contains(q)) E.Add(q);

            var F = new List<Symbol>();
            if (b.F.Contains(b.Q0)) foreach (Symbol q in a.F) F.Add(q);
            foreach (Symbol q in b.F) F.Add(q);

            var merge  = new FSAutomate(Q, E, F, a.Q0.ToString()); // прикол в a.Q0 ???????
            foreach (DeltaQSigma d in a.Delta) merge.AddRule(d.LHSQ.symbol, d.LHSS.symbol, d.RHSQ[0].symbol);
            foreach (DeltaQSigma d in b.Delta)
            {
                merge.AddRule(d.LHSQ.symbol, d.LHSS.symbol, d.RHSQ[0].symbol);
                //extra rules
                if (d.LHSQ.symbol == b.Q0.ToString()) // 
                {
                    foreach (Symbol f in a.F) merge.AddRule(f.symbol, d.LHSS.symbol, d.RHSQ[0].symbol);
                }
            }
            return merge;
        }

        public static FSAutomate Union2(FSAutomate a, FSAutomate b)
        {
            var Q = new List<Symbol>() { new Symbol("S") }; // ??????????
            foreach (Symbol q in a.Q) Q.Add(q);
            foreach (Symbol q in b.Q) Q.Add(q);

            var E = new List<Symbol>();
            foreach (Symbol q in a.Sigma) E.Add(q);
            foreach (Symbol q in b.Sigma) if (!E.Contains(q)) E.Add(q);

            var F = new List<Symbol>();
            // if (b.F.Contains(b.Q0)) foreach (string q in a.F) F.Add(q);
            foreach (Symbol q in a.F) F.Add(q);
            foreach (Symbol q in b.F) F.Add(q);

            var union = new FSAutomate(Q, E, F, "S");
            union.AddRule("S", "", a.Q0.symbol);
            union.AddRule("S", "", b.Q0.symbol);
            foreach (DeltaQSigma d in a.Delta) union.AddRule(d.LHSQ.symbol, d.LHSS.symbol, d.RHSQ[0].symbol);
            foreach (DeltaQSigma d in b.Delta) union.AddRule(d.LHSQ.symbol, d.LHSS.symbol, d.RHSQ[0].symbol);// [0]

            return union;
        }
        public static FSAutomate Merge(FSAutomate[] automates) // слияние массива автоматов
        {
            var temp = automates[0];
            for (int i = 1; i < automates.Length; i++)
                temp = Merge2(temp, automates[i]);
            return temp;
        }

        public static FSAutomate Union(FSAutomate[] automates)
        {
            var temp = automates[0];
            for (int i = 1; i < automates.Length; i++)
                temp = Union2(temp, automates[i]);
            return temp;
        }
    }
}