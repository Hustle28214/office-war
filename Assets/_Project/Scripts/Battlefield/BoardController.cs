// =============================================================================
// 课 06 | BoardController | 教案.md
// =============================================================================

using System.Collections.Generic;

namespace OfficeWar.Battlefield
{
    public sealed class BoardController
    {
        readonly List<BoardSlot> _slots = new();

        public IReadOnlyList<BoardSlot> Slots => _slots;

        // LEARN: Initialize, FrontlineSlot, GetBackSlot, GetAllUnits, ResetAllActedFlags
    }
}
