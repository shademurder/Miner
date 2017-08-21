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

        public Bitmap Image
        {
            get { return _image; }
            private set { _image = value; }
        }

        public short Weight
        {
            get { return _weight; }
            private set { _weight = value; }
        }
    }
}
