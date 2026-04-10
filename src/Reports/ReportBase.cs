namespace Reports
{
    /// <summary>
    /// Base class for report generators. Implementations produce a report file
    /// at the provided filename using lists of <c>Model.Point</c> for the two functions
    /// and the location and value of the maximum.
    /// </summary>
    public abstract class ReportBase
    {
        /// <summary>
        /// Generate a report and save it to <paramref name="filename"/>.
        /// </summary>
        /// <param name="filename">Output file path where the report will be written.</param>
        /// <param name="fPoints">List of points for function F.</param>
        /// <param name="gPoints">List of points for function G.</param>
        /// <param name="maxX">X coordinate of the found maximum.</param>
        /// <param name="maxValue">Value of the maximum function h(x) = F(x) - G(x).</param>
        public abstract void GenerateReport(string filename, List<Model.Point> fPoints, List<Model.Point> gPoints, double maxX, double maxValue);
    }
}
