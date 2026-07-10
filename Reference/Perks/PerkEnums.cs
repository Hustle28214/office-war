// ============================================================================
// LEARN [阶段8] 福利系统 — 枚举与数据结构
// ============================================================================
// 阅读 plan.md §三.5，然后：
//   1. 取消下方 TODO 注释，补全你需要的枚举成员
//   2. 在 GameEnums.cs 或本文件维护均可（保持项目内一致即可）
// ============================================================================

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
