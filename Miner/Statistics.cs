using System;
using System.Windows.Forms;

namespace Miner
{
    public partial class Statistics : Form
    {
        private Player _player;
        public Statistics(Player player)
        {
            InitializeComponent();
            _player = player;
        }

        private void Config_Load(object sender, EventArgs e)
        {
            UpdateData();
        }

        /// <summary>
        /// Обновление данных игрока
        /// </summary>
        private void UpdateData()
        {
            totalGamesValue.Text = _player.TotalGames.ToString();
            winGamesValue.Text = _player.WinGames.ToString();
            winRateValue.Text = $"{Math.Round(_player.WinRate, 3)}%";
            bestTimeValue.Text = _player.BestTime.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _player.BestTime = 999;
            _player.WinGames = 0;
            _player.TotalGames = 0;
            UpdateData();
        }
    }
}
