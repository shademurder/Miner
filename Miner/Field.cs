using System.Drawing;
using System.Net;

namespace Miner
{
    class Field
    {
        /*
        Размерность поля:
            Высота в клетках
            Ширина в клетках




        Массив клеток (Cell[,]) - зависит от высоты и ширины
        Размер клетки (int)
        Начальный цвет градиента скрытой клетки
        Конечный цвет градиента скрытой клетки
        Начальный цвет градиента открытой клетки
        Конечный цвет градиента открытой клетки

        Предположительно размер бордера = 1/20 от размера ячейки
        */

        public Field(int rows, int columns)
        {
            FieldSize = new Size(columns, rows);
        }

        private int _cellSize = 20;
        private int _borderSize = 1;
        private Size _fieldSize = new Size(9, 9);
        private Color _borderColor = Color.Black;
        private Color _startFieldColor = Color.FromArgb(165, 185, 244);
        private Color _endFieldColor = Color.FromArgb(19, 65, 203);

        public int CellSize
        {
            get
            {
                return _cellSize;
            }

            set
            {
                _cellSize = value >= 20 ? value : 20;
                for (var row = 0; row < Cells.GetLength(0); row++)
                {
                    for (var column = 0; column < Cells.GetLength(1); column++)
                    {
                        Cells[row, column].CellSize = _cellSize;
                    }
                }
            }
        }
        internal Cell[,] Cells { get; set; }
   
        public int BorderSize
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
                            Cells[row, column].BorderSize = _borderSize;
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
                        Cells[row, column].BorderColor = _borderColor;
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
                Recreate();
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
                Recreate();
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
                Cells = new Cell[_fieldSize.Height, _fieldSize.Width];
                Recreate();
            }
        }

        /// <summary>
        /// Пересоздаёт все ячейки поля
        /// </summary>
        private void Recreate()
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
                    Cells[row, column] = new Cell(
                        Color.FromArgb(StartFieldColor.R + r*step, StartFieldColor.G + g*step,
                            StartFieldColor.B + b*step),
                        Color.FromArgb(StartFieldColor.R + r*(step + 1), StartFieldColor.G + g*(step + 1),
                            StartFieldColor.B + b*(step + 1)),
                        BorderColor, BorderSize, CellSize, new Point(column, row));
                }
            }
        }

        public void SetCellSizes(int size)
        {
            if (BorderSize == 0)
            {
                if (CellSize != size)
                {
                    CellSize = size;
                }
            }
            else
            {
                var proportions = (double)BorderSize / CellSize;
                var newBorderSize = (int)(proportions * size);
                newBorderSize = newBorderSize == 0 ? 1 : newBorderSize;
                if (BorderSize != newBorderSize || CellSize != size - newBorderSize)
                {
                    BorderSize = newBorderSize;
                    CellSize = size - BorderSize;
                }
            }
            
            for (var row = 0; row < Cells.GetLength(0); row++)
            {
                for (var column = 0; column < Cells.GetLength(1); column++)
                {
                    Cells[row, column].CellSize = CellSize;
                    Cells[row, column].BorderSize = BorderSize;
                }
            }
        }
    }
}
