namespace Coursework
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dataGridViewF = new DataGridView();
            dataGridViewG = new DataGridView();
            panelGraph = new Panel();
            txtSearchMin = new TextBox();
            txtSearchMax = new TextBox();
            txtTolerance = new TextBox();
            txtResult = new TextBox();
            btnLoadF = new Button();
            btnLoadG = new Button();
            btnSaveG = new Button();
            btnSaveF = new Button();
            btnFindMaximum = new Button();
            btnGenerateReportHTML = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            showReport = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridViewF).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewG).BeginInit();
            SuspendLayout();
            // 
            // dataGridViewF
            // 
            dataGridViewF.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewF.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewF.Cursor = Cursors.IBeam;
            dataGridViewF.Location = new Point(23, 38);
            dataGridViewF.Margin = new Padding(3, 4, 3, 4);
            dataGridViewF.Name = "dataGridViewF";
            dataGridViewF.Size = new Size(208, 309);
            dataGridViewF.TabIndex = 0;
            dataGridViewF.CellContentClick += dataGridViewF_CellContentClick;
            // 
            // dataGridViewG
            // 
            dataGridViewG.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewG.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewG.Cursor = Cursors.IBeam;
            dataGridViewG.Location = new Point(24, 384);
            dataGridViewG.Margin = new Padding(3, 4, 3, 4);
            dataGridViewG.Name = "dataGridViewG";
            dataGridViewG.Size = new Size(207, 309);
            dataGridViewG.TabIndex = 1;
            dataGridViewG.CellContentClick += dataGridViewG_CellContentClick;
            // 
            // panelGraph
            // 
            panelGraph.AutoSize = true;
            panelGraph.Font = new Font("Segoe Print", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 204);
            panelGraph.Location = new Point(421, 38);
            panelGraph.Margin = new Padding(3, 4, 3, 4);
            panelGraph.Name = "panelGraph";
            panelGraph.Size = new Size(810, 655);
            panelGraph.TabIndex = 2;
            panelGraph.Paint += panel1_Paint;
            // 
            // txtSearchMin
            // 
            txtSearchMin.Cursor = Cursors.IBeam;
            txtSearchMin.Location = new Point(673, 722);
            txtSearchMin.Margin = new Padding(3, 4, 3, 4);
            txtSearchMin.Name = "txtSearchMin";
            txtSearchMin.Size = new Size(121, 29);
            txtSearchMin.TabIndex = 0;
            txtSearchMin.TextChanged += txtSearchMin_TextChanged;
            // 
            // txtSearchMax
            // 
            txtSearchMax.Cursor = Cursors.IBeam;
            txtSearchMax.Location = new Point(800, 722);
            txtSearchMax.Margin = new Padding(3, 4, 3, 4);
            txtSearchMax.Name = "txtSearchMax";
            txtSearchMax.Size = new Size(121, 29);
            txtSearchMax.TabIndex = 3;
            txtSearchMax.TextChanged += txtSearchMax_TextChanged;
            // 
            // txtTolerance
            // 
            txtTolerance.Cursor = Cursors.IBeam;
            txtTolerance.Location = new Point(927, 722);
            txtTolerance.Margin = new Padding(3, 4, 3, 4);
            txtTolerance.Name = "txtTolerance";
            txtTolerance.Size = new Size(121, 29);
            txtTolerance.TabIndex = 4;
            txtTolerance.TextChanged += txtTolerance_TextChanged;
            // 
            // txtResult
            // 
            txtResult.Font = new Font("Segoe Print", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            txtResult.Location = new Point(673, 780);
            txtResult.Margin = new Padding(3, 4, 3, 4);
            txtResult.Name = "txtResult";
            txtResult.ReadOnly = true;
            txtResult.Size = new Size(375, 29);
            txtResult.TabIndex = 5;
            txtResult.TextChanged += txtResult_TextChanged;
            // 
            // btnLoadF
            // 
            btnLoadF.Location = new Point(238, 38);
            btnLoadF.Margin = new Padding(3, 4, 3, 4);
            btnLoadF.Name = "btnLoadF";
            btnLoadF.Size = new Size(176, 32);
            btnLoadF.TabIndex = 6;
            btnLoadF.Text = "Load (f)";
            btnLoadF.UseVisualStyleBackColor = true;
            // 
            // btnLoadG
            // 
            btnLoadG.Location = new Point(238, 78);
            btnLoadG.Margin = new Padding(3, 4, 3, 4);
            btnLoadG.Name = "btnLoadG";
            btnLoadG.Size = new Size(176, 32);
            btnLoadG.TabIndex = 8;
            btnLoadG.Text = "Load (g)";
            btnLoadG.UseVisualStyleBackColor = true;
            // 
            // btnSaveG
            // 
            btnSaveG.Location = new Point(238, 159);
            btnSaveG.Margin = new Padding(3, 4, 3, 4);
            btnSaveG.Name = "btnSaveG";
            btnSaveG.Size = new Size(176, 32);
            btnSaveG.TabIndex = 9;
            btnSaveG.Text = "Save (g)";
            btnSaveG.UseVisualStyleBackColor = true;
            btnSaveG.Click += btnSaveG_Click_1;
            // 
            // btnSaveF
            // 
            btnSaveF.Location = new Point(238, 118);
            btnSaveF.Margin = new Padding(3, 4, 3, 4);
            btnSaveF.Name = "btnSaveF";
            btnSaveF.Size = new Size(176, 32);
            btnSaveF.TabIndex = 7;
            btnSaveF.Text = "Save (f)";
            btnSaveF.UseVisualStyleBackColor = true;
            // 
            // btnFindMaximum
            // 
            btnFindMaximum.Location = new Point(238, 200);
            btnFindMaximum.Margin = new Padding(3, 4, 3, 4);
            btnFindMaximum.Name = "btnFindMaximum";
            btnFindMaximum.Size = new Size(176, 32);
            btnFindMaximum.TabIndex = 10;
            btnFindMaximum.Text = "Find Maximum";
            btnFindMaximum.UseVisualStyleBackColor = true;
            // 
            // btnGenerateReportHTML
            // 
            btnGenerateReportHTML.Location = new Point(238, 242);
            btnGenerateReportHTML.Margin = new Padding(3, 4, 3, 4);
            btnGenerateReportHTML.Name = "btnGenerateReportHTML";
            btnGenerateReportHTML.Size = new Size(176, 32);
            btnGenerateReportHTML.TabIndex = 11;
            btnGenerateReportHTML.Text = "Save Report  ";
            btnGenerateReportHTML.UseVisualStyleBackColor = true;
            btnGenerateReportHTML.Click += btnGenerateReport_Click_1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(91, 12);
            label1.Name = "label1";
            label1.Size = new Size(65, 21);
            label1.TabIndex = 12;
            label1.Text = "Table f(x)";
            label1.Click += label1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(91, 358);
            label2.Name = "label2";
            label2.Size = new Size(68, 21);
            label2.TabIndex = 13;
            label2.Text = "Table g(x)";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(718, 697);
            label3.Name = "label3";
            label3.Size = new Size(32, 21);
            label3.TabIndex = 14;
            label3.Text = "Min";
            label3.Click += label3_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(842, 697);
            label4.Name = "label4";
            label4.Size = new Size(34, 21);
            label4.TabIndex = 15;
            label4.Text = "Max";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(955, 697);
            label5.Name = "label5";
            label5.Size = new Size(67, 21);
            label5.TabIndex = 16;
            label5.Text = "Tolerance";
            label5.Click += label5_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(842, 755);
            label6.Name = "label6";
            label6.Size = new Size(46, 21);
            label6.TabIndex = 19;
            label6.Text = "Result";
            label6.Click += label6_Click;
            // 
            // showReport
            // 
            showReport.Location = new Point(238, 282);
            showReport.Margin = new Padding(3, 4, 3, 4);
            showReport.Name = "showReport";
            showReport.Size = new Size(176, 32);
            showReport.TabIndex = 21;
            showReport.Text = "Save and check report";
            showReport.UseVisualStyleBackColor = true;
            showReport.Click += showReport_Click;
            // 
            // Form1
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(1256, 831);
            Controls.Add(showReport);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnGenerateReportHTML);
            Controls.Add(btnFindMaximum);
            Controls.Add(btnSaveG);
            Controls.Add(btnLoadG);
            Controls.Add(btnSaveF);
            Controls.Add(btnLoadF);
            Controls.Add(txtResult);
            Controls.Add(txtTolerance);
            Controls.Add(txtSearchMax);
            Controls.Add(txtSearchMin);
            Controls.Add(panelGraph);
            Controls.Add(dataGridViewG);
            Controls.Add(dataGridViewF);
            Font = new Font("Segoe Print", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            ForeColor = SystemColors.WindowText;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            HelpButton = true;
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Program";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridViewF).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewG).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridViewF;
        private DataGridView dataGridViewG;
        private Panel panelGraph;
        private TextBox txtSearchMin;
        private TextBox txtSearchMax;
        private TextBox txtTolerance;
        private TextBox txtResult;
        private Button btnLoadF;
        private Button btnLoadG;
        private Button btnSaveG;
        private Button btnSaveF;
        private Button btnFindMaximum;
        private Button btnGenerateReportHTML;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Button showReport;
    }
}
