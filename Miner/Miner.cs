using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Miner
{
    class Miner : Control
    {
        public Miner()
        {
            DoubleBuffered = true;
            Clear();
        }

        private Size _fieldSize = new Size(9, 9);
        private Field _field = new Field(9, 9);

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
                Clear();
            }
        }

        public int CellSize
        {
            get { return Field.CellSize; }
            set
            {
                Field.CellSize = value;
                ReSize();
            }
        }

        public int CellBorderSize
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
                RecreateField();// + constructor
                ReSize();
            }
        }
        //TODO все данные поля должны храниться в поле!
        public Color BorderColor
        {
            get { return Field.BorderColor; }
            set
            {
                Field.BorderColor = value;
                //RecreateField(); -
            }
        }

        public Color StartFieldColor
        {
            get { return Field.StartFieldColor; }
            set
            {
                Field.StartFieldColor = value;
                //RecreateField(); +
            }
        }
        public Color EndFieldColor
        {
            get { return Field.EndFieldColor; }
            set
            {
                Field.EndFieldColor = value;
                //RecreateField(); +
            }
        }

        public event Action<Point, bool> CellSelect;
        public event Action<Point, bool> CellClick;
        public event Action<Point, bool> CellPress;


        private void RecreateField()
        {
            //TODO тут только удаление и добавление контролов из Field, а так же подписывание на их евенты
            //var steps = FieldSize.Height + FieldSize.Width - 1;
            //var r = (EndFieldColor.R - StartFieldColor.R) / steps;
            //var g = (EndFieldColor.G - StartFieldColor.G) / steps;
            //var b = (EndFieldColor.B - StartFieldColor.B) / steps;
            //Field.Cells = new Cell[FieldSize.Height, FieldSize.Width];
            Controls.Clear();
            for (var row = 0; row < FieldSize.Height; row++)
            {
                for (var column = 0; column < FieldSize.Width; column++)
                {
                    //var step = row + column;
                    //Field.Cells[row, column] = new Cell(
                    //    Color.FromArgb(StartFieldColor.R + r * step, StartFieldColor.G + g * step, StartFieldColor.B + b * step),
                    //    Color.FromArgb(StartFieldColor.R + r * (step + 1), StartFieldColor.G + g * (step + 1), StartFieldColor.B + b * (step + 1)),
                    //    BorderColor, CellBorderSize, CellSize, new Point(column, row));
                    Controls.Add(Field.Cells[row, column]);
                    Field.Cells[row, column].CellClick += Miner_CellClick;
                    Field.Cells[row, column].CellSelect += Miner_CellSelect;
                    Field.Cells[row, column].CellPress += Miner_CellPress;
                }
            }
        }

        private void Miner_CellPress(Point position, bool flag)
        {
            CellPress?.Invoke(position, flag);
        }

        private void Miner_CellSelect(Point position, bool flag)
        {
            CellSelect?.Invoke(position, flag);
        }

        private void Miner_CellClick(Point position, bool flag)
        {
            CellClick?.Invoke(position, flag);
        }

        private void ReSize()
        {
            Size = new Size(FieldSize.Width * (CellSize + CellBorderSize) + CellBorderSize, FieldSize.Height * (CellSize + CellBorderSize) + CellBorderSize);
        }

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

        public void Clear()
        {
            Field = new Field(FieldSize.Height, FieldSize.Width);
            MinimumSize = new Size(FieldSize.Width * (20 + 0) + 0, FieldSize.Height * (20 + 0) + 0);
            ReSize();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            var proportions = (double) (_fieldSize.Width*(CellSize + CellBorderSize) + CellBorderSize)/
                              (_fieldSize.Height*(CellSize + CellBorderSize) + CellBorderSize);

            Size = Size.Width > Size.Height ? new Size(Size.Width, (int)(Size.Width / proportions)) : new Size((int)(Size.Height * proportions), Size.Height);
            var a = (int) ((double) (Size.Width + (FieldSize.Width - 1)*CellBorderSize)/FieldSize.Width);
           
            Field.SetCellSizes(a);
            //MessageBox.Show($"{Size.Height} - {Size.Width} -> {a}");
            //if (Size.Width != FieldSize.Width*(CellBorderSize + CellSize) + CellBorderSize ||
            //    Size.Height != FieldSize.Height*(CellBorderSize + CellSize) + CellBorderSize)
            //{
            //    ReSize();
            //}
            base.OnSizeChanged(e);
        }

        //что такое OnPaintBackground - когда применяется?
    }
}
