// =============================================================================
// 课 05 | DepartmentState | 教案.md · plan.md §三.1
// =============================================================================
// Team：共享咖啡池（MVP）。Crisis：CrisisBudget 作「压力池/阻塞血量」空间战层。
// 终局胜负以 KpiBoardState 为准，不单靠把 CrisisBudget 打到 0。
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

        // LEARN: CoffeeMax, CoffeeCurrent（Team 用；Crisis 可不用咖啡或用简化 AI 资源）
        // LEARN: CrisisBudget（仅 Crisis：初始 InitialCrisisBudget，被清障时扣减）
        // LEARN: TrySpendCoffee, RefillCoffee, IncreaseCoffeeMax
        // LEARN: TakeCrisisDamage(int) — 替代旧 TakeHqDamage
        // LEARN: 变更时触发 OnChanged
    }
}
