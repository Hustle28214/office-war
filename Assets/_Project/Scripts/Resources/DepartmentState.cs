// =============================================================================
// 课 05 | DepartmentState | 教案.md · Reference/Resources/DepartmentState.cs
// =============================================================================

using System;
using OfficeWar.Core;

namespace OfficeWar.Resources
{
    public sealed class DepartmentState
    {
        public Faction Faction { get; }

        public event Action<DepartmentState> OnChanged;

        public DepartmentState(Faction faction) => Faction = faction;

        // LEARN: HqBudget, CoffeeMax, CoffeeCurrent 属性
        // LEARN: TrySpendCoffee, RefillCoffee, IncreaseCoffeeMax, TakeHqDamage
    }
}
