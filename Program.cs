using System;

namespace LabWork
{
    // ============================
    // БАЗОВИЙ КЛАС: ConicBase (загальна крива 2-го порядку)
    // ============================
    public abstract class ConicBase
    {
        private const double _tolerance = 1e-9;

        public abstract double Evaluate(double x, double y);

        public virtual bool Contains(double x, double y)
        {
            return Math.Abs(Evaluate(x, y)) <= _tolerance;
        }
    }

    // ============================
    // КЛАС: Загальна крива другого порядку (Ax^2 + Bxy + Cy^2 + Dx + Ey + F)
    // ============================
    public class Conic : ConicBase
    {
        private double _a11, _a12, _a22, _b1, _b2, _c;

        public double A11 { get => _a11; set => _a11 = value; }
        public double A12 { get => _a12; set => _a12 = value; }
        public double A22 { get => _a22; set => _a22 = value; }
        public double B1  { get => _b1;  set => _b1  = value; }
        public double B2  { get => _b2;  set => _b2  = value; }
        public double C   { get => _c;   set => _c   = value; }

        public Conic() { }

        public Conic(double a11, double a12, double a22, double b1, double b2, double c)
        {
            _a11 = a11;
            _a12 = a12;
            _a22 = a22;
            _b1  = b1;
            _b2  = b2;
            _c   = c;
        }

        public override double Evaluate(double x, double y)
        {
            return _a11 * x * x + _a12 * x * y + _a22 * y * y + _b1 * x + _b2 * y + _c;
        }

        public void Print()
        {
            Console.WriteLine("=== Загальна крива другого порядку ===");
            Console.WriteLine($"{A11}*x^2 + {A12}xy + {A22}*y^2 + {B1}x + {B2}y + {C} = 0");
        }
    }

    // ============================
    // КЛАС: Еліпс (похідний від ConicBase)
    // Рівняння: x^2 / a^2 + y^2 / b^2 = 1
    // ============================
    public class Ellipse : ConicBase
    {
        private double _a;
        private double _b;

        public double A
        {
            get => _a;
            set
            {
                if (value <= 0) throw new ArgumentException("Піввісь a повинна бути додатною.");
                _a = value;
            }
        }

        public double B
        {
            get => _b;
            set
            {
                if (value <= 0) throw new ArgumentException("Піввісь b повинна бути додатною.");
                _b = value;
            }
        }

        public Ellipse() { }

        public Ellipse(double a, double b)
        {
            SetCoefficients(a, b);
        }

        public void SetCoefficients(double a, double b)
        {
            if (a <= 0 || b <= 0)
                throw new ArgumentException("Півосі еліпса повинні бути додатними.");

            _a = a;
            _b = b;
        }

        public override double Evaluate(double x, double y)
        {
            return (x * x) / (_a * _a) + (y * y) / (_b * _b) - 1;
        }

        public void Print()
        {
            Console.WriteLine("=== Еліпс ===");
            Console.WriteLine($"a = {_a}, b = {_b}");
            Console.WriteLine($"Рівняння: x^2/{_a * _a} + y^2/{_b * _b} = 1");
        }
    }

    // ============================
    // ГОЛОВНА ПРОГРАМА
    // ============================
    class Program
    {
        static void Main()
        {
            Console.WriteLine("=== Перевірка еліпса ===");
            Ellipse ellipse = new Ellipse(5, 3);
            ellipse.Print();
            Console.WriteLine("Contains(3,2): " + ellipse.Contains(3, 2));

            Console.WriteLine();
            Console.WriteLine("=== Перевірка коніки ===");
            Conic conic = new Conic(1, 0, 1, -4, 0, -5);
            conic.Print();
            Console.WriteLine("Contains(2,1): " + conic.Contains(2, 1));
        }
    }
}
