using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Miner
{
    class Mine
    {
        private Bitmap _image;
        private short _weight;

        public Mine(Bitmap image, short weight)
        {
            _image = image;
            _weight = weight;
        }

        public Bitmap Image { get => _image; private set => _image = value; }
        public short Weight { get => _weight; private set => _weight = value; }
    }
}
