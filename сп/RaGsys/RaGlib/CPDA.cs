using System;
using System.Collections.Generic;

using RaGlib.Core;
using RaGlib.Automata;

namespace RaGlib {

  using Configurations = Queue<(Symbol, int, List<Symbol>)>;

  public class CPDA<R> : Automate { /* Configurable Pushdown Automata */

    public delegate bool Move(Configurations configs,
                              (Symbol q, int i, List<Symbol> pdl) config,
                              string chain);

    protected List<Symbol> _gammaSet;
    protected Symbol _z0;

    protected Move _behaviour;

    public CPDA(List<Symbol> q_set,
                List<Symbol> sigma_set,
                List<Symbol> gamma_set,
                Symbol q0,Symbol z0,
                List<Symbol> f_set) {
      { // NOTE: `Automate` class fields initialisation
        this.Q=q_set;

        this.Q0=q0;

        this.Sigma=sigma_set;

        this.F=f_set;

        this.Delta=new List<R>();
      }

      { // NOTE: `CPDA` class fields initialisation
        _gammaSet=gamma_set;

        _z0=z0;

        _behaviour=null;
      }
    }

    public void SetUp(Move new_behaviour) {
      if (new_behaviour==null)
        throw new ArgumentNullException("new_behaviour");

      _behaviour=new_behaviour;
    }

    public bool Execute(string chain) {
      if (chain==null)
        throw new ArgumentNullException("chain");

      if (_behaviour==null)
        throw new ArgumentException("Pushdown automata was NOT set up! Use `Setup` method to define its behaviour.");

      chain+="ε";

      Configurations configs = new Queue<(Symbol, int, List<Symbol>)>();
      configs.Enqueue((this.Q0, 0, new List<Symbol>() { _z0 }));

      while (configs.Count>0) {
        var config = configs.Dequeue();

        if (_behaviour(configs,config,chain))
          return true;
      }

      return false;
    }

    public virtual void AddRule(R rule) {
      this.Delta.Add(rule);
    }

    public void Debug() {
      foreach (var rule in this.Delta)
        rule.Debug();
    }

  }
}
