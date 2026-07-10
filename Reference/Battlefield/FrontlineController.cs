using OfficeWar.Core;

namespace OfficeWar.Battlefield
{
    public sealed class FrontlineController
    {
        readonly BoardController _board;

        public FrontlineController(BoardController board)
        {
            _board = board;
        }

        public Faction? GetOwner()
        {
            var occupant = _board.FrontlineSlot.Occupant;
            return occupant == null ? null : occupant.Faction;
        }

        public bool IsControlledBy(Faction faction) => GetOwner() == faction;

        public bool CanAdvanceToFrontline(Faction faction)
        {
            var owner = GetOwner();
            return owner == null || owner == faction;
        }
    }
}
