// =============================================================================
// 课 20 | OAEnums | 教案.md 课 20 · plan.md §三.6
// =============================================================================

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
