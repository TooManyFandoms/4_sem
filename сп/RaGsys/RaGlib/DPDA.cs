using System;
using System.Linq;
using System.Collections.Generic;

using RaGlib.Core;
using RaGlib.Automata;

namespace RaGlib {

  public class DPDA : CPDA<DeltaQSigmaGamma> {

    public DPDA(List<Symbol> q_set,
               List<Symbol> sigma_set,
               List<Symbol> gamma_set,
               Symbol q0,Symbol z0,
               List<Symbol> f_set)
    : base(q_set,sigma_set,gamma_set,q0,z0,f_set) {
      _behaviour=(configs,config,chain) => {
        DeltaQSigmaGamma delta;

        { // NOTE: Логирование текущей конфигурации
          Console.Error.Write("Current config: ({0}, {1}, ",
             config.q,config.i<chain.Length ? chain.Remove(0,config.i) : "ε");
          for (int i = 0; i<config.pdl.Count; ++i)
            Console.Error.Write("{0}",config.pdl[i]);
          Console.Error.Write("{0}\n",config.pdl.Count>0 ? ")" : "ε)");
        }

        // Шаг 1. Взять правило по содержимому стека
        delta=findDelta(config.pdl.Last());

        if (delta==null)
          return false;

        Console.Error.Write("Step 1:");
        delta.Debug();

        if (!delta.LHSS.symbol.Equals("ε")) { // Найденное правило не является эпсилон-тактом
          for (; config.i<chain.Length;) { // Модель считывающего устройства
            if (chain[config.i].Equals("ε")) // Цепочка считана полностью
              return true;

            Console.Error.WriteLine("Step 2: "+chain[config.i]+" "+delta.LHSS.symbol);

            if (chain[config.i].ToString().Equals(delta.LHSS.symbol)) {
              Console.Error.WriteLine("Step 3: "+chain[config.i]);

              if (delta.RHSZ.First().symbol.Equals("ε")) // NOTE: То же самое, что delta.RHSZ[0]..
                config.pdl.RemoveAt(config.pdl.Count-1); // NOTE: То же самое, что Stack.Pop()
              else // Пример конфигурации: (q0, a, az0)
                config.pdl.Add(delta.RHSZ.First()); // NOTE: То же самое, что ..delta.RHSZ[0]..
            } else { // Считать другое правило
              delta=findDelta(chain[config.i].ToString(),config.pdl.Last());

              if (delta==null) // Правило не найдено
                return false;

              Console.Error.Write("Step 4:");
              delta.Debug();

              if (delta.RHSZ.First().symbol.Equals("ε")) { // NOTE: То же самое, что delta.RHSZ[0]..
                Console.Error.WriteLine("Step 5: "+config.pdl.Last().symbol);

                config.pdl.RemoveAt(config.pdl.Count-1); // NOTE: То же самое, что Stack.Pop()

                if (delta.RHSQ.First().symbol.Equals(this.F.First().symbol)) // NOTE: То же самое, что ..delta.RHSQ[0]..Equals("qf")
                  return true; // Пример правила: d(q0, ε, z0) = (qf, ε), т.е. магазин пуст
              } else // Пример конфигурации: (q0, a, az0)
                config.pdl.Add(delta.RHSZ.First()); // NOTE: То же самое, что delta.RHSZ[0]
            }

            ++config.i;
            break;
          }
        } else { // Найденное правило является эпсилон-тактом
          config.pdl.RemoveAt(config.pdl.Count-1); // NOTE: То же самое, что Stack.Pop()

          foreach (var symbol in delta.RHSZ)
            config.pdl.Add(symbol);
        }

        Console.Error.Write('\n');

        // NOTE: Добавим новую конфигурацию в очередь на исполнение
        configs.Enqueue((delta.RHSQ.First(), config.i, config.pdl));
        return false;
      };
    }

    /* Алгоритм построения МП (PDA) по КС-грамматике */
    public DPDA(Grammar KCgrammar)
    : this(new List<Symbol>() { "q" }, // Q
           new List<Symbol>(KCgrammar.T), // Sigma
           new List<Symbol>(KCgrammar.V), // Gamma
           "q0",KCgrammar.S0, // q0, z0
           new List<Symbol>()) {
      foreach (var t in KCgrammar.T) // добавить в Gamma
        _gammaSet.Add(t);

      DeltaQSigmaGamma delta = null;

      // Algorithm  build DeltaQSigmaGamma by P !! доработать
      // 1. build эпсилон правила
      var rhsq = new List<Symbol>(); // RHS for delta
      var rhsz = new List<Symbol>();
      int i = 0;

      foreach (var v in KCgrammar.V) {
        foreach (var p in KCgrammar.P) { // A -> BcD
          if (p.LHS.Equals(v)) {
            var list = new List<Symbol>();
            var rhs = new List<Symbol>(p.RHS);
            rhs.Reverse();
            foreach (var simbol in rhs) {
              i++;
              list.Add(simbol);
              rhsz.Concat(list);
              rhsq.Add("q"+i);
              this.Q.Add(simbol);
            }
          }

          delta=new DeltaQSigmaGamma("q"+i,"e",v.symbol,rhsq,rhsz);
          Delta.Add(delta);
        }
      } // end foreach

      i++;
      // 2. build для терминалов переходы
      foreach (var t in KCgrammar.T) {
        delta=new DeltaQSigmaGamma("q"+i,t.symbol,t.symbol,
                                     new List<Symbol>() { "q"+i },
                                     new List<Symbol>() { "e" });
        Delta.Add(delta);
      }
    }

    public void addDeltaRule(Symbol LHSQ,Symbol LHSS,Symbol LHSZ,List<Symbol> RHSQ,List<Symbol> RHSZ) {
      this.Delta.Add(new DeltaQSigmaGamma(LHSQ,LHSS,LHSZ,RHSQ,RHSZ));
    }

    /* Поиск правила delta (q0,a,z0) по символу "a" и в вершине магазина "z0" */
    private DeltaQSigmaGamma findDelta(Symbol sigma,Symbol z) {
      foreach (var delta in this.Delta) {
        if (delta.LHSS.symbol.Equals(sigma.symbol)&&
            delta.LHSZ.symbol.Equals(z.symbol))
          return delta;
      }

      return null;
    }

    /* Поиск правила delta (q0,a,z0) по символу в вершине магазина "z0" */
    private DeltaQSigmaGamma findDelta(Symbol z) {
      foreach (var delta in this.Delta) {
        if (delta.LHSZ.symbol.Equals(z.symbol))
          return delta;
      }

      return null;
    }
  }
}
