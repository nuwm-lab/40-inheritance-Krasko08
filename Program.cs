using System;

namespace LabWork
{
    /// <summary>
    /// Загальний трикутник. Базовий клас.
    /// Визначається однією стороною та двома кутами при ній.
    /// </summary>
    public class Triangle
    {
        protected double Side;    // відома сторона
        protected double Angle1;  // перший прилеглий кут
        protected double Angle2;  // другий прилеглий кут
        protected double Angle3;  // третій кут (180 - Angle1 - Angle2)

        /// <summary>
        /// Встановлення сторони та двох прилеглих кутів.
        /// </summary>
        public virtual void SetValues(double side, double angle1, double angle2)
        {
            if (side <= 0)
                throw new ArgumentException("Довжина сторони повинна бути додатною.");

            if (angle1 <= 0 || angle2 <= 0 || angle1 + angle2 >= 180)
                throw new ArgumentException("Сума двох кутів має бути меншою за 180°.");

            Side = side;
            Angle1 = angle1;
            Angle2 = angle2;
            Angle3 = 180 - angle1 - angle2;
        }

        /// <summary>
        /// Перевантажений метод: встановлення зі зчитуванням з консолі.
        /// </summary>
        public virtual void SetValues()
        {
            Console.WriteLine("Enter side and two angles (A and B):");

            if (!double.TryParse(Console.ReadLine(), out double s) ||
                !double.TryParse(Console.ReadLine(), out double a1) ||
                !double.TryParse(Console.ReadLine(), out double a2))
            {
                throw new ArgumentException("Некоректні введені дані.");
            }

            SetValues(s, a1, a2);
        }

        /// <summary>
        /// Обчислення трьох сторін за теоремою синусів.
        /// </summary>
        public virtual double[] GetSides()
        {
            double a = Side;
            double b = a * Math.Sin(DegToRad(Angle2)) / Math.Sin(DegToRad(Angle1));
            double c = a * Math.Sin(DegToRad(Angle3)) / Math.Sin(DegToRad(Angle1));

            return new double[] { a, b, c };
        }

        /// <summary>
        /// Периметр трикутника.
        /// </summary>
        public virtual double GetPerimeter()
        {
            double[] s = GetSides();
            return s[0] + s[1] + s[2];
        }

        protected double DegToRad(double d) => d * Math.PI / 180.0;

        public override string ToString()
        {
            double[] s = GetSides();

            return
                "=== Звичайний трикутник ===\n" +
                $"Сторона a = {Side}\n" +
                $"Кути: A={Angle1}°, B={Angle2}°, C={Angle3}°\n" +
                $"Сторони: a={s[0]:F2}, b={s[1]:F2}, c={s[2]:F2}\n" +
                $"Периметр = {GetPerimeter():F2}";
        }
    }

    /// <summary>
    /// Рівносторонній трикутник. Похідний клас.
    /// Містить лише сторону, кути рівні 60°.
    /// </summary>
    public class EquilateralTriangle : Triangle
    {
        private static readonly double FixedAngle = 60;

        /// <summary>
        /// Встановлення сторони рівностороннього трикутника.
        /// </summary>
        public void SetValues(double side)
        {
            if (side <= 0)
                throw new ArgumentException("Довжина сторони повинна бути додатною.");

            Side = side;
            Angle1 = Angle2 = Angle3 = FixedAngle;
        }

        /// <summary>
        /// Перевантажений варіант без параметрів.
        /// </summary>
        public void SetValues()
        {
            Console.WriteLine("Enter side of equilateral triangle:");

            if (!double.TryParse(Console.ReadLine(), out double s))
                throw new ArgumentException("Некоректне значення сторони.");

            SetValues(s);
        }

        /// <summary>
        /// У рівностороннього трикутника всі сторони однакові.
        /// </summary>
        public override double[] GetSides()
        {
