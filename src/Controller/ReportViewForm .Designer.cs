namespace Coursework
{
    partial class ReportViewForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            webViewReport = new Microsoft.Web.WebView2.WinForms.WebView2();
            textBoxReport = new RichTextBox();
            ((System.ComponentModel.ISupportInitialize)webViewReport).BeginInit();
            SuspendLayout();
            // 
            // webViewReport
            // 
            webViewReport.AllowExternalDrop = true;
            webViewReport.CreationProperties = null;
            webViewReport.DefaultBackgroundColor = Color.White;
            webViewReport.Location = new Point(-3, -4);
            webViewReport.Margin = new Padding(3, 4, 3, 4);
            webViewReport.Name = "webViewReport";
            webViewReport.Size = new Size(624, 623);
            webViewReport.TabIndex = 0;
            webViewReport.ZoomFactor = 1D;
            webViewReport.Click += webViewReport_Click;
            // 
            // textBoxReport
            // 
            textBoxReport.Location = new Point(-3, -4);
            textBoxReport.Margin = new Padding(3, 4, 3, 4);
            textBoxReport.Name = "textBoxReport";
            textBoxReport.Size = new Size(620, 623);
            textBoxReport.TabIndex = 1;
            textBoxReport.Text = "";
            textBoxReport.TextChanged += textBoxReport_TextChanged;
            // 
            // ReportViewForm
            // 
            AutoScaleDimensions = new SizeF(8F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(618, 618);
            Controls.Add(textBoxReport);
            Controls.Add(webViewReport);
            Font = new Font("Segoe Print", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            FormBorderStyle = FormBorderStyle.SizableToolWindow;
            Margin = new Padding(3, 4, 3, 4);
            Name = "ReportViewForm";
            Text = "ReportViewForm";
            Load += ReportViewForm_Load;
            ((System.ComponentModel.ISupportInitialize)webViewReport).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Microsoft.Web.WebView2.WinForms.WebView2 webViewReport;
        private RichTextBox textBoxReport;
    }
}