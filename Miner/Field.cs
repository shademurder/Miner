using System;
using System.Drawing;
using System.Linq;

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
        private int _gradientAngle = 45;
        private readonly Random random = new Random();
        private double _brightnessCoefficient = 1.3;

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
        internal Cell[,] Cells { get; private set; }
   
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

        public int GradientAngle
        {
            get
            {
                return _gradientAngle;
            }

            set
            {
                _gradientAngle = value % 360;
                for (var row = 0; row < Cells.GetLength(0); row++)
                {
                    for (var column = 0; column < Cells.GetLength(1); column++)
                    {
                        Cells[row, column].GradientAngle = _gradientAngle;
                    }
                }
            }
        }

        public double BrightnessCoefficient
        {
            get
            {
                return _brightnessCoefficient;
            }

            set
            {
                _brightnessCoefficient = value;
                for (var row = 0; row < Cells.GetLength(0); row++)
                {
                    for (var column = 0; column < Cells.GetLength(1); column++)
                    {
                        Cells[row, column].BrightnessCoefficient = value;
                    }
                }
            }
        }

        public event Action<Point, bool> CellSelect;
        public event Action<Point, bool> CellClick;
        public event Action<Point, bool> CellPress;
        public event Action<Point, MarkType> CellMarkChanged;
        public event Action<Point, bool> CellBlockChanged;
        public event Action<Point> FieldComplete;

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
            if (recreate)
            {
                CellSelect = null;
                CellClick = null;
                CellPress = null;
                CellMarkChanged = null;
                CellBlockChanged = null;
                FieldComplete = null;
            }
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
                        Cells[row, column].CellClick += OnCellClick;
                        Cells[row, column].CellSelect += OnCellSelect;
                        Cells[row, column].CellPress += OnCellPress;
                        Cells[row, column].CellMarkChanged += OnCellMarkChanged;
                        Cells[row, column].CellBlockChanged += OnCellBlockChanged;
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

        public void OpenField()
        {
            for (var row = 0; row < FieldSize.Height; row++)
            {
                for (var column = 0; column < FieldSize.Width; column++)
                {

                    Cells[row, column].Blocked = false;
                    if (!Cells[row, column].Pressed)
                    {
                        Cells[row, column].Pressed = true;
                    }
                    Cells[row, column].Blocked = true;
                }
            }
        }

        public void HideField()
        {
            for (var row = 0; row < FieldSize.Height; row++)
            {
                for (var column = 0; column < FieldSize.Width; column++)
                {
                    //Cells[row, column].Blocked = false;
                    //if (Cells[row, column].Pressed)
                    //{
                    //    Cells[row, column].Pressed = false;
                    //}
                    //if (Cells[row, column].Type == CellType.Mine)
                    //{
                    //    Cells[row, column].Type = CellType.Empty;
                    //}
                    Cells[row, column].Clear();
                }
            }
        }

        public void BlockField(bool block)
        {
            for (var row = 0; row < FieldSize.Height; row++)
            {
                for (var column = 0; column < FieldSize.Width; column++)
                {
                    Cells[row, column].Blocked = block;
                }
            }
        }

        /// <summary>
        /// Циклическое раскрытие клеток с нулями
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        public void OpenEmptyCells(int row, int column)
        {
            if (Cells[row, column].Type != CellType.Empty) return;
            Cells[row, column].Pressed = true;
            if (Cells[row, column].Weight != 0) return;
            for (var y = row - 1; y <= row + 1; y++)
            {
                for (var x = column - 1; x <= column + 1; x++)
                {
                    if (x < 0 || y < 0 || x >= Cells.GetLength(1) || y >= Cells.GetLength(0))
                    {
                        continue;
                    }
                    if (!Cells[y, x].Pressed)
                    {
                        OpenEmptyCells(y, x);
                    }
                }
            }
        }

        public int CreateMines(Mine[] mines, Point pressedPoint)
        {
            if (mines == null) return 0;
            //получать ещё и точку, на которую нажали, чтобы не поставить мину на неё
            var emptyCells = GetEmptyCellCount() - 1;
            //получить количество свободных от мин клеток
            var notPlanted = mines.Length;
            foreach (var mine in mines)
            {
                if (!CreateMine(emptyCells, mine, pressedPoint))
                {
                    break;
                }
                notPlanted--;
                emptyCells--;
            }
            CalculateField();
            if (Cells[pressedPoint.Y, pressedPoint.X].Type == CellType.Empty &&
                Cells[pressedPoint.Y, pressedPoint.X].Weight == 0)
            {
                OpenEmptyCells(pressedPoint.Y, pressedPoint.X);
            }
            //вызвать метод рандомизации одной мины нужное количество раз
            //если мину поставить не удалось, значит поле заполнено
            //вернуть количество мин, которые не удалось поставить
            return notPlanted;
        }

        private bool CreateMine(int emptyCells, Mine mine, Point pressedPoint)
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
                    if(pressedPoint.X == column && pressedPoint.Y == row) continue;
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
            return false;
        }

        private int GetEmptyCellCount()
        {
            var count = 0;
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

        private int GetNonpressedCellCount()
        {
            var count = 0;
            for (var row = 0; row < FieldSize.Height; row++)
            {
                for (var column = 0; column < FieldSize.Width; column++)
                {
                    if (!Cells[row, column].Pressed && Cells[row, column].Type != CellType.Mine)
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

        protected virtual void OnCellSelect(Point position, bool flag)
        {
            CellSelect?.Invoke(position, flag);
        }

        protected virtual void OnCellClick(Point position, bool flag)
        {
            CellClick?.Invoke(position, flag);
        }

        protected virtual void OnCellPress(Point position, bool flag)
        {
            if (GetNonpressedCellCount() == 0)
            {
                OnFieldComplete(position);
                BlockField(true);
            }
            CellPress?.Invoke(position, flag);
        }

        protected virtual void OnFieldComplete(Point lastPoint)
        {
            FieldComplete?.Invoke(lastPoint);
        }

        protected virtual void OnCellMarkChanged(Point position, MarkType type)
        {
            CellMarkChanged?.Invoke(position, type);
        }

        protected virtual void OnCellBlockChanged(Point position, bool flag)
        {
            CellBlockChanged?.Invoke(position, flag);
        }
    }
}
