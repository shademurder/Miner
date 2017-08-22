using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Miner
{
    /// <summary>
    /// Класс клетки поля
    /// </summary>
    class Cell : UserControl
    {
        private float _borderSize;
        private float _cellSize;
        private bool _clicked;
        private bool _pressed;
        private bool _selected;
        private int _gradientAngle = 45;
        private Color _startGradientColor;
        private Color _endGradientColor;
        private Color _clickStartGradientColor;
        private Color _clickEndGradientColor;
        private Color _selectStartGradientColor;
        private Color _selectEndGradientColor;
        private Color _borderColor;
        private double _brightnessCoefficient = 1.3;
        private short _weight;
        private CellType _type = CellType.Empty;
        private Mine _mine;
        private MarkType _markType = MarkType.Empty;
        private bool _blocked;

        /// <summary>
        /// Начальный цвет градиента для отображения клетки
        /// </summary>
        public Color StartGradientColor
        {
            get { return _startGradientColor; }
            set
            {
                _startGradientColor = value;
                Refresh();
            }
        }
        /// <summary>
        /// Конечный цвет градиента для отображения клетки
        /// </summary>
        public Color EndGradientColor
        {
            get { return _endGradientColor; }
            set
            {
                _endGradientColor = value;
                Refresh();
            }
        }
        /// <summary>
        /// Начальный цвет градиента для отображения клетки в момент нажатия на неё
        /// </summary>
        public Color ClickStartGradientColor
        {
            get { return _clickStartGradientColor; }
            set
            {
                _clickStartGradientColor = value;
                Refresh();
            }
        }
        /// <summary>
        /// Конечный цвет градиента для отображения клетки в момент нажатия на неё
        /// </summary>
        public Color ClickEndGradientColor
        {
            get { return _clickEndGradientColor; }
            set
            {
                _clickEndGradientColor = value;
                Refresh();
            }
        }
        /// <summary>
        /// Начальный цвет градиента для отображения клетки в момент наведения на неё курсора
        /// </summary>
        public Color SelectStartGradientColor
        {
            get { return _selectStartGradientColor; }
            set
            {
                _selectStartGradientColor = value;
                Refresh();
            }
        }
        /// <summary>
        /// Конечный цвет градиента для отображения клетки в момент наведения на неё курсора
        /// </summary>
        public Color SelectEndGradientColor
        {
            get { return _selectEndGradientColor; }
            set
            {
                _selectEndGradientColor = value;
                Refresh();
            }
        }
        /// <summary>
        /// Угол отображения градиента на клетке
        /// </summary>
        public int GradientAngle
        {
            get { return _gradientAngle; }
            set
            {
                _gradientAngle = value % 360;
                Refresh();
            }
        }

        /// <summary>
        /// Цвет границы ячейки
        /// </summary>
        public Color BorderColor
        {
            get { return _borderColor; }
            set
            {
                _borderColor = value;
                Refresh();
            }
        }
        /// <summary>
        /// Размер границы ячейки
        /// </summary>
        public float BorderSize
        {
            get { return _borderSize; }
            set
            {
                if (value < 0) return;
                _borderSize = value;
                var newSize = new Size((int)Math.Ceiling((CellSize + 2 * value)), (int)Math.Ceiling(CellSize + 2 * value));
                if(Size != newSize)
                    Size = newSize;
                else
                    Refresh();
            }
        }
        /// <summary>
        /// Размер клетки без границ
        /// </summary>
        public float CellSize
        {
            get { return _cellSize; }
            set
            {
                if (value < 0) return;
                _cellSize = value;
                var newSize = new Size((int) Math.Ceiling((value + 2*BorderSize)),
                    (int) Math.Ceiling(value + 2*BorderSize));
                if(Size != newSize)
                    Size = newSize;
                else
                    Refresh();
            }
        }

        /// <summary>
        /// Позиция клетки на поле
        /// </summary>
        public Point Position { get; set; }

        /// <summary>
        /// Коэффициент яркости, применяемый при наведении на клетку
        /// </summary>
        public double BrightnessCoefficient
        {
            get { return _brightnessCoefficient; }
            set
            {
                _brightnessCoefficient = value;
                _selectStartGradientColor = ChangeBrightness(_startGradientColor, BrightnessCoefficient);
                _selectEndGradientColor = ChangeBrightness(_endGradientColor, BrightnessCoefficient);
                Refresh();
            }
        }

        /// <summary>
        /// Определяет факт зажатия кнопки мыши над клеткой
        /// </summary>
        public bool Clicked
        {
            get
            {
                return _clicked;
            }

            set
            {
                if (_clicked == value || Blocked) return;
                _clicked = value;
                OnCellClick(Position, _clicked);
                Refresh();
            }
        }
        /// <summary>
        /// Определяет факт нажатия на клетку
        /// </summary>
        public bool Pressed
        {
            get
            {
                return _pressed;
            }

            set
            {
                if (_pressed == value || Blocked) return;
                _pressed = value;
                OnCellPress(Position, _pressed);
                Refresh();
            }
        }
        /// <summary>
        /// Определяет факт выделения клетки
        /// </summary>
        public bool Selected
        {
            get
            {
                return _selected;
            }

            set
            {
                if (_selected == value || Blocked) return;
                _selected = value;
                OnCellSelect(Position, _selected);
                Refresh();
            }
        }

        /// <summary>
        /// Вес клетки: количество мин вокруг
        /// </summary>
        public short Weight
        {
            get { return _weight; }
            set { _weight = value; }
        }

        /// <summary>
        /// Тип клетки (мина или обычная клетка)
        /// </summary>
        internal CellType Type
        {
            get { return _type; }
            set { _type = value; }
        }

        /// <summary>
        /// Мина, находящаяся в данной клетке
        /// Используется, если тип клетки - мина
        /// </summary>
        internal Mine Mine
        {
            get { return _mine; }
            set { _mine = value; }
        }

        /// <summary>
        /// Блокировка клетки
        /// </summary>
        public bool Blocked
        {
            get
            {
                return _blocked;
            }

            set
            {
                if (_blocked == value) return;
                _blocked = value;
                OnCellBlockChanged(Position, _blocked);
            }
        }

        /// <summary>
        /// Тип метки на клетке (Флаг или знак вопроса)
        /// </summary>
        internal MarkType MarkType
        {
            get
            {
                return _markType;
            }

            private set
            {
                if (_markType == value || Blocked) return;
                _markType = value;
                OnCellMarkChanged(Position, _markType);
            }
        }

        /// <summary>
        /// Происходит при выделении клетки
        /// </summary>
        public event Action<Point, bool> CellSelect;
        /// <summary>
        /// Происходит при нажатии на клетку
        /// </summary>
        public event Action<Point, bool> CellPress;
        /// <summary>
        /// Происходит при удержании нажатой кнопки мыши над клеткой
        /// </summary>
        public event Action<Point, bool> CellClick;
        /// <summary>
        /// Происходит при изменении метки на клетке
        /// </summary>
        public event Action<Point, MarkType> CellMarkChanged;
        /// <summary>
        /// Происходит при блокировке клетки
        /// </summary>
        public event Action<Point, bool> CellBlockChanged;

        public Cell(Color startGradientColor, Color endGradientColor, Color borderColor, float borderSize, float size, Point position)
        {
            ChangeColors(startGradientColor, endGradientColor, false);
            _borderColor = borderColor;
            _borderSize = borderSize;
            _cellSize = size;
            Position = position;
            Size = new Size((int)Math.Ceiling((CellSize + 2 * BorderSize)), (int)Math.Ceiling(CellSize + 2 * BorderSize));
            DoubleBuffered = true;
            UpdateLocation();
        }

        /// <summary>
        /// Очищает клетку
        /// </summary>
        public void Clear()
        {
            _blocked = false;
            _pressed = false;
            _selected = false;
            _clicked = false;
            _type = CellType.Empty;
            _mine = null;
            _markType = MarkType.Empty;
            _weight = 0;
            Refresh();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var fullSize = CellSize + 2 * BorderSize;
            var borderBrush = new SolidBrush(BorderColor);
            e.Graphics.FillRectangle(borderBrush, 0, 0, fullSize, fullSize);
            //e.Graphics.DrawRectangle(pen, new Rectangle(0, 0, CellSize + BorderSize, CellSize + BorderSize));
            //e.Graphics.DrawRectangle(pen, 0, 0, CellSize + BorderSize, CellSize + BorderSize);
            var state = Clicked && Selected || Pressed;
            //var brush = new LinearGradientBrush(new PointF(0, 0), new PointF(sizeF, sizeF),
            var brush = new LinearGradientBrush(new RectangleF(0, 0, CellSize, CellSize),//(0, 0, CellSize, CellSize)
                state ? ClickStartGradientColor : Selected ? SelectStartGradientColor : StartGradientColor,
                state ? ClickEndGradientColor : Selected ? SelectEndGradientColor : EndGradientColor, 
                GradientAngle);
            e.Graphics.FillRectangle(brush, BorderSize, BorderSize, CellSize, CellSize);
            var rectangle = new RectangleF(BorderSize + CellSize * 0.1F, BorderSize + CellSize * 0.1F, CellSize * 0.8F, CellSize * 0.8F);
            var stringFormat = StringFormat.GenericDefault;
            stringFormat.Alignment = StringAlignment.Center;
            var font = new Font("Arial", _cellSize * 0.6F);
            if (Pressed)
            {
                if (_type == CellType.Mine)
                {
                    e.Graphics.DrawImage(Mine.Image, rectangle);
                }
                else if(_type == CellType.Empty && _weight != 0)
                {
                    e.Graphics.DrawString(_weight.ToString(), font, new SolidBrush(Color.Red), rectangle, stringFormat);
                }
            }
            else
            {
                if (MarkType == MarkType.Flag)
                {
                    var image = Properties.Resources.Flag;
                    e.Graphics.DrawImage(image, rectangle);
                }
                else if (MarkType == MarkType.Unknown)
                {
                    e.Graphics.DrawString("?", font, new SolidBrush(Color.Red), rectangle, stringFormat);
                }
            }
            //e.Graphics.DrawRectangle(pen, 0, 0, (int)Math.Round(sizeF), (int)Math.Round(sizeF));
            base.OnPaint(e);
        }

        /// <summary>
        /// Применяются указанные цвета градиента к ячейке
        /// Высчитываются цвета градиентов для состояния нажатия и наведения мыши
        /// </summary>
        /// <param name="startGradientColor">Начальный цвет градиента клетки</param>
        /// <param name="endGradientColor">Конечный цвет градиента клетки</param>
        /// <param name="refresh">Нужно ли обновлять внешний вид поля</param>
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

        /// <summary>
        /// Применить модификатор яркости к указанному цвету
        /// </summary>
        /// <param name="color">Цвет</param>
        /// <param name="coefficient">Коэффициент яркости</param>
        /// <returns></returns>
        private Color ChangeBrightness(Color color, double coefficient)
        {
            var r = color.R * coefficient;
            var g = color.G * coefficient;
            var b = color.B * coefficient;
            return Color.FromArgb((byte)(r >= 255 ? 255 : r), (byte)(g >= 255 ? 255 : g), (byte)(b >= 255 ? 255 : b));
        }

        /// <summary>
        /// Перерассчитать положение клетки
        /// </summary>
        private void UpdateLocation()
        {
            var step = CellSize + BorderSize;
            Location = new Point((int)(Position.X * step), (int)(Position.Y * step));
            Refresh();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Clicked = true;
                Capture = false;
            }
            if(e.Button == MouseButtons.Right)
            {
                if(!Pressed)
                {
                    if(MarkType == MarkType.Empty)
                    {
                        MarkType = MarkType.Flag;
                    }
                    else if(MarkType == MarkType.Flag)
                    {
                        MarkType = MarkType.Unknown;
                    }
                    else
                    {
                        MarkType = MarkType.Empty;
                    }
                }
            }
            Refresh();
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (Selected && ClientRectangle.Contains(PointToClient(Cursor.Position)) && e.Button == MouseButtons.Left)
            {
                if (MarkType == MarkType.Empty)
                {
                    Pressed = true;
                }
            }
            Clicked = false;
            Refresh();
            base.OnMouseUp(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            Selected = false;
            Refresh();
            base.OnMouseLeave(e);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            Selected = true;
            Refresh();
            base.OnMouseEnter(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            Selected = ClientRectangle.Contains(PointToClient(Cursor.Position));
            Clicked = Selected && e.Button == MouseButtons.Left;
            Refresh();
            base.OnMouseMove(e);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            UpdateLocation();
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

        protected virtual void OnCellMarkChanged(Point point, MarkType type)
        {
            CellMarkChanged?.Invoke(point, type);
        }

        protected virtual void OnCellBlockChanged(Point point, bool state)
        {
            CellBlockChanged?.Invoke(point, state);
        }
    }
}
