namespace Model
{
    /// <summary>
    /// Implements the Fibonacci search algorithm to find the maximum of a unimodal
    /// univariate function on a closed interval. Also provides a helper to compute
    /// the maximum difference between two tabulated functions interpolated with
    /// Lagrange polynomials.
    /// </summary>
    internal static class FibonacciSearch
    {
        /// <summary>
        /// Finds an approximation of the x coordinate that maximizes the provided
        /// univariate function on the interval [a, b] using the Fibonacci search.
        /// </summary>
        /// <param name="func">Target single-variable function to maximize.</param>
        /// <param name="a">Left bound of the search interval.</param>
        /// <param name="b">Right bound of the search interval.</param>
        /// <param name="tol">Desired tolerance for the x coordinate.</param>
        /// <returns>Approximate x coordinate of the maximum on [a,b].</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="func"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown when interval or tolerance are invalid.</exception>
        /// </summary>
        public static double FindMaximum(Func<double, double> func, double a, double b, double tol = 1e-5)
        {
            if (func == null)
                throw new ArgumentNullException(nameof(func));

            if (tol <= 0)
                throw new ArgumentException("Точність повинна бути більшою за нуль.", nameof(tol));

            if (a >= b)
                throw new ArgumentException("Ліва межа відрізка повинна бути меншою за праву.");

            // Build Fibonacci sequence until F_n >= (b-a)/tol
            var fib = new List<long> { 1, 1 };
            while (fib[fib.Count - 1] < (b - a) / tol)
            {
                // prevent overflow by stopping if next value would overflow long
                long next = fib[fib.Count - 1] + fib[fib.Count - 2];
                if (next < 0)
                    break;
                fib.Add(next);
            }

            if (fib.Count < 3)
            {
                // Fallback: interval too small or tol too large; return midpoint
                return (a + b) / 2.0;
            }

            int n = fib.Count - 1; // index of last Fibonacci number in list

            double x1 = a + (double)fib[n - 2] / fib[n] * (b - a);
            double x2 = a + (double)fib[n - 1] / fib[n] * (b - a);
            double f1 = func(x1);
            double f2 = func(x2);

            for (int k = n; k > 1; k--)
            {
                // if current interval already within tolerance, stop early
                if (Math.Abs(b - a) <= tol)
                    break;

                if (f1 < f2)
                {
                    // maximum is in [x1, b]
                    a = x1;
                    x1 = x2;
                    f1 = f2;

                    x2 = a + (double)fib[k - 1] / fib[k] * (b - a);
                    f2 = func(x2);
                }
                else
                {
                    // maximum is in [a, x2]
                    b = x2;
                    x2 = x1;
                    f2 = f1;

                    x1 = a + (double)fib[k - 2] / fib[k] * (b - a);
                    f1 = func(x1);
                }
            }

            // Choose the best among a, b, x1, x2 and midpoint to improve robustness
            double mid = (a + b) / 2.0;
            double fa = func(a);
            double fb = func(b);
            double fmid = func(mid);

            double bestX = a;
            double bestF = fa;

            if (fb > bestF) { bestF = fb; bestX = b; }
            if (f1 > bestF) { bestF = f1; bestX = x1; }
            if (f2 > bestF) { bestF = f2; bestX = x2; }
            if (fmid > bestF) { bestF = fmid; bestX = mid; }

            return bestX;
        }

        /// <summary>
        /// Finds the maximum of the difference between two tabulated functions
        /// (f - g) on [a, b] by interpolating each function and applying the
        /// Fibonacci search on the interpolated difference.
        /// </summary>
        /// <param name="f">Function data for f(x).</param>
        /// <param name="g">Function data for g(x).</param>
        /// <param name="a">Left bound of the search interval.</param>
        /// <param name="b">Right bound of the search interval.</param>
        /// <param name="tol">Desired tolerance for x.</param>
        /// <returns>Tuple containing XMax and MaxValue for the maximum of f(x)-g(x).</returns>
        public static (double XMax, double MaxValue) FindMaximumDifference(
            FunctionDataBindingList f,
            FunctionDataBindingList g,
            double a,
            double b,
            double tol = 1e-5)
        {
            if (f == null)
                throw new ArgumentNullException(nameof(f));

            if (g == null)
                throw new ArgumentNullException(nameof(g));

            double xMax = FindMaximum(x => f.Interpolate(x) - g.Interpolate(x), a, b, tol);
            double maxValue = f.Interpolate(xMax) - g.Interpolate(xMax);

            return (xMax, maxValue);
        }
    }
}