using System;
using System.ComponentModel;
using System.Linq;

namespace Model
{
    /// <summary>
    /// Stores a binding list of points representing a tabulated function and
    /// exposes Lagrange interpolation to evaluate the function at arbitrary x.
    /// </summary>
    public class FunctionDataBindingList
    {
        /// <summary>
        /// Points representing the tabulated function. This collection is exposed
        /// as a BindingList so it can be used as a data source for UI controls.
        /// </summary>
        public BindingList<Point> Points { get; } = new BindingList<Point>();

        /// <summary>
        /// Adds a new point to the collection. Throws ArgumentException if a point
        /// with the same X coordinate already exists.
        /// </summary>
        /// <param name="x">X coordinate of the point.</param>
        /// <param name="y">Y coordinate of the point.</param>
        public void AddPoint(double x, double y)
        {
            if (Points.Any(p => p.X == x))
                throw new ArgumentException("Точки з однаковими значеннями X не допускаються.", nameof(x));

            Points.Add(new Point(x, y));
        }

        /// <summary>
        /// Evaluates the function at the given x using Lagrange polynomial interpolation
        /// based on the stored points.
        /// </summary>
        /// <param name="x">X coordinate at which to evaluate the interpolated function.</param>
        /// <returns>Interpolated function value at x.</returns>
        public double Interpolate(double x)
        {
            if (Points.Count == 0)
                throw new InvalidOperationException("Неможливо виконати інтерполяцію: список точок порожній.");

            if (Points.Count == 1)
                return Points[0].Y;

            double result = 0.0;
            int n = Points.Count;

            for (int i = 0; i < n; i++)
            {
                double term = Points[i].Y;

                for (int j = 0; j < n; j++)
                {
                    if (j == i)
                        continue;

                    double denominator = Points[i].X - Points[j].X;
                    if (denominator == 0)
                        throw new InvalidOperationException("Неможливо побудувати поліном Лагранжа: виявлено однакові вузли X.");

                    term *= (x - Points[j].X) / denominator;
                }

                result += term;
            }

            return result;
        }

        /// <summary>
        /// Returns a delegate that evaluates the interpolated function. Useful for
        /// passing the function to optimization/search algorithms.
        /// </summary>
        /// <returns>Delegate that evaluates the interpolated function.</returns>
        public Func<double, double> AsInterpolatedFunction()
        {
            return Interpolate;
        }
    }
}