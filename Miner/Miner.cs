using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Miner
{
    class Miner : UserControl
    {
        public Miner()
        {
            DoubleBuffered = true;
            Field = new Field(FieldSize.Height, FieldSize.Width);
            Clear();
        }

        private Field _field = new Field(9, 9);
        private int _errors = 1;
        /// <summary>
        /// Количество допустимых ошибок, заданное пользователем
        /// </summary>
        private int _userErrors = 1;
        private GameState _gameState = GameState.NotStarted;
        /// <summary>
        /// Список мин на поле
        /// </summary>
        private readonly List<Mine> _mines = new List<Mine>();


        /// <summary>
        /// Размер поля
        /// </summary>
        [Category("Внешний вид поля"), Description("Размер поля")]
        public Size FieldSize
        {
            get
            {
                return Field.FieldSize;
            }
            set
            {
                if (value.Height <= 0 || value.Width <= 0) return;
                Field.FieldSize = value;
                Clear();
            }
        }

        /// <summary>
        /// Размер клеток без учёта границ на поле
        /// </summary>
        [Category("Внешний вид поля"), Description("Размер клеток без учёта границ")]
        public float CellSize
        {
            get { return Field.CellSize; }
            set
            {
                Field.CellSize = value;
                ReSize();
            }
        }
        /// <summary>
        /// Размер границ клеток на поле
        /// </summary>
        [Category("Внешний вид поля"), Description("Размер границ клеток на поле")]
        public float CellBorderSize
        {
            get { return Field.BorderSize; }
            set
            {
                Field.BorderSize = value;
                ReSize();
            }
        }

        /// <summary>
        /// Игровое поле
        /// </summary>
        public Field Field
        {
            get { return _field; }
            private set
            {
                _field = value;
                Clear();
            }
        }
        /// <summary>
        /// Цвет границ клеток на поле
        /// </summary>
        [Category("Внешний вид поля"), Description("Цвет границ клеток на поле")]
        public Color BorderColor
        {
            get { return Field.BorderColor; }
            set
            {
                Field.BorderColor = value;
            }
        }

        /// <summary>
        /// Начальный цвет градиента на поле
        /// </summary>
        [Category("Внешний вид поля"), Description("Начальный цвет градиента на поле")]
        public Color StartFieldColor
        {
            get { return Field.StartFieldColor; }
            set
            {
                Field.StartFieldColor = value;
            }
        }
        /// <summary>
        /// Конечный цвет градиента на поле
        /// </summary>
        [Category("Внешний вид поля"), Description("Конечный цвет градиента на поле")]
        public Color EndFieldColor
        {
            get { return Field.EndFieldColor; }
            set
            {
                Field.EndFieldColor = value;
            }
        }

        /// <summary>
        /// Коэффициент яркости, применяемый при наведении на клетку
        /// </summary>
        [Category("Внешний вид поля"), Description("Коэффициент яркости, применяемый при наведении на клетку")]
        public double BrightnessCoefficient
        {
            get { return Field.BrightnessCoefficient; }
            set { Field.BrightnessCoefficient = value; }
        }

        /// <summary>
        /// Угол отображения градиента на клетках поля
        /// </summary>
        [Category("Внешний вид поля"), Description("Угол отображения градиента на клетках поля")]
        public int GradientAngle
        {
            get { return Field.GradientAngle; }
            set { Field.GradientAngle = value; }
        }

        /// <summary>
        /// Количество оставшихся допустимых ошибок
        /// </summary>
        [Category("Особенности поля"), Description("Количество оставшихся допустимых ошибок")]
        public int Errors
        {
            get
            {
                return _errors >= 0 ? _errors : 0;
            }

            set
            {
                if (_gameState == GameState.NotStarted)
                {
                    _userErrors = value;
                }
                _errors = value;
                OnErrorsChanged(value);
            }
        }

        /// <summary>
        /// Состояние игры
        /// </summary>
        [Category("Особенности поля"), Description("Состояние игры")]
        public GameState GameState
        {
            get
            {
                return _gameState;
            }

            private set
            {
                _gameState = value;
                OnGameStateChanged(value);
            }
        }

        /// <summary>
        /// Максимально допустимое количество мин на поле (90% поля)
        /// </summary>
        [Category("Особенности поля"), Description("Максимально допустимое количество мин на поле (90% поля)")]
        public int MaxMines => Field != null ? (int)(FieldSize.Width * FieldSize.Height * 0.9) : 0;

        /// <summary>
        /// Количество мин на поле
        /// </summary>
        [Category("Особенности поля"), Description("Количество мин на поле")]
        public int Mines => _mines?.Count ?? 0;


        /// <summary>
        /// Происходит при выделении какой-либо клетки на поле
        /// </summary>
        [Category("Изменение состояния клетки"), Description("Выделение клетки")]
        public event Action<Point, bool> CellSelect;
        /// <summary>
        /// Происходит при удержании нажатой кнопки мыши над какой-либо клеткой на поле
        /// </summary>
        [Category("Изменение состояния клетки"), Description("Удержание нажатой кнопки мыши над клеткой")]
        public event Action<Point, bool> CellClick;
        /// <summary>
        /// Происходит при нажатии на какую-либо клетку на поле
        /// </summary>
        [Category("Изменение состояния клетки"), Description("Нажатие на клетку")]
        public event Action<Point, bool> CellPress;
        /// <summary>
        /// Происходит при изменении метки на какой-либо клетке
        /// </summary>
        [Category("Изменение состояния клетки"), Description("Изменение метки на клетке")]
        public event Action<Point, MarkType> CellMarkChanged;
        /// <summary>
        /// Происходит при блокировке какой-либо клетки на поле
        /// </summary>
        [Category("Изменение состояния клетки"), Description("Блокировка клетки")]
        public event Action<Point, bool> CellBlockChanged;
        /// <summary>
        /// Происходит при изменении количества допустимых ошибок:
        /// при попадании на мину или при ручном обновлении
        /// </summary>
        [Category("Изменения в игровом процессе"), Description("Изменение количества оставшихся допустимых попыток")]
        public event Action<int> ErrorsChanged;
        /// <summary>
        /// Происходит при изменении состояния игры
        /// </summary>
        [Category("Изменения в игровом процессе"), Description("Изменение состояния игры")]
        public event Action<GameState> GameStateChanged;
        /// <summary>
        /// Происходит в конце игры:
        /// если допущено максимальное количество ошибок или на поле открыты все незаминированные ячейки
        /// </summary>
        [Category("Изменения в игровом процессе"), Description("Игра закончена")]
        public event Action<Point, bool> GameOver;

        
        /// <summary>
        /// Переопределяются все контролы(ячейки) на поле
        /// </summary>
        private void RecreateField()
        {
            Controls.Clear();
            for (var row = 0; row < FieldSize.Height; row++)
            {
                for (var column = 0; column < FieldSize.Width; column++)
                {
                    Controls.Add(Field.Cells[row, column]);
                }
            }
        }

        /// <summary>
        /// Перерассчёт размера поля
        /// </summary>
        private void ReSize()
        {
            var width = FieldSize.Width * (CellSize + CellBorderSize) + CellBorderSize;
            var height = FieldSize.Height * (CellSize + CellBorderSize) + CellBorderSize;
            if (Size.Width != width || Size.Height != height)
            {
                Size = new Size((int)width, (int)height);
            }
        }


        //Инструменты внешнего управления

        /// <summary>
        /// Отмена выделения клетки
        /// </summary>
        /// <param name="row">Номер строки на поле от 0</param>
        /// <param name="column">Номер колонки на поле от 0</param>
        public void UnselectCell(int row, int column)
        {
            Field.Cells[row, column].Selected = false;
        }
        /// <summary>
        /// Выделение клетки
        /// </summary>
        /// <param name="row">Номер строки на поле от 0</param>
        /// <param name="column">Номер колонки на поле от 0</param>
        public void SelectCell(int row, int column)
        {
            Field.Cells[row, column].Selected = true;
        }

        /// <summary>
        /// Отменяет наведение зажатой кнопки мыши над клеткой
        /// </summary>
        /// <param name="row">Номер строки на поле от 0</param>
        /// <param name="column">Номер колонки на поле от 0</param>
        public void UnClickCell(int row, int column)
        {
            Field.Cells[row, column].Clicked = false;
        }
        /// <summary>
        /// Наведение зажатой кнопки мыши над клеткой
        /// </summary>
        /// <param name="row">Номер строки на поле от 0</param>
        /// <param name="column">Номер колонки на поле от 0</param>
        public void ClickCell(int row, int column)
        {
            Field.Cells[row, column].Clicked = true;
        }

        /// <summary>
        /// Отменяет нажатие на клетку
        /// </summary>
        /// <param name="row">Номер строки на поле от 0</param>
        /// <param name="column">Номер колонки на поле от 0</param>
        public void UnpressCell(int row, int column)
        {
            Field.Cells[row, column].Pressed = false;
        }
        /// <summary>
        /// Нажатие на клетку
        /// </summary>
        /// <param name="row">Номер строки на поле от 0</param>
        /// <param name="column">Номер колонки на поле от 0</param>
        public void PressCell(int row, int column)
        {
            Field.Cells[row, column].Pressed = true;
        }

        /// <summary>
        /// Отменяет блокировку клетки
        /// </summary>
        /// <param name="row">Номер строки на поле от 0</param>
        /// <param name="column">Номер колонки на поле от 0</param>
        public void UnblockCell(int row, int column)
        {
            Field.Cells[row, column].Blocked = false;
        }
        /// <summary>
        /// Блокирует клетку
        /// </summary>
        /// <param name="row">Номер строки на поле от 0</param>
        /// <param name="column">Номер колонки на поле от 0</param>
        public void BlockCell(int row, int column)
        {
            Field.Cells[row, column].Blocked = true;
        }

        /// <summary>
        /// Снимает маркер с клетки
        /// </summary>
        /// <param name="row">Номер строки на поле от 0</param>
        /// <param name="column">Номер колонки на поле от 0</param>
        public void UnMarkCell(int row, int column)
        {
            Field.Cells[row, column].MarkType = MarkType.Empty;
        }
        /// <summary>
        /// Маркирует указанную клетку на поле следующим по очереди маркером
        /// </summary>
        /// <param name="row">Номер строки на поле от 0</param>
        /// <param name="column">Номер колонки на поле от 0</param>
        public void MarkCellNext(int row, int column)
        {
            if(Field.Cells[row, column].MarkType == MarkType.Empty)
            {
                Field.Cells[row, column].MarkType = MarkType.Flag;
            }
            else if(Field.Cells[row, column].MarkType == MarkType.Flag)
            {
                Field.Cells[row, column].MarkType = MarkType.Unknown;
            }
            else if (Field.Cells[row, column].MarkType == MarkType.Unknown)
            {
                Field.Cells[row, column].MarkType = MarkType.Empty;
            }
        }
        /// <summary>
        /// Маркирует клетку указанным маркером
        /// </summary>
        /// <param name="row">Номер строки на поле от 0</param>
        /// <param name="column">Номер колонки на поле от 0</param>
        /// <param name="type">Маркер</param>
        public void MarkCell(int row, int column, MarkType type)
        {
            Field.Cells[row, column].MarkType = type;
        }

        /// <summary>
        /// Добавляет на поле указанную мину нужное количество раз
        /// Не больше, чем 90% от области поля
        /// </summary>
        /// <param name="mines">Количество мин</param>
        /// <param name="mine">Мина</param>
        public void AddMines(int mines, Mine mine)
        {
            for (var iter = 0; iter < mines; iter++)
            {
                if (_mines.Count < MaxMines)
                {
                    _mines.Add(mine);
                }
            }
        }

        /// <summary>
        /// Удаляет все мины из списка
        /// </summary>
        public void ClearMines()
        {
            _mines.Clear();
        }

        /// <summary>
        /// Переопределяет поле
        /// </summary>
        private void Clear()
        {
            ReSize();
            RecreateField();
            Field.CellClick += OnCellClick;
            Field.CellSelect += OnCellSelect;
            Field.CellPress += OnCellPress;
            Field.FieldComplete += OnFieldComplete;
            Field.CellMarkChanged += OnCellMarkChanged;
            Field.CellBlockChanged += OnCellBlockChanged;
            if (_gameState != GameState.Playing) return;
            NewGame();
        }

        /// <summary>
        /// Начало новой игры
        /// </summary>
        public void NewGame()
        {
            //if (_gameState != GameState.Playing) return;
            Field.HideField();
            _errors = _userErrors;
            GameState = GameState.NotStarted;
        }


        protected override void OnSizeChanged(EventArgs e)
        {
            //отношение ширины поля к высоте
            var proportions = (double)(Field.FieldSize.Width * (CellSize + CellBorderSize) + CellBorderSize) /
                                      (Field.FieldSize.Height * (CellSize + CellBorderSize) + CellBorderSize);
            //применяется наибольший размер с учётом пропорций
            if((int)(Field.FieldSize.Width * (CellSize + CellBorderSize) + CellBorderSize) != Size.Width ||
                (int)(Field.FieldSize.Height * (CellSize + CellBorderSize) + CellBorderSize) != Size.Height)
            Size = Size.Width > (Size.Height * proportions) ? new Size(Size.Width, (int)(Size.Width / proportions)) : new Size((int)(Size.Height * proportions), Size.Height);

            //новый рассчётный размер клетки без учёта границ
            var newCellSize = (Size.Width - CellBorderSize) / FieldSize.Width - CellBorderSize;
            if (newCellSize != Field.CellSize)
            {
                Field.CellSize = newCellSize;
                Size = new Size(Field.Cells[FieldSize.Height - 1, FieldSize.Width - 1].Location.X + Field.Cells[FieldSize.Height - 1, FieldSize.Width - 1].Size.Width,
                    Field.Cells[FieldSize.Height - 1, FieldSize.Width - 1].Location.Y + Field.Cells[FieldSize.Height - 1, FieldSize.Width - 1].Size.Height);
            }
            Refresh();
            base.OnSizeChanged(e);
        }

        protected virtual void OnCellPress(Point position, bool flag)
        {
            if (GameState == GameState.NotStarted)
            {
                GameState = GameState.Playing;
                Field.CreateMines(_mines.ToArray(), position);
            }
            if (Field.Cells[position.Y, position.X].Type == CellType.Empty &&
                Field.Cells[position.Y, position.X].Weight == 0)
            {
                Field.OpenEmptyCells(position.Y, position.X);
            }
            if (Field.Cells[position.Y, position.X].Type == CellType.Mine)
            {
                Errors--;
            }
            if (_errors == 0)
            {
                _errors--;
                Field.OpenField();
                OnGameOver(position, false);
            }
            CellPress?.Invoke(position, flag);
        }

        protected virtual void OnCellSelect(Point position, bool flag)
        {
            CellSelect?.Invoke(position, flag);
        }

        protected virtual void OnCellClick(Point position, bool flag)
        {
            CellClick?.Invoke(position, flag);
        }

        protected virtual void OnFieldComplete(Point lastPoint)
        {
            if (_errors > 0)
                OnGameOver(lastPoint, true);
        }

        protected virtual void OnGameOver(Point lastPoint, bool win)
        {
            if (GameState == GameState.Playing)
            {
                GameState = win ? GameState.Win : GameState.Lose;
            }
            else
            {
                return;
            }
            GameOver?.Invoke(lastPoint, win);
        }

        protected virtual void OnErrorsChanged(int errors)
        {
            ErrorsChanged?.Invoke(errors);
        }

        protected virtual void OnCellMarkChanged(Point position, MarkType type)
        {
            CellMarkChanged?.Invoke(position, type);
        }

        protected virtual void OnCellBlockChanged(Point position, bool flag)
        {
            CellBlockChanged?.Invoke(position, flag);
        }

        protected virtual void OnGameStateChanged(GameState gameState)
        {
            GameStateChanged?.Invoke(gameState);
        }
    }
}
