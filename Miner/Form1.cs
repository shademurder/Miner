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
            miner1.AddMines(10, new Mine(Properties.Resources.Mine, 1));
        }

        private void button1_Click(object sender, EventArgs e)
        {
           // miner1.Size = new Size(miner1.Size.Width + 1, miner1.Size.Height);
            //miner1.FieldSize = new Size(miner1.FieldSize.Width + 1, miner1.FieldSize.Height);
           // miner1.Clear();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //miner1.CellSize = 30;
            //MessageBox.Show()
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //miner1.CellSize = 25;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //miner1.CellBorderSize = miner1.CellBorderSize == 0 ? 1 : 0;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //miner1.CellSize += 1;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //miner1.AutoSize = miner1.AutoSize ? false : true;
        }
    }
}
