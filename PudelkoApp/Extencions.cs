using PudelkoLib;
using System;

namespace PudelkoApp
{
    //15
    public static class Extencions
    {
        public static Pudelko Kompresuj(this Pudelko p)
        {
            double objetosc = Math.Pow(p.Objetosc, 1D / 3);
            Pudelko szescian = new Pudelko(objetosc, objetosc, objetosc);
            return szescian;
        }
    }
}
