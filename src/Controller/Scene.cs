using System.Globalization;
using Model;
using View;
using Reports;

namespace Coursework
{
    /// <summary>
    /// The main form of the program. Contains the logic for loading/saving data, finding the maximum, and generating reports..
    /// </summary>
    public partial class Form1 : Form
    {
        ButtonLoadAndSave buttonLaS = new ButtonLoadAndSave();
        private FibonacciMaximizerFacade facade = new FibonacciMaximizerFacade();
        private GraphPlotter graphPlotter;
        private Parsings parsings = new Parsings();
        private void Log(string message) => Console.WriteLine($"{DateTime.Now:HH:mm:ss} {message}");

        /// <summary>
        /// Function to initialize the necessary components and create contracts for the application to work.
        /// </summary>
        void InitializeData()
        {
            dataGridViewF.DataSource = facade.FunctionF.Points;
            dataGridViewG.DataSource = facade.FunctionG.Points;
            graphPlotter = new GraphPlotter(panelGraph);

            btnLoadF.Click += btnLoadF_Click;
            btnSaveF.Click += btnSaveF_Click;
            btnLoadG.Click += btnLoadG_Click;
            btnSaveG.Click += btnSaveG_Click;
            btnFindMaximum.Click += btnFindMaximum_Click;
            btnGenerateReportHTML.Click += btnGenerateReport_Click;

            dataGridViewF.CellParsing += parsings.dataGridView_CellParsing;
            dataGridViewG.CellParsing += parsings.dataGridView_CellParsing;
        }

        public Form1()
        {
            InitializeComponent();
            InitializeData();
        }

        /// <summary>
        /// Point load button handler for function F.
        /// Loads points from XML and updates the data source.
        /// </summary>
        private void btnLoadF_Click(object sender, EventArgs e)
        {
            buttonLaS.btnLoad(() => facade.FunctionF,  dataGridViewF, "F"); 
        }
        /// <summary>
        /// Point Load Button Handler for G Function.
        /// Loads points from XML and updates the data source.
        /// </summary>
        private void btnLoadG_Click(object sender, EventArgs e)
        {
            buttonLaS.btnLoad(() => facade.FunctionG, dataGridViewG, "G");
        }

        /// <summary>
        /// Saves the points of the function F to an XML file.
        /// </summary>
        private void btnSaveF_Click(object sender, EventArgs e)
        {
            buttonLaS.btnSave(() => facade.FunctionF, "F");
        }

        /// <summary>
        /// Saves the points of the G function to an XML file.
        /// </summary>
        private void btnSaveG_Click(object sender, EventArgs e)
        {
            buttonLaS.btnSave(() => facade.FunctionG, "G");
        }

        /// <summary>
        /// Attempt to parse a number and validate it in a separate method.
        /// </summary>
        private bool TryParseParameters(out double min, out double max, out double tol, out List<string> errors)
        {
            errors = new List<string>();
            min = max = tol = 0;

            bool parseSuccess = double.TryParse(txtSearchMin.Text.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out min) &&
                                double.TryParse(txtSearchMax.Text.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out max) &&
                                double.TryParse(txtTolerance.Text.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out tol);

            if (!parseSuccess)
                errors.Add($"{DateTime.Now:HH:mm:ss} Parsing failed for min/max/tolerance. Enter correct numbers.");
            else
            {
                if (double.IsNaN(min) || double.IsNaN(max) || double.IsNaN(tol) ||
                    double.IsInfinity(min) || double.IsInfinity(max) || double.IsInfinity(tol))
                    errors.Add($"{DateTime.Now:HH:mm:ss} Invalid values (NaN/Infinity).");

                if (tol <= 0)
                    errors.Add($"{DateTime.Now:HH:mm:ss} Accuracy (tolerance) must be greater than zero.");

                if (min >= max)
                    errors.Add($"{DateTime.Now:HH:mm:ss} The minimum must be less than the maximum.");

                if (Math.Abs(max - min) <= tol)
                    errors.Add($"{DateTime.Now:HH:mm:ss} The search interval is too small in relation to the specified accuracy.");
            }
            return errors.Count == 0;
        }

        /// <summary>
        /// Checking the presence and number of points in other methods.
        /// </summary>
        private List<string> ValidateDataPoints()
        {
            var errors = new List<string>();

            if (facade.FunctionF?.Points == null || facade.FunctionF.Points.Count == 0)
                errors.Add($"{DateTime.Now:HH:mm:ss} F(x) does not contain points. Load data for F.");

            if (facade.FunctionG?.Points == null || facade.FunctionG.Points.Count == 0)
                errors.Add($"{DateTime.Now:HH:mm:ss} G(x) does not contain points. Load data for G.");

            if ((facade.FunctionF?.Points?.Count ?? 0) < 2 || (facade.FunctionG?.Points?.Count ?? 0) < 2)
                errors.Add($"{DateTime.Now:HH:mm:ss} There must be at least 2 points in each function for correct operation.");

            return errors;
        }

        /// <summary>
        /// Handler for the Find Maximum button. Performs validation, invokes the
        /// facade to compute the maximum and draws the resulting graph.
        /// </summary>
        private void btnFindMaximum_Click(object sender, EventArgs e)
        {
            if (!TryParseParameters(out double min, out double max, out double tol, out var errors))
            {
                ShowValidationErrors(errors);
                return;
            }

            errors.AddRange(ValidateDataPoints());
            if (errors.Count > 0)
            {
                ShowValidationErrors(errors);
                return;
            }

            facade.SearchMin = min;
            facade.SearchMax = max;
            facade.Tolerance = tol;

            try
            {
                facade.FindMaximum();
                txtResult.Text = facade.LastResult;
                graphPlotter.DrawGraph(
                    facade.FunctionF.Points.ToList(),
                    facade.FunctionG.Points.ToList(),
                    facade.MaxX,
                    facade.MaxValue,
                    facade.SearchMin,
                    facade.SearchMax,
                    panelGraph);

                Log($"Maximum was found and graph drawn. | {facade.LastResult}");
            }
            catch (ArgumentException ex)
            {
                HandleException("Error in argument during calculation: " + ex.Message, ex);
            }
            catch (InvalidOperationException ex)
            {
                HandleException("Cannot search: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                HandleException("An error occurred while searching for the maximum: " + ex.Message, ex);
            }
        }

        /// <summary>
        /// Shows validation errors in a MessageBox and logs them to the console.
        /// </summary>
        /// <param name="errors">List of validation error messages.</param>
        private void ShowValidationErrors(List<string> errors)
        {
            var msg = string.Join(Environment.NewLine, errors);
            Log("Validation errors: " + msg);
            MessageBox.Show(msg, "Validation errors", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Handles exceptions by logging details to the console and showing a
        /// user-friendly message box.
        /// </summary>
        /// <param name="userMessage">Text shown to the user describing the error.</param>
        /// <param name="ex">Exception instance to be logged for diagnostics.</param>
        private void HandleException(string userMessage, Exception ex)
        {
            Console.WriteLine($"{DateTime.Now:HH:mm:ss} {ex}");
            MessageBox.Show(userMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Maps file extensions to factory functions that create report objects.
        /// Used to pick the appropriate report generator based on the selected
        /// file extension.
        /// </summary>
        private readonly Dictionary<string, Func<ReportBase>> reportFactories = new Dictionary<string, Func<ReportBase>>(StringComparer.OrdinalIgnoreCase)
        {
            { ".html", () => new HtmlReport() },
            { ".xml", () => new XmlReport() },
            { ".pdf", () => new PdfReport() }
        };

        /// <summary>
        /// Generates a report in the selected format (HTML, XML, PDF) and saves
        /// it to the chosen file.
        /// </summary>
        private void btnGenerateReport_Click(object sender, EventArgs e)
        {
            using var sfd = new SaveFileDialog()
            {
                Filter = "HTML files|*.html|XML files|*.xml|PDF files|*.pdf",
                Title = "Save report"
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var ext = Path.GetExtension(sfd.FileName).ToLower();

                    if (!reportFactories.TryGetValue(ext, out var reportFactory))
                        throw new Exception("Unsupported file format");

                    var report = reportFactory();

                    report.GenerateReport(
                        sfd.FileName,
                        facade.FunctionF.Points.ToList(),
                        facade.FunctionG.Points.ToList(),
                        facade.MaxX,
                        facade.MaxValue
                    );

                    Log($"{DateTime.Now:HH:mm:ss} " + report.ToString());
                    MessageBox.Show("The report was successfully saved!");
                }
                catch (Exception ex)
                {
                    Log($"{DateTime.Now:HH:mm:ss} Error in {ex}");
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        /// <summary>
        /// Generates a report and displays it (the file is saved first). HTML and
        /// XML reports are shown in an internal viewer; PDF reports are opened
        /// with the system viewer.
        /// </summary>
        private void showReport_Click(object sender, EventArgs e)
        {
            using var sfd = new SaveFileDialog()
            {
                Filter = "HTML files|*.html|XML files|*.xml|PDF files|*.pdf",
                Title = "Save report"
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var ext = Path.GetExtension(sfd.FileName).ToLower();

                    if (!reportFactories.TryGetValue(ext, out var reportFactory))
                        throw new Exception("Unsupported file format");

                    var report = reportFactory();

                    report.GenerateReport(
                        sfd.FileName,
                        facade.FunctionF.Points.ToList(),
                        facade.FunctionG.Points.ToList(),
                        facade.MaxX,
                        facade.MaxValue
                    );

                    if (ext == ".html" || ext == ".xml")
                    {
                        using var reportForm = new ReportViewForm();

                        if (ext == ".html")
                            reportForm.ShowHtmlReport(sfd.FileName);
                        else
                            reportForm.ShowXmlReport(sfd.FileName);

                        reportForm.ShowDialog();
                    }
                    else if (ext == ".pdf")
                    {
                        System.Diagnostics.Process.Start("explorer.exe", sfd.FileName);
                    }

                    Log($"{DateTime.Now:HH:mm:ss} " + report.ToString());
                }
                catch (Exception ex)
                {
                    Log($"{DateTime.Now:HH:mm:ss} Error in {ex}");
                    MessageBox.Show("Unexpected Error: " + ex.Message);
                }
            }
        }

        private void dataGridViewF_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridViewG_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }


        private void txtSearchMin_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSearchMax_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTolerance_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtResult_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnGenerateReport_Click_1(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void btnSaveG_Click_1(object sender, EventArgs e)
        {

        }


    }
}