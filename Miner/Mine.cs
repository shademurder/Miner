using System.Drawing;

namespace Miner
{
    /// <summary>
    /// Класс мины
    /// </summary>
    class Mine
    {
        private Bitmap _image;
        private short _weight;

        /// <summary>
        /// Создание мины
        /// </summary>
        /// <param name="image">Изображение, отвечающее за отображение мины на клетке</param>
        /// <param name="weight">Вес клетки</param>
        public Mine(Bitmap image, short weight)
        {
            _image = image;
            _weight = weight;
        }

        /// <summary>
        /// Изображение, отвечающее за отображение мины на клетке
        /// </summary>
        public Bitmap Image
        {
            get { return _image; }
            private set { _image = value; }
        }

        /// <summary>
        /// Вес клетки
        /// Для возможности отличать её тип от остальных (в случае, если типов мин много)
        /// </summary>
        public short Weight
        {
            get { return _weight; }
            private set { _weight = value; }
        }
    }
}
