namespace Proj2
{
    partial class chartDisplay
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.stockChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.patternBox = new System.Windows.Forms.ComboBox();
            this.patternLabel = new System.Windows.Forms.Label();
            this.clearAnnotations = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.stockChart)).BeginInit();
            this.SuspendLayout();
            // 
            // stockChart
            // 
            chartArea1.Name = "ChartArea1";
            this.stockChart.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.stockChart.Legends.Add(legend1);
            this.stockChart.Location = new System.Drawing.Point(14, 4);
            this.stockChart.Name = "stockChart";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Candlestick;
            series1.Legend = "Legend1";
            series1.Name = "Stock";
            series1.YValuesPerPoint = 4;
            this.stockChart.Series.Add(series1);
            this.stockChart.Size = new System.Drawing.Size(1311, 579);
            this.stockChart.TabIndex = 15;
            this.stockChart.Text = "StockChart";
            this.stockChart.Visible = false;
            // 
            // patternBox
            // 
            this.patternBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.patternBox.FormattingEnabled = true;
            this.patternBox.Location = new System.Drawing.Point(109, 589);
            this.patternBox.Name = "patternBox";
            this.patternBox.Size = new System.Drawing.Size(121, 21);
            this.patternBox.TabIndex = 16;
            this.patternBox.SelectedIndexChanged += new System.EventHandler(this.patternBox_SelectedIndexChanged);
            // 
            // patternLabel
            // 
            this.patternLabel.AutoSize = true;
            this.patternLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.patternLabel.Location = new System.Drawing.Point(45, 592);
            this.patternLabel.Name = "patternLabel";
            this.patternLabel.Size = new System.Drawing.Size(48, 13);
            this.patternLabel.TabIndex = 17;
            this.patternLabel.Text = "Pattern";
            // 
            // clearAnnotations
            // 
            this.clearAnnotations.Location = new System.Drawing.Point(264, 587);
            this.clearAnnotations.Name = "clearAnnotations";
            this.clearAnnotations.Size = new System.Drawing.Size(111, 23);
            this.clearAnnotations.TabIndex = 18;
            this.clearAnnotations.Text = "Clear Annotations";
            this.clearAnnotations.UseVisualStyleBackColor = true;
            this.clearAnnotations.Click += new System.EventHandler(this.clearAnnotations_Click);
            // 
            // chartDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1375, 670);
            this.Controls.Add(this.clearAnnotations);
            this.Controls.Add(this.patternLabel);
            this.Controls.Add(this.patternBox);
            this.Controls.Add(this.stockChart);
            this.Name = "chartDisplay";
            this.Text = "chartDisplay";
            this.Load += new System.EventHandler(this.chartDisplay_Load);
            ((System.ComponentModel.ISupportInitialize)(this.stockChart)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart stockChart;
        private System.Windows.Forms.ComboBox patternBox;
        private System.Windows.Forms.Label patternLabel;
        private System.Windows.Forms.Button clearAnnotations;
    }
}