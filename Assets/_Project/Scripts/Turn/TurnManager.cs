// =============================================================================
// 课 09 | TurnManager | 教案.md
// =============================================================================

using System;
using OfficeWar.Core;

namespace OfficeWar.Turn
{
    public sealed class TurnManager
    {
        public Faction ActiveFaction { get; private set; } = Faction.Unset;
        public int TurnNumber { get; private set; } = 1;

        public event Action<Faction> OnTurnStarted;

        // LEARN: StartBattle, EndTurn, OnTurnEnded
    }
}
