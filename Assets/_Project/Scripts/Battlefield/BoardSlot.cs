using OfficeWar.Core;

namespace OfficeWar.Battlefield
{
    public sealed class BoardSlot
    {
        public BoardRow Row { get; }
        public int ColumnIndex { get; }
        public UnitEntity Occupant { get; set; }

        public bool IsEmpty => Occupant == null;

        public BoardSlot(BoardRow row, int columnIndex)
        {
            Row = row;
            ColumnIndex = columnIndex;
        }
    }
}
