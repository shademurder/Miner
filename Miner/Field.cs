using System;
using System.Drawing;

namespace Miner
{
    class Field
    {
        public Field(int rows, int columns)
        {
            FieldSize = new Size(columns, rows);
        }

        private float _cellSize = 20;
        private float _borderSize = 1;
        private Size _fieldSize = new Size(9, 9);
        private Color _borderColor = Color.Black;
        private Color _startFieldColor = Color.FromArgb(165, 185, 244);
        private Color _endFieldColor = Color.FromArgb(19, 65, 203);
        private readonly Random random = new Random();

        public float CellSize
        {
            get
            {
                return _cellSize;
            }

            set
            {
                _cellSize = value > 0 ? value : 0;
                for (var row = 0; row < Cells.GetLength(0); row++)
                {
                    for (var column = 0; column < Cells.GetLength(1); column++)
                    {
                        Cells[row, column].CellSize = value;
                    }
                }
            }
        }
        internal Cell[,] Cells { get; set; }
   
        public float BorderSize
        {
            get
            {
                return _borderSize;
            }
            set
            {
                if (value >= 0)
                {
                    _borderSize = value;
                    for (var row = 0; row < Cells.GetLength(0); row++)
                    {
                        for (var column = 0; column < Cells.GetLength(1); column++)
                        {
                            Cells[row, column].BorderSize = value;
                        }
                    }
                }
            }
        }

        public Color BorderColor
        {
            get
            {
                return _borderColor;
            }

            set
            {
                _borderColor = value;
                for (var row = 0; row < Cells.GetLength(0); row++)
                {
                    for (var column = 0; column < Cells.GetLength(1); column++)
                    {
                        Cells[row, column].BorderColor = value;
                    }
                }
            }
        }

        public Color StartFieldColor
        {
            get
            {
                return _startFieldColor;
            }
            set
            {
                _startFieldColor = value;
                RedefineField(false);
            }
        }

        public Color EndFieldColor
        {
            get
            {
                return _endFieldColor;
            }

            set
            {
                _endFieldColor = value;
                RedefineField(false);
            }
        }

        public Size FieldSize
        {
            get
            {
                return _fieldSize;
            }

            set
            {
                if (value.Height <= 0 || value.Width <= 0) return;
                _fieldSize = value;
                Cells = new Cell[value.Height, value.Width];
                RedefineField(true);
            }
        }

        /// <summary>
        /// Пересоздаёт все ячейки поля
        /// </summary>
        public void RedefineField(bool recreate)
        {
            var steps = Cells.GetLength(0) + Cells.GetLength(1) - 1;
            steps = steps <= 0 ? 1 : steps;
            var r = (EndFieldColor.R - StartFieldColor.R) / steps;
            var g = (EndFieldColor.G - StartFieldColor.G) / steps;
            var b = (EndFieldColor.B - StartFieldColor.B) / steps;
            for (var row = 0; row < FieldSize.Height; row++)
            {
                for (var column = 0; column < FieldSize.Width; column++)
                {
                    var step = row + column;
                    if (recreate)
                    {
                        Cells[row, column] = new Cell(
                            Color.FromArgb(StartFieldColor.R + r * step, StartFieldColor.G + g * step,
                                StartFieldColor.B + b * step),
                            Color.FromArgb(StartFieldColor.R + r * (step + 1), StartFieldColor.G + g * (step + 1),
                                StartFieldColor.B + b * (step + 1)),
                            BorderColor, BorderSize, CellSize, new Point(column, row));
                    }
                    else
                    {
                        Cells[row, column].ChangeColors(
                            Color.FromArgb(StartFieldColor.R + r * step, StartFieldColor.G + g * step,
                                StartFieldColor.B + b * step),
                            Color.FromArgb(StartFieldColor.R + r * (step + 1), StartFieldColor.G + g * (step + 1),
                                StartFieldColor.B + b * (step + 1)),
                                true);
                    }
                }
            }
        }

        public int CreateMines(int mines, Mine mine)
        {
            var emptyCells = GetEmptyCellCount();
            //получить количество свободных от мин клеток
            for(; mines > 0; mines--, emptyCells--)
            {
                if(!CreateMine(emptyCells, mine))
                {
                    break;
                }
            }
            CalculateField();
            //вызвать метод рандомизации одной мины нужное количество раз
            //если мину поставить не удалось, значит поле заполнено
            //вернуть количество мин, которые не удалось поставить
            return mines;
        }

        private bool CreateMine(int emptyCells, Mine mine)
        {
            if(emptyCells == 0)
            {
                return false;
            }
            var randValue = random.Next(emptyCells);
            for (var row = 0; row < FieldSize.Height; row++)
            {
                for (var column = 0; column < FieldSize.Width; column++)
                {
                    if (Cells[row, column].Type != CellType.Mine)
                    {
                        if (randValue == 0)
                        {
                            SetMine(row, column, mine);
                            return true;
                        }
                        randValue--;
                    }
                }
            }
            
            //срандомить число от 0 до количества свободных клеток
            //пройти по циклу, уменьшая значение пустых клеток на 1 каждый раз, когда встречается незаминированная клетка
            //если количество пустых клеток = 0, поставить в это место мину и вернуть true
            //если цикл кончился,вернуть false
            return false;
        }

        private int GetEmptyCellCount()
        {
            int count = 0;
            for (var row = 0; row < FieldSize.Height; row++)
            {
                for (var column = 0; column < FieldSize.Width; column++)
                {
                    if(Cells[row, column].Type != CellType.Mine)
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        public bool SetMine(int row, int column, Mine mine)
        {
            if(Cells.GetLength(0) <= row || row < 0 || Cells.GetLength(1) <= column || column < 0 || Cells[row, column].Type == CellType.Mine)
            {
                return false;
            }
            Cells[row, column].Type = CellType.Mine;
            Cells[row, column].Mine = mine;
            return true;
        }

        public void CalculateField()
        {
            for(var row = 0; row < Cells.GetLength(0); row++)
            {
                for(var column = 0; column < Cells.GetLength(1); column++)
                {
                    if(Cells[row, column].Type != CellType.Mine)
                    {
                        Cells[row, column].Weight = GetCellWeight(row, column);
                    }
                }
            }
        }

        private short GetCellWeight(int row, int column)
        {
            short weight = 0;
            //for(var x = row < 0 ? 0 : row - 1; x < (row >= Cells.GetLength(0) - 1 ? Cells.GetLength(0) : row + 1); row++)
            //{
            //    for(var y = column < 0 ? 0 : column - 1; y < (column >= Cells.GetLength(1) - 1 ? Cells.GetLength(1) : column + 1); column++)
            //    {
            //        if(Cells[x, y].Type == CellType.Mine)
            //        {
            //            weight++;
            //        }
            //    }
            //}
            for(var y = row - 1; y <= row + 1; y++)
            {
                for(var x = column - 1; x <= column + 1; x++)
                {
                    if(x < 0 || y < 0 || x >= Cells.GetLength(1) || y >= Cells.GetLength(0))
                    {
                        continue;
                    }
                    if(Cells[y, x].Type == CellType.Mine)
                    {
                        weight++;
                    }
                }
            }
            return weight;
        }
    }
}
