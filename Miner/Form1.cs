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
        private Timer _timer = new Timer();
        private int _time = 0;
        private int _mines = 0;
        public Form1()
        {
            InitializeComponent();
            miner1.AddMines(10, new Mine(Properties.Resources.Mine, 1));
            miner1.GameOver += Miner1_GameOver;
            miner1.GameStateChanged += Miner1_GameStateChanged;
            SizeChanged += Form1_SizeChanged;
            label1.TextAlign = ContentAlignment.MiddleCenter;
            label2.TextAlign = ContentAlignment.MiddleCenter;
            _mines = miner1.Mines;
            label2.Text = _mines.ToString();
            _timer.Interval = 1000;
            _timer.Tick += Timer_Tick;
            miner1.CellMarkChanged += Miner1_CellMarkChanged;
        }

        private void Miner1_CellMarkChanged(Point position, MarkType type)
        {
            if (type == MarkType.Flag)
            {
                _mines--;
            }
            else
            {
                _mines++;
            }
            label2.Text = _mines.ToString();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            _time++;
            label1.Text = _time.ToString();
        }

        private void Miner1_GameStateChanged(GameState state)
        {
            if (state == GameState.Playing)
            {
                _time = 0;
                _timer.Start();
            }
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            if (Size.Width != Size.Height)
            {
                var newSize = Size.Width > Size.Height ? Size.Width : Size.Height;
                Size = new Size(newSize, newSize);
            }
        }

        private void Miner1_GameOver(Point lastPoint, bool win)
        {
            _timer.Stop();
            MessageBox.Show(win ? "Вы победили!" : "Вы проиграли.");
        }

        private void новаяИграToolStripMenuItem_Click(object sender, EventArgs e)
        {
            miner1.NewGame();
            _time = 0;
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
