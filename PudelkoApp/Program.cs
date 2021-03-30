using PudelkoLib;
using PudelkoLib.enums;
using System;
using System.Collections.Generic;

namespace PudelkoApp
{
    class Program
    {

        static void Main(string[] args)
        {
            //16 lista pudełek
            List<Pudelko> Pudelka = new List<Pudelko>();
            Pudelko p1 = new Pudelko(2.5, 9.321, 0.1, UnitOfMeasure.meter);
            Pudelko p2 = new Pudelko(9, 6, 0.5, UnitOfMeasure.meter);
            Pudelko p3 = new Pudelko(9, 60, 14, UnitOfMeasure.milimeter);
            Pudelko p4 = new Pudelko(9.41, null, 1.7, UnitOfMeasure.meter);
            Pudelko p5 = new Pudelko(6, 6, 0.5);
            Pudelko p6 = new Pudelko();
            Pudelko p7 = p2.Kompresuj();
            Pudelko p8 = Pudelko.Parse("2.500 m × 9.321 m × 0.100 m");
            Pudelka.Add(p1);
            Pudelka.Add(p2);
            Pudelka.Add(p3);
            Pudelka.Add(p4);
            Pudelka.Add(p5);
            Pudelka.Add(p6);
            Pudelka.Add(p7);
            Pudelka.Add(p8);
            //16 wypisanie pudełek
            foreach (var i in Pudelka)
            {
                Console.WriteLine(i.ToString());
            }
            //16 Compare
            Pudelka.Sort(CompareThroughObjThenPoleThenSuma);
            Console.WriteLine();
            Console.WriteLine("Lista Pudełek Posortowana");
            Console.WriteLine();
            foreach (var i in Pudelka)
            {
                Console.WriteLine(i.ToString());
            }
        }

        private static int CompareThroughObjThenPoleThenSuma(Pudelko x, Pudelko y)
        {
            if (x == null)
            {
                if (y == null) return 0;
                else return -1;
            }
            else
            {
                if (y == null) return 1;
                else
                {
                    int objetosc = x.Objetosc.CompareTo(y.Objetosc);
                    if (objetosc != 0) return objetosc;
                    else
                    {
                        int pole = x.Pole.CompareTo(y.Pole);
                        if (pole != 0) return pole;
                        else
                        {
                            int sumaBokow = (x.A + x.B + x.C).CompareTo(y.A + y.B + y.C);
                            return sumaBokow;
                        }
                    }
                }
            }
        }
    }
}
