using System;
using System.Drawing;

namespace Miner
{
    /// <summary>
    /// Класс игрового поля
    /// </summary>
    class Field
    {
        /// <summary>
        /// Создание игрового поля
        /// </summary>
        /// <param name="rows">Количество строк</param>
        /// <param name="columns">Количество колонок</param>
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

        /// <summary>
        /// Размер клеток без учёта границ на поле
        /// </summary>
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
        /// <summary>
        /// Массив клеток поля
        /// </summary>
        internal Cell[,] Cells { get; private set; }
   
        /// <summary>
        /// Размер границ клеток на поле
        /// </summary>
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

        /// <summary>
        /// Цвет границ клеток на поле
        /// </summary>
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

        /// <summary>
        /// Начальный цвет градиента на поле
        /// </summary>
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

        /// <summary>
        /// Конечный цвет градиента на поле
        /// </summary>
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

        /// <summary>
        /// Размер поля
        /// </summary>
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
        /// Угол отображения градиента на клетках поля
        /// </summary>
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

        /// <summary>
        /// Коэффициент яркости, применяемый при наведении на клетку
        /// </summary>
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

        /// <summary>
        /// Происходит при выделении какой-либо клетки на поле
        /// </summary>
        public event Action<Point, bool> CellSelect;
        /// <summary>
        /// Происходит при удержании нажатой кнопки мыши над какой-либо клеткой на поле
        /// </summary>
        public event Action<Point, bool> CellClick;
        /// <summary>
        /// Происходит при нажатии на какую-либо клетку на поле
        /// </summary>
        public event Action<Point, bool> CellPress;
        /// <summary>
        /// Происходит при изменении метки на какой-либо клетке
        /// </summary>
        public event Action<Point, MarkType> CellMarkChanged;
        /// <summary>
        /// Происходит при блокировке какой-либо клетки на поле
        /// </summary>
        public event Action<Point, bool> CellBlockChanged;
        /// <summary>
        /// Происходит при открытии всех незаминированных клеток на поле
        /// </summary>
        public event Action<Point> FieldComplete;


        /// <summary>
        /// Переопределяет все ячейки поля
        /// </summary>
        /// <param name="recreate">Флаг пересоздания клеток</param>
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

        /// <summary>
        /// Открыть все клетки на поле
        /// </summary>
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

        /// <summary>
        /// Скрыть все клетки на поле
        /// </summary>
        public void HideField()
        {
            for (var row = 0; row < FieldSize.Height; row++)
            {
                for (var column = 0; column < FieldSize.Width; column++)
                {
                    Cells[row, column].Clear();
                }
            }
        }

        /// <summary>
        /// Заблокировать все клетки на поле
        /// </summary>
        /// <param name="block"></param>
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
        /// Циклическое раскрытие клеток с нулевым весом
        /// </summary>
        /// <param name="row">Номер строки от 0</param>
        /// <param name="column">Номер колонки от 0</param>
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

        /// <summary>
        /// Генерирует на поле нужное количество мин, не трогая клетку, на которую нажали
        /// </summary>
        /// <param name="mines">Мины</param>
        /// <param name="pressedPoint">Нажатая клетка</param>
        /// <returns>Количество мин, которые не удалось поставить (если не хватило места)</returns>
        public int CreateMines(Mine[] mines, Point pressedPoint)
        {
            if (mines == null) return 0;
            var emptyCells = GetEmptyCellCount() - 1;
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
            return notPlanted;
        }

        /// <summary>
        /// Генерирует мину на поле
        /// </summary>
        /// <param name="emptyCells">Количество пустых клеток на поле</param>
        /// <param name="mine">Мина</param>
        /// <param name="pressedPoint">Нажатая кнопка</param>
        /// <returns>Удалось ли поместить мину на поле (хватило ли места)</returns>
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

        /// <summary>
        /// Получает количество незаминированных клеток на поле
        /// </summary>
        /// <returns>Незаминированные клетки</returns>
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

        /// <summary>
        /// Получает количество неоткрытых незаминированных клеток
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Устанавливает мину в указанное место на поле
        /// </summary>
        /// <param name="row">Номер строки от 0</param>
        /// <param name="column">Номер колонки от 0</param>
        /// <param name="mine">Мина</param>
        private void SetMine(int row, int column, Mine mine)
        {
            Cells[row, column].Type = CellType.Mine;
            Cells[row, column].Mine = mine;
        }

        /// <summary>
        /// Рассчитывает веса (количество мин вокруг) всех клеток на поле
        /// </summary>
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

        /// <summary>
        /// Рассчитывает количество мин вокруг указанной клетки
        /// </summary>
        /// <param name="row">Номер строки от 0</param>
        /// <param name="column">Номер колонки от 0</param>
        /// <returns>Количество мин вокруг клетки</returns>
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
