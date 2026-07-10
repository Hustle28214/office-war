using System;
using OfficeWar.Core;

namespace OfficeWar.Turn
{
    public sealed class TurnManager
    {
        public Faction ActiveFaction { get; private set; } = Faction.Player;
        public TurnPhase Phase { get; private set; } = TurnPhase.Main;
        public int TurnNumber { get; private set; } = 1;

        public event Action<Faction> OnTurnStarted;
        public event Action<Faction> OnTurnEnded;

        public void StartBattle()
        {
            TurnNumber = 1;
            ActiveFaction = Faction.Player;
            Phase = TurnPhase.Main;
            OnTurnStarted?.Invoke(ActiveFaction);
        }

        public void EndTurn()
        {
            OnTurnEnded?.Invoke(ActiveFaction);
            ActiveFaction = ActiveFaction == Faction.Player ? Faction.Enemy : Faction.Player;
            if (ActiveFaction == Faction.Player)
                TurnNumber++;

            Phase = TurnPhase.Main;
            OnTurnStarted?.Invoke(ActiveFaction);
        }
    }
}
