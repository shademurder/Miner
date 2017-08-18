using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Miner
{
    class Cell : Control
    {
        private int _borderSize = 1;
        private int _cellSize = 20;
        private bool _clicked = false;
        private bool _pressed = false;
        private bool _selected = false;
        private Color _startGradientColor;
        private Color _endGradientColor;
        private Color _clickStartGradientColor;
        private Color _clickEndGradientColor;
        private Color _selectStartGradientColor;
        private Color _selectEndGradientColor;
        private Color _borderColor;

        //TODO после каждого изменения увета в полях делать Refresh
        public Color StartGradientColor
        {
            get { return _startGradientColor; }
            set
            {
                _startGradientColor = value;
                Refresh();
            }
        }
        public Color EndGradientColor
        {
            get { return _endGradientColor; }
            set
            {
                _endGradientColor = value;
                Refresh();
            }
        }
        public Color ClickStartGradientColor
        {
            get { return _clickStartGradientColor; }
            set
            {
                _clickStartGradientColor = value;
                Refresh();
            }
        }
        public Color ClickEndGradientColor
        {
            get { return _clickEndGradientColor; }
            set
            {
                _clickEndGradientColor = value;
                Refresh();
            }
        }
        public Color SelectStartGradientColor
        {
            get { return _selectStartGradientColor; }
            set
            {
                _selectStartGradientColor = value;
                Refresh();
            }
        }
        public Color SelectEndGradientColor
        {
            get { return _selectEndGradientColor; }
            set
            {
                _selectEndGradientColor = value;
                Refresh();
            }
        }
        public int GradientAngle { get; set; } = 45;

        public Color BorderColor
        {
            get { return _borderColor; }
            set
            {
                _borderColor = value;
                Refresh();
            }
        }
        public int BorderSize
        {
            get { return _borderSize; }
            set
            {
                if (value < 0) return;
                _borderSize = value;
                Size = new Size(CellSize + value, CellSize + value);
                Refresh();
            }
        }
        public int CellSize
        {
            get { return _cellSize; }
            set
            {
                if (value < 20) return;
                _cellSize = value;
                Size = new Size(value + BorderSize, value + BorderSize);
                Refresh();
            }
        }

        public Point Position { get; set; }

        public double BrightnessCoefficient { get; set; } = 1.3;

        public bool Clicked
        {
            get
            {
                return _clicked;
            }

            set
            {
                _clicked = value;
                OnCellClick(Position, _clicked);
            }
        }
        public bool Pressed
        {
            get
            {
                return _pressed;
            }

            set
            {
                _pressed = value;
                OnCellPress(Position, _pressed);
            }
        }
        public bool Selected
        {
            get
            {
                return _selected;
            }

            set
            {
                _selected = value;
                OnCellSelect(Position, _selected);
            }
        }

        public event Action<Point, bool> CellSelect;
        public event Action<Point, bool> CellPress;
        public event Action<Point, bool> CellClick;

        public Cell(Color startGradientColor, Color endGradientColor, Color borderColor, int borderSize, int size, Point position)
        {
            _startGradientColor = startGradientColor;
            _endGradientColor = endGradientColor;
            //0.3, 0.59, 0.11 - коэффициенты преобразования цвета в ч/б
            var startColorless = (byte)((startGradientColor.R * 0.3) + (startGradientColor.G * 0.59) + (startGradientColor.B * 0.11));
            var endColorless = (byte)((endGradientColor.R * 0.3) + (endGradientColor.G * 0.59) + (endGradientColor.B * 0.11));
            _clickStartGradientColor = Color.FromArgb(startColorless, startColorless, startColorless);
            _clickEndGradientColor = Color.FromArgb(endColorless, endColorless, endColorless);
            _selectStartGradientColor = ChangeBrightness(startGradientColor, BrightnessCoefficient);
            _selectEndGradientColor = ChangeBrightness(endGradientColor, BrightnessCoefficient);
            _borderColor = borderColor;
            _borderSize = borderSize;
            _cellSize = size;
            Position = position;
            Size = new Size(CellSize + 2 * BorderSize, CellSize + 2 * BorderSize);
            MinimumSize = new Size(20, 20);
            DoubleBuffered = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var pen = new Pen(BorderColor, BorderSize);
            e.Graphics.DrawRectangle(pen, new Rectangle(0, 0, CellSize + BorderSize, CellSize + BorderSize));
            var state = _clicked && _selected || _pressed;
            var brush = new LinearGradientBrush(new Rectangle(0, 0, CellSize, CellSize),
                state ? ClickStartGradientColor : _selected ? SelectStartGradientColor : StartGradientColor,
                state ? ClickEndGradientColor : _selected ? SelectEndGradientColor : EndGradientColor, 
                GradientAngle);
            e.Graphics.FillRectangle(brush, BorderSize, BorderSize, CellSize, CellSize);
            base.OnPaint(e);
        }

        private Color ChangeBrightness(Color color, double coefficient)
        {
            var r = color.R * coefficient;
            var g = color.G * coefficient;
            var b = color.B * coefficient;
            return Color.FromArgb((byte)(r >= 255 ? 255 : r), (byte)(g >= 255 ? 255 : g), (byte)(b >= 255 ? 255 : b));
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _clicked = true;
                Capture = false;
            }
            Refresh();
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (_selected && ClientRectangle.Contains(PointToClient(Cursor.Position)) && e.Button == MouseButtons.Left)
            {
                _pressed = true;
            }
            _clicked = false;
            Refresh();
            base.OnMouseUp(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            _selected = false;
            Refresh();
            base.OnMouseLeave(e);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            _selected = true;
            Refresh();
            base.OnMouseEnter(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            _selected = ClientRectangle.Contains(PointToClient(Cursor.Position));
            _clicked = _selected && e.Button == MouseButtons.Left;
            Refresh();
            base.OnMouseMove(e);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            if (Size.Width != Size.Height)
            {
                Size = new Size(Size.Width, Size.Width);
            }
            if (BorderSize == 0)
            {
                if (CellSize != Size.Width)
                {
                    CellSize = Size.Width;
                }
            }
            else
            {
                var proportions = (double)BorderSize/CellSize;
                var newBorderSize = (int)(proportions * Size.Width);
                if (BorderSize != newBorderSize || CellSize != Size.Width - newBorderSize)
                {
                    BorderSize = newBorderSize;
                    CellSize = Size.Width - BorderSize;
                }
            }
            Location = new Point(Position.X * (CellSize + BorderSize), Position.Y * (CellSize + BorderSize));
            Refresh();
            base.OnSizeChanged(e);
        }

        //Можно задать клетке начальный и конечный цвет, чтобы рисовать градиент

        protected virtual void OnCellSelect(Point point, bool state)
        {
            CellSelect?.Invoke(point, state);
        }

        protected virtual void OnCellPress(Point point, bool state)
        {
            CellPress?.Invoke(point, state);
        }

        protected virtual void OnCellClick(Point point, bool state)
        {
            CellClick?.Invoke(point, state);
        }
    }
}
