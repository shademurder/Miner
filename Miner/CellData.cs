namespace Miner
{
    /// <summary>
    /// Класс данных клетки поля
    /// </summary>
    class CellData
    {
        /// <summary>
        /// Указывает, скрыта клетка или нет
        /// </summary>
        public bool Hidden { get; set; } = true;
        
        /// <summary>
        /// Тип клетки
        /// </summary>
        internal CellType Type { get; set; } = CellType.Empty;
        
        /// <summary>
        /// Количество мин вокруг клетки
        /// Значимо, только если это клетка без мины
        /// </summary>
        public short Weight { get; set; } = 0;

        /// <summary>
        /// Состояние клетки для визуализации
        /// </summary>
        internal CellState State { get; set; } = CellState.Empty;


    }
}
