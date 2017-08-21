using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Miner
{
    public partial class Form1 : Form
    {
        private Timer _timer = new Timer();
        private int _time = 0;
        private int _mines = 0;
        private Player _player = new Player();

        public Form1()
        {
            InitializeComponent();
            miner1.GameOver += Miner1_GameOver;
            miner1.GameStateChanged += Miner1_GameStateChanged;
            SizeChanged += Form1_SizeChanged;
           // label1.TextAlign = ContentAlignment.MiddleCenter;
            //label2.TextAlign = ContentAlignment.MiddleCenter;
            _mines = miner1.Mines;
            label2.Text = _mines.ToString();
            _timer.Interval = 1000;
            _timer.Tick += Timer_Tick;
            miner1.CellMarkChanged += Miner1_CellMarkChanged;
            bool loaded = false;
            if (File.Exists("data.miner"))
            {
                try
                {
                    _player = LoadPlayer();
                    loaded = true;
                    UpdateMiner(miner1, _player);
                }
                catch
                {
                    //ошибка при десериализации данных игрока
                    if(_player == null)
                    {
                        _player = new Player();
                    }
                    Save(_player);
                }
            }
            else
                Save(_player);
            if(!loaded)
            AddMines(10, new Mine(Properties.Resources.Mine, 1));
        }


        private void Save(Player player)
        {
            File.Delete("data.miner");
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (Stream stream = new FileStream("data.miner", FileMode.Create, FileAccess.Write, FileShare.None))
            {
                binaryFormatter.Serialize(stream, player);
            }
        }
        private Player LoadPlayer()
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (Stream stream = new FileStream("data.miner", FileMode.Open, FileAccess.Read, FileShare.None))
            {
                _player = (Player)binaryFormatter.Deserialize(stream);
            }
            return null;
        }

        private void UpdateMiner(Miner miner, Player player)
        {
            miner.FieldSize = player.FieldSize;
            miner.ClearMines();
            foreach(MinesData data in player.Mines)
            {
                miner.AddMines(data.MineCount, new Mine(data.Image, data.Weight));
            }
            miner.Errors = player.Errors;
        }

        private void AddMines(int mines, Mine mine)
        {
            _player.Mines.Add(new MinesData(mines, mine.Weight, mine.Image));
            miner1.AddMines(mines, mine);
            Save(_player);
        }

        private void SetMines(int mines, Mine mine)
        {
            _player.Mines.Clear();
            miner1.ClearMines();
            AddMines(mines, mine);
        }

        private void SetFieldSize(int rows, int columns)
        {
            _player.FieldSize = new Size(columns, rows);
            miner1.FieldSize = new Size(columns, rows);
            Save(_player);
        }

        private void SetErrors(int errors)
        {
            _player.Errors = errors;
            miner1.Errors = errors;
            Save(_player);
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
            if(_time < 999)
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
            Size = new Size(Size.Width, Size.Width + 25);
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
            _mines = miner1.Mines;
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void статистикаToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void параметрыToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
