namespace Proj2
{
    partial class stockDisplay
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
            this.components = new System.ComponentModel.Container();
            this.startDate = new System.Windows.Forms.Label();
            this.endDate = new System.Windows.Forms.Label();
            this.ticker = new System.Windows.Forms.Label();
            this.startDatePicker = new System.Windows.Forms.DateTimePicker();
            this.endDatePicker = new System.Windows.Forms.DateTimePicker();
            this.stockTicker = new System.Windows.Forms.ComboBox();
            this.period = new System.Windows.Forms.Label();
            this.dailyButton = new System.Windows.Forms.RadioButton();
            this.weeklyButton = new System.Windows.Forms.RadioButton();
            this.monthlyButton = new System.Windows.Forms.RadioButton();
            this.uploadButton = new System.Windows.Forms.Button();
            this.database1 = new Proj2.Database1();
            this.stockTableBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.stockTableTableAdapter = new Proj2.Database1TableAdapters.StockTableTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.database1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stockTableBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // startDate
            // 
            this.startDate.AutoSize = true;
            this.startDate.Location = new System.Drawing.Point(12, 25);
            this.startDate.Name = "startDate";
            this.startDate.Size = new System.Drawing.Size(55, 13);
            this.startDate.TabIndex = 0;
            this.startDate.Text = "Start Date";
            // 
            // endDate
            // 
            this.endDate.AutoSize = true;
            this.endDate.Location = new System.Drawing.Point(12, 62);
            this.endDate.Name = "endDate";
            this.endDate.Size = new System.Drawing.Size(52, 13);
            this.endDate.TabIndex = 1;
            this.endDate.Text = "End Date";
            // 
            // ticker
            // 
            this.ticker.AutoSize = true;
            this.ticker.Location = new System.Drawing.Point(14, 102);
            this.ticker.Name = "ticker";
            this.ticker.Size = new System.Drawing.Size(37, 13);
            this.ticker.TabIndex = 2;
            this.ticker.Text = "Ticker";
            // 
            // startDatePicker
            // 
            this.startDatePicker.Location = new System.Drawing.Point(74, 25);
            this.startDatePicker.Name = "startDatePicker";
            this.startDatePicker.Size = new System.Drawing.Size(200, 20);
            this.startDatePicker.TabIndex = 3;
            this.startDatePicker.Value = new System.DateTime(2022, 1, 3, 0, 0, 0, 0);
            // 
            // endDatePicker
            // 
            this.endDatePicker.Location = new System.Drawing.Point(74, 62);
            this.endDatePicker.Name = "endDatePicker";
            this.endDatePicker.Size = new System.Drawing.Size(200, 20);
            this.endDatePicker.TabIndex = 4;
            this.endDatePicker.Value = new System.DateTime(2022, 12, 2, 0, 0, 0, 0);
            // 
            // stockTicker
            // 
            this.stockTicker.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.stockTicker.FormattingEnabled = true;
            this.stockTicker.Location = new System.Drawing.Point(74, 99);
            this.stockTicker.Name = "stockTicker";
            this.stockTicker.Size = new System.Drawing.Size(121, 21);
            this.stockTicker.TabIndex = 5;
            // 
            // period
            // 
            this.period.AutoSize = true;
            this.period.Location = new System.Drawing.Point(12, 210);
            this.period.Name = "period";
            this.period.Size = new System.Drawing.Size(37, 13);
            this.period.TabIndex = 6;
            this.period.Text = "Period";
            // 
            // dailyButton
            // 
            this.dailyButton.AutoSize = true;
            this.dailyButton.Checked = true;
            this.dailyButton.Location = new System.Drawing.Point(15, 226);
            this.dailyButton.Name = "dailyButton";
            this.dailyButton.Size = new System.Drawing.Size(48, 17);
            this.dailyButton.TabIndex = 7;
            this.dailyButton.TabStop = true;
            this.dailyButton.Text = "Daily";
            this.dailyButton.UseVisualStyleBackColor = true;
            this.dailyButton.CheckedChanged += new System.EventHandler(this.dailyButton_CheckedChanged);
            // 
            // weeklyButton
            // 
            this.weeklyButton.AutoSize = true;
            this.weeklyButton.Location = new System.Drawing.Point(15, 249);
            this.weeklyButton.Name = "weeklyButton";
            this.weeklyButton.Size = new System.Drawing.Size(61, 17);
            this.weeklyButton.TabIndex = 8;
            this.weeklyButton.Text = "Weekly";
            this.weeklyButton.UseVisualStyleBackColor = true;
            this.weeklyButton.CheckedChanged += new System.EventHandler(this.weeklyButton_CheckedChanged);
            // 
            // monthlyButton
            // 
            this.monthlyButton.AutoSize = true;
            this.monthlyButton.Location = new System.Drawing.Point(15, 272);
            this.monthlyButton.Name = "monthlyButton";
            this.monthlyButton.Size = new System.Drawing.Size(62, 17);
            this.monthlyButton.TabIndex = 9;
            this.monthlyButton.Text = "Monthly";
            this.monthlyButton.UseVisualStyleBackColor = true;
            this.monthlyButton.CheckedChanged += new System.EventHandler(this.monthlyButton_CheckedChanged);
            // 
            // uploadButton
            // 
            this.uploadButton.Location = new System.Drawing.Point(106, 269);
            this.uploadButton.Name = "uploadButton";
            this.uploadButton.Size = new System.Drawing.Size(75, 23);
            this.uploadButton.TabIndex = 10;
            this.uploadButton.Text = "Upload";
            this.uploadButton.UseVisualStyleBackColor = true;
            this.uploadButton.Click += new System.EventHandler(this.uploadButton_Click);
            // 
            // database1
            // 
            this.database1.DataSetName = "Database1";
            this.database1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // stockTableBindingSource
            // 
            this.stockTableBindingSource.DataMember = "StockTable";
            this.stockTableBindingSource.DataSource = this.database1;
            // 
            // stockTableTableAdapter
            // 
            this.stockTableTableAdapter.ClearBeforeFill = true;
            // 
            // stockDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(302, 341);
            this.Controls.Add(this.uploadButton);
            this.Controls.Add(this.monthlyButton);
            this.Controls.Add(this.weeklyButton);
            this.Controls.Add(this.dailyButton);
            this.Controls.Add(this.period);
            this.Controls.Add(this.stockTicker);
            this.Controls.Add(this.endDatePicker);
            this.Controls.Add(this.startDatePicker);
            this.Controls.Add(this.ticker);
            this.Controls.Add(this.endDate);
            this.Controls.Add(this.startDate);
            this.Name = "stockDisplay";
            this.Text = "stockDisplay";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.database1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stockTableBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label startDate;
        private System.Windows.Forms.Label endDate;
        private System.Windows.Forms.Label ticker;
        private System.Windows.Forms.DateTimePicker startDatePicker;
        private System.Windows.Forms.DateTimePicker endDatePicker;
        private System.Windows.Forms.ComboBox stockTicker;
        private System.Windows.Forms.Label period;
        private System.Windows.Forms.RadioButton dailyButton;
        private System.Windows.Forms.RadioButton weeklyButton;
        private System.Windows.Forms.RadioButton monthlyButton;
        private System.Windows.Forms.Button uploadButton;
        private Database1 database1;
        private System.Windows.Forms.BindingSource stockTableBindingSource;
        private Database1TableAdapters.StockTableTableAdapter stockTableTableAdapter;
    }
}

