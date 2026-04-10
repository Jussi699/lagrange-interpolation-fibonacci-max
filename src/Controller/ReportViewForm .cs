namespace Coursework
{
    /// <summary>
    /// Form used to preview generated reports. Supports showing HTML in a web view,
    /// XML as raw text and opening PDF files with the default system viewer.
    /// </summary>
    public partial class ReportViewForm : Form
    {
        /// <summary>
        /// Initializes form components.
        /// </summary>
        public ReportViewForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Displays an HTML report file in the embedded web view control.
        /// </summary>
        /// <param name="filePath">Path to the HTML file to show.</param>
        public void ShowHtmlReport(string filePath)
        {
            webViewReport.Source = new Uri("file:///" + filePath);
            webViewReport.Visible = true;
            textBoxReport.Visible = false;
        }

        /// <summary>
        /// Reads an XML file and shows its contents in a multi-line text box. Uses
        /// retries to tolerate short-term file locks by other processes.
        /// </summary>
        /// <param name="filePath">Path to the XML file to show.</param>
        public void ShowXmlReport(string filePath)
        {
            const int maxRetries = 8;
            const int delayMs = 200;
            for (int attempt = 0; attempt < maxRetries; attempt++)
            {
                try
                {
                    using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    using (var sr = new StreamReader(fs))
                    {
                        textBoxReport.Text = sr.ReadToEnd();
                    }

                    textBoxReport.Visible = true;
                    webViewReport.Visible = false;
                    return;
                }
                catch (FileNotFoundException)
                {
                    MessageBox.Show("File not found: " + filePath);
                    return;
                }
                catch (UnauthorizedAccessException)
                {
                    MessageBox.Show("No access to file: " + filePath);
                    return;
                }
                catch (IOException)
                {
                    if (attempt == maxRetries - 1)
                    {
                        MessageBox.Show("The file could not be read - it is being used by another program: " + filePath);
                        return;
                    }
                    Thread.Sleep(delayMs);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error reading file: " + ex.Message);
                    return;
                }
            }
        }

        /// <summary>
        /// Opens a PDF report using the system default program associated with PDFs.
        /// </summary>
        /// <param name="filePath">Path to the PDF file to open.</param>
        public void ShowPdfReport(string filePath)
        {
            System.Diagnostics.Process.Start("explorer.exe", filePath);
        }

        private void ReportViewForm_Load(object sender, EventArgs e)
        {

        }

        private void textBoxReport_TextChanged(object sender, EventArgs e)
        {

        }

        private void webViewReport_Click(object sender, EventArgs e)
        {

        }
    }
}
