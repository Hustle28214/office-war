using System;
using OfficeWar.Cards;
using OfficeWar.Core;

namespace OfficeWar.Battlefield
{
    public sealed class UnitEntity
    {
        public RuntimeCard Source { get; }
        public Faction Faction { get; }
        public BoardSlot Slot { get; set; }
        public int CurrentMorale { get; private set; }
        public int KpiBonus { get; set; }
        public bool HasActedThisTurn { get; set; }
        public bool SupportUsedThisTurn { get; set; }

        public int EffectiveKpi => Source.Data.kpi + KpiBonus;

        public event Action<UnitEntity> OnChanged;
        public event Action<UnitEntity> OnDied;

        public UnitEntity(RuntimeCard source, Faction faction, BoardSlot slot)
        {
            Source = source;
            Faction = faction;
            Slot = slot;
            CurrentMorale = source.Data.maxMorale;
            slot.Occupant = this;
        }

        public bool CanPayActionCost(int availableCoffee) =>
            !HasActedThisTurn && availableCoffee >= Source.Data.actionCost;

        public void TakeDamage(int amount)
        {
            if (amount <= 0)
                return;

            CurrentMorale = Math.Max(0, CurrentMorale - amount);
            OnChanged?.Invoke(this);

            if (IsDead)
                OnDied?.Invoke(this);
        }

        public void Heal(int amount)
        {
            if (amount <= 0)
                return;

            CurrentMorale = Math.Min(Source.Data.maxMorale, CurrentMorale + amount);
            OnChanged?.Invoke(this);
        }

        public void ApplyKpiBuff(int amount)
        {
            KpiBonus += amount;
            OnChanged?.Invoke(this);
        }

        public bool IsDead => CurrentMorale <= 0;

        public bool IsInBackRow =>
            Slot.Row == (Faction == Faction.Player ? BoardRow.PlayerBack : BoardRow.EnemyBack);

        public bool IsOnFrontline => Slot.Row == BoardRow.Frontline;
    }
}
