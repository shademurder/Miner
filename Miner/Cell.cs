using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Miner
{
    class Cell : UserControl
    {
        private float _borderSize = 1;
        private float _cellSize = 20;
        private bool _clicked = false;
        private bool _pressed = false;
        private bool _selected = false;
        private int _gradientAngle = 45;
        private Color _startGradientColor;
        private Color _endGradientColor;
        private Color _clickStartGradientColor;
        private Color _clickEndGradientColor;
        private Color _selectStartGradientColor;
        private Color _selectEndGradientColor;
        private Color _borderColor;
        private double _brightnessCoefficient = 1.3;
        private short _weight = 0;
        private CellType _type = CellType.Empty;
        private Mine _mine = null;
        private MarkType _markType = MarkType.Empty;

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
        public int GradientAngle
        {
            get { return _gradientAngle; }
            set
            {
                _gradientAngle = value % 360;
                Refresh();
            }
        }

        public Color BorderColor
        {
            get { return _borderColor; }
            set
            {
                _borderColor = value;
                Refresh();
            }
        }
        public float BorderSize
        {
            get { return _borderSize; }
            set
            {
                if (value < 0) return;
                _borderSize = value;
                Size = new Size((int)Math.Ceiling((CellSize + 2 * value)), (int)Math.Ceiling(CellSize + 2 * value));
            }
        }
        public float CellSize
        {
            get { return _cellSize; }
            set
            {
                if (value < 0) return;
                _cellSize = value;
                Size = new Size((int)Math.Ceiling((value + 2 * BorderSize)), (int)Math.Ceiling(value + 2 * BorderSize));
            }
        }

        public Point Position { get; set; }

        public double BrightnessCoefficient
        {
            get { return _brightnessCoefficient; }
            set
            {
                _brightnessCoefficient = value;
                Refresh();
            }
        }

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

        public short Weight { get => _weight; set => _weight = value; }
        internal CellType Type { get => _type; set => _type = value; }
        internal Mine Mine { get => _mine; set => _mine = value; }

        public event Action<Point, bool> CellSelect;
        public event Action<Point, bool> CellPress;
        public event Action<Point, bool> CellClick;

        public Cell(Color startGradientColor, Color endGradientColor, Color borderColor, float borderSize, float size, Point position)
        {
            ChangeColors(startGradientColor, endGradientColor, false);
            _borderColor = borderColor;
            _borderSize = borderSize;
            _cellSize = size;
            Position = position;
            Size = new Size((int)Math.Ceiling((CellSize + 2 * BorderSize)), (int)Math.Ceiling(CellSize + 2 * BorderSize));
            DoubleBuffered = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            float sizeF = CellSize + 2 * BorderSize;
            var pen = new Pen(BorderColor, BorderSize);
            var borderBrush = new SolidBrush(BorderColor);
            e.Graphics.FillRectangle(borderBrush, 0, 0, sizeF, sizeF);
            //e.Graphics.DrawRectangle(pen, new Rectangle(0, 0, CellSize + BorderSize, CellSize + BorderSize));
            //e.Graphics.DrawRectangle(pen, 0, 0, CellSize + BorderSize, CellSize + BorderSize);
            var state = _clicked && _selected || _pressed;
             //var brush = new LinearGradientBrush(new PointF(0, 0), new PointF(sizeF, sizeF),
            var brush = new LinearGradientBrush(new RectangleF(0, 0, CellSize, CellSize),//(0, 0, CellSize, CellSize)
                state ? ClickStartGradientColor : _selected ? SelectStartGradientColor : StartGradientColor,
                state ? ClickEndGradientColor : _selected ? SelectEndGradientColor : EndGradientColor, 
                GradientAngle);
            e.Graphics.FillRectangle(brush, BorderSize, BorderSize, CellSize, CellSize);
            var rectangle = new RectangleF(BorderSize + CellSize * 0.1F, BorderSize + CellSize * 0.1F, CellSize * 0.8F, CellSize * 0.8F);
            StringFormat sf = StringFormat.GenericDefault;
            sf.Alignment = StringAlignment.Center;
            Font font = new Font("Arial", _cellSize * 0.6F);
            if (_pressed)
            {
                if (_type == CellType.Mine)
                {
                    e.Graphics.DrawImage(Mine.Image, rectangle);
                }
                else if(_type == CellType.Empty)
                {
                    e.Graphics.DrawString(_weight.ToString(), font, new SolidBrush(Color.Red), rectangle, sf);
                }
            }
            else
            {
                if (_markType == MarkType.Flag)
                {
                    var image = Properties.Resources.Flag1;
                    e.Graphics.DrawImage(image, rectangle);
                }
                else if (_markType == MarkType.Unknown)
                {
                    e.Graphics.DrawString("?", font, new SolidBrush(Color.Red), rectangle, sf);
                }
            }
            //e.Graphics.DrawRectangle(pen, 0, 0, (int)Math.Round(sizeF), (int)Math.Round(sizeF));
            base.OnPaint(e);
        }

        public void ChangeColors(Color startGradientColor, Color endGradientColor, bool refresh)
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
            if(refresh)
            {
                Refresh();
            }
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
            if(e.Button == MouseButtons.Right)
            {
                if(!_pressed)
                {
                    if(_markType == MarkType.Empty)
                    {
                        _markType = MarkType.Flag;
                    }
                    else if(_markType == MarkType.Flag)
                    {
                        _markType = MarkType.Unknown;
                    }
                    else
                    {
                        _markType = MarkType.Empty;
                    }
                }
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
            var step = CellSize + BorderSize;
            Location = new Point((int)(Position.X * step), (int)(Position.Y * step));
            Refresh();
            base.OnSizeChanged(e);
        }

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
