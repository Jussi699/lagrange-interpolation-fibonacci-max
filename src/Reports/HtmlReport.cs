
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Reports
{
    public class HtmlReport : ReportBase
    {
        /// <summary>
        /// Returns a short user-facing description of the result after saving.
        /// </summary>
        public override string ToString()
        {
            return $"HTML-report was successfully saved!";
        }

        public override void GenerateReport(string filename, List<Model.Point> fPoints, List<Model.Point> gPoints, double maxX, double maxValue)
        {
            try
            {
                var html = $"<html><head><title>Report</title></head><body>" +
                    $"<h1>Maximum function h(x)</h1><p>Maximum: x = {maxX:F5}, h(x) = {maxValue:F5}</p>" +
                    "<h2>f(x):</h2><ul>" +
                    string.Join("", fPoints.ConvertAll(p => $"<li>x = {p.X}, y = {p.Y}</li>")) +
                    "</ul><h2>g(x):</h2><ul>" +
                    string.Join("", gPoints.ConvertAll(p => $"<li>x = {p.X}, y = {p.Y}</li>")) +
                    "</ul></body></html>";

                using (var writer = new StreamWriter(filename, false, Encoding.UTF8))
                {
                    writer.Write(html);
                }
            }
            catch (System.Exception ex)
            {
                throw new System.Exception($"Error generating HTML report:: {ex.Message}");
            }
        }
    }
}