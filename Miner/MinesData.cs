using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miner
{
    [Serializable]
    class MinesData
    {
        public MinesData(int mineCount, short weight, Bitmap image)
        {
            MineCount = mineCount;
            Weight = weight;
            Image = image;
        }

        public int MineCount = 0;
        public short Weight = 0;
        public Bitmap Image;
    }
}
