// =============================================================================
// 课 18 | PerkEnums | 教案.md 课 18~19 · plan.md §三.5
// =============================================================================

namespace OfficeWar.Perks
{
    // TODO: 从 plan.md 的福利 ID 列出 PerkType（TeaBreak, TeamBuilding, SnackRush, StealReception...）
    public enum PerkType
    {
        None = 0,
        // TeaBreak,
        // TeamBuilding,
        // ...
    }

    // TODO: Self / Both / Competitive / Risky — 见 plan §5.1
    public enum PerkScope
    {
        Self,
        // Both,
        // Competitive,
        // Risky,
    }

    // TODO: Manual, OnTurnStart, OnFrontlineHold, GlobalSchedule, OnKill, OnOAApproved
    public enum PerkTrigger
    {
        Manual,
    }
}
