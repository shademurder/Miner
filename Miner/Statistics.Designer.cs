namespace Miner
{
    partial class Statistics
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
            this.totalGamesText = new System.Windows.Forms.Label();
            this.totalGamesValue = new System.Windows.Forms.Label();
            this.winGamesText = new System.Windows.Forms.Label();
            this.winGamesValue = new System.Windows.Forms.Label();
            this.winRateText = new System.Windows.Forms.Label();
            this.winRateValue = new System.Windows.Forms.Label();
            this.bestTimeText = new System.Windows.Forms.Label();
            this.bestTimeValue = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // totalGamesText
            // 
            this.totalGamesText.AutoSize = true;
            this.totalGamesText.Location = new System.Drawing.Point(13, 43);
            this.totalGamesText.Name = "totalGamesText";
            this.totalGamesText.Size = new System.Drawing.Size(86, 13);
            this.totalGamesText.TabIndex = 0;
            this.totalGamesText.Text = "Количество игр";
            // 
            // totalGamesValue
            // 
            this.totalGamesValue.AutoSize = true;
            this.totalGamesValue.Location = new System.Drawing.Point(220, 43);
            this.totalGamesValue.Name = "totalGamesValue";
            this.totalGamesValue.Size = new System.Drawing.Size(13, 13);
            this.totalGamesValue.TabIndex = 1;
            this.totalGamesValue.Text = "0";
            // 
            // winGamesText
            // 
            this.winGamesText.AutoSize = true;
            this.winGamesText.Location = new System.Drawing.Point(13, 71);
            this.winGamesText.Name = "winGamesText";
            this.winGamesText.Size = new System.Drawing.Size(39, 13);
            this.winGamesText.TabIndex = 2;
            this.winGamesText.Text = "Побед";
            // 
            // winGamesValue
            // 
            this.winGamesValue.AutoSize = true;
            this.winGamesValue.Location = new System.Drawing.Point(220, 71);
            this.winGamesValue.Name = "winGamesValue";
            this.winGamesValue.Size = new System.Drawing.Size(13, 13);
            this.winGamesValue.TabIndex = 3;
            this.winGamesValue.Text = "0";
            // 
            // winRateText
            // 
            this.winRateText.AutoSize = true;
            this.winRateText.Location = new System.Drawing.Point(13, 99);
            this.winRateText.Name = "winRateText";
            this.winRateText.Size = new System.Drawing.Size(83, 13);
            this.winRateText.TabIndex = 5;
            this.winRateText.Text = "Процент побед";
            // 
            // winRateValue
            // 
            this.winRateValue.AutoSize = true;
            this.winRateValue.Location = new System.Drawing.Point(220, 99);
            this.winRateValue.Name = "winRateValue";
            this.winRateValue.Size = new System.Drawing.Size(13, 13);
            this.winRateValue.TabIndex = 4;
            this.winRateValue.Text = "0";
            // 
            // bestTimeText
            // 
            this.bestTimeText.AutoSize = true;
            this.bestTimeText.Location = new System.Drawing.Point(13, 127);
            this.bestTimeText.Name = "bestTimeText";
            this.bestTimeText.Size = new System.Drawing.Size(80, 13);
            this.bestTimeText.TabIndex = 7;
            this.bestTimeText.Text = "Лучшее время";
            // 
            // bestTimeValue
            // 
            this.bestTimeValue.AutoSize = true;
            this.bestTimeValue.Location = new System.Drawing.Point(220, 127);
            this.bestTimeValue.Name = "bestTimeValue";
            this.bestTimeValue.Size = new System.Drawing.Size(13, 13);
            this.bestTimeValue.TabIndex = 6;
            this.bestTimeValue.Text = "0";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(16, 189);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "Закрыть";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(180, 189);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 9;
            this.button2.Text = "Сбросить";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Statistics
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(267, 229);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.bestTimeText);
            this.Controls.Add(this.bestTimeValue);
            this.Controls.Add(this.winRateText);
            this.Controls.Add(this.winRateValue);
            this.Controls.Add(this.winGamesValue);
            this.Controls.Add(this.winGamesText);
            this.Controls.Add(this.totalGamesValue);
            this.Controls.Add(this.totalGamesText);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Statistics";
            this.Text = "Статистика игры \"Сапёр\"";
            this.Load += new System.EventHandler(this.Config_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label totalGamesText;
        private System.Windows.Forms.Label totalGamesValue;
        private System.Windows.Forms.Label winGamesText;
        private System.Windows.Forms.Label winGamesValue;
        private System.Windows.Forms.Label winRateText;
        private System.Windows.Forms.Label winRateValue;
        private System.Windows.Forms.Label bestTimeText;
        private System.Windows.Forms.Label bestTimeValue;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}