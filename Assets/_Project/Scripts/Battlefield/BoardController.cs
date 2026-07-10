using System.Collections.Generic;
using System.Linq;
using OfficeWar.Core;

namespace OfficeWar.Battlefield
{
    public sealed class BoardController
    {
        readonly List<BoardSlot> _slots = new();

        public IReadOnlyList<BoardSlot> Slots => _slots;

        public BoardSlot FrontlineSlot => _slots.First(s => s.Row == BoardRow.Frontline);

        public BoardSlot GetBackSlot(Faction faction) =>
            faction == Faction.Player
                ? _slots.First(s => s.Row == BoardRow.PlayerBack)
                : _slots.First(s => s.Row == BoardRow.EnemyBack);

        public void Initialize(int columns = 1)
        {
            _slots.Clear();
            for (var c = 0; c < columns; c++)
            {
                _slots.Add(new BoardSlot(BoardRow.EnemyBack, c));
                _slots.Add(new BoardSlot(BoardRow.Frontline, c));
                _slots.Add(new BoardSlot(BoardRow.PlayerBack, c));
            }
        }

        public IEnumerable<UnitEntity> GetAllUnits()
        {
            foreach (var slot in _slots)
            {
                if (slot.Occupant != null)
                    yield return slot.Occupant;
            }
        }

        public IEnumerable<UnitEntity> GetUnits(Faction faction)
        {
            return GetAllUnits().Where(u => u.Faction == faction);
        }

        public void ResetAllActedFlags(Faction faction)
        {
            foreach (var unit in GetUnits(faction))
                unit.HasActedThisTurn = false;
        }
    }
}
