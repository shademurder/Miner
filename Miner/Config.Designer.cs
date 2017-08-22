namespace Miner
{
    partial class Config
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
            this.Beginner = new System.Windows.Forms.RadioButton();
            this.Amateur = new System.Windows.Forms.RadioButton();
            this.Professional = new System.Windows.Forms.RadioButton();
            this.Special = new System.Windows.Forms.RadioButton();
            this.heightText = new System.Windows.Forms.Label();
            this.widthText = new System.Windows.Forms.Label();
            this.minesText = new System.Windows.Forms.Label();
            this.heightValue = new System.Windows.Forms.NumericUpDown();
            this.widthValue = new System.Windows.Forms.NumericUpDown();
            this.minesValue = new System.Windows.Forms.NumericUpDown();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.heightValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.widthValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minesValue)).BeginInit();
            this.SuspendLayout();
            // 
            // Beginner
            // 
            this.Beginner.AutoSize = true;
            this.Beginner.Checked = true;
            this.Beginner.Location = new System.Drawing.Point(12, 12);
            this.Beginner.Name = "Beginner";
            this.Beginner.Size = new System.Drawing.Size(109, 43);
            this.Beginner.TabIndex = 0;
            this.Beginner.TabStop = true;
            this.Beginner.Text = "Новичок\r\n10 мин\r\nПоле 9 х 9 ячеек";
            this.Beginner.UseVisualStyleBackColor = true;
            this.Beginner.CheckedChanged += new System.EventHandler(this.Beginner_CheckedChanged);
            // 
            // Amateur
            // 
            this.Amateur.AutoSize = true;
            this.Amateur.Location = new System.Drawing.Point(11, 61);
            this.Amateur.Name = "Amateur";
            this.Amateur.Size = new System.Drawing.Size(121, 43);
            this.Amateur.TabIndex = 1;
            this.Amateur.Text = "Любитель\r\n40 мин\r\nПоле 16 х 16 ячеек";
            this.Amateur.UseVisualStyleBackColor = true;
            this.Amateur.CheckedChanged += new System.EventHandler(this.Amateur_CheckedChanged);
            // 
            // Professional
            // 
            this.Professional.AutoSize = true;
            this.Professional.Location = new System.Drawing.Point(12, 110);
            this.Professional.Name = "Professional";
            this.Professional.Size = new System.Drawing.Size(121, 43);
            this.Professional.TabIndex = 2;
            this.Professional.Text = "Профессионал\r\n99 мин\r\nПоле 16 х 30 ячеек";
            this.Professional.UseVisualStyleBackColor = true;
            this.Professional.CheckedChanged += new System.EventHandler(this.Professional_CheckedChanged);
            // 
            // Special
            // 
            this.Special.AutoSize = true;
            this.Special.Location = new System.Drawing.Point(171, 12);
            this.Special.Name = "Special";
            this.Special.Size = new System.Drawing.Size(65, 17);
            this.Special.TabIndex = 3;
            this.Special.Text = "Особый";
            this.Special.UseVisualStyleBackColor = true;
            this.Special.CheckedChanged += new System.EventHandler(this.Special_CheckedChanged);
            // 
            // heightText
            // 
            this.heightText.AutoSize = true;
            this.heightText.Location = new System.Drawing.Point(189, 41);
            this.heightText.Name = "heightText";
            this.heightText.Size = new System.Drawing.Size(78, 13);
            this.heightText.TabIndex = 4;
            this.heightText.Text = "Высота (9-24):";
            // 
            // widthText
            // 
            this.widthText.AutoSize = true;
            this.widthText.Location = new System.Drawing.Point(189, 70);
            this.widthText.Name = "widthText";
            this.widthText.Size = new System.Drawing.Size(79, 13);
            this.widthText.TabIndex = 5;
            this.widthText.Text = "Ширина (9-30):";
            // 
            // minesText
            // 
            this.minesText.AutoSize = true;
            this.minesText.Location = new System.Drawing.Point(189, 100);
            this.minesText.Name = "minesText";
            this.minesText.Size = new System.Drawing.Size(104, 13);
            this.minesText.TabIndex = 6;
            this.minesText.Text = "Число мин (10-648)";
            // 
            // heightValue
            // 
            this.heightValue.Enabled = false;
            this.heightValue.Location = new System.Drawing.Point(301, 37);
            this.heightValue.Maximum = new decimal(new int[] {
            24,
            0,
            0,
            0});
            this.heightValue.Minimum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.heightValue.Name = "heightValue";
            this.heightValue.Size = new System.Drawing.Size(64, 20);
            this.heightValue.TabIndex = 7;
            this.heightValue.Value = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.heightValue.TextChanged += new System.EventHandler(this.heightValue_TextChanged);
            this.heightValue.ValueChanged += new System.EventHandler(this.heightValue_ValueChanged);
            // 
            // widthValue
            // 
            this.widthValue.Enabled = false;
            this.widthValue.Location = new System.Drawing.Point(301, 63);
            this.widthValue.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.widthValue.Minimum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.widthValue.Name = "widthValue";
            this.widthValue.Size = new System.Drawing.Size(64, 20);
            this.widthValue.TabIndex = 8;
            this.widthValue.Value = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.widthValue.TextChanged += new System.EventHandler(this.widthValue_TextChanged);
            this.widthValue.ValueChanged += new System.EventHandler(this.widthValue_ValueChanged);
            // 
            // minesValue
            // 
            this.minesValue.Enabled = false;
            this.minesValue.Location = new System.Drawing.Point(301, 93);
            this.minesValue.Maximum = new decimal(new int[] {
            648,
            0,
            0,
            0});
            this.minesValue.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.minesValue.Name = "minesValue";
            this.minesValue.Size = new System.Drawing.Size(64, 20);
            this.minesValue.TabIndex = 9;
            this.minesValue.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.minesValue.TextChanged += new System.EventHandler(this.minesValue_TextChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(298, 193);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 10;
            this.button1.Text = "Отмена";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.cancel_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(217, 193);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 11;
            this.button2.Text = "Ок";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.ok_Click);
            // 
            // Config
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(385, 228);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.minesValue);
            this.Controls.Add(this.widthValue);
            this.Controls.Add(this.heightValue);
            this.Controls.Add(this.minesText);
            this.Controls.Add(this.widthText);
            this.Controls.Add(this.heightText);
            this.Controls.Add(this.Special);
            this.Controls.Add(this.Professional);
            this.Controls.Add(this.Amateur);
            this.Controls.Add(this.Beginner);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Config";
            this.Text = "Параметры";
            this.Load += new System.EventHandler(this.Config_Load);
            ((System.ComponentModel.ISupportInitialize)(this.heightValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.widthValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minesValue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton Beginner;
        private System.Windows.Forms.RadioButton Amateur;
        private System.Windows.Forms.RadioButton Professional;
        private System.Windows.Forms.RadioButton Special;
        private System.Windows.Forms.Label heightText;
        private System.Windows.Forms.Label widthText;
        private System.Windows.Forms.Label minesText;
        private System.Windows.Forms.NumericUpDown heightValue;
        private System.Windows.Forms.NumericUpDown widthValue;
        private System.Windows.Forms.NumericUpDown minesValue;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}