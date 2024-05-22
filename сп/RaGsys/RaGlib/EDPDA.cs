using System;
using System.Linq;
using System.Collections.Generic;

using RaGlib.Core;
using RaGlib.Automata;

namespace RaGlib {

  public class EDPDA : CPDA<DeltaQSigmaGammaEx> {

    public EDPDA(List<Symbol> q_set,
                 List<Symbol> sigma_set,
                 List<Symbol> gamma_set,
                 Symbol q0,Symbol z0,
                 List<Symbol> f_set)
    : base(q_set,sigma_set,gamma_set,q0,z0,f_set) {
      _behaviour=(configs,config,chain) => {
        DeltaQSigmaGammaEx delta;

        { // NOTE: Логирование текущей конфигурации
          Console.Error.Write("Current config: ({0}, {1}, ",
             config.q,config.i<chain.Length ? chain.Remove(0,config.i) : "ε");
          for (int i = 0; i<config.pdl.Count; ++i)
            Console.Error.Write("{0}",config.pdl[i]);
          Console.Error.Write("{0}\n",config.pdl.Count>0 ? ")" : "ε)");
        }

        // Шаг 1. Взять правило по содержимому стека и входному символу
        delta=findDelta(chain[config.i].ToString(),config.pdl);

        if (delta==null)
          return false;

        Console.Error.Write("Step 1: ");
        delta.Debug();

        if (!delta.LHSS.symbol.Equals("ε")) { // Найденное правило не является эпсилон-тактом
          for (; config.i<chain.Length;) { // Модель устройства чтения
            if (chain[config.i].Equals("ε"))
              return true;

            Console.Error.WriteLine("Step 2: "+chain[config.i]+" "+delta.LHSS.symbol);

            if (!delta.RHSZ.First().symbol.Equals("ε"))
              config.pdl.AddRange(delta.RHSZ);

            ++config.i;
            break;
          }
        } else { // Найденное правило является эпсилон-тактом
          int size = delta.LHSZX.Count;
          int index = config.pdl.Count-size;

          Console.Error.WriteLine("Step 3: "+chain[config.i]);

          if (!delta.LHSZ.symbol.Equals("ε"))
            config.pdl.RemoveRange(index,size);

          if (!delta.RHSZ.First().symbol.Equals("ε"))
            config.pdl.AddRange(delta.RHSZ);

          Console.Error.Write("Step 4: ");
          delta.Debug();

          DeltaQSigmaGammaEx next_delta = findDelta(config.pdl);
          List<Symbol> next_stack = new List<Symbol>(config.pdl);

          if (next_delta!=null) {
            size=next_delta.LHSZX.Count;
            index=next_stack.Count-size;

            if (next_delta.RHSZ.First().symbol.Equals("ε")) {
              next_stack.RemoveRange(index,size);

              Console.Error.WriteLine("Step 5: "+config.pdl.Last().symbol);

              if (next_delta.RHSQ.First().symbol.Equals(this.F.First().symbol))
                return true;
            } else
              next_stack.AddRange(next_delta.RHSZ);
          }
        }

        Console.Error.Write('\n');

        // NOTE: Добавим новую конфигурацию в очередь на исполнение
        configs.Enqueue((delta.RHSQ.First(), config.i, config.pdl));
        return false;
      };
    }

    public void addDeltaRule(Symbol LHSQ,Symbol LHSS,List<Symbol> LHSZ,List<Symbol> RHSQ,List<Symbol> RHSZ) {
      this.Delta.Add(new DeltaQSigmaGammaEx(LHSQ,LHSS,LHSZ,RHSQ,RHSZ));
    }

    /* Поиск правила delta (q0,a,z0) по символу "a" и в вершине магазина "z0" */
    private DeltaQSigmaGammaEx findDelta(Symbol sigma,List<Symbol> z) {
      foreach (var delta in this.Delta) {
        int size = delta.LHSZX.Count;
        int index = z.Count-size;

        if (index>=0) {
          for (int i = 0; i<size; ++i) {
            if (z[index+i]!=delta.LHSZX[i])
              goto skip;
          }

          return delta;
        }

      skip: continue;
      }

      // NOTE: Найти правило с эпсилон в LHSZX
      foreach (var delta in this.Delta) {
        if (delta.LHSS.symbol.Equals(sigma.symbol)&&
            delta.LHSZ.symbol.Equals("ε")) {
          return delta;
        }
      }
      return null;
    }

    /* Поиск правила delta (q0,a,z0) по символу в вершине магазина "z0" */
    private DeltaQSigmaGammaEx findDelta(List<Symbol> z) {
      // NOTE: Найти правило без эпсилон в LHSZX
      foreach (var delta in this.Delta) {
        int size = delta.LHSZX.Count;
        int index = z.Count-size;

        if (index>=0) {
          for (int i = 0; i<size; ++i) {
            if (z[index+i]!=delta.LHSZX[i])
              goto skip;
          }

          return delta;
        }
      skip: continue;
      }
      return null;
    }
  }
}