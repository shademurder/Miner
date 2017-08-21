using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Miner
{
    [Serializable]
    class Player
    {
        private double _winRate = 0;
        private int _totalGames = 0;
        private int _winGames = 0;
        private Size _fieldSize = new Size(9, 9);
        private int _errors = 1;
        private List<MinesData> _mines = new List<MinesData>();
        private int _bestTime = 999;

        public double WinRate { get => _winRate; set => _winRate = value; }
        public int TotalGames { get => _totalGames; set => _totalGames = value; }
        public int WinGames { get => _winGames; set => _winGames = value; }
        public Size FieldSize { get => _fieldSize; set => _fieldSize = value; }
        public int Errors { get => _errors; set => _errors = value; }
        public List<MinesData> Mines { get => _mines; set => _mines = value; }
        public int BestTime { get => _bestTime; set => _bestTime = value; }


       

        //Статистика
        //Параметры
    }
}
