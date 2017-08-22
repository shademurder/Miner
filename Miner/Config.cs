using System;
using System.Drawing;
using System.Windows.Forms;

namespace Miner
{
    /// <summary>
    /// Форма конфигурации
    /// </summary>
    public partial class Config : Form
    {
        private Player _player;
        public Config(Player player)
        {
            InitializeComponent();
            _player = player;
        }

        private void Config_Load(object sender, EventArgs e)
        {
            if (_player.FieldSize == new Size(9, 9) && _player.TotalMines == 10)
            {
                Beginner.Checked = true;
            }
            else if (_player.FieldSize == new Size(16, 16) && _player.TotalMines == 40)
            {
                Amateur.Checked = true;
            }
            else if (_player.FieldSize == new Size(30, 16) && _player.TotalMines == 99)
            {
                Professional.Checked = true;
            }
            else
            {
                Special.Checked = true;
            }
            minesValue.Maximum = (int)((int)heightValue.Value * (int)widthValue.Value * 0.9);
        }

        /// <summary>
        /// Активирует или деактивирует поля для ввода размеров поля и количества мин
        /// </summary>
        /// <param name="flag"></param>
        private void Enable(bool flag)
        {
            heightValue.Enabled = flag;
            widthValue.Enabled = flag;
            minesValue.Enabled = flag;
        }

        /// <summary>
        /// Сохраняет конфигурацию пользователя
        /// </summary>
        /// <param name="mines">Количество мин</param>
        /// <param name="fieldSize">Размеры поля</param>
        private void SetPlayer(int mines, Size fieldSize)
        {
            _player.Mines.Clear();
            _player.Mines.Add(new MinesData(mines, 1, Properties.Resources.Mine));
            _player.FieldSize = fieldSize;
        }

        private void ok_Click(object sender, EventArgs e)
        {
            if (Beginner.Checked)
            {
                SetPlayer(10, new Size(9, 9));
            }
            else if (Amateur.Checked)
            {
                SetPlayer(40, new Size(16, 16));
            }
            else if (Professional.Checked)
            {
                SetPlayer(99, new Size(30, 16));
            }
            else
            {
                SetPlayer((int)minesValue.Value, new Size((int)widthValue.Value, (int)heightValue.Value));
            }
            Close();
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Beginner_CheckedChanged(object sender, EventArgs e)
        {
            if (!Beginner.Checked) return;
            Amateur.Checked = false;
            Professional.Checked = false;
            Special.Checked = false;
            Enable(false);
        }

        private void Amateur_CheckedChanged(object sender, EventArgs e)
        {
            if (!Amateur.Checked) return;
            Beginner.Checked = false;
            Professional.Checked = false;
            Special.Checked = false;
            Enable(false);
        }

        private void Professional_CheckedChanged(object sender, EventArgs e)
        {
            if (!Professional.Checked) return;
            Beginner.Checked = false;
            Amateur.Checked = false;
            Special.Checked = false;
            Enable(false);
        }

        private void Special_CheckedChanged(object sender, EventArgs e)
        {
            if (!Special.Checked) return;
            Beginner.Checked = false;
            Amateur.Checked = false;
            Professional.Checked = false;
            Enable(true);
        }

        private void minesValue_TextChanged(object sender, EventArgs e)
        {
            if (minesValue.Text == "") minesValue.Text = $"{minesValue.Value}";
        }

        private void heightValue_ValueChanged(object sender, EventArgs e)
        {
            minesValue.Maximum = (int) ((int) heightValue.Value*(int) widthValue.Value*0.9);
        }
        private void heightValue_TextChanged(object sender, EventArgs e)
        {
            if (heightValue.Text == "") heightValue.Text = $"{heightValue.Value}";
        }

        private void widthValue_ValueChanged(object sender, EventArgs e)
        {
            minesValue.Maximum = (int)((int)heightValue.Value * (int)widthValue.Value * 0.9);
        }
        private void widthValue_TextChanged(object sender, EventArgs e)
        {
            if (widthValue.Text == "") widthValue.Text = $"{widthValue.Value}";
        }
    }
}
