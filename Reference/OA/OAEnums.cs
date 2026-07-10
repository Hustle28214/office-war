// ============================================================================
// LEARN [阶段9] OA 审批 — 枚举
// ============================================================================
// 对照 plan.md §三.6.2 单据类型与 §6.3 审批状态
// ============================================================================

namespace OfficeWar.OA
{
    public enum OAFormType
    {
        None = 0,
        // TeaBreakApply,
        // TeamBuildingApply,
        // SnackRequisition,
        // UrgentStamp,
        // SupplyRequest,  // 可掩护偷前台
    }

    public enum OARequestStatus
    {
        Draft,
        Pending,
        Approved,
        Rejected,
        Withdrawn,
        Applied,
    }
}
