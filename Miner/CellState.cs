using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miner
{
    enum CellState
    {
        /// <summary>
        /// С клеткой ничего не происходит
        /// </summary>
        Empty,
        /// <summary>
        /// Клетка выделена
        /// </summary>
        Selected,
        /// <summary>
        /// На клетку нажали
        /// </summary>
        Pressed
    }
}
