using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Miner
{
    public partial class MinerForm : Form
    {
        private readonly Timer _timer = new Timer();
        private int _time;
        private int _mines;
        private Player _player = new Player();

        public MinerForm()
        {
            InitializeComponent();
            miner1.GameOver += Miner1_GameOver;
            miner1.GameStateChanged += Miner1_GameStateChanged;
            miner1.CellMarkChanged += Miner1_CellMarkChanged;
            miner1.SizeChanged += Miner_SizeChanged;
            SizeChanged += Form1_SizeChanged;
            miner1.CellSize = 20;
            _timer.Interval = 1000;
            _timer.Tick += Timer_Tick;
            
            var loaded = false;
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
                    if (_player == null)
                    {
                        _player = new Player();
                    }
                    Save(_player);
                }
            }
            else
            {
                Save(_player);
            }
            if (!loaded || _player.Mines.Count == 0)
            {
                SetMines(10, new Mine(Properties.Resources.Mine, 1));
            }

            _mines = miner1.Mines;
            minesLeft.Text = _mines.ToString();
        }

        /// <summary>
        /// Сохранение данных игрока
        /// </summary>
        /// <param name="player">Игрок</param>
        private void Save(Player player)
        {
            File.Delete("data.miner");
            var binaryFormatter = new BinaryFormatter();
            using (Stream stream = new FileStream("data.miner", FileMode.Create, FileAccess.Write, FileShare.None))
            {
                binaryFormatter.Serialize(stream, player);
            }
        }
        /// <summary>
        /// Загрузка данных игрока
        /// </summary>
        /// <returns>Игрок</returns>
        private Player LoadPlayer()
        {
            var binaryFormatter = new BinaryFormatter();
            Player player;
            using (Stream stream = new FileStream("data.miner", FileMode.Open, FileAccess.Read, FileShare.None))
            {
                player = (Player)binaryFormatter.Deserialize(stream);
            }
            return player;
        }

        /// <summary>
        /// Обновление поля игры с помощью данных указанного игрока
        /// </summary>
        /// <param name="miner">Игра</param>
        /// <param name="player">Игрок с данными</param>
        private void UpdateMiner(Miner miner, Player player)
        {
            miner.FieldSize = player.FieldSize;
            miner.ClearMines();
            foreach(var data in player.Mines)
            {
                miner.AddMines(data.MineCount, new Mine(data.Image, data.Weight));
            }
            miner.Errors = player.Errors;
        }

        /// <summary>
        /// Добавление мин
        /// </summary>
        /// <param name="mines">Количество мин</param>
        /// <param name="mine">Мина</param>
        private void AddMines(int mines, Mine mine)
        {
            _player.Mines.Add(new MinesData(mines, mine.Weight, mine.Image));
            miner1.AddMines(mines, mine);
            Save(_player);
        }

        /// <summary>
        /// Очищает список мин и добавляет в него указанные
        /// </summary>
        /// <param name="mines">Количество мин</param>
        /// <param name="mine">Мина</param>
        private void SetMines(int mines, Mine mine)
        {
            _player.Mines.Clear();
            miner1.ClearMines();
            AddMines(mines, mine);
        }

        /// <summary>
        /// Изменяет размер поля
        /// </summary>
        /// <param name="rows">Количество строк</param>
        /// <param name="columns">Количество колонок</param>
        private void SetFieldSize(int rows, int columns)
        {
            _player.FieldSize = new Size(columns, rows);
            miner1.FieldSize = new Size(columns, rows);
            Save(_player);
        }

        /// <summary>
        /// Изменяет максимально допустимое количество ошибок
        /// </summary>
        /// <param name="errors">Количество ошибок</param>
        private void SetErrors(int errors)
        {
            _player.Errors = errors;
            miner1.Errors = errors;
            Save(_player);
        }

        /// <summary>
        /// Обновляет данные игрока по результатам игры
        /// </summary>
        /// <param name="player">Игрок</param>
        /// <param name="win">Победил или проиграл</param>
        /// <param name="time"></param>
        private void AddGameData(Player player, bool win, int time)
        {
            player.TotalGames++;
            if (win) player.WinGames++;
            if (win && time < player.BestTime) player.BestTime = time;
        }


        private void Miner_SizeChanged(object sender, EventArgs e)
        {
            var width = miner1.Size.Width + miner1.Location.X + 70;
            var height = miner1.Size.Height + miner1.Location.Y + 95;
            Size = new Size(width, height);
        }
        private void Miner1_CellMarkChanged(Point position, MarkType type)
        {
            if (type == MarkType.Flag)
            {
                _mines--;
            }
            else if(type == MarkType.Empty)
            {
                _mines++;
            }
            minesLeft.Text = _mines.ToString();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if(_time < 999)
            _time++;
            timerValue.Text = _time.ToString();
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
            if (FormBorderStyle == FormBorderStyle.Sizable)
            {
                miner1.Size = new Size(Size.Width - 70 - miner1.Location.X, Size.Height - 95 - miner1.Location.Y);
            }
        }

        private void Miner1_GameOver(Point lastPoint, bool win)
        {
            if (_timer.Enabled)
            {
                _timer.Stop();
            }
            AddGameData(_player, win, _time);
            Save(_player);
            MessageBox.Show(win ? "Вы победили!" : "Вы проиграли.");
        }



        private void новаяИграToolStripMenuItem_Click(object sender, EventArgs e)
        {
            miner1.NewGame();
            if(_timer.Enabled)
            {
                _timer.Stop();
            }
            timerValue.Text = (_time = 0).ToString();
            minesLeft.Text = (_mines = miner1.Mines).ToString();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_timer.Enabled) _timer.Stop();
            Application.Exit();
        }

        private void статистикаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool timerStart = _timer.Enabled;
            if (timerStart)
            {
                _timer.Stop();
            }
            var statisticsForm = new Statistics(_player);
            statisticsForm.ShowDialog();
            if (timerStart)
            {
                _timer.Start();
            }
        }

        private void параметрыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool timerStart = _timer.Enabled;
            if (timerStart)
            {
                _timer.Stop();
            }
            var configForm = new Config(_player);
            configForm.ShowDialog();
            if(configForm.NeedUpdate)
            {
                Save(_player);
                UpdateMiner(miner1, _player);
                miner1.NewGame();
                timerValue.Text = (_time = 0).ToString(); ;
                minesLeft.Text = (_mines = miner1.Mines).ToString();
            }
            else
            {
                if (timerStart)
                {
                    _timer.Start();
                }
            }

        }
    }
}
