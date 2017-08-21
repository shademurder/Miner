using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;

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
        private int _userErrors = 1;
        private GameState _gameState = GameState.NotStarted;
        private readonly List<Mine> _mines = new List<Mine>();


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

        public float CellSize
        {
            get { return Field.CellSize; }
            set
            {
                Field.CellSize = value;
                ReSize();
            }
        }
        public float CellBorderSize
        {
            get { return Field.BorderSize; }
            set
            {
                Field.BorderSize = value;
                ReSize();
            }
        }

        public Field Field
        {
            get { return _field; }
            private set
            {
                _field = value;
                Clear();
            }
        }
        public Color BorderColor
        {
            get { return Field.BorderColor; }
            set
            {
                Field.BorderColor = value;
            }
        }

        public Color StartFieldColor
        {
            get { return Field.StartFieldColor; }
            set
            {
                Field.StartFieldColor = value;
            }
        }
        public Color EndFieldColor
        {
            get { return Field.EndFieldColor; }
            set
            {
                Field.EndFieldColor = value;
            }
        }

        public double BrightnessCoefficient
        {
            get { return Field.BrightnessCoefficient; }
            set { Field.BrightnessCoefficient = value; }
        }

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

        public int MaxMines => Field != null ? (int) (FieldSize.Width*FieldSize.Height*0.9) : 0;

        public int GradientAngle { get; set; } = 45;

        public int Mines => _mines?.Count ?? 0;


        public event Action<Point, bool> CellSelect;
        public event Action<Point, bool> CellClick;
        public event Action<Point, bool> CellPress;
        public event Action<Point, MarkType> CellMarkChanged;
        public event Action<Point, bool> CellBlockChanged;
        public event Action<int> ErrorsChanged;
        public event Action<GameState> GameStateChanged;
        public event Action<Point, bool> GameOver;


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
        public void UnselectCell(int row, int column)
        {
            Field.Cells[row, column].Selected = false;
        }
        public void SelectCell(int row, int column)
        {
            Field.Cells[row, column].Selected = true;
        }

        public void UnClickCell(int row, int column)
        {
            Field.Cells[row, column].Clicked = false;
        }
        public void ClickCell(int row, int column)
        {
            Field.Cells[row, column].Clicked = true;
        }

        public void UnpressCell(int row, int column)
        {
            Field.Cells[row, column].Pressed = false;
        }
        public void PressCell(int row, int column)
        {
            Field.Cells[row, column].Pressed = true;
        }

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

        public void ClearMines()
        {
            _mines.Clear();
        }

        public void Clear()
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
            Field.HideField();
            _errors = _userErrors;
            GameState = GameState.NotStarted;
        }

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
            var proportions = (double) (Field.FieldSize.Width*(CellSize + CellBorderSize) + CellBorderSize) /
                                      (Field.FieldSize.Height*(CellSize + CellBorderSize) + CellBorderSize);
            //применяется наибольший размер с учётом пропорций
            Size = Size.Width > Size.Height * proportions ? new Size(Size.Width, (int)(Size.Width / proportions)) : new Size((int)(Size.Height * proportions), Size.Height);
            var newCellSize = (Size.Width - CellBorderSize) / FieldSize.Width - CellBorderSize;
            if (newCellSize != Field.CellSize)
            {
                Field.CellSize = newCellSize;
            }
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
