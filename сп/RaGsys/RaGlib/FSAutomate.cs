using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using RaGlib.Core;
using RaGlib.Automata;
using System.Text.RegularExpressions;

namespace RaGlib {
  /// Finite State automata (КА)
  public class FSAutomate : Automate {
        public FSAutomate(List<Symbol> Q, List<Symbol> Sigma, List<Symbol> F, string q0) : base(Q, Sigma, F, q0) {}

        public void AddRule(string state, List<Symbol> terms, string nextState)
        {
            foreach (var term in terms)
            {
                this.Delta.Add(new DeltaQSigma(state, term.ToString(), new List<Symbol> { new Symbol(nextState) }));
            }
        }

        public void AddRule(string state, string term, string nextState)
        {
            var regexOfRange = new Regex(@"\[.-.\]"); // для распознавания [x-a]
            var regexSet = new Regex(@"\[.+\]"); // для распознавания [asdf]
            Match match1 = regexOfRange.Match(term);
            Match match2 = regexSet.Match(term);
            if (match1.Success) // если обнаружен первый [x-a]
            {
                string res = match1.Value;
                var inList = new List<Symbol>();
                for (char i = res[1]; i <= res[3]; ++i) //[0-9] or [a-z]
                {
                    var j = new Symbol();
                    j = i.ToString();
                    inList.Add(j);
                }
                this.AddRule(state, inList, nextState);
            }
            else if (match2.Success) // если обнаружен второй [asdf]
            {
                string res = match2.Value;
                var inList = new List<Symbol>();
                for (int i = 1; i < res.Length - 1; ++i) //[0123456789]
                {
                    char s = res[i];
                    var j = new Symbol();
                    j = s.ToString();
                    inList.Add(j);
                }
                this.AddRule(state, inList, nextState);
            }
            else
            {
                this.Delta.Add(new DeltaQSigma(state, term, new List<Symbol> { new Symbol(nextState) }));
            }
        }

        public FSAutomate() : base() {}
        public void Execute(string chineSymbol) {
            var currState = this.Q0;
            int flag = 0;
            int i = 0;
            for (; i < chineSymbol.Length; i++) {
                flag = 0;
                foreach (var d in this.Delta) {
                    if (d.LHSQ == currState && d.LHSS == chineSymbol.Substring(i, 1))
                    {
                        currState = d.RHSQ[0].symbol; // Для детерминированного К автомата
                        flag = 1;
                        break;
                    }
                }
                if (flag == 0) break;
            } // end for

            Console.WriteLine("Length: " + chineSymbol.Length);
            Console.WriteLine(" i :" + i.ToString());
            Debug("curr", currState.symbol);
            if (this.F.Contains(currState) && i == chineSymbol.Length)
                Console.WriteLine("chineSymbol belongs to language");
            else
                Console.WriteLine("chineSymbol doesn't belong to language");
        } // end Execute

    public bool Execute_FSA(string chineSymbol) {  // 
      var currState = this.Q0; // убрать [0]
      int flag = 0;
      int i = 0;
      for (; i<chineSymbol.Length; i++) {
        flag=0;
        foreach (var d in this.Delta) { // var d
          if (d.LHSQ == currState && d.LHSS==chineSymbol.Substring(i,1)) {
            currState = d.RHSQ[0]; // Для детерминированного КA
            flag=1;
            break;
          }
        }
        if (flag==0) break;
      } // end for

      // Console.WriteLine("Length: " + chineSymbol.Length);
      //Console.WriteLine(" i :" + i.ToString());
      //Debug("curr", currState);
      return (this.F.Contains(currState) && i==chineSymbol.Length);
    } // end Execute_FSA

  } // KAutomate

}  