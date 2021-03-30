using PudelkoLib.enums;
using System;
using System.Collections;
using System.Globalization;

namespace PudelkoLib
{
    public sealed class Pudelko : IFormattable, IEquatable<Pudelko>, IEnumerator, IEnumerable
    {
        public double a { get; }
        public double b { get; }
        public double c { get; }

        public double aMeters { get => this.a; }
        public double bMeters { get => this.b; }
        public double cMeters { get => this.c; }
        public double aCentiMeters { get => this.a / 0.01; }
        public double bCentiMeters { get => this.b / 0.01; }
        public double cCentiMeters { get => this.c / 0.01; }

        public double aMiliMeters { get => this.a / 0.001; }
        public double bMiliMeters { get => this.b / 0.001; }
        public double cMiliMeters { get => this.c / 0.001; }

        //3
        public double A { get => this.aMeters; }
        public double B { get => this.bMeters; }
        public double C { get => this.cMeters; }
        public UnitOfMeasure Unit { get; set; } = UnitOfMeasure.meter;
        int Position = -1;

        private double SetValue(double value)
        {
            switch (Unit)
            {
                case UnitOfMeasure.milimeter:
                    value *= 0.001;
                    break;
                case UnitOfMeasure.centimeter:
                    value *= 0.01;
                    break;
                case UnitOfMeasure.meter:
                    break;
            }
            var tmp = Math.Round(value, 3);
            //1
            if (tmp <= 0 || tmp > 10)
                throw new ArgumentOutOfRangeException("Wartość podana poza skalą ");

            return value;
        }
        //2 konstruktor
        public Pudelko(double? a = null, double? b = null, double? c = null, UnitOfMeasure unit = UnitOfMeasure.meter)
        {
            this.Unit = unit;
            //2 domyślny konstruktor
            this.a = (a != null) ? SetValue((double)a) : 0.1;
            this.b = (b != null) ? SetValue((double)b) : 0.1;
            this.c = (c != null) ? SetValue((double)c) : 0.1;
        }


        //4
        public override string ToString()
        {
            return this.ToString("m", CultureInfo.CurrentCulture);
        }

        public string ToString(string format, IFormatProvider formatProvider = null)
        {
            if (String.IsNullOrEmpty(format)) format = "m";
            if (formatProvider == null) formatProvider = CultureInfo.CurrentCulture;
            switch (format)
            {
                case "m":
                    return $"{aMeters.ToString("0.000", formatProvider)} m \u00D7 " +
                        $"{bMeters.ToString("0.000", formatProvider)} m \u00D7 " +
                        $"{cMeters.ToString("0.000", formatProvider)} m";
                case "cm":
                    return $"{aCentiMeters.ToString("000.0".TrimStart(new Char[] { '0' }), formatProvider)} cm \u00D7 " +
                       $"{bCentiMeters.ToString("000.0".TrimStart(new Char[] { '0' }), formatProvider)} cm \u00D7 " +
                       $"{cCentiMeters.ToString("000.0".TrimStart(new Char[] { '0' }), formatProvider)} cm";
                case "mm":
                    return $"{aMiliMeters.ToString("0000".TrimStart(new Char[] { '0' }), formatProvider)} mm \u00D7 " +
                       $"{bMiliMeters.ToString("0000".TrimStart(new Char[] { '0' }), formatProvider)} mm \u00D7 " +
                       $"{cMiliMeters.ToString("0000".TrimStart(new Char[] { '0' }), formatProvider)} mm";
                default:
                    throw new FormatException("Zły typ rozmiaru");
            }
        }

        //5
        public double Objetosc { get => Math.Round(a * b * c, 9); }


        //6
        public double Pole { get => Math.Round((2 * (a * b)) + (2 * (b * c)) + (2 * (c * a)), 6); }


        //7
        public bool Equals(Pudelko other)
        {
            if (other is null) return false;
            if (Object.ReferenceEquals(this, other))
                return true;

            return (Pole == other.Pole && Objetosc == other.Objetosc);
        }

        public override bool Equals(object obj)
        {
            if (obj is Pudelko)
                return Equals((Pudelko)obj);
            else
                return false;
        }

        public override int GetHashCode()
        {
            return A.GetHashCode() + B.GetHashCode() + C.GetHashCode();
        }

        public static bool Equals(Pudelko p1, Pudelko p2)
        {
            if ((p1 is null) && (p2 is null)) return true;
            if (p1 is null) return false;

            return p1.Equals(p2);
        }

        public static bool operator ==(Pudelko p1, Pudelko p2) => Equals(p1, p2);
        public static bool operator !=(Pudelko p1, Pudelko p2) => !(p1 == p2);


        //9 implicit
        public static implicit operator Pudelko(ValueTuple<int, int, int> v) => new Pudelko(v.Item1, v.Item2, v.Item3, UnitOfMeasure.milimeter);

        //9 explicit
        public static explicit operator double[](Pudelko p) => new double[] { p.a, p.b, p.c };


        //10
        public double this[int index]
        {
            get
            {
                if (index == 0)
                {
                    return A;
                }
                else if (index == 1)
                {
                    return B;
                }
                else if (index == 2)
                {
                    return C;
                }
                else
                {
                    throw new IndexOutOfRangeException();
                }
            }
        }

        //11
        public IEnumerator GetEnumerator()
        {
            return (IEnumerator)this;
        }

        public bool MoveNext()
        {
            Position++;
            return Position < 3;
        }

        public void Reset()
        {
            Position = 0;
        }

        public object Current
        {
            get { return this[Position]; }
        }
        //12
        public static Pudelko Parse(string a)
        {
            string[] temp = a.Split(' ');
            double bokA = double.Parse(temp[0], CultureInfo.InvariantCulture);
            double bokB = double.Parse(temp[3], CultureInfo.InvariantCulture);
            double bokC = double.Parse(temp[6], CultureInfo.InvariantCulture);
            if (temp[1] != temp[4] || temp[1] != temp[7]) throw new FormatException();

            switch (temp[1])
            {
                case "m":
                    return new Pudelko(bokA, bokB, bokC, UnitOfMeasure.meter);
                case "cm":
                    return new Pudelko(bokA, bokB, bokC, UnitOfMeasure.centimeter);
                case "mm":
                    return new Pudelko(bokA, bokB, bokC, UnitOfMeasure.milimeter);
                default:
                    throw new FormatException();
            }
        }
    }
}
