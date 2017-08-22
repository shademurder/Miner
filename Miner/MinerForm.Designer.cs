namespace Miner
{
    partial class MinerForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.играToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.новаяИграToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.статистикаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.параметрыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timerPictureBox = new System.Windows.Forms.PictureBox();
            this.minePictureBox = new System.Windows.Forms.PictureBox();
            this.timerValue = new System.Windows.Forms.Label();
            this.minesLeft = new System.Windows.Forms.Label();
            this.miner1 = new Miner();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.timerPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minePictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.играToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(359, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // играToolStripMenuItem
            // 
            this.играToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.новаяИграToolStripMenuItem,
            this.статистикаToolStripMenuItem,
            this.параметрыToolStripMenuItem,
            this.выходToolStripMenuItem});
            this.играToolStripMenuItem.Name = "играToolStripMenuItem";
            this.играToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.играToolStripMenuItem.Text = "Игра";
            // 
            // новаяИграToolStripMenuItem
            // 
            this.новаяИграToolStripMenuItem.Name = "новаяИграToolStripMenuItem";
            this.новаяИграToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.новаяИграToolStripMenuItem.Text = "Новая игра";
            this.новаяИграToolStripMenuItem.Click += new System.EventHandler(this.новаяИграToolStripMenuItem_Click);
            // 
            // статистикаToolStripMenuItem
            // 
            this.статистикаToolStripMenuItem.Name = "статистикаToolStripMenuItem";
            this.статистикаToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.статистикаToolStripMenuItem.Text = "Статистика";
            this.статистикаToolStripMenuItem.Click += new System.EventHandler(this.статистикаToolStripMenuItem_Click);
            // 
            // параметрыToolStripMenuItem
            // 
            this.параметрыToolStripMenuItem.Name = "параметрыToolStripMenuItem";
            this.параметрыToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.параметрыToolStripMenuItem.Text = "Параметры";
            this.параметрыToolStripMenuItem.Click += new System.EventHandler(this.параметрыToolStripMenuItem_Click);
            // 
            // выходToolStripMenuItem
            // 
            this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
            this.выходToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.выходToolStripMenuItem.Text = "Выход";
            this.выходToolStripMenuItem.Click += new System.EventHandler(this.выходToolStripMenuItem_Click);
            // 
            // timerPictureBox
            // 
            this.timerPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.timerPictureBox.Image = global::Miner.Properties.Resources.Timer;
            this.timerPictureBox.Location = new System.Drawing.Point(50, 320);
            this.timerPictureBox.Name = "timerPictureBox";
            this.timerPictureBox.Size = new System.Drawing.Size(25, 25);
            this.timerPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.timerPictureBox.TabIndex = 2;
            this.timerPictureBox.TabStop = false;
            // 
            // minePictureBox
            // 
            this.minePictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.minePictureBox.Image = global::Miner.Properties.Resources.Mine;
            this.minePictureBox.Location = new System.Drawing.Point(280, 320);
            this.minePictureBox.Name = "minePictureBox";
            this.minePictureBox.Size = new System.Drawing.Size(25, 25);
            this.minePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.minePictureBox.TabIndex = 3;
            this.minePictureBox.TabStop = false;
            // 
            // timerValue
            // 
            this.timerValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.timerValue.AutoSize = true;
            this.timerValue.Location = new System.Drawing.Point(80, 326);
            this.timerValue.Name = "timerValue";
            this.timerValue.Size = new System.Drawing.Size(13, 13);
            this.timerValue.TabIndex = 4;
            this.timerValue.Text = "0";
            // 
            // minesLeft
            // 
            this.minesLeft.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.minesLeft.AutoSize = true;
            this.minesLeft.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.minesLeft.Location = new System.Drawing.Point(250, 327);
            this.minesLeft.Name = "minesLeft";
            this.minesLeft.Size = new System.Drawing.Size(13, 13);
            this.minesLeft.TabIndex = 5;
            this.minesLeft.Text = "0";
            // 
            // miner1
            // 
            this.miner1.BorderColor = System.Drawing.Color.Black;
            this.miner1.BrightnessCoefficient = 1.3D;
            this.miner1.CellBorderSize = 1F;
            this.miner1.CellSize = 27.11111F;
            this.miner1.EndFieldColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.miner1.Errors = 1;
            this.miner1.FieldSize = new System.Drawing.Size(9, 9);
            this.miner1.GradientAngle = 45;
            this.miner1.Location = new System.Drawing.Point(50, 50);
            this.miner1.Name = "miner1";
            this.miner1.Size = new System.Drawing.Size(254, 254);
            this.miner1.StartFieldColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(185)))), ((int)(((byte)(244)))));
            this.miner1.TabIndex = 0;
            // 
            // MinerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(359, 362);
            this.Controls.Add(this.minesLeft);
            this.Controls.Add(this.timerValue);
            this.Controls.Add(this.minePictureBox);
            this.Controls.Add(this.timerPictureBox);
            this.Controls.Add(this.miner1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MinerForm";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.timerPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minePictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion

        private Miner miner1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem играToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem новаяИграToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem статистикаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem параметрыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem выходToolStripMenuItem;
        private System.Windows.Forms.PictureBox timerPictureBox;
        private System.Windows.Forms.PictureBox minePictureBox;
        private System.Windows.Forms.Label timerValue;
        private System.Windows.Forms.Label minesLeft;
    }
}

