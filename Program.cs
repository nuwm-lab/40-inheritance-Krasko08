using System;

namespace LabWork
{
    /// <summary>
    /// Базовий клас кривої другого порядку.
    /// </summary>
    public abstract class ConicBase
    {
        protected const double Tolerance = 1e-9;

        /// <summary>
        /// Обчислення значення рівняння F(x,y).
        /// </summary>
        public abstract double Evaluate(double x, double y);

        /// <summary>
        /// Перевірка належності точки рівнянню F(x,y)=0 з допуском.
        /// </summary>
        public virtual bool Contains(double x, double y)
        {
            return Math.Abs(Evaluate(x, y)) <= Tolerance;
        }
    }

    /// <summary>
    /// Загальна крива другого порядку:
    /// a11*x^2 + a12*x*y + a22*y^2 + b1*x + b2*y + c = 0
    /// </summary>
    public class Conic : ConicBase
    {
        private double _a11, _a12, _a22, _b1, _b2, _c;

        public double A11 { get => _a11; set => _a11 = value; }
        public double A12 { get => _a12; set => _a12 = value; }
        public double A22 { get => _a22; set => _a22 = value; }
        public double B1  { get => _b1;  set => _b1 = value; }
        public double B2  { get => _b2;  set => _b2 = value; }
        public double C   { get => _c;   set => _c = value; }

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
            return A11 * x * x + A12 * x * y + A22 * y * y + B1 * x + B2 * y + C;
        }

        public void Print()
        {
            Console.WriteLine($"Conic: {A11}x² + {A12}xy + {A22}y² + {B1}x + {B2}y + {C} = 0");
        }
    }

    /// <summary>
    /// Еліпс: x²/a² + y²/b² = 1
    /// </summary>
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

        public Ellipse(double a, double b)
        {
            A = a;
            B = b;
        }

        public void SetCoefficients(double a, double b)
        {
            A = a;
            B = b;
        }

        public override double Evaluate(double x, double y)
        {
            return (x * x) / (A * A) + (y * y) / (B * B) - 1;
        }

        public void Print()
        {
            Console.WriteLine($"Ellipse: a={A}, b={B}");
            Console.WriteLine($"Equation: x²/{A * A} + y²/{B * B} = 1");
        }
    }

    /// <summary>
    /// Головна програма для тестування класів.
    /// </summary>
    class Program
    {
        static void Main()
        {
            // ✅ Еліпс
            Ellipse e = new Ellipse(5, 3);
            e.Print();

            Console.WriteLine("Point (3,1) belongs: " + e.Contains(3, 1));
            Console.WriteLine("Point (6,0) belongs: " + e.Contains(6, 0));

            Console.WriteLine();

            // ✅ Загальна коніка
            Conic c = new Conic(1, 0, 1, -4, 0, -5);
            c.Print();
            Console.WriteLine("Point (2,1) belongs: " + c.Contains(2, 1));
        }
    }
}
