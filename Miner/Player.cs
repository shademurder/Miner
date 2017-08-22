using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Miner
{
    [Serializable]
    public class Player
    {
        private int _totalGames = 0;
        private int _winGames = 0;
        private Size _fieldSize = new Size(9, 9);
        private int _errors = 1;
        private List<MinesData> _mines = new List<MinesData>();
        private int _bestTime = 999;

        /// <summary>
        /// Процент побед
        /// </summary>
        public double WinRate => _totalGames == 0 ? 0 : (double)_winGames * 100 / _totalGames;

        /// <summary>
        /// Общее количество сыгранных игр
        /// </summary>
        public int TotalGames
        {
            get
            {
                return _totalGames;
            }

            set
            {
                _totalGames = value;
            }
        }

        /// <summary>
        /// Количество победных игр
        /// </summary>
        public int WinGames
        {
            get
            {
                return _winGames;
            }

            set
            {
                _winGames = value;
            }
        }

        /// <summary>
        /// Размер игрового поля
        /// </summary>
        public Size FieldSize
        {
            get
            {
                return _fieldSize;
            }

            set
            {
                _fieldSize = value;
            }
        }

        /// <summary>
        /// Количество допустимых ошибок
        /// </summary>
        public int Errors
        {
            get
            {
                return _errors;
            }

            set
            {
                _errors = value;
            }
        }

        /// <summary>
        /// Список данных о минах
        /// </summary>
        internal List<MinesData> Mines
        {
            get
            {
                return _mines;
            }

            set
            {
                _mines = value;
            }
        }

        /// <summary>
        /// Лушчее время прохождения
        /// </summary>
        public int BestTime
        {
            get
            {
                return _bestTime;
            }

            set
            {
                _bestTime = value;
            }
        }

        /// <summary>
        /// Общее количество мин
        /// </summary>
        public int TotalMines => Mines.Sum(data => data.MineCount);
    }
}
