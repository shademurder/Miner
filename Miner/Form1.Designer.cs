namespace Miner
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.miner1 = new Miner();
            this.SuspendLayout();
            // 
            // miner1
            // 
            this.miner1.BorderColor = System.Drawing.Color.Black;
            this.miner1.CellBorderSize = 1F;
            this.miner1.CellSize = 44.11111F;
            this.miner1.EndFieldColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(65)))), ((int)(((byte)(203)))));
            this.miner1.FieldSize = new System.Drawing.Size(9, 9);
            this.miner1.Location = new System.Drawing.Point(13, 13);
            this.miner1.Name = "miner1";
            this.miner1.Size = new System.Drawing.Size(407, 407);
            this.miner1.StartFieldColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(185)))), ((int)(((byte)(244)))));
            this.miner1.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(672, 573);
            this.Controls.Add(this.miner1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion
        private Miner miner1;
    }
}

