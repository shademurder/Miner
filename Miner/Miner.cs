using System;
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

        private Size _fieldSize = new Size(9, 9);
        private Field _field = new Field(9, 9);

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
                //RecreateField();
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
                //RecreateField();
                //ReSize();
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

        public event Action<Point, bool> CellSelect;
        public event Action<Point, bool> CellClick;
        public event Action<Point, bool> CellPress;


        private void RecreateField()
        {
            Controls.Clear();
            for (var row = 0; row < FieldSize.Height; row++)
            {
                for (var column = 0; column < FieldSize.Width; column++)
                {
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
            var width = FieldSize.Width * (CellSize + CellBorderSize) + CellBorderSize;
            var height = FieldSize.Height * (CellSize + CellBorderSize) + CellBorderSize;
            if (Size.Width != width || Size.Height != height)
            {
                Size = new Size((int)width, (int)height);
            }
            //RecreateField();
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mines"></param>
        /// <param name="mine"></param>
        /// <returns>Количество мин, для которых не хватило места на поле</returns>
        public int AddMines(int mines, Mine mine)
        {
            return Field.CreateMines(mines, mine);
        }

        public void Clear()
        {
            ReSize();
            RecreateField();
        }
        protected override void OnSizeChanged(EventArgs e)
        {
            
            //отношение ширины поля к высоте
            var proportions = (double) (Field.FieldSize.Width*(CellSize + CellBorderSize) + CellBorderSize) /
                              (Field.FieldSize.Height*(CellSize + CellBorderSize) + CellBorderSize);
            //применяется наибольший размер с учётом пропорций
            Size = Size.Width > Size.Height * proportions ? new Size(Size.Width, (int)(Size.Width / proportions)) : new Size((int)(Size.Height * proportions), Size.Height);
            //var newCellFullSize = ((Size.Width + (FieldSize.Width - 1)*CellBorderSize)/FieldSize.Width);
            //var cellProportion = _userBorderSize / _userCellSize;
            //var newCellSize = Size.Width / (FieldSize.Width * (1 + cellProportion) + cellProportion);
            //var newBorderSize = (Size.Width - newCellSize * FieldSize.Width) / (FieldSize.Width + 1);
            //var newBorderSize = cellProportion * newCellSize;//(Size.Width - newCellSize * FieldSize.Width) / (FieldSize.Width + 1);
            //Field.BorderSize = newBorderSize;
            var newCellSize = (Size.Width - CellBorderSize) / FieldSize.Width - CellBorderSize;
            if (newCellSize != Field.CellSize)
            {
                Field.CellSize = newCellSize;
            }
            //var newCellFullSize = (float)Size.Width / FieldSize.Width;
            //if (newCellFullSize != CellSize + 2 * CellBorderSize)
            //{
            //    Field.SetCellSizes(newCellFullSize);
            //}
            //ReSize();
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
