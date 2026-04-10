namespace Model
{
    /// <summary>
    /// Simple data transfer object representing a two-dimensional point
    /// with double precision coordinates used by the application for
    /// function sampling and reporting.
    /// </summary>
    public class Point
    {
        /// <summary>
        /// X-coordinate.
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// Y-coordinate.
        /// </summary>
        public double Y { get; set; }

        /// <summary>
        /// Parameterless constructor required for serializers.
        /// </summary>
        public Point() { }

        /// <summary>
        /// Create a point with specified coordinates.
        /// </summary>
        /// <param name="x">X-coordinate.</param>
        /// <param name="y">Y-coordinate.</param>
        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }
    }
}
