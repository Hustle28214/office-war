// =============================================================================
// 课 04 | Hand | 教案.md
// =============================================================================

using System.Collections.Generic;

namespace OfficeWar.Cards
{
    public sealed class Hand
    {
        readonly List<RuntimeCard> _cards = new();

        public IReadOnlyList<RuntimeCard> Cards => _cards;

        // LEARN: Add, Remove, IsFull
    }
}
