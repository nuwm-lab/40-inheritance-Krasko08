using System;

namespace LabWork
{
    // ============================
    // КЛАС: Рівносторонній трикутник
    // ============================
    public class EquilateralTriangle
    {
        protected double Side;     // довжина сторони
        protected double Angle;    // кут (60°)

        // Введення значення сторони трикутника
        public virtual void SetValues(double side)
        {
            if (side <= 0)
            {
                throw new ArgumentException("Довжина сторони повинна бути додатною.");
            }

            Side = side;
            Angle = 60; // у рівносторонньому всі кути 60°
        }

        // Довжини сторін (усі однакові)
        public virtual double[] GetSides()
        {
            return new double[] { Side, Side, Side };
        }

        // Периметр
        public virtual double GetPerimeter()
        {
            return 3 * Side;
        }

        // Друк інформації
        public virtual void PrintInfo()
        {
            Console.WriteLine("=== Рівносторонній трикутник ===");
            Console.WriteLine($"Сторона: {Side}");
            Console.WriteLine($"Кути: 60°, 60°, 60°");
            Console.WriteLine($"Периметр: {GetPerimeter()}");
        }
    }

    // ============================
    // КЛАС: Загальний трикутник (похідний)
    // ============================
    public class Triangle : EquilateralTriangle
    {
        private double Angle2;
        private double Angle3;

        // Введення довжини сторони та двох прилеглих кутів
        public void SetValues(double side, double angleA, double angleB)
        {
            if (side <= 0)
                throw new ArgumentException("Довжина сторони повинна бути додатною.");

            if (angleA <= 0 || angleB <= 0 || angleA + angleB >= 180)
                throw new ArgumentException("Кути введено некоректно.");

            Side = side;
            Angle = angleA;
            Angle2 = angleB;
            Angle3 = 180 - angleA - angleB;
        }

        // Обчислення довжин інших сторін за теоремою синусів
        public override double[] GetSides()
        {
            double a = Side; // відома сторона
            double b = a * Math.Sin(DegToRad(Angle2)) / Math.Sin(DegToRad(Angle));
            double c = a * Math.Sin(DegToRad(Angle3)) / Math.Sin(DegToRad(Angle));

            return new double[] { a, b, c };
        }

        // Периметр
        public override double GetPerimeter()
        {
            double[] s = GetSides();
            return s[0] + s[1] + s[2];
        }

        private double DegToRad(double deg)
        {
            return deg * Math.PI / 180.0;
        }

        public override void PrintInfo()
        {
            Console.WriteLine("=== Звичайний трикутник ===");
            Console.WriteLine($"Сторона: {Side}");
            Console.WriteLine($"Кути: {Angle}°, {Angle2}°, {Angle3}°");

            double[] sides = GetSides();
            Console.WriteLine($"Сторони: {sides[0]:F2}, {sides[1]:F2}, {sides[2]:F2}");
            Console.WriteLine($"Периметр: {GetPerimeter():F2}");
        }
    }

    // ============================
    // ГОЛОВНА ПРОГРАМА
    // ============================
    class Program
    {
        static void Main()
        {
            // Рівносторонній трикутник
            EquilateralTriangle eq = new EquilateralTriangle();
            eq.SetValues(5);           // довжина сторони = 5
            eq.PrintInfo();

            // Звичайний трикутник
            Triangle tr = new Triangle();
            tr.SetValues(6, 40, 60);   // сторона = 6, прилеглі кути 40° та 60°
            tr.PrintInfo();
        }
    }
}
