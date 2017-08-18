using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Miner
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //miner1.Hide();
            //CellButton cellButton = new CellButton(Color.FromArgb(97, 129, 227), Color.FromArgb(215, 246, 250), Color.Black, 1, 20);
            //cellButton.Location = new Point(150, 150);
            //CellButton cellButton1 = new CellButton(Color.FromArgb(97, 129, 227), Color.FromArgb(215, 246, 250), Color.Black, 1, 20);
            //cellButton1.Location = new Point(129, 150);
            //Controls.Add(cellButton);
            //Controls.Add(cellButton1);
            //var steps = 17;//rows + columns - 1;
            //Color start = Color.FromArgb(215, 246, 250);
            //Color end = Color.FromArgb(97, 129, 227);
            //var r = (end.R - start.R) / steps;
            //var g = (end.G - start.G) / steps;
            //var b = (end.B - start.B) / steps;
            //for (var i = 0; i < 9; i++)
            //{
            //    for (var j = 0; j < 9; j++)
            //    {
            //        var step = i + j;
            //        var cellButton = new Cell(
            //            Color.FromArgb(start.R + r * step, start.G + g * step, start.B + b * step), 
            //            Color.FromArgb(start.R + r * (step + 1), start.G + g * (step + 1), start.B + b * (step + 1)), 
            //            Color.Black, 1, 20, new Point(i, j));
            //        cellButton.Position = new Point(10 + i * 21, 10 + j * 21);
            //        //Controls.Add(cellButton);
            //    }
            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            miner1.FieldSize = new Size(miner1.FieldSize.Width + 1, miner1.FieldSize.Height);
            miner1.Clear();
        }
    }
}
