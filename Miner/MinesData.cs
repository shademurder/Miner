using System;
using System.Drawing;

namespace Miner
{
    /// <summary>
    /// Класс - хранилище данных списка мин
    /// </summary>
    [Serializable]
    class MinesData
    {
        /// <param name="mineCount">Количество мин</param>
        /// <param name="weight">Вес каждой мины</param>
        /// <param name="image">Изображение, используемое для отображаения мины</param>
        public MinesData(int mineCount, short weight, Bitmap image)
        {
            MineCount = mineCount;
            Weight = weight;
            Image = image;
        }

        /// <summary>
        /// Количество мин
        /// </summary>
        public int MineCount = 0;
        /// <summary>
        /// Вес каждой мины
        /// </summary>
        public short Weight = 0;
        /// <summary>
        /// Изображение, используемое для отображаения мины
        /// </summary>
        public Bitmap Image;
    }
}
