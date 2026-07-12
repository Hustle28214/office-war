// =============================================================================
// 课 06 | BoardController | 教案.md · plan.md §二.2
// =============================================================================
// 行序：CrisisBack → Frontline → TeamBack
// =============================================================================

using System.Collections.Generic;
using OfficeWar.Core;

namespace OfficeWar.Battlefield
{
    public sealed class BoardController
    {
        readonly List<BoardSlot> _slots = new();

        public IReadOnlyList<BoardSlot> Slots => _slots;

        // LEARN: Initialize(columnsPerRow=1)
        // LEARN: FrontlineSlot
        // LEARN: GetBackSlot(Faction) — Team→TeamBack, Crisis→CrisisBack
        // LEARN: GetAllUnits / ResetAllActedFlags
    }
}
