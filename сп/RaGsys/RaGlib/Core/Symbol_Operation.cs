using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaGlib.Core {

    public class Symbol_Operation : Symbol
    {   
        public OPSymbolOperatrion function { set; get; } = null;   
        public Symbol_Operation(string s,List<Symbol> a,List<Symbol> L, List<Symbol> R): 
            base(s,a) {
            function = new OPSymbolOperatrion(this, this.attr);
//      Console.WriteLine("############################### OPSymbol " );
        }

        public Symbol_Operation(string s, List<Symbol> a) : base(s, a) {}

        public Symbol_Operation(string s) : base(s) { }

        public override void print() {      
            Console.Write(this.symbol + "\n");    
        }
    }

}
