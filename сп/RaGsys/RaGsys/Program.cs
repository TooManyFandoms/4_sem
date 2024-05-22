using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Data;
using System.Text;
using System.Linq.Expressions;
using System.IO;         // for 4.0 Generator so far   
using System.Reflection; // for 4.0 Generator

using RaGlib;
using RaGlib.Core;
using RaGlib.Grammars;
using RaGlib.Automata;
using RaGlib.SetL;

namespace RaGsystems {

  class Program {
    static void Dialog() {
      Console.WriteLine("Наберите соответствующий номер лабораторной или курсовой и нажмите <Enter>");
      Console.WriteLine("Лабораторные работы:");
      Console.WriteLine("1.1  Лемма о накачке рег., КС языки");
      Console.WriteLine("1.2  Составные автоматы");
      Console.WriteLine("2    Спроектировать конечный автомат DFS (lab 2)");
      Console.WriteLine("2.1  Алгоритм построения КА по заданной грамматике");
      Console.WriteLine("2.2  Грамматика с операционными символами и алгоритм  преобразование в КА");
      Console.WriteLine("2.3  Алгоритм построения КА по заданной грамматике + составные автоматы (конкатенация - merge(), объединение - union())");
      Console.WriteLine("3    НДКА ");
      Console.WriteLine("3.1  Convert NDFS to DFS (lab 3)");
      Console.WriteLine("3.2  Convert NDFS to DFS (lab 3) example");
      Console.WriteLine("3.3  Example FA with the extension of the AddRule by Regex [A-Z]  ");
      Console.WriteLine("");
      Console.WriteLine("4.0  Генератор заданий Grammar_generator");
      Console.WriteLine("");
      Console.WriteLine("4.1  Приведение граматики к нормальной форме CFGrammar (lab 4 - 6)");
      Console.WriteLine("6.1  Grammar in Greibach normal form");
      Console.WriteLine("6.2  Grammar in Chomsky normal form");
      Console.WriteLine("7    Алгоритм построения МП автомата по приведенной КС-грамматики PDA (lab 7-8)");
      Console.WriteLine("7.1 КС-грамматика в МП-автомат пример 1");
      Console.WriteLine("7.2 КС-грамматика в МП-автомат пример 2");
      Console.WriteLine("7.3 МП - автомат пример 3");
      Console.WriteLine("7.4 НДМП - автомат пример 4");
      Console.WriteLine("7.5 МП - автомат пример 5");
      Console.WriteLine("7.10 Конфигурируемый МП-автомат: недетерминированный МПА");
      Console.WriteLine("7.11 Расширенный МП-автомат (детерминированный)");
      Console.WriteLine("7.12 Конфигурируемый МП-автомат: недетерминированный РМПА");


      Console.WriteLine("");
      Console.WriteLine("9.1  Для LL(1) анализатора построить управляющую таблицу M.\n"+
          "     Аналитически написать такты работы LL(1) анализатора для выведенной цепочки.");
      Console.WriteLine("9.2  Для LL(1) анализатора построить управляющую таблицу M.\n"+
          "     Аналитически написать такты работы LL(1) анализатора для выведенной цепочки с подробным разбором.");
      Console.WriteLine("");
      Console.WriteLine("14   Построить каноническую форму множества ситуаций.\n"+
          "     Построить управляющую таблицу для функции перехода  g(х) и действий f(u)");
      Console.WriteLine("16.1 LR(0) using g(X), f(a)");
      Console.WriteLine("16.2 LR(0) using g(X), f(a) example");
      Console.WriteLine("16.3 LR(1) using g(X), f(a)");
      Console.WriteLine("16.4 LR(1) using g(X), f(a) example ");
      Console.WriteLine("");
      Console.WriteLine("Курсовые проеты: I");
      Console.WriteLine("I1   Терия перевода SDTSchemata");
      Console.WriteLine("I2   Преобразование КС-грамматики в транслирующую с операционными символами");
      Console.WriteLine("I3   Grammar to AT-Grammar");
      Console.WriteLine("I4   AT-Grammar");
      Console.WriteLine("I5   AT-Grammar for python vars types");
      Console.WriteLine("I6   AT-grammar for translating for C ++ into Python");
      Console.WriteLine("");
      Console.WriteLine("I7   Chain Translation example");
      Console.WriteLine("I8   L-attribute translation");
      Console.WriteLine("I9   Parse Tree translation");
      Console.WriteLine("");
      Console.WriteLine("I10  SDT schema builder"); 
      Console.WriteLine("I11  SetL");
      Console.WriteLine("I18  Преобразование деревьев при помощи СУ-схемы");
    }

    static void Main() {
      // "ε", "\u03B5"  Greek alphabet
      Console.OutputEncoding=System.Text.Encoding.Unicode;  

      for (;;) {
        //Dialog();
        switch (Console.ReadLine()) {
          case "0":
              KP kp0 = new KP();
              kp0.execute();
              break;
          case "1.1": // Pumping lemma

            var check_pumping = new Pumping();
            check_pumping.Dialog();

            break;
          case "1.2": // Compound automata  Lab 1.
            FSAutomate[] automats = new FSAutomate[] {
                new FSAutomate(
                        new List<Symbol>() { " S01 " , "A1" , "B1" , "C1" , "D1" , "E1" , "F1" , "G1" , "H1" , " I1 " , " J1 " , "K1" , " L1 " , "M1" },
                        new List<Symbol>() { "0 " , " 1 " , " 2 " , " 3 " , " 4 " , " 5 " , " 6 " , " 7 " , " 8 " , " 9 " , " . " },
                        new List<Symbol>() { "qf1"  },
                        "S01"
                    ),
                new FSAutomate(
                        new List<Symbol>() {" S02 " , "A2" , "B2" , "C2" , "D2" , "E2" , "F2" , "G2" , "H2" , " I2 " , " J2 " , 
                        "K2" , " L2 " , "M2" , "N2" , "O2" , "P2" , "Q2" , "R2" , " S2 " , "T2" , "U2" , 
                        "V2" , "W2" , "X2" , "Y2" , "Z2" , "AA2" , " qf2 " },
                        new List<Symbol>() { "B" , "b" , "A" , " a " , "L" , " l " , "O" , " o " , "N" , "n" , "E" , " e " , 
                        "Y" , " y " , "D" , "d" , "R" , " r " , "N" , "n" , "T" , " t " , "F" , " f " , 
                        "G" , " g " , "S" , " s " , "H" , "h" , " " , " I " , " i " , "C" , " c " , @" \W" },
                        new List<Symbol>() { "qf2" },
                        "S02"
                    ),
                new FSAutomate(
                        new List<Symbol>() { "S03", "qf3" },
                        new List<Symbol>() { "0", "1" },
                        new List<Symbol>() { "qf3" },
                        "S03"
                    ),
            };

                        //string[] numbers = { " 1 ", " 2 ", " 3 ", " 4 ", " 5 ", " 6 ", " 7 ", " 8 ", " 9 ", " 0 " };
            //deltas
            automats[0].AddRule("S01","1","A1");
            automats[0].AddRule("A1","9","B1");
            automats[0].AddRule("B1","2","C1");
            automats[0].AddRule("C1",".","D1");
            automats[0].AddRule("D1","1","E1");
            automats[0].AddRule("E1","6","F1");
            automats[0].AddRule("F1", "8", "G1");
            automats[0].AddRule("G1", ".", "H1");
            automats[0].AddRule("H1", "1", "I1");
            automats[0].AddRule("I1", ".", "J1");
                        automats[0].AddRule("J1", "0", "K1");
                        automats[0].AddRule("J1", "1", "K1");
                        automats[0].AddRule("J1", "2", "K1");
                        automats[0].AddRule("J1", "3", "K1");
                        automats[0].AddRule("J1", "4", "K1");
                        automats[0].AddRule("J1", "5", "K1");
                        automats[0].AddRule("J1", "6", "K1");
                        automats[0].AddRule("J1", "7", "K1");
                        automats[0].AddRule("J1", "8", "K1");
                        automats[0].AddRule("J1", "9", "K1");
                        automats[0].AddRule("K1", "$", "qf1");
                        automats[0].AddRule("K1", "0", "L1");
                        automats[0].AddRule("K1", "1", "L1");
                        automats[0].AddRule("K1", "2", "L1");
                        automats[0].AddRule("K1", "3", "L1");
                        automats[0].AddRule("K1", "4", "L1");
                        automats[0].AddRule("K1", "5", "L1");
                        automats[0].AddRule("K1", "6", "L1");
                        automats[0].AddRule("K1", "7", "L1");
                        automats[0].AddRule("K1", "8", "L1");
                        automats[0].AddRule("K1", "9", "L1");
                        automats[0].AddRule("L1", "$", "qf1");
                        automats[0].AddRule("L1", "0", "M1");
                        automats[0].AddRule("L1", "1", "M1");
                        automats[0].AddRule("L1", "2", "M1");
                        automats[0].AddRule("L1", "3", "M1");
                        automats[0].AddRule("L1", "4", "M1");
                        automats[0].AddRule("L1", "5", "M1");
                        automats[0].AddRule("L1", "6", "M1");
                        automats[0].AddRule("L1", "7", "M1");
                        automats[0].AddRule("L1", "8", "M1");
                        automats[0].AddRule("L1", "9", "M1");
                        automats[0].AddRule("M1", "$", "qf1");
                        /*foreach (string number in numbers){
                            automats[0].AddRule("J1", number, "K1");
                        }
                         foreach (string number in numbers) {
                            automats[0].AddRule("K1", number, "L1");
                         }
                         foreach (string number in numbers)
                         {
                             automats[0].AddRule("L1", number, "M1");
                         }*/

                        automats[1].AddRule("S02", @"\W", "S02");
                        automats[1].AddRule("S02", "b", "A2");
                        automats[1].AddRule("S02", "B", "A2");
                        automats[1].AddRule("A2", "a", "B2");
                        automats[1].AddRule("A2", "A", "B2");
                        automats[1].AddRule("B2", "l", "C2");
                        automats[1].AddRule("B2", "L", "C2");
                        automats[1].AddRule("C2", "o", "D2");
                        automats[1].AddRule("C2", "O", "D2");
                        automats[1].AddRule("D2", "n", "E2");
                        automats[1].AddRule("D2", "N", "E2");
                        automats[1].AddRule("E2", "e", "F2");
                        automats[1].AddRule("E2", "E", "F2");
                        automats[1].AddRule("F2", "y", "qf2");
                        automats[1].AddRule("F2", "Y", "qf2");
                        automats[1].AddRule("S02", "d", "G2");
                        automats[1].AddRule("S02", "D", "G2");
                        automats[1].AddRule("G2", "a", "H2");
                        automats[1].AddRule("G2", "A", "H2");
                        automats[1].AddRule("H2", "r", "I2");
                        automats[1].AddRule("H2", "R", "I2");
                        automats[1].AddRule("I2", "n", "qf2");
                        automats[1].AddRule("I2", "N", "qf2");
                        automats[1].AddRule("G2", "r", "J2");
                        automats[1].AddRule("G2", "R", "J2");
                        automats[1].AddRule("J2", "a", "K2");
                        automats[1].AddRule("J2", "A", "K2");
                        automats[1].AddRule("K2", "t", "qf2");
                        automats[1].AddRule("K2", "T", "qf2");
                        automats[1].AddRule("S02", "f", "L2");
                        automats[1].AddRule("S02", "F", "L2");
                        automats[1].AddRule("L2", "o", "M2");
                        automats[1].AddRule("L2", "O", "M2");
                        automats[1].AddRule("M2", "o", "E2");
                        automats[1].AddRule("M2", "O", "E2");
                        automats[1].AddRule("S02", "g", "O2");
                        automats[1].AddRule("S02", "G", "O2");
                        automats[1].AddRule("O2", "o", "P2");
                        automats[1].AddRule("O2", "O", "P2");
                        automats[1].AddRule("P2", "s", "Q2");
                        automats[1].AddRule("P2", "S", "Q2");
                        automats[1].AddRule("Q2", "h", "R2");
                        automats[1].AddRule("Q2", "H", "R2");
                        automats[1].AddRule("R2", " ", "S2");
                        automats[1].AddRule("S2", "d", "T2");
                        automats[1].AddRule("S2", "D", "T2");
                        automats[1].AddRule("T2", "a", "U2");
                        automats[1].AddRule("T2", "A", "U2");
                        automats[1].AddRule("U2", "r", "V2");
                        automats[1].AddRule("U2", "R", "V2");
                        automats[1].AddRule("V2", "n", "W2");
                        automats[1].AddRule("V2", "N", "W2");
                        automats[1].AddRule("W2", "i", "X2");
                        automats[1].AddRule("W2", "I", "X2");
                        automats[1].AddRule("X2", "t", "qf2");
                        automats[1].AddRule("X2", "T", "qf2");
                        automats[1].AddRule("S02", "h", "Y2");
                        automats[1].AddRule("S02", "H", "Y2");
                        automats[1].AddRule("Y2", "e", "Z2");
                        automats[1].AddRule("Y2", "E", "Z2");
                        automats[1].AddRule("Z2", "c", "AA2");
                        automats[1].AddRule("Z2", "C", "AA2");
                        automats[1].AddRule("AA2", "k", "qf2");
                        automats[1].AddRule("AA2", "K", "qf2");
                        automats[1].AddRule("qf2", @"\W", "qf2");

                        automats[2].AddRule("S03","0","S03");
            automats[2].AddRule("S03","0","qf3");
            automats[2].AddRule("S03","1","S03");
            automats[2].AddRule("S03","1","qf3");
            automats[2].AddRule("S03","","qf3");


                        var dka1 = new FSAutomate();
                        dka1.BuildDeltaDKAutomate(automats[0], false);
                        var dka2 = new FSAutomate();
                        dka2.BuildDeltaDKAutomate(automats[1], false);
                        var automats1 = new FSAutomate[] { dka1, dka2 };
                        var exectionOrder = new FSAutomate[] { dka1, dka2 };
                        string[] names = { "KA1", "KA2" };

                        Console.WriteLine();

                        Console.WriteLine();
                        Console.WriteLine("Enter number of automate:");
                        int automateNumber = -1;
                        automateNumber = int.Parse(Console.ReadLine());
                        while (automateNumber != 1 && automateNumber != 2)
                        {
                            Console.WriteLine("Bad input, try again");
                            Console.WriteLine("Enter number of automate:");
                            automateNumber = int.Parse(Console.ReadLine());
                        }
                        Console.WriteLine("You chose automate number {0}", automateNumber);
                        --automateNumber;
                        if (automateNumber == 0)
                        {
                            Console.WriteLine("Enter line (\"192\\.168\\.1\\.\\d{1, 3}\", for ex.: 192.168.1.1) to execute");
                            Console.WriteLine("or \"exit\" to exit:");
                        }
                        else if (automateNumber == 1)
                        {
                            Console.WriteLine("Enter line (\"(?i)(\\W|^)(baloney|darn|drat|fooey|gosh\\sdarnit|heck)(\\W|$)\", for ex.: drat) to execute");
                            Console.WriteLine("or \"exit\" to exit:");
                        }

                        string input;
                        while ((input = Console.ReadLine()) != "exit")
                        {
                            automats[automateNumber].Execute(input);
                            Console.WriteLine("Enter line to execute or \"exit\" to exit:");
                        }
                            /*
                                        var dka1 = new FSAutomate(); // правильно
                                        dka1.BuildDeltaDKAutomate(automats[0],false); // правильно
                                        var dka2 = new FSAutomate();
                                        dka2.BuildDeltaDKAutomate(automats[1],false);
                                        var dka3 = new FSAutomate();
                                        dka3.BuildDeltaDKAutomate(automats[2],false);

                                        // правильно
                                        var automats1 = new FSAutomate[] { dka1,dka2,dka3,};
                                        //merge
                                        var merged12 = Compound_FSAutomate.Merge2(dka1,dka2); // неправильно
                                        var merged23 = Compound_FSAutomate.Merge2(dka2,dka3);
                                        var merged123 = Compound_FSAutomate.Merge(automats1);
                                        var union12 = Compound_FSAutomate.Union2(dka1,dka2);
                                        var union1 = new FSAutomate();
                                        union1.BuildDeltaDKAutomate(union12,false);
                                        var union123 = Compound_FSAutomate.Union(automats1);
                                        var union2 = new FSAutomate();
                                        union2.BuildDeltaDKAutomate(union123,false);
                            */
                               //var exectionOrder = new FSAutomate[] { union1,union2,merged12,merged23,merged123 };
                           // var exectionOrder = new FSAutomate[] { automats[0], automats[1] };
                        //string[] names = { "объединение КА1, КА2","объединение КА1, КА2, КА3","КА1+КА2","КА2+КА3","КА1+КА2+КА3" };

            Console.WriteLine();

            /*Console.WriteLine("Были построены составные автоматы:");
            Console.WriteLine("1. объединение КА1, КА2;");
            Console.WriteLine("2. объединение КА1, КА2, КА3;");
            Console.WriteLine("3. КА1 +КА2;");
            Console.WriteLine("4. КА2+КА3;");
            Console.WriteLine("5. КА1+КА2+КА3;");

            Console.WriteLine();
            Console.WriteLine("Примеры цепочек для постоенных составных автоматов 1, 2, 3, 4, 5: ");
            Console.WriteLine("0, 1, 01, 0000, 0000000, (0+1), 0000(0+1), 0000(0+1)0, 0000(0+1)1");
            for (; ; ) {
              Console.WriteLine();
              Console.WriteLine("Введите цепочку (или выйти stop):");
              string s = Console.ReadLine();
              Console.WriteLine();
              if (s=="stop") break;

              for (int i = 0; i<exectionOrder.Length; i++) {
                if (exectionOrder[i].Execute_FSA(s)) {
                  Console.WriteLine("Автомат "+names[i]+" распознал цепочку "+s);
                  break;
                } else {
                  Console.WriteLine("Автомат "+names[i]+" не распознал цепочку "+s);
                  if (i==exectionOrder.Length-1)
                    Console.WriteLine("Данная цепочка не была распознана");
                  //  Console.ReadLine();
                }
              }
            }*/
            break;
          case "2":
            var ka = new FSAutomate(new List<Symbol>() { "S0","A","B","C","D","E","F","G","H","I","J","K","L","M","N","qf" },
                                    new List<Symbol>() { "0","1","-","+","" },
                                    new List<Symbol>() { "qf" },
            "S0");
            ka.AddRule("S0","1","A");
            ka.AddRule("A","0","B");
            ka.AddRule("B","1","C");
            ka.AddRule("C","0","D");
            ka.AddRule("D","-","E");
            ka.AddRule("E","1","F");
            ka.AddRule("F","+","G");
            ka.AddRule("G","0","qf");
            ka.AddRule("G","1","qf");

            Console.WriteLine("Enter line to execute :");
            ka.Execute(Console.ReadLine());
            break;

          case "2.1":
            var Gram = new Grammar(new List<Symbol>() { "0","1" },
            new List<Symbol>() { "S0","A","B" },
            "S0");
            Gram.AddRule("S0",new List<Symbol>() { "0" });
            Gram.AddRule("S0",new List<Symbol>() { "0","A" });
            Gram.AddRule("A",new List<Symbol>() { "1","B" });
            Gram.AddRule("B",new List<Symbol>() { "0" });
            Gram.AddRule("B",new List<Symbol>() { "0","A" });

            // From Automaton Grammar to State Machine(KA)
            var KA = Gram.Transform();
            KA.DebugAuto();
            break;

          case "2.2":
            var GramOP = new GrammarWithOpSymbol(new List<Symbol>() { "a","e","f","d" },
                                    new List<Symbol_Operation>() { new Symbol_Operation("{save}"),new Symbol_Operation("{split}"),new Symbol_Operation("{load}") },
                                   new List<Symbol>() { "S0","E","F","D" },
                                   "S0");
            GramOP.AddRule("S0",new List<Symbol>() { "a","E" });
            GramOP.AddRule("E",new List<Symbol>() { "e","F","{load}" });
            GramOP.AddRule("F",new List<Symbol>() { "f","D","{split}" });
            GramOP.AddRule("D",new List<Symbol>() { "d","{save}" });

            var KAOP = GramOP.Transform();
            KAOP.DebugAuto();
            Console.WriteLine("Enter line to execute :");
            KAOP.Execute(Console.ReadLine());
            Console.ReadLine();
            break;

          case "2.3":

            // grammar 1
            var grammar2_2__1 = new Grammar(new List<Symbol>() { "a","l","t","i","p","s"," " },
                           new List<Symbol>() { "S0","A","B","C","D","E","F","G" },"S0");
            grammar2_2__1.AddRule("S0",new List<Symbol>() { "a","A" });
            grammar2_2__1.AddRule("A",new List<Symbol>() { "l","B" });
            grammar2_2__1.AddRule("B",new List<Symbol>() { "l","C" });
            grammar2_2__1.AddRule("C",new List<Symbol>() { " ","D" });
            grammar2_2__1.AddRule("D",new List<Symbol>() { "t","E" });
            grammar2_2__1.AddRule("E",new List<Symbol>() { "i","F" });
            grammar2_2__1.AddRule("F",new List<Symbol>() { "p","G" });
            grammar2_2__1.AddRule("G",new List<Symbol>() { "s" });

            // From Automaton Grammar to State Machine(KA)
            var fsa_1 = grammar2_2__1.Transform();
            Console.WriteLine("fsa_1:\n");
            fsa_1.DebugAuto();
            Console.WriteLine("Enter line to execute(fsa1) :");
            string str_1 = "all tips";
            fsa_1.Execute(str_1);

            // grammar 3
            var grammar2_2__3 = new Grammar(new List<Symbol>() { "g","r","a","v","i","a","!","?","*"," " },
                           new List<Symbol>() { "S0","A","B","C","D","E","F" },"S0");
            grammar2_2__3.AddRule("S0",new List<Symbol>() { "!","S0" });
            grammar2_2__3.AddRule("S0",new List<Symbol>() { "*","S0" });
            grammar2_2__3.AddRule("S0",new List<Symbol>() { "?","S0" });
            grammar2_2__3.AddRule("S0",new List<Symbol>() { "g","A" });
            grammar2_2__3.AddRule("A",new List<Symbol>() { "r","B" });
            grammar2_2__3.AddRule("B",new List<Symbol>() { "a","C" });
            grammar2_2__3.AddRule("B",new List<Symbol>() { "@","C" });
            grammar2_2__3.AddRule("C",new List<Symbol>() { "v","D" });
            grammar2_2__3.AddRule("D",new List<Symbol>() { "i","E" });
            grammar2_2__3.AddRule("D",new List<Symbol>() { "1","E" });
            grammar2_2__3.AddRule("D",new List<Symbol>() { "!","E" });
            grammar2_2__3.AddRule("E",new List<Symbol>() { "a","C" });
            grammar2_2__3.AddRule("E",new List<Symbol>() { "@","C" });
            grammar2_2__3.AddRule("F",new List<Symbol>() { "!","F" });
            grammar2_2__3.AddRule("F",new List<Symbol>() { "*","F" });
            grammar2_2__3.AddRule("F",new List<Symbol>() { "?","F" });
            grammar2_2__3.AddRule("F",new List<Symbol>() { " " });

            // From Automaton Grammar to State Machine(KA)
            var fsa_3 = grammar2_2__3.Transform();
            Console.WriteLine("fsa_3:\n");
            fsa_3.DebugAuto();
            Console.WriteLine("Enter line to execute(fsa3) :");
            string str_3 = "gravia ";
            fsa_3.Execute(str_3);

            // grammar 9
            var grammar2_2__9 = new Grammar(new List<Symbol>() { "0","1","2","3","4","5","6","7","8","9"," " },
                           new List<Symbol>() { "S0","A","B","C","D","E","F","G" },"S0");
            grammar2_2__9.AddRule("S0",new List<Symbol>() { "0","A" });
            grammar2_2__9.AddRule("S0",new List<Symbol>() { "1","A" });
            grammar2_2__9.AddRule("S0",new List<Symbol>() { "2","A" });
            grammar2_2__9.AddRule("S0",new List<Symbol>() { "3","A" });
            grammar2_2__9.AddRule("S0",new List<Symbol>() { "4","A" });
            grammar2_2__9.AddRule("S0",new List<Symbol>() { "5","A" });
            grammar2_2__9.AddRule("S0",new List<Symbol>() { "6","A" });
            grammar2_2__9.AddRule("S0",new List<Symbol>() { "7","A" });
            grammar2_2__9.AddRule("S0",new List<Symbol>() { "8","A" });
            grammar2_2__9.AddRule("S0",new List<Symbol>() { "9","A" });

            grammar2_2__9.AddRule("A",new List<Symbol>() { "0","A" });
            grammar2_2__9.AddRule("A",new List<Symbol>() { "1","A" });
            grammar2_2__9.AddRule("A",new List<Symbol>() { "2","A" });
            grammar2_2__9.AddRule("A",new List<Symbol>() { "3","A" });
            grammar2_2__9.AddRule("A",new List<Symbol>() { "4","A" });
            grammar2_2__9.AddRule("A",new List<Symbol>() { "5","A" });
            grammar2_2__9.AddRule("A",new List<Symbol>() { "6","A" });
            grammar2_2__9.AddRule("A",new List<Symbol>() { "7","A" });
            grammar2_2__9.AddRule("A",new List<Symbol>() { "8","A" });
            grammar2_2__9.AddRule("A",new List<Symbol>() { "9","A" });
            grammar2_2__9.AddRule("A",new List<Symbol>() { " " });

            // From Automaton Grammar to State Machine(KA)
            var fsa_9 = grammar2_2__9.Transform();
            Console.WriteLine("fsa_9:\n");
            fsa_9.DebugAuto();
            Console.WriteLine("Enter line to execute(fsa9) :");
            string str_9 = "124134 ";
            fsa_9.Execute(str_9);


            var FSAs = new FSAutomate[] { fsa_1,fsa_3,fsa_9 };
            //merge
            var Concatenation123 = Compound_FSAutomate.Merge(FSAs);
            var Union123 = Compound_FSAutomate.Union(FSAs);

            Console.WriteLine("\nMerge of three automatas");
            Concatenation123.DebugAuto();

            Console.WriteLine("\nUnion of three automatas");
            Union123.DebugAuto();
            Console.WriteLine();

            Console.WriteLine("Enter line to execute(Concatenation123) :");
            Concatenation123.Execute("ara tips ");
            Console.WriteLine("Enter line to execute(union123) :");
            Union123.Execute("eeall tips");

            break;


          case "3.1":
            var ndfsa = new FSAutomate(new List<Symbol>() { "S0","1","2","3","4","5","6","7","8","9","10","11","12","13","14","15","16","17","18","19","20","qf" },
                                       new List<Symbol>() { "1","0","+","2" },
                                       new List<Symbol>() { "qf" },
                                       "S0");
            ndfsa.AddRule("S0","1","1");          //W1
            ndfsa.AddRule("1","0","2");
            ndfsa.AddRule("2","+","3");

            ndfsa.AddRule("3","","4");            //W2
            ndfsa.AddRule("4","","5");
            ndfsa.AddRule("4","","7");
            ndfsa.AddRule("4","","9");
            ndfsa.AddRule("5","1","6");
            ndfsa.AddRule("7","2","8");
            ndfsa.AddRule("6","","9");
            ndfsa.AddRule("8","","9");
            ndfsa.AddRule("9","","4");
            ndfsa.AddRule("9","","10");

            ndfsa.AddRule("10","1","11");          //W3
            ndfsa.AddRule("11","0","12");
            ndfsa.AddRule("12","","13");
            ndfsa.AddRule("13","","9");
            ndfsa.AddRule("13","","14");

            ndfsa.AddRule("14","","15");           //W4
            ndfsa.AddRule("14","","17");
            ndfsa.AddRule("15","0","16");
            ndfsa.AddRule("17","1","18");
            ndfsa.AddRule("16","","19");
            ndfsa.AddRule("18","","19");
            ndfsa.AddRule("19","","14");
            ndfsa.AddRule("19","","20");
            ndfsa.AddRule("20","","15");
            ndfsa.AddRule("14","","qf");
            ndfsa.AddRule("20","","qf");

            var dka = new FSAutomate();
            dka.BuildDeltaDKAutomate(ndfsa,false);
            dka.DebugAuto();
            Console.WriteLine("Enter line to execute 10+10 :");
            dka.Execute(Console.ReadLine());
            break;

          case "3.2":
            var example = new FSAutomate(new List<Symbol>() { "S0","1","2","3","4","5","6","7","8","9","qf" },
                                         new List<Symbol>() { "a","b" },
                                         new List<Symbol>() { "qf" },
                                         "S0");
            example.AddRule("S0","","1");
            example.AddRule("S0","","7");
            example.AddRule("1","","2");
            example.AddRule("1","","4");
            example.AddRule("2","a","3");
            example.AddRule("4","b","5");
            example.AddRule("3","","6");
            example.AddRule("5","","6");
            example.AddRule("6","","1");
            example.AddRule("6","","7");
            example.AddRule("7","a","8");
            example.AddRule("8","b","9");
            example.AddRule("9","b","qf");

            var dkaEX = new FSAutomate();
            dkaEX.BuildDeltaDKAutomate(example,false); // false по таблице  
            dkaEX.DebugAuto();                         // true Алгоритм Томсона по очереди 
            Console.WriteLine("Enter line to execute :");
            dkaEX.Execute(Console.ReadLine());
            break;
          case "3.3": {
              var ka8 = new FSAutomate(new List<Symbol>() { "S0","A","C","B","D","qf" },
                              new List<Symbol>() { "0","1","2","3","4","5","6","7","8","9","#","$","%" },
                              new List<Symbol>() { "qf" },"S0");
              ka8.AddRule("S0","[0-8]","A");
              ka8.AddRule("A","[#-%]","B");
              ka8.AddRule("B","[02468]","C");
              foreach (char i in "0123456789".ToCharArray())
                ka8.AddRule("C",i.ToString(),"D");
              foreach (char i in "0123456789".ToCharArray())
                ka8.AddRule("D",i.ToString(),"qf");

              ka8.DebugAuto();
              Console.WriteLine("Enter line to execute 1#845 :");
              ka8.Execute(Console.ReadLine());
              break;
            }
          case "4.0":
            // v2.2022 201- Тихонов 
            var generator = new Tools.CoreGrammarGenerator();
            var newGrammarGenerator = new Tools.NewGrammarGenerator();
            string path40 = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)+"\\"+"log.txt";
            Console.WriteLine("Выберите: Сгенерировать варианты КС грамматики и сохранить в  файл(1), и загрузить требуемый вариант из файла(2)");
            int var40 = Convert.ToInt32(Console.ReadLine());
            if (var40==1) {
              File.WriteAllText(@path40,string.Empty);
              Console.WriteLine("Введите количество вариантов в таблице одним числом:");
              int numbers40 = Convert.ToInt32(Console.ReadLine());
              numbers40++;
              Console.WriteLine("Введите seed (целое число > 0)");
              int seed40 = Convert.ToInt32(Console.ReadLine());
              newGrammarGenerator.LogWrite(seed40,numbers40);
              Console.WriteLine("Файл с вариантами был добавлен на ваше устройство");
            } else if (var40==2) {
              Console.WriteLine("Введите ваш вариант");
              int variant40 = Convert.ToInt32(Console.ReadLine());
              var vg40 = newGrammarGenerator.GetTask(variant40,path40);
              string beb_ra = generator.PrintGrammar(vg40,variant40,"V");
              Console.WriteLine(beb_ra);
              //загрузка из файла одного варианта в объект Grammar для приведения
              var NewRegGr = new Grammar(vg40.T,vg40.V,"S");
              foreach (Production pr40 in vg40.P) {
                string LHSp = pr40.LHS.ToString();
                NewRegGr.AddRule(LHSp,pr40.RHS);
              }
            }
            Console.ReadKey();
            break;

          /* v1
          var generator = new Assignment.GrammarGenerator();
          var grammar = generator.GenerateGrammar();
          Console.ReadKey();
          break;
          */
          case "4.1":
            var regGr = new Grammar(new List<Symbol>() { "b","c" },
                                    new List<Symbol>() { "S","A","B","C" },
                                    "S");

            regGr.AddRule("S",new List<Symbol>() { "c","A","B" });
            regGr.AddRule("S",new List<Symbol>() { "b" });
            regGr.AddRule("B",new List<Symbol>() { "c","B" });
            regGr.AddRule("B",new List<Symbol>() { "b" });
            regGr.AddRule("A",new List<Symbol>() { "Ab" });
            regGr.AddRule("A",new List<Symbol>() { "B" });
            regGr.AddRule("A",new List<Symbol>() { "" });
            Console.WriteLine("Grammar:");
            regGr.Debug("T",regGr.T);
            regGr.Debug("T",regGr.V);
            regGr.DebugPrules();

            Grammar G1 = regGr.EpsDelete();
            G1.DebugPrules();

            Grammar G2 = G1.unUsefulDelete();
            G2.DebugPrules();

            Grammar G3 = G2.ChainRuleDelete();
            G3.DebugPrules();

            Grammar G4 = G3.LeftRecursDelete_new6();
            G4.DebugPrules();
            // G4 - приведенная грамматика

            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("Normal Grammatic:");
            G4.Debug("T",G4.T);
            G4.Debug("V",G4.V);
            G4.DebugPrules();
            Console.Write("Start symbol: ");
            Console.WriteLine(G4.S0+"\n");
            break;
          case "6.1": {
              var g = new Grammar(new List<Symbol>() { "+","*","(",")","i" },
                                    new List<Symbol>() { "S","F","L","K" },
                                    "S");
              g.AddRule("S",new List<Symbol>() { "S","+","F" });
              g.AddRule("S",new List<Symbol>() { "F" });
              g.AddRule("F",new List<Symbol>() { "F","*","L" });
              g.AddRule("F",new List<Symbol>() { "L" });
              g.AddRule("L",new List<Symbol>() { "(","S",")" });
              g.AddRule("L",new List<Symbol>() { "i" });
              g.AddRule("K",new List<Symbol>() { "i" });

              Console.WriteLine("Grammar:");
              g.Debug("T",g.T);
              g.Debug("T",g.V);
              g.DebugPrules();

              var g1 = g.DeleteLongRules();

              var g2 = g1.unUsefulDelete();

              var g3 = g2.EpsDelete();

              var g4 = g3.DeleteS0Rules();

              var g5 = g4.ChainRuleDelete();

              var g6 = g5.DeleteTermRules();
              g6.DebugPrules();

              var g7 = g6.LeftRecursDelete_new6();
              g7.DebugPrules();
              g7.Debug("V",g7.V);

              var g9 = g7.TransformGrForm();

              Console.WriteLine("--------------------------------------------");
              Console.WriteLine("Greibach normal form:");
              g9.Debug("T",g9.T);
              g9.Debug("V",g9.V);
              g9.DebugPrules();
              Console.Write("Start symbol: ");
              Console.WriteLine(g9.S0+"\n");
              break;
            }
          case "6.2": {  // Algorithms приведенная грамматика G            
              var g = new Grammar(new List<Symbol>() { "+","*","(",")","i" },
                                   new List<Symbol>() { "S","F","L","K" },
                                   "S");
              g.AddRule("S",new List<Symbol>() { "S","+","F" });
              g.AddRule("S",new List<Symbol>() { "F" });
              g.AddRule("F",new List<Symbol>() { "F","*","L" });
              g.AddRule("F",new List<Symbol>() { "L" });
              g.AddRule("L",new List<Symbol>() { "(","S",")" });
              g.AddRule("L",new List<Symbol>() { "i" });
              g.AddRule("K",new List<Symbol>() { "i" });

              Console.WriteLine("Grammar:");
              g.Debug("T",g.T);
              g.Debug("T",g.V);
              g.DebugPrules();

              var g1 = g.DeleteLongRules();
              g1.Debug("T",g1.T);
              g1.Debug("V",g1.V);
              g1.DebugPrules();

              var g2 = g1.unUsefulDelete();
              g2.DebugPrules();

              var g3 = g2.EpsDelete();
              g3.DebugPrules();

              var g4 = g3.DeleteS0Rules();
              g4.Debug("T",g4.T);
              g4.Debug("V",g4.V);
              g4.DebugPrules();
              Console.Write("Start symbol: ");
              Console.WriteLine(g4.S0+"\n");

              var g5 = g4.ChainRuleDelete();
              g5.DebugPrules();

              var g6 = g5.DeleteTermRules();

              Console.WriteLine("--------------------------------------------");
              Console.WriteLine("Chomsky normal form:");
              g6.Debug("T",g6.T);
              g6.Debug("V",g6.V);
              g6.DebugPrules();
              Console.Write("Start symbol: ");
              Console.WriteLine(g6.S0+"\n");
              break;
            }
           case "7.0": { 
            var CFGrammar = new Grammar(new List<Symbol>() { "a","z","#","@" },
                            new List<Symbol>() { "D","U","G","Y","M","F" },
                            "F");
            CFGrammar.AddRule("F",new List<Symbol>() { "z","D" });
            CFGrammar.AddRule("D",new List<Symbol>() { "#","U" });
            //CFGrammar.AddRule("U",new List<Symbol>() { "*" });
            CFGrammar.AddRule("U",new List<Symbol>() { "z","G" });
            CFGrammar.AddRule("G",new List<Symbol>() { "@","Y" });
            CFGrammar.AddRule("Y",new List<Symbol>() { "z","M" });
            CFGrammar.AddRule("M",new List<Symbol>() { "a" });
            CFGrammar.DebugPrules();

            var pda = new PDA(CFGrammar);
            pda.Debug();
            Console.Write("Enter chain:\n");
            Console.WriteLine(pda.Execute(Console.ReadLine()).ToString());

            //  pda.execute((Console.ReadLine()).ToString());
            break;
          } 
          case "7": { // Algorithm Grammar to PDA не детерменированный 
            var CFGrammar = new Grammar(new List<Symbol>() { "a","c","d","o" },
                                        new List<Symbol>() { "S","A","C","D","I","K","L","M","E" },
                                        "S");

              CFGrammar.AddRule("S",new List<Symbol>() { "b" });
              CFGrammar.AddRule("S",new List<Symbol>() { "c","A","B" });
              CFGrammar.AddRule("S",new List<Symbol>() { "c","B" });

              CFGrammar.AddRule("A",new List<Symbol>() { "b","D" });
              CFGrammar.AddRule("A",new List<Symbol>() { "b" });
              CFGrammar.AddRule("A",new List<Symbol>() { "c","B","D" });
              CFGrammar.AddRule("A",new List<Symbol>() { "c","B" });

              CFGrammar.AddRule("D",new List<Symbol>() { "b" });
              CFGrammar.AddRule("D",new List<Symbol>() { "b","D" });

              CFGrammar.AddRule("B",new List<Symbol>() { "b" });
              CFGrammar.AddRule("B",new List<Symbol>() { "c","B" });

              Console.Write("Debug KC-Grammar ");
              CFGrammar.DebugPrules();

              var pda = new PDA(CFGrammar);
              pda.Debug();

              Console.WriteLine("\nEnter the line   :");
              Console.WriteLine(pda.Execute(Console.ReadLine()).ToString());
              break;
          }
          case "7.1": { // !! Algorithm Grammar to PDA {aabb, aaaabbbb}
              // see 7.2 PDA  
              var cfgr = new Grammar(new List<Symbol>() { "a", "c", "d", "o" },
                                     new List<Symbol>() { "S", "A", "C", "D", "I", "K", "L", "M", "E" },
                                     "S");

              cfgr.AddRule("S",new List<Symbol>() { "A","E" });
              cfgr.AddRule("S", new List<Symbol>() { "K", "M" });
              cfgr.AddRule("K",new List<Symbol>() { "o" });
              cfgr.AddRule("A",new List<Symbol>() { "C","D" });
              cfgr.AddRule("C", new List<Symbol>() { "d" });
              cfgr.AddRule("D", new List<Symbol>() { "c" });
              cfgr.AddRule("E", new List<Symbol>() { "a" });
              cfgr.AddRule("M", new List<Symbol>() { "L", "I" });
              cfgr.AddRule("L", new List<Symbol>() { "c" });
              cfgr.AddRule("M", new List<Symbol>() { "c" });
              cfgr.AddRule("I", new List<Symbol>() { "L", "I" });
              cfgr.AddRule("I", new List<Symbol>() { "c" });
              Console.Write("Debug KC-Grammar ");
              cfgr.DebugPrules();

                var pda = new PDA(new List<Symbol>() { "q0", "q1","q2","qf"},
                                                       new List<Symbol>() { "a", "c", "d", "o" },
                                                       new List<Symbol>() {"z0", "a", "c", "d", "o", "S", "A", "C", "D", "I", "K", "L", "M", "E" },
                                                       "q0",
                                                       "S",
                                                       new List<Symbol>() {"qf"});
                pda.addDeltaRule("q0", "ε", "S",new List<Symbol>() { "q1" },new List<Symbol>() { "A","E" });
                pda.addDeltaRule("q0", "ε", "S", new List<Symbol>() { "q1" }, new List<Symbol>() { "K", "M" });
                pda.addDeltaRule("q", "ε", "K",new List<Symbol>() { "q" },new List<Symbol>() {"o" });
                pda.addDeltaRule("q", "ε", "A",new List<Symbol>() { "q" },new List<Symbol>() { "C","D" });
                pda.addDeltaRule("q", "ε", "C",new List<Symbol>() { "q" },new List<Symbol>() { "d" });
                pda.addDeltaRule("q", "ε", "D",new List<Symbol>() { "q" },new List<Symbol>() { "c" });
                pda.addDeltaRule("q", "ε", "E",new List<Symbol>() { "q" },new List<Symbol>() { "a" });
                pda.addDeltaRule("q", "ε", "M",new List<Symbol>() { "q" },new List<Symbol>() { "L","I" });
                pda.addDeltaRule("q", "ε", "L", new List<Symbol>() { "q" }, new List<Symbol>() { "c" });
                pda.addDeltaRule("q", "ε", "M", new List<Symbol>() { "q" }, new List<Symbol>() { "c" });
                pda.addDeltaRule("q", "ε", "I", new List<Symbol>() { "q" }, new List<Symbol>() { "L","I" });
                pda.addDeltaRule("q", "ε", "I", new List<Symbol>() { "q" }, new List<Symbol>() { "c" });
                pda.addDeltaRule("q", "o", "o", new List<Symbol>() { "q" }, new List<Symbol>() { "ε" });
                pda.addDeltaRule("q", "d", "d", new List<Symbol>() { "q" }, new List<Symbol>() { "ε" });
                pda.addDeltaRule("q", "c", "c", new List<Symbol>() { "q" }, new List<Symbol>() { "ε" });
                pda.addDeltaRule("q", "a", "a", new List<Symbol>() { "q" }, new List<Symbol>() { "ε" });
                pda.addDeltaRule("q", "ε", "d", new List<Symbol>() { "qf" }, new List<Symbol>() { "ε" });
                pda.addDeltaRule("q", "ε", "o", new List<Symbol>() { "qf" }, new List<Symbol>() { "ε" });

                            pda.Debug();

                Console.WriteLine("\nВведите строку, пример :"); // aaabbb
                Console.WriteLine(pda.Execute(Console.ReadLine()).ToString());                         
              break;
          }
          case "7.2": { // !! Algorithm Grammar to PDA {aabb, aaaabbbb}
              // see 7.2 PDA
                //
              // example expression  i = i  
              var cfgr1 = new Grammar(new List<Symbol>() { "i","=" },
                                      new List<Symbol>() { "S","F","L" },
                                      "S");

              cfgr1.AddRule("S",new List<Symbol>() { "F","=","L" }); //S -> F=L
              cfgr1.AddRule("F",new List<Symbol>() { "i" });    //F -> i
              cfgr1.AddRule("L",new List<Symbol>() { "F" });    //L -> F
              Console.Write("Debug KC-Grammar ");
              cfgr1.DebugPrules();
                var pda = new PDA(new List<Symbol>() { "q0","q1","q2","qf" },
                                                       new List<Symbol>() { "i","="},
                                                       new List<Symbol>() {"z0","i"},
                                                       "q0",
                                                       "z0",
                                                       new List<Symbol>() {"qf"});
                pda.addDeltaRule("q0", "i", "z0",new List<Symbol>() { "q1" },new List<Symbol>() { "i", "z0" });//δ(q0,i,z0) = (i,z0)
                pda.addDeltaRule("q", "=", "i",new List<Symbol>() { "q" },new List<Symbol>() { "ε" });  //δ(q,=,i) = (ε)
                pda.addDeltaRule("q", "i", "i",new List<Symbol>() { "q" },new List<Symbol>() { "i", "i"}); //δ(q,i,i) = (i,i)
                pda.addDeltaRule("q", "ε", "i",new List<Symbol>() { "qf" },new List<Symbol>() { "ε" });   //δ(q,ε,i) = (ε)
                            pda.Debug();
                Console.WriteLine("\nВведите строку, пример :"); // i=i
                Console.WriteLine(pda.Execute(Console.ReadLine()).ToString());
              break;
          }
          case "7.3": { //МП - автоматы  {ab, aaaabbbb}         
              var pda = new PDA(new List<Symbol>() { "q0","q1","q2","qf" },
                                 new List<Symbol>() { "a","b" },
                                 new List<Symbol>() { "z0","a" },
                                 "q0",
                                 "z0",
                                 new List<Symbol>() { "qf" });
              pda.addDeltaRule("q0","a","z0",new List<Symbol>() { "q1"},new List<Symbol>() { "a","z0"}); //δ(q0,a,z0) = (a, z0)
              pda.addDeltaRule("q","a","a",  new List<Symbol>() { "q" },new List<Symbol>() { "a","a" }); //δ(q,a,a) = (a,a)
              pda.addDeltaRule("q","b","a",  new List<Symbol>() { "q" },new List<Symbol>() { "ε" }); //δ(q,b,a) = (ε)
              pda.addDeltaRule("q","ε","z0",new List<Symbol>() { "qf" },new List<Symbol>() { "ε" });  //δ(q,ε,z0) = (ε)

                            pda.Debug();
              // Console.ReadKey();
              //              Console.WriteLine("\nEnter the line :");
              //              Console.WriteLine(pda.Execute(Console.ReadLine()).ToString());
              Console.WriteLine("Execute example: ab");
              Console.WriteLine(pda.Execute("ab"));
              //pda.Execute("aaabbb");
              break;
          }
          case "7.4": {// NPDA  automata  (v + v )
            var npda = new PDA(
                    new List<Symbol>() { "q","qf" },
                    new List<Symbol>() { "v","+","*","(",")" },
                    new List<Symbol>() { "v", "+", "*", "(", ")", "S", "F", "L"},
                    "q0",
                    "S",
                    new List<Symbol>() { "qf" });

            // S -> S + F | F
            // F -> F * L || L
            // L -> v || (S)
            npda.addDeltaRule("q","v","v",new List<Symbol>() { "q" },new List<Symbol>() { "ε" }); //δ(q,v,v) = (ε)
            npda.addDeltaRule("q","+","+",new List<Symbol>() { "q" },new List<Symbol>() { "ε" }); //δ(q,+,+) = (ε)
            npda.addDeltaRule("q","*","*",new List<Symbol>() { "q" },new List<Symbol>() { "ε" }); //δ(q,*,*) = (ε)
            npda.addDeltaRule("q","(","(",new List<Symbol>() { "q" },new List<Symbol>() { "ε" }); //δ(q,(,() = (ε)
            npda.addDeltaRule("q",")",")",new List<Symbol>() { "q" },new List<Symbol>() { "ε" }); //δ(q,),) ) = (ε)

            npda.addDeltaRule("q", "+", "*",new List<Symbol>() { "q" },new List<Symbol>() { "ε" }); //δ(q,+,*) = (ε)
            npda.addDeltaRule("q", "ε", "*",new List<Symbol>() { "qf" },new List<Symbol>() { "ε" }); //δ(q,ε,*) = (ε)
            npda.addDeltaRule("q", "(", "v",new List<Symbol>() { "q" },new List<Symbol>() { "ε" }); //δ(q,(,v) = (ε)
            npda.addDeltaRule("q", "v", "*",new List<Symbol>() { "q" },new List<Symbol>() { "ε" }); //δ(q,v,*) = (ε)
            npda.addDeltaRule("q", "+", "v",new List<Symbol>() { "q" },new List<Symbol>() { "ε" }); //δ(q,+,v) = (ε)
            npda.addDeltaRule("q", ")", "v",new List<Symbol>() { "q" },new List<Symbol>() { "ε" }); //δ(q,),v) = (ε)

            npda.addDeltaRule("q", "ε", "S",new List<Symbol>() { "q" },new List<Symbol>() { "F" }); //δ(q,ε,S) = (F)
            npda.addDeltaRule("q", "ε", "F",new List<Symbol>() { "q" },new List<Symbol>() { "F","*","L" }); //δ(q,ε,F) = (F,*,L)
            npda.addDeltaRule("q", "ε", "F",new List<Symbol>() { "q" },new List<Symbol>() { "L" }); //δ(q,ε,F) = (L)
            npda.addDeltaRule("q", "ε", "L",new List<Symbol>() { "q" },new List<Symbol>() { "v" }); //δ(q,ε,L) = (v)
            npda.addDeltaRule("q", "ε", "L",new List<Symbol>() { "q" },new List<Symbol>() { "(","S",")" }); //δ(q,ε,L) = ((,S,))

                            npda.Debug();
            Console.WriteLine("\nEnter the line :");
                            // Example: v+v
                            //          v*(v+v)
            Console.WriteLine(npda.Execute(Console.ReadLine()).ToString());

            break;
          }
          case "7.5" : { // i @ i
                            // S -> F@L
                            // F -> i
                            // L -> i
              var pda = new PDA(new List<Symbol>() { "q0","q1","q2","qf" },
                                 new List<Symbol>() { "i","@" },
                                 new List<Symbol>() { "z0","i", },
                                 "q0",
                                 "z0",
                                 new List<Symbol>() { "qf" });
              pda.addDeltaRule("q0", "i", "z0",new List<Symbol>() { "q1" },new List<Symbol>() { "i", "z0" }); //δ(q0,i,z0) = (i,z0)
              pda.addDeltaRule("q", "@", "i",new List<Symbol>() { "q" },new List<Symbol>() { "ε" }); //δ(q,@,i) = (ε)
              pda.addDeltaRule("q", "i", "i",new List<Symbol>() { "q" },new List<Symbol>() { "i", "i"}); //δ(q,i,i) = (i, i)
              pda.addDeltaRule("q", "ε", "i",new List<Symbol>() { "qf" },new List<Symbol>() { "ε" }); //δ(q,ε,i) = (ε)
                            pda.Debug();
              Console.WriteLine("Example: i@i\n");
              Console.WriteLine(pda.Execute("i@i"));
              break;
                      
          }
          case "7.6":  //проверка PDA 
            var pda1 = new PDA(
                     new List<Symbol>() { "q0","q","qf" },
                     new List<Symbol>() { "m","h" },
                     new List<Symbol>() { "m","h","z0","F","D" },
                     "q0",
                     "z0",
                     new List<Symbol>() { "qf" });
            pda1.addDeltaRule("q0","ε","z0",new List<Symbol>() { "q" },new List<Symbol>() { "F","D" });
            pda1.addDeltaRule("q","ε","D",new List<Symbol>() { "q" },new List<Symbol>() { "m" });
            pda1.addDeltaRule("q","ε","F",new List<Symbol>() { "q" },new List<Symbol>() { "h" });
            pda1.addDeltaRule("q","h","h",new List<Symbol>() { "q" },new List<Symbol>() { "ε" });
            pda1.addDeltaRule("q","m","m",new List<Symbol>() { "qf" },new List<Symbol>() { "ε" });
            pda1.Debug();

            Console.WriteLine("Example: hm\n");
            Console.WriteLine(pda1.Execute(Console.ReadLine()).ToString());
            break;

          case "7.10": { /* Конфигурируемый МП-автомат: недетерминированный МПА */

              // NOTE: Распознаёт любой набор из 0 и 1
              CPDA<DeltaQSigmaGamma> cpda = new CPDA<DeltaQSigmaGamma>(
                 new List<Symbol>() { "q","p","r" },
                 new List<Symbol>() { "0","1" },
                 new List<Symbol>() { "0","1","z" },
                 "q","z",
                 new List<Symbol>() { "r" }
              );

              cpda.AddRule(("q", "0", "ε", new List<Symbol>() { "q" }, new List<Symbol>() { "0" }));
              cpda.AddRule(("q", "1", "ε", new List<Symbol>() { "q" }, new List<Symbol>() { "1" }));
              cpda.AddRule(("q", "ε", "ε", new List<Symbol>() { "p" }, new List<Symbol>() { "ε" }));

              cpda.AddRule(("p", "0", "0", new List<Symbol>() { "p" }, new List<Symbol>() { "ε" }));
              cpda.AddRule(("p", "1", "1", new List<Symbol>() { "p" }, new List<Symbol>() { "ε" }));
              cpda.AddRule(("p", "ε", "z", new List<Symbol>() { "r" }, new List<Symbol>() { "ε" }));

              cpda.SetUp((Queue<(Symbol, int, List<Symbol>)> configs,(Symbol q, int i, List<Symbol> pdl) config,string chain) => {
                // NOTE: Accept if the whole chain was read AND the current state is accepting (belongs to F)
                if (config.i==chain.Length-1&&cpda.F.Contains(config.q))
                  return true;

                foreach (DeltaQSigmaGamma rule in cpda.Delta) {
                  // NOTE: If current state does not match rule's state, skip
                  if (rule.LHSQ!=config.q)
                    continue;

                  // NOTE: If current input symbol does not match rule's symbol AND it is not epsilon-move, skip
                  if (!((config.i<chain.Length-1&&chain[config.i].ToString().Equals(rule.LHSS.symbol))||
                        rule.LHSS.IsEpsilon())) {
                    continue;
                  }

                  // NOTE: Skip rules that make stack empty
                  if (config.pdl.Count<1)
                    continue;

                  int new_i = config.i;
                  List<Symbol> new_pdl = new List<Symbol>(config.pdl);

                  // NOTE: Pop symbol from the stack if LHSZ is not epsilon
                  if (!rule.LHSZ.IsEpsilon())
                    new_pdl.RemoveAt(new_pdl.Count-1);

                  // NOTE: Push symbols on to the stack if RHSZ is not epsilon
                  if (! rule.RHSZ.First().IsEpsilon())
                    new_pdl.AddRange(Enumerable.Reverse(rule.RHSZ));

                  // NOTE: Move reading head one symbol to the right if the input symbol is not epsilon
                  if (!rule.LHSS.IsEpsilon())
                    ++new_i;

                  // NOTE: Put new configurations in the queue for further execution
                  configs.Enqueue((rule.RHSQ.First(), new_i, new_pdl));
                }

                return false;
              });

              string[] tests = {
                                  "0",
                                  "1",
                                  "00",
                                  "0101",
                                  "0110",
                                  "10101",
                                  "0012100",
                                  "011001",
                                  "111111",
                                  "0110110",
                               };

              cpda.Debug();
              foreach (string test in tests)
                Console.WriteLine("{0} {1} recognised by custom PDA",test,cpda.Execute(test) ? "was" : "was NOT");
            }
            break;

          case "7.11": { /* Расширенный МП-автомат (детерминированный) */
              EDPDA epda = new EDPDA(new List<Symbol>() { "q, r" },
                                     new List<Symbol>() { "w","v","+","*","(",")" },
                                     new List<Symbol>() { "w","v","+","*","(",")","S0","E","F","L","⊥" },
                                     "q","⊥",
                                     new List<Symbol>() { "r" });

              epda.addDeltaRule("q","w",new List<Symbol>() { "ε" },new List<Symbol>() { "q" },new List<Symbol>() { "w" });
              epda.addDeltaRule("q","v",new List<Symbol>() { "ε" },new List<Symbol>() { "q" },new List<Symbol>() { "v" });
              epda.addDeltaRule("q","+",new List<Symbol>() { "ε" },new List<Symbol>() { "q" },new List<Symbol>() { "+" });
              epda.addDeltaRule("q","*",new List<Symbol>() { "ε" },new List<Symbol>() { "q" },new List<Symbol>() { "*" });
              epda.addDeltaRule("q","(",new List<Symbol>() { "ε" },new List<Symbol>() { "q" },new List<Symbol>() { "(" });
              epda.addDeltaRule("q",")",new List<Symbol>() { "ε" },new List<Symbol>() { "q" },new List<Symbol>() { ")" });

              epda.addDeltaRule("q","ε",new List<Symbol>() { "(","E",")","*","L" },new List<Symbol>() { "q" },new List<Symbol>() { "S0" });
              epda.addDeltaRule("q","ε",new List<Symbol>() { "E","+","F" },new List<Symbol>() { "q" },new List<Symbol>() { "E" });
              epda.addDeltaRule("q","ε",new List<Symbol>() { "F" },new List<Symbol>() { "q" },new List<Symbol>() { "S0" });
              epda.addDeltaRule("q","ε",new List<Symbol>() { "w" },new List<Symbol>() { "q" },new List<Symbol>() { "E" });
              epda.addDeltaRule("q","ε",new List<Symbol>() { "F","*","L" },new List<Symbol>() { "q" },new List<Symbol>() { "F" });
              epda.addDeltaRule("q","ε",new List<Symbol>() { "L" },new List<Symbol>() { "q" },new List<Symbol>() { "F" });
              epda.addDeltaRule("q","ε",new List<Symbol>() { "v" },new List<Symbol>() { "q" },new List<Symbol>() { "L" });
              epda.addDeltaRule("q","ε",new List<Symbol>() { "⊥","S0" },new List<Symbol>() { "r" },new List<Symbol>() { "ε" });

              string[] tests = {
                                  "(w+v)*v",
                                  "v+v",
                                  "v+(w+v)*v",
                                  "v",
                                  "w*w"
                               };

              epda.Debug();
              foreach (string test in tests)
                Console.WriteLine("{0} {1} recognised by EPDA\n",test,epda.Execute(test) ? "was" : "was NOT");
            }
            break;

          case "7.12": { /* Конфигурируемый МП-автомат: недетерминированный РМПА */

              CPDA<DeltaQSigmaGammaEx> cpda = new CPDA<DeltaQSigmaGammaEx>(
                 new List<Symbol>() { "q, r" },
                 new List<Symbol>() { "a","+","*","(",")" },
                 new List<Symbol>() { "a","+","*","(",")","S","F","L","Z" },
                 "q","Z",
                 new List<Symbol>() { "r" }
              );

              cpda.AddRule(("q", "a", new List<Symbol>() { "ε" }, new List<Symbol>() { "q" }, new List<Symbol>() { "a" }));
              cpda.AddRule(("q", "+", new List<Symbol>() { "ε" }, new List<Symbol>() { "q" }, new List<Symbol>() { "+" }));
              cpda.AddRule(("q", "*", new List<Symbol>() { "ε" }, new List<Symbol>() { "q" }, new List<Symbol>() { "*" }));
              cpda.AddRule(("q", "(", new List<Symbol>() { "ε" }, new List<Symbol>() { "q" }, new List<Symbol>() { "(" }));
              cpda.AddRule(("q", ")", new List<Symbol>() { "ε" }, new List<Symbol>() { "q" }, new List<Symbol>() { ")" }));

              cpda.AddRule(("q", "ε", new List<Symbol>() { "S","+","L" }, new List<Symbol>() { "q" }, new List<Symbol>() { "S" }));
              cpda.AddRule(("q", "ε", new List<Symbol>() { "F" }, new List<Symbol>() { "q" }, new List<Symbol>() { "S" }));
              cpda.AddRule(("q", "ε", new List<Symbol>() { "F","*","L" }, new List<Symbol>() { "q" }, new List<Symbol>() { "F" }));
              cpda.AddRule(("q", "ε", new List<Symbol>() { "L" }, new List<Symbol>() { "q" }, new List<Symbol>() { "F" }));
              cpda.AddRule(("q", "ε", new List<Symbol>() { "a" }, new List<Symbol>() { "q" }, new List<Symbol>() { "L" }));
              cpda.AddRule(("q", "ε", new List<Symbol>() { "(","S",")" }, new List<Symbol>() { "q" }, new List<Symbol>() { "L" }));
              cpda.AddRule(("q", "ε", new List<Symbol>() { "Z","S" }, new List<Symbol>() { "r" }, new List<Symbol>() { "ε" }));

              cpda.SetUp((Queue<(Symbol, int, List<Symbol>)> configs,(Symbol q, int i, List<Symbol> pdl) config,string chain) => {
                if (config.i==chain.Length-1&&cpda.F.Contains(config.q))
                  return true;

                foreach (DeltaQSigmaGammaEx rule in cpda.Delta) {
                  if (rule.LHSQ!=config.q)
                    continue;

                  if (!((config.i<chain.Length-1&&chain[config.i].ToString().Equals(rule.LHSS.symbol))||
                        rule.LHSS.IsEpsilon())) {
                    continue;
                  }

                  int size = rule.LHSZX.Count;
                  int index = config.pdl.Count-size;

                  if (index<0)
                    continue;

                  int new_i = config.i;
                  List<Symbol> new_pdl = new List<Symbol>(config.pdl);

                  if (!rule.LHSZX.First().IsEpsilon()) {
                    for (int i = 0; i<size; ++i) {
                      if (new_pdl[index+i]!=rule.LHSZX[i])
                        goto skip;
                    }
                    new_pdl.RemoveRange(index,size);
                  }

                  if (!rule.RHSZ.First().IsEpsilon())
                    new_pdl.AddRange(rule.RHSZ);

                  if (! rule.LHSS.IsEpsilon())
                    ++new_i;

                  configs.Enqueue((rule.RHSQ.First(), new_i, new_pdl));

                skip: continue;
                }

                return false;
              });

              string[] tests = {
                              "(a*a)",
                              "a+(a+a)",
                              "a+a+a",
                              "(a+)",
                              "a+(a*a)+a+a+(a+a)"
               };

              cpda.Debug();
              foreach (string test in tests)
                Console.WriteLine("{0} {1} recognised by custom PDA",test,cpda.Execute(test) ? "was" : "was NOT");
            }
            break;


          case "9.1": { // LL Разбор
              var LL = new Grammar(new List<Symbol>() { "a","c","d","o" },
                                   new List<Symbol>() { "S","A","C","D","E","I","K","L","M","S0" },
                                   "S0");
              LL.AddRule("S0", new List<Symbol>() { "S" });
              LL.AddRule("S",new List<Symbol>() { "A","E" });
              LL.AddRule("S",new List<Symbol>() { "K","M" });
              LL.AddRule("K",new List<Symbol>() { "o" });
              LL.AddRule("A",new List<Symbol>() { "C","D" });
              LL.AddRule("C", new List<Symbol>() { "d" });
              LL.AddRule("D", new List<Symbol>() { "c" });
              LL.AddRule("E", new List<Symbol>() { "a" });
              LL.AddRule("M", new List<Symbol>() { "L", "I" });
              LL.AddRule("L", new List<Symbol>() { "c" });
              LL.AddRule("M", new List<Symbol>() { "c" });
              LL.AddRule("I", new List<Symbol>() { "L","I" });
              LL.AddRule("I", new List<Symbol>() { "c" });

              var parser = new LLParser(LL);
              Console.WriteLine("Пример вводимых строк: (i+i), (i+*i)");
              Console.WriteLine("Введите строку: ");
              string stringChain = Console.ReadLine();

              var chain = new List<Symbol> { };              
              foreach (var x in stringChain)
                chain.Add(new Symbol(x.ToString()));
              if (parser.Parse(chain)) {
                Console.WriteLine("Допуск. Цепочка символов = L(G).");
                Console.WriteLine(parser.OutputConfigure);
              } else {
                Console.WriteLine("Не допуск. Цепочка символов не = L(G).");
              }
              break;
          }
          case "9.2": { // LL Разбор
              var LL1 = new Grammar(new List<Symbol>() { "i","&","^","(",")","" },
                                    new List<Symbol>() { "S","S'","F" },
                                    "S");

              LL1.AddRule("S",new List<Symbol>() { "(","S'" });
              LL1.AddRule("S'",new List<Symbol>() { "F","^","F",")" });
              LL1.AddRule("S'",new List<Symbol>() { "S",")" });
              LL1.AddRule("F",new List<Symbol>() { "&","F" });
              LL1.AddRule("F",new List<Symbol>() { "i" });

              var parser1 = new LLParser(LL1);
              Console.WriteLine("Введите строку: ");
              string stringChain = Console.ReadLine();

              var chain = new List<Symbol> { };              
              foreach (var x in stringChain)
                chain.Add(new Symbol(x.ToString()));
              if (parser1.Parse1(chain)) {
                Console.WriteLine("Допуск. Цепочка символов = L(G).");
                Console.WriteLine(parser1.OutputConfigure);
              } else {
                Console.WriteLine("Не допуск. Цепочка символов не = L(G).");
              }
              break;
          }
          case "14":
            var LR0Grammar = new SLRGrammar(new List<Symbol>() { "i","j","&","^","(",")" },
                                            new List<Symbol>() { "S","F","L" },
                                            new List<Production>(),
                                            "S");

            LR0Grammar.AddRule("S",new List<Symbol>() { "F","^","L" });
            LR0Grammar.AddRule("S",new List<Symbol>() { "(","S",")" });
            LR0Grammar.AddRule("F",new List<Symbol>() { "&","L" });
            LR0Grammar.AddRule("F",new List<Symbol>() { "i" });
            LR0Grammar.AddRule("L",new List<Symbol>() { "j" });

            LR0Grammar.Construct();
            LR0Grammar.Inference();
            break;
          case "16.1": {
              var parser = new MyLRParser();
              parser.ReadGrammar();
              parser.Execute();
              break;
          }
          case "16.2": {
              var parser = new MyLRParser();
              Console.WriteLine("Пример ввода продукций:");
              parser.Example();
              parser.Execute();
              break;
          }
          case "16.3": {
              var parser = new MyLRParser();
              parser.ReadGrammar();
              parser.Execute_LR1();
              break;
          }
          case "16.4": {
              var parser = new MyLRParser();
              Console.WriteLine("Пример ввода продукций:");
              parser.Example_LR1();
              parser.Execute_LR1();
              break;
          }
          case "I1": // SDT
            try {
              var sdt = new mySDTSchemata(new List<Symbol>() { "S","A" },
                        new List<Symbol>() { "0","1" },
                        new List<Symbol>() { "a","b" },
                        "S");

              sdt.addRule(new Symbol("S"),
                      new List<Symbol>() { "0","A","S" },
                      new List<Symbol>() { "S","A","a" });
              sdt.addRule(new Symbol("A"),
                      new List<Symbol>() { "0","S","A" },
                      new List<Symbol>() { "A","S","a" });
              sdt.addRule(new Symbol("S"),
                      new List<Symbol>() { "1" },
                      new List<Symbol>() { "b" });
              sdt.addRule(new Symbol("A"),
                      new List<Symbol>() { "1" },
                      new List<Symbol>() { "b" });

              Console.Write("\nDebug SDTranslator:");
              sdt.debugSDTS();

              sdt.Translate(new List<Symbol>() { "0","0","1","0","1","1","1" });

            } catch (Exception e) {
              Console.WriteLine($"\nОшибка: {e.Message}");
            }

            // Homomorphism
            try {
              var h_table = new myHTable(new List<Symbol>() { "0","1" },
                                          new List<Symbol>() { "1","0" });

              Console.WriteLine("\nDebug Homomorphism:");
              h_table.debugHTable();

              Console.WriteLine("\nInput chain:");
              var r = new List<Symbol>() { "0","1","0","0","1","1" };
              Console.WriteLine(Utility.convert(r));
              Console.WriteLine("\nTranslation:");
              Console.WriteLine(h_table.h(r));
            } catch (Exception e) {
              Console.WriteLine($"\nОшибка: {e.Message}");
            }

            try {
              var sdt = new mySDTSchemata(new List<Symbol>() { "S" },
                                          new List<Symbol>() { "+","i" },
                                          new List<Symbol>() { "+","i" },
                                          "S");

              sdt.addRule(new Symbol("S"),
                              new List<Symbol>() { "+","S_1","S_2" },
                              new List<Symbol>() { "S_2","+","S_1" });

              sdt.addRule(new Symbol("S"),
                              new List<Symbol>() { "i" },
                              new List<Symbol>() { "i" });

              Console.Write("\nDebug SDTranslator:");
              sdt.debugSDTS();

              sdt.Translate(new List<Symbol>() { "+","+","+","i","i","i","i" });

            } catch (Exception e) {
              Console.WriteLine($"\nОшибка: {e.Message}");
            }
            break;

          case "I2":
            var inputGrammar = new Grammar(new List<Symbol>() { "i","+","*","(",")" },
                                           new List<Symbol>() { "E","T","P" },
                                           new List<Production>(),
                                           "E");

            inputGrammar.AddRule("E",new List<Symbol> { "T","+","E" });
            inputGrammar.AddRule("E",new List<Symbol> { "T" });
            inputGrammar.AddRule("T",new List<Symbol> { "P","*","T" });
            inputGrammar.AddRule("T",new List<Symbol> { "P" });
            inputGrammar.AddRule("P",new List<Symbol> { "i" });
            inputGrammar.AddRule("P",new List<Symbol> { "(","E",")" });

            var converter = new ConverterInTransGrammar(new List<Symbol>() { "i","+","*","(",")" },
                                                        new List<Symbol>() { "E","T","P" },
                                                        new List<Production>(),
                                                        "E");

            converter.AddRule("E",new List<Symbol> { "T","+","E" });
            converter.AddRule("E",new List<Symbol> { "T" });
            converter.AddRule("T",new List<Symbol> { "P","*","T" });
            converter.AddRule("T",new List<Symbol> { "P" });
            converter.AddRule("P",new List<Symbol> { "i" });
            converter.AddRule("P",new List<Symbol> { "(","E",")" });

            converter.Construct();
            var tgrm = new TransGrammar();
            tgrm= converter.ConvertInTransGrammar(inputGrammar,"i+i*i","iii*+");

            break;
          case "I3": { // ATGrammar(V,T,OP,S,P)
              var atgr = new ATGrammar(new List<Symbol>() { "P","E","T","S" },
                                       new List<Symbol>() { "*","+","(",")","c" },
                                       new List<Symbol_Operation>(),"S");

              //правила для грамматики
              atgr.Addrule("S",new List<Symbol>() { "E" });
              atgr.Addrule("E",new List<Symbol>() { "E","+","T" });
              atgr.Addrule("E",new List<Symbol>() { "T" });
              atgr.Addrule("T",new List<Symbol>() { "T","*","P" });
              atgr.Addrule("T",new List<Symbol>() { "P" });
              atgr.Addrule("P",new List<Symbol>() { "c" });
              atgr.Addrule("P",new List<Symbol>() { "(","E",")" });

              atgr.NewAT(new List<Symbol>() { "p","q","r" },new List<Symbol>() { "*","+" },new List<Symbol>() { "c" });

              atgr.Print();
              break;
          }
          case "I4": { // ATGrammar(V,T,OP,S,P)
              // S, Er    *, +, cr     {ANS}r
              var atgr = new ATGrammar(
                new List<Symbol>() { "S",new Symbol("E",new List<Symbol>() { "r" }) },
                new List<Symbol>() { "*","+",new Symbol("c",new List<Symbol>() { "r" }) },
                new List<Symbol_Operation>() { new Symbol_Operation("{ANS}",new List<Symbol>() { "r" }) },
                new Symbol("S"));
              atgr.Addrule(new Symbol("S"), // LHS        LHS -> RHS  
                                       new List<Symbol>() { // RHS
                                              new Symbol("E", // S -> Ep {ANS}r r -> p
                                              new List<Symbol>() { "p" }), new Symbol_Operation("{ANS}",new List<Symbol>() { "r" }) },
              new List<AttrFunction>() { new AttrFunction(new List<Symbol>() {"r" },new List<Symbol> { "p" })
                                               }
                                      );

              atgr.Addrule(new Symbol("E",new List<Symbol>() { "p" }), // Ep -> +EpEr p -> q + r
                      new List<Symbol>() { "+",new Symbol("E",new List<Symbol>() { "p" }),
                                                                         new Symbol("E",new List<Symbol>() { "r" }) },
                      new List<AttrFunction>() { new AttrFunction(new List<Symbol>() {
                                     "p" },new List<Symbol> { "q", "+", "r" })
                      });

              atgr.Addrule(new Symbol("E",new List<Symbol>() { "p" }),  // Ep -> *EpEr   p -> q * r
                      new List<Symbol>() { "*",new Symbol("E",new List<Symbol>() { "p" }),new Symbol("E",new List<Symbol>() { "r" }) },
                      new List<AttrFunction>() { new AttrFunction(new List<Symbol>() {
                                     "p" },new List<Symbol> { "q", "+", "r" })
                      });

              atgr.Addrule(new Symbol("E",new List<Symbol>() { "p" }), // Ep -> Cr   p -> r
                   new List<Symbol>() { new Symbol("C",new List<Symbol>() { "r" }) },
                   new List<AttrFunction>() { new AttrFunction(new List<Symbol>() {
                                 "p" },new List<Symbol> { "r" })
                   });

              atgr.Print();

              atgr.transform();   //преобразование в простую форму присваивания

              Console.WriteLine("\nPress Enter to show result\n");
              Console.ReadLine();

              atgr.Print();
              Console.WriteLine("\nPress Enter to end\n");
              Console.ReadLine();
              break;
          } 
          case "I5": {
             /* нужна доработка пример  Минеева 201, 2021  i1 , i2=i3 , i4
              D, Lr, E, F      i_b, =, <,>, n_e    {type}a,c , {push}k, {pull}t
               ATGrammar(V,T,OP,S,P) 
             */
             var atgr = new ATGrammar(
                new List<Symbol>() { "D",new Symbol("L",new List<Symbol>() { "r" }),"E","F" },
                new List<Symbol>() { "=",",",new Symbol("i",new List<Symbol>() { "b" }),new Symbol("n",new List<Symbol>() { "e" }) },
                new List<Symbol_Operation>() {
                    new Symbol_Operation("{type}",new List<Symbol>() { "a", "c" }),
                    new Symbol_Operation("{push}",new List<Symbol>() { "k" }),
                    new Symbol_Operation("{pull}",new List<Symbol>() { "t" }),
                    new Symbol_Operation("{stack}",new List<Symbol>() { "u" }) },
                new Symbol("D"));
              atgr.Addrule(new Symbol("D"), // LHS
                   new List<Symbol>() { // RHS
                        new Symbol("i", // D -> i_b {тип}a,c Lr   a <- b c <- r
                        new List<Symbol>() { "b" }),new Symbol("{type}",new List<Symbol>() { "a", "c"}),
                        new Symbol("L",
                        new List<Symbol>() { "r" })  },

                        new List<AttrFunction>() {
                          new AttrFunction(new List<Symbol>() { "a" },new List<Symbol> { "b" }),
                          new AttrFunction(new List<Symbol>() { "c" },new List<Symbol> { "r" })
                        }
                   );

              atgr.Addrule(new Symbol("L",
              new List<Symbol>() { new Symbol("r") }), // Lr -> n_e    r <- e
              new List<Symbol>() {
                        new Symbol("n",
                        new List<Symbol>() { "e" }) },

              new List<AttrFunction>() {
                        new AttrFunction(new List<Symbol>() { "r" },new List<Symbol> { "e" })
              });

              atgr.Addrule(new Symbol("L",new List<Symbol>() { "r" }),
              new List<Symbol>() {
                            new Symbol("i",
                            new List<Symbol>() { "b" }),
                            new Symbol("{type}",
                            new List<Symbol>() { "a", "c" })
          },

          new List<AttrFunction>() {
             new AttrFunction(new List<Symbol>() { "a" },new List<Symbol> { "b" }),
              new AttrFunction(new List<Symbol>() { "r" },new List<Symbol> { "c" })
          });

              atgr.Addrule(new Symbol("L",new List<Symbol>() { "r" }),
              new List<Symbol>() {
                            ",",
                            new Symbol("i",
                            new List<Symbol>() { "b" }),
                            new Symbol("{type}",
                            new List<Symbol>() { "a", "c" }),
                            new Symbol("{pull}",
                            new List<Symbol>() { "t" }),
                            new Symbol("{pull}",
                            new List<Symbol>() { "p" }),
                            "E"
          },


          new List<AttrFunction>() {
             new AttrFunction(new List<Symbol>() { "a" },new List<Symbol> { "b" }),
             new AttrFunction(new List<Symbol>() { "c" },new List<Symbol> { "t" }),
             new AttrFunction(new List<Symbol>() { "r" },new List<Symbol> { "p" })
          });

              atgr.Addrule(new Symbol("E"),
                  new List<Symbol>() {
                            ",",
                            new Symbol("i",
                            new List<Symbol>() { "b" }),
                            new Symbol("{type}",
                            new List<Symbol>() { "a", "c" }),
                            new Symbol("{pull}",
                            new List<Symbol>() { "t" }),
                            "E"
                  },

                  new List<AttrFunction>() {
                            new AttrFunction(new List<Symbol>() { "a" },new List<Symbol> { "b" }),
                            new AttrFunction(new List<Symbol>() { "c" },new List<Symbol> { "t" })
                  });

              atgr.Addrule(new Symbol("E"),
                  new List<Symbol>() {
                            "=",
                            new Symbol("i",
                            new List<Symbol>() { "b" }),
                            new Symbol("{type}",
                            new List<Symbol>() { "a", "c" }),
                            new Symbol("{push}",
                            new List<Symbol>() { "k" }),
                            "F"
                          },

              new List<AttrFunction>() {
                            new AttrFunction(new List<Symbol>() { "a" },new List<Symbol> { "" }),
                            new AttrFunction(new List<Symbol>() { "k" },new List<Symbol> { "c" })
              });

              atgr.Addrule(new Symbol("F"),
                  new List<Symbol>() {
                            ",",
                            new Symbol("i",
                            new List<Symbol>() { "b" }),
                            new Symbol("{type}",
                            new List<Symbol>() { "a", "c" }),
                            new Symbol("{push}",
                            new List<Symbol>() { "k" }),
                            "F"
                  },

                  new List<AttrFunction>() {
                            new AttrFunction(new List<Symbol>() { "a" },new List<Symbol> { "b" }),
                            new AttrFunction(new List<Symbol>() { "k" },new List<Symbol> { "c" })
                  });

              atgr.Addrule(new Symbol("F"),
                  new List<Symbol>() {
                            new Symbol("{stack}",
                            new List<Symbol>() { "u" })
              },


              new List<AttrFunction>() {
                            new AttrFunction(new List<Symbol>() { "u" },new List<Symbol> { "1" })
              });

              atgr.Print();

              atgr.transform();

              Console.WriteLine("\nPress Enter to show result\n");
              Console.ReadLine();

              atgr.Print();
              Console.WriteLine("\nPress Enter to end\n");
              Console.ReadLine();
              break;
              //###### Тимофеев 207 2021  ###### 
          }
          case "I6": {
              var atgr = new ATGrammar(
                new List<Symbol>() { "A","B","C","D","E","F","S" },
                new List<Symbol>() { "for","+","(",")","int","=","id",";","<","const2","const1","const3","{","body","}" },
                new List<Symbol_Operation>() { new Symbol_Operation  ("D->{ OUT1:= ' for idn1 in range(const1, '}", new List<Symbol>() { "r" }),
                      new Symbol_Operation("E->{OUT2:= 'const2,'}", new List<Symbol>() { "r" }),
                      new Symbol_Operation("F->{OUT1:= 'const3): '}", new List<Symbol>() { "r" }),
                      new Symbol_Operation("B->{ OUT1:= ' body '}", new List<Symbol>() { "r" }),
                      new Symbol_Operation("C->{ if n3 > n4 then Error}", new List<Symbol>() { "n3", "n4" }) },
                      new Symbol("S"));

              atgr.Addrule("S0",new List<Symbol>() { "S","{ OUT1 := OUT1 || OUT2 }" });
              atgr.Addrule("S",new List<Symbol>() { "A","B","C" });
              atgr.Addrule("A",new List<Symbol>() { "for","(","D","E","F" });
              atgr.Addrule("B",new List<Symbol>() { "body","{OUT1:= ' body'}" });
              atgr.Addrule("C",new List<Symbol>() { "}" });
              atgr.Addrule("D",new List<Symbol>() { "int","id","=","const1",";",new Symbol("{ OUT1:= ' for id_a in range(const1_n, '}",new List<Symbol>() { "n" }) });
              atgr.Addrule("E",new List<Symbol>() { "id","<","const2",";",new Symbol("{OUT2:= 'const2_p,'}",new List<Symbol>() { "p" }) });
              atgr.Addrule("F",new List<Symbol>() { "id","+","const3",")","{",new Symbol("{OUT2:= 'const3_p): '}",new List<Symbol>() { "p" }) });

              atgr.ATG_C_Py(new List<Symbol>() { "s","a","n","p","q","r" },new List<Symbol>() { "=","<","+" });

              atgr.Print();

              Console.WriteLine("\nPress Enter to end\n");
              Console.ReadLine();
              break;
          }
          //######    ######

          case "I7": {
              /* Пример "цепочечного" перевода  11 12 13   
              Грамматика транслирует выражения из инфиксной записи в постфиксную
              Выражения состоят из i, +, * и скобок
              i+i*i без чисел 
              */
              var chainPostfix = new SDT.Scheme(new List<SDT.Symbol>() { "i","+","*","(",")" },
                                                       new List<SDT.Symbol>() { "E","E'","T","T'","F" },
                                                       "E");

              chainPostfix.AddRule("E",new List<SDT.Symbol>() { "T","E'" });
              chainPostfix.AddRule("E'",new List<SDT.Symbol>() { "+","T",SDT.Actions.Print("+"),"E'" });
              chainPostfix.AddRule("E'",new List<SDT.Symbol>() { SDT.Symbol.Epsilon });
              chainPostfix.AddRule("T",new List<SDT.Symbol>() { "F","T'" });
              chainPostfix.AddRule("T'",new List<SDT.Symbol>() { "*","F",SDT.Actions.Print("*"),"T'" });
              chainPostfix.AddRule("T'",new List<SDT.Symbol>() { SDT.Symbol.Epsilon });
              chainPostfix.AddRule("F",new List<SDT.Symbol>() { "i",SDT.Actions.Print("i") });
              chainPostfix.AddRule("F",new List<SDT.Symbol>() { "(","E",")" });

              //SDT.LLTranslator chainTranslator = new SDT(chainPostfix);
              var chainTranslator = new SDT.LLTranslator(chainPostfix);
              // Console.WriteLine("Введите строку: ");
              var inp_str = new SDT.SimpleLexer().Parse(Console.ReadLine());
              if (chainTranslator.Parse(inp_str)) {
                Console.WriteLine("\nУспех. Строка соответствует грамматике.");
              } else {
                Console.WriteLine("\nНе успех. Строка не соответствует грамматике.");
              }
              break;
          }
          case "I8": {
              /* L-атрибутивная грамматика
                 Грамматика вычисляет результат арифметического выражения
                 Выражения состоят из целых положительных чисел, +, * и скобок
                 1+2*3 без чисел
              */

              SDT.Types.Attrs sAttrs = new() { ["value"]=0 };
              SDT.Types.Attrs lAttrs = new() { ["inh"]=0,["syn"]=0 };
              var lAttrSDT = new SDT.Scheme(new List<SDT.Symbol>() { new SDT.Symbol("number",sAttrs),"+","*","(",")" },
                                                    new List<SDT.Symbol>() { "S", new SDT.Symbol("E", sAttrs), new SDT.Symbol("E'", lAttrs),
                                                                                       new SDT.Symbol("T", sAttrs), new SDT.Symbol("T'", lAttrs), new SDT.Symbol("F", sAttrs) },
                                                    "S");

              lAttrSDT.AddRule("S",new List<SDT.Symbol>() { "E",new SDT.Types.Actions((S) => Console.Write(S["E"]["value"].ToString())) });

              lAttrSDT.AddRule("E",new List<SDT.Symbol>() { "T",new SDT.Types.Actions((S) => S["E'"]["inh"]=S["T"]["value"]),"E'",new SDT.Types.Actions((S) => S["E"]["value"]=S["E'"]["syn"]) });

              lAttrSDT.AddRule("E'",new List<SDT.Symbol>() { "+","T",new SDT.Types.Actions((S) => S["E'1"]["inh"]=(int)S["E'"]["inh"]+(int)S["T"]["value"]),"E'",new SDT.Types.Actions((S) => S["E'"]["syn"]=S["E'1"]["syn"]) });

              lAttrSDT.AddRule("E'",new List<SDT.Symbol>() { SDT.Symbol.Epsilon,new SDT.Types.Actions((S) => S["E'"]["syn"]=S["E'"]["inh"]) });

              lAttrSDT.AddRule("T",new List<SDT.Symbol>() { "F",new SDT.Types.Actions((S) => S["T'"]["inh"]=S["F"]["value"]),"T'",new SDT.Types.Actions((S) => S["T"]["value"]=S["T'"]["syn"]) });

              lAttrSDT.AddRule("T'",new List<SDT.Symbol>() { "*","F",new SDT.Types.Actions((S) => S["T'1"]["inh"]=(int)S["T'"]["inh"]*(int)S["F"]["value"]),"T'",new SDT.Types.Actions((S) => S["T'"]["syn"]=S["T'1"]["syn"]) });

              lAttrSDT.AddRule("T'",new List<SDT.Symbol>() { SDT.Symbol.Epsilon,new SDT.Types.Actions((S) => S["T'"]["syn"]=S["T'"]["inh"]) });

              lAttrSDT.AddRule("F",new List<SDT.Symbol>() { "number",new SDT.Types.Actions((S) => S["F"]["value"]=S["number"]["value"]) });

              lAttrSDT.AddRule("F",new List<SDT.Symbol>() { "(","E",")",new SDT.Types.Actions((S) => S["F"]["value"]=S["E"]["value"]) });

              SDT.LLTranslator lAttrTranslator = new(lAttrSDT);
              if (lAttrTranslator.Parse(new SDT.ArithmLexer().Parse(Console.ReadLine()))) {
                Console.WriteLine("\nУспех. Строка соответствует грамматике.");
              } else {
                Console.WriteLine("\nНе успех. Строка не соответствует грамматике.");
              }

              break;
          }

          case "I9": {      // Капалин Д.С.
                            /* Дерево разбора  1*3 только для умножения 
                               Грамматика вычисляет арифметические выражения состоящие из произведений целых положительных числе
                               Дерево разбора печатается на экран, конвертируется в .dot файл и выполняется
                            */

                            var atgr = new ATGrammar(
                              new List<Symbol>() { "S", new Symbol("E", new List<Symbol>() { "r" }) },
                              new List<Symbol>() { "*", new Symbol("c", new List<Symbol>() { "r" }) },
                              new List<Symbol_Operation>() { new Symbol_Operation("{ANS}") },   //new List<Symbol>() { "r" }
                              new Symbol("S"));

                            atgr.Addrule(new Symbol("S"), // LHS        LHS -> RHS  
                                                     new List<Symbol>() { // RHS
                                              new Symbol("E", // S -> Ep {ANS}r r -> p
                                              new List<Symbol>() { "p" }), new Symbol_Operation("{ANS}") },  //new List<Symbol>() { "r" }
                            new List<AttrFunction>() { new AttrFunction(new List<Symbol>() {"r" },new List<Symbol> { "p" })
                                                             }
                                                    );

                            atgr.Addrule(new Symbol("E", new List<Symbol>() { "p" }),  // Ep -> *EqEr   p -> q * r
                                    new List<Symbol>() {  new Symbol("E", new List<Symbol>() { "q" }), new Symbol("E", new List<Symbol>() { "r" }), "*" },
                                    new List<AttrFunction>() { new AttrFunction(new List<Symbol>() {
                                     "p" },new List<Symbol> { "q", "*", "r" })
                                    });

                            atgr.Addrule(new Symbol("E", new List<Symbol>() { "p" }), // Ep -> Cr   p -> r
                                 new List<Symbol>() { new Symbol("c", new List<Symbol>() { "r" }) },  //, new List<Symbol>() { "r" }
                                 new List<AttrFunction>() { new AttrFunction(new List<Symbol>() {
                                 "p" },new List<Symbol> { "r" })
                                 });

                            atgr.Print();
                            Console.WriteLine("Пример вывода цепочки грамматики:");
                            Console.WriteLine("S ⇒   E{ANS} ⇒ E * E {ADD} ⇒  E*E*E{ANS} ⇒  c3 * E*E{ANS}⇒  c3*c4*E{ANS} ⇒   c3*c4*c5{ANS} ");
                            Console.WriteLine("Для построения дерева разбора необходимо будет ввести цепочку вывода в виде последовательности правил");
                            Console.WriteLine("Например: 1 2 2 3 3 3");
                            var a = new ATScheme(atgr, atgr);
                            a.BuildInputTree();
                            a.PrintTree();
                            
                            

                            /* 
                            SDT.Types.Attrs sAttrs2 = new() { ["value"]=0 };
                            SDT.Types.Attrs lAttrs2 = new() { ["inh"]=0,["syn"]=0 };
                            SDT.Scheme treeGrammar = new SDT.Scheme(new List<SDT.Symbol>() { new SDT.Symbol("number",sAttrs2),"*" },
                                                                    new List<SDT.Symbol>() { "S",new SDT.Symbol("T",sAttrs2),new SDT.Symbol("T'",lAttrs2),new SDT.Symbol("F",sAttrs2) },
                                                                    "S");

                            SDT.OperationSymbol op1 = new(new SDT.Types.Actions((S) => Console.Write(S["T"]["value"].ToString())),"print(T.value)");
                            SDT.OperationSymbol op2 = new(new SDT.Types.Actions((S) => S["T'"]["inh"]=S["F"]["value"]),"T'.inh = F.value",new() { "T'.inh" },new() { "F.value" });
                            SDT.OperationSymbol op3 = new(new SDT.Types.Actions((S) => S["T"]["value"]=S["T'"]["syn"]),"T.value = T'.syn",new() { "T.value" },new() { "T'.syn" });
                            SDT.OperationSymbol op4 = new(new SDT.Types.Actions((S) => S["T'1"]["inh"]=(int)S["T'"]["inh"]*(int)S["F"]["value"]),"T'1.inh = T'.inh * F.value",new() { "T'1.inh" },new() { "T'.inh","F.value" });
                            SDT.OperationSymbol op5 = new(new SDT.Types.Actions((S) => S["T'"]["syn"]=S["T'1"]["syn"]),"T'.syn = T'1.syn",new() { "T'.syn" },new() { "T'1.syn" });
                            SDT.OperationSymbol op6 = new(new SDT.Types.Actions((S) => S["T'"]["syn"]=S["T'"]["inh"]),"T'.syn = T'.inh",new() { "T'.syn" },new() { "T'.inh" });
                            SDT.OperationSymbol op7 = new(new SDT.Types.Actions((S) => S["F"]["value"]=S["number"]["value"]),"F.value = number.value",new() { "F.value" },new() { "number.value" });

                            treeGrammar.AddRule("S",new List<SDT.Symbol>() { "T",op1 });
                            treeGrammar.AddRule("T",new List<SDT.Symbol>() { "F",op2,"T'",op3 });
                            treeGrammar.AddRule("T'",new List<SDT.Symbol>() { "*","F",op4,"T'",op5 });
                            treeGrammar.AddRule("T'",new List<SDT.Symbol>() { SDT.Symbol.Epsilon,op6 });
                            treeGrammar.AddRule("F",new List<SDT.Symbol>() { "number",op7 });

                            SDT.ParseTreeTranslator treeTr = new(treeGrammar);
                            SDT.ParseTree root = treeTr.Parse(new SDT.ArithmLexer().Parse(Console.ReadLine()));
                            if (root!=null) {
                              root.Print();
                              root.Execute();
                              // утилиты для прорисовки дерева в файл  
                              root.PrintToFile("../../../../parse_tree.dot",true);
                              root.PrintToFile("../../../../parse_tree2.dot",false);
                            } else {
                              Console.WriteLine("Строка не соответствует грамматике");
                            }
                            */
                            break;
          }


          case "I10": { /* Барсов А.В. , Савин А.А. , М8О-307Б-19
                         * «Перевод конечных автоматов и грамматик в код на С# на основе СУ-схемы» */
                /* В общем виде задаётся правило вида: S →   #→$   .addRule(#, new List<Symbol> { $ });
                   - это общее правило для порождения правил грамматики.
                   Из него достаём нужные элементы и записываем их следующим образом 
                   Реализация для работы с файлами не включена 
                */

                string[] ms = "@ # $ %".Split(' ');                             // мета символы для подстановки
                string grammarPattern = "# → $";                                // левое правило для порождения правил грамматики 
                string grammarRules = ".addRule(#, new List<Symbol> { $ });";   // правило грамматики для кода(правая)
                List<string> r = new List<string>()                             // правила для перевода
                {   "Å → Å1 + B", 
                    "Å → T", 
                    "Å → T * P", 
                    "T → T * P",
                    "T → P", 
                    "P → ( E )", 
                    "P → i" 
                };
                Console.WriteLine("Заданная грамматика: S →    " + grammarPattern + " ,   " + grammarRules);
                SDTBuilder sdtBuilder = new SDTBuilder(ms, grammarPattern, grammarRules, r);
                sdtBuilder.BuildTable();
                
                break;
            }
            case "I11": {
              var setlFileContent = @"
                          # G(T,V,P,S)

                          # Grammar 1
                          T = {c, d, f, t, g}
                          V = {A, B, C, D, F, G, T}
                          S = A
                          P = {A -> B, B -> CD, C -> c, D -> d, D -> FG, F -> f, G -> g, G -> T, T -> t}
                          C = { a ∈ T* | S => a }

                          # Grammar 2
                          T = {c, d, f, t, g}
                          V = {A, B, C, D, F, G, T}
                          S = A
                          P = {A -> B, B -> CD, C -> c, D -> d}
                          L = { a ∈ T* | S => a }

                          # Final chains intersection
                          M = C ∩ L
                        ";
              Console.WriteLine("\nEXECUTING SETL:");
              var setl = new SETL(setlFileContent);
              foreach (var variable in setl.Variables) {
                Console.WriteLine($"{variable.Value.Name} = {variable.Value.Data}");
              }
              Console.ReadLine();
              break;
            }
          case "I18": {
              //Алгоритм 6.1  Преобразование деревьев при помощи СУ-схемы
              //Белоусов М8О-207Б-20 2022

              var V = new List<Symbol>() { new Symbol("E",new List<Symbol>() { "p" }),new Symbol("T",new List<Symbol>() { "q" }) }; // Множество нетерминалов
              var OP = new List<Symbol_Operation>() { new Symbol_Operation("Add"),new Symbol_Operation("Mul") }; // Множество операционных символов
              var TerminalInput = new List<Symbol>() { "+","*",new Symbol("i",new List<Symbol>() { "r" }) }; // Множество терминалов входной грамматики
              var TerminalOutput = new List<Symbol>() { "+","*",new Symbol("i",new List<Symbol>() { "r" }) }; // Множество терминалов выходной грамматики
              var S = new Symbol("E",new List<Symbol>() { "p" }); // Начальный символ

              //1. E_p -> (T_q, T_q)
              //   p <- q
              //2. E_p -> (E_q + T_r{Add}, E_q T_r + {Add})
              //   p <- q + r
              //3. E_p -> (E_q * T_r{Mul}, E_q T_r * {Mul})
              //   p <- q * r
              //4. T_p -> (i_q, i_q)
              //   p <- q


              // Задаем входную грамматику
              var GrammarInput = new ATGrammar(V,TerminalInput,OP,S);

              GrammarInput.Addrule(new Symbol("E",new List<Symbol>() { "p" }),
                  new List<Symbol>() { new Symbol("T",new List<Symbol>() { "q" }) },
                  new List<AttrFunction>() { new AttrFunction(new List<Symbol>() { "p" },new List<Symbol>() { "q" }) });

              GrammarInput.Addrule(new Symbol("E",new List<Symbol>() { "p" }),
                  new List<Symbol>() { new Symbol("E",new List<Symbol>() { "q" }),"+",new Symbol("T",new List<Symbol>() { "r" }),"Add" },
                  new List<AttrFunction>() { new AttrFunction(new List<Symbol>() { "p" },new List<Symbol>() { "q","+","r" }) });

              GrammarInput.Addrule(new Symbol("E",new List<Symbol>() { "p" }),
                  new List<Symbol>() { new Symbol("E",new List<Symbol>() { "q" }),"*",new Symbol("T",new List<Symbol>() { "r" }),"Mul" },
                  new List<AttrFunction>() { new AttrFunction(new List<Symbol>() { "p" },new List<Symbol>() { "q","*","r" }) });

              GrammarInput.Addrule(new Symbol("T",new List<Symbol>() { "p" }),
                  new List<Symbol>() { new Symbol("i",new List<Symbol>() { "q" }) },
                  new List<AttrFunction>() { new AttrFunction(new List<Symbol>() { "p" },new List<Symbol>() { "q" }) });
              Console.WriteLine("Входная грамматика:");
              GrammarInput.Print();

              // Задаем выходную грамматику
              var GrammarOutput = new ATGrammar(V,TerminalOutput,OP,S);

              GrammarOutput.Addrule(new Symbol("E",new List<Symbol>() { "p" }),
                  new List<Symbol>() { new Symbol("T",new List<Symbol>() { "q" }) },
                  new List<AttrFunction>() { new AttrFunction(new List<Symbol>() { "p" },new List<Symbol>() { "q" }) });

              GrammarOutput.Addrule(new Symbol("E",new List<Symbol>() { "p" }),
                  new List<Symbol>() { new Symbol("E",new List<Symbol>() { "q" }),new Symbol("T",new List<Symbol>() { "r" }),"+","Add" },
                  new List<AttrFunction>() { new AttrFunction(new List<Symbol>() { "p" },new List<Symbol>() { "q","+","r" }) });

              GrammarOutput.Addrule(new Symbol("E",new List<Symbol>() { "p" }),
                  new List<Symbol>() { new Symbol("E",new List<Symbol>() { "q" }),new Symbol("T",new List<Symbol>() { "r" }),"*","Mul" },
                  new List<AttrFunction>() { new AttrFunction(new List<Symbol>() { "p" },new List<Symbol>() { "q","*","r" }) });

              GrammarOutput.Addrule(new Symbol("T",new List<Symbol>() { "p" }),
                  new List<Symbol>() { new Symbol("i",new List<Symbol>() { "q" }) },
                  new List<AttrFunction>() { new AttrFunction(new List<Symbol>() { "p" },new List<Symbol>() { "q" }) });

              // Построение СУ-схемы на основе входной и выходной грамматик
              var Scheme = new ATScheme(GrammarInput,GrammarOutput);

              // i_7 + i_5 * i_3
              // 2 3 1 4 4 4
              // 7 5 3

              // Построение дерева вывода для входной грамматики
              Console.WriteLine(" Введите цепочку для входной грамматики, ввод через пробелы, например 2 3 1 4 4 4:");
              Scheme.BuildInputTree();

              // Вывод дерева
              Scheme.PrintTree();
              // Преобразование дерева
              Scheme.TreeTransform();
              // Вывод преобразованного дерева
              Scheme.PrintTree();
              break;
            }
          default:
            Console.WriteLine("Выход из программы");
            return;
        } // end switch
      } // end while
    } // end void Main()

  } // end class Program
}
