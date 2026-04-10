using Aspose.Pdf;
using Aspose.Pdf.Text;

namespace Reports
{
    public class PdfReport : ReportBase
    {
        /// <summary>
        /// Returns a short user-facing description of the result after saving.
        /// </summary>
        public override string ToString()
        {
            return $"PDF-report was successfully saved!";
        }

        /// <summary>
        /// Generates a PDF report containing the supplied points and maximum info
        /// and writes it to the specified filename.
        /// </summary>
        /// <param name="filename">Target PDF file path.</param>
        /// <param name="fPoints">List of points for function F.</param>
        /// <param name="gPoints">List of points for function G.</param>
        /// <param name="maxX">X coordinate of the computed maximum.</param>
        /// <param name="maxValue">Value of the maximum function h(x)=F(x)-G(x).</param>
        public override void GenerateReport(string filename, List<Model.Point> fPoints,
            List<Model.Point> gPoints, double maxX, double maxValue)
        {
            try
            {
                    var doc = new Document();
                    var page = doc.Pages.Add();

                    page.Paragraphs.Add(new TextFragment("Report by functions")
                    {
                        TextState = { FontSize = 14, FontStyle = FontStyles.Bold }
                    });

                    page.Paragraphs.Add(new TextFragment($"Maximum H(x) = F(x) - G(x): {maxValue:F3} at x = {maxX:F3}")
                    {
                        Margin = new MarginInfo { Top = 10, Bottom = 10 }
                    });

                    var tableF = new Aspose.Pdf.Table { ColumnWidths = "80 80" };
                    tableF.DefaultCellPadding = new MarginInfo { Left = 5, Right = 5, Top = 2, Bottom = 2 };
                    tableF.Border = new BorderInfo(BorderSide.All, .5f, Aspose.Pdf.Color.Gray);

                    var headerRowF = tableF.Rows.Add();
                    var headerCellF1 = headerRowF.Cells.Add();
                    var fragF1 = new TextFragment("X F(x)");
                    fragF1.TextState.FontStyle = FontStyles.Bold;
                    headerCellF1.Paragraphs.Add(fragF1);
                    var headerCellF2 = headerRowF.Cells.Add();
                    var fragF2 = new TextFragment("Y F(x)");
                    fragF2.TextState.FontStyle = FontStyles.Bold;
                    headerCellF2.Paragraphs.Add(fragF2);

                    foreach (var p in fPoints)
                    {
                        var row = tableF.Rows.Add();
                        row.Cells.Add(p.X.ToString("F3"));
                        row.Cells.Add(p.Y.ToString("F3"));
                    }
                    page.Paragraphs.Add(new TextFragment("Points F(x):") { Margin = new MarginInfo { Top = 10 } });
                    page.Paragraphs.Add(tableF);

                    var tableG = new Aspose.Pdf.Table { ColumnWidths = "80 80" };
                    tableG.DefaultCellPadding = new MarginInfo { Left = 5, Right = 5, Top = 2, Bottom = 2 };
                    tableG.Border = new BorderInfo(BorderSide.All, .5f, Aspose.Pdf.Color.Gray);

                    var headerRowG = tableG.Rows.Add();
                    var headerCellG1 = headerRowG.Cells.Add();
                    var fragG1 = new TextFragment("X G(x)");
                    fragG1.TextState.FontStyle = FontStyles.Bold;
                    headerCellG1.Paragraphs.Add(fragG1);
                    var headerCellG2 = headerRowG.Cells.Add();
                    var fragG2 = new TextFragment("Y G(x)");
                    fragG2.TextState.FontStyle = FontStyles.Bold;
                    headerCellG2.Paragraphs.Add(fragG2);

                    foreach (var p in gPoints)
                    {
                        var row = tableG.Rows.Add();
                        row.Cells.Add(p.X.ToString("F3"));
                        row.Cells.Add(p.Y.ToString("F3"));
                    }
                    page.Paragraphs.Add(new TextFragment("Points G(x):") { Margin = new MarginInfo { Top = 10 } });
                    page.Paragraphs.Add(tableG);

                    doc.Save(filename);
                }
                catch (System.Exception ex)
                {
                    throw new System.Exception($"Error generating PDF report: {ex.Message}");
                }
        }
    }
}
