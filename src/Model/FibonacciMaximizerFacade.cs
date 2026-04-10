namespace Model
{
    /// <summary>
    /// A facade for finding the maximum difference between two interpolated functions
    /// using the Fibonacci search method. This class holds function data and exposes
    /// properties that control the search range and tolerance.
    /// </summary>
    public class FibonacciMaximizerFacade
    {
        /// <summary>
        /// A collection of points and an interpolated function for a function F.
        /// </summary>
        public FunctionDataBindingList FunctionF { get; } = new FunctionDataBindingList();

        /// <summary>
        /// A collection of points and an interpolated function for a function G.
        /// </summary>
        public FunctionDataBindingList FunctionG { get; } = new FunctionDataBindingList();

        /// <summary>
        /// Left boundary of the search interval.
        /// </summary>
        public double SearchMin { get; set; }

        /// <summary>
        /// Right boundary of the search interval.
        /// </summary>
        public double SearchMax { get; set; }

        /// <summary>
        /// Tolerance for stopping the search algorithm.
        /// </summary>
        public double Tolerance { get; set; }

        /// <summary>
        /// X-coordinate of the found maximum difference.
        /// </summary>
        public double MaxX { get; private set; }

        /// <summary>
        /// Function value h(x) = F(x) - G(x) at the maximum point.
        /// </summary>
        public double MaxValue { get; private set; }

        /// <summary>
        /// Text representation of the last search result.
        /// </summary>
        public string LastResult { get; private set; }

        private double Difference(double x) => FunctionF.Interpolate(x) - FunctionG.Interpolate(x);

        /// <summary>
        /// Find the maximum difference between functions on a given interval.
        /// </summary>
        public void FindMaximum()
        {
            MaxX = FibonacciSearch.FindMaximum(Difference, SearchMin, SearchMax, Tolerance);
            MaxValue = Difference(MaxX);
            LastResult = $"Maximum reached at x = {MaxX:F5}, h(x) = {MaxValue:F5}";
        }
    }
}
