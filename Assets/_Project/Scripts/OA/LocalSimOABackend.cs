// ============================================================================
// LEARN [阶段9] 本地模拟审批 — MVP 不对接真实 OA
// ============================================================================
// 练习：实现 RollApproval(OARequest req) → Approved / Rejected + 原因文案
// 规则见 plan.md §6.4 审批人表格（主管 70%、HR 60% 等）
//
// 【后期扩展】实现 IOABackend，对接飞书/钉钉/自研 REST：
//   interface IOABackend { Task<string> SubmitAsync(...); Task<OARequestStatus> PollAsync(...); }
// ============================================================================

namespace OfficeWar.OA
{
    public static class LocalSimOABackend
    {
        public static OARequestStatus RollApproval(OARequest request)
        {
            // LEARN: switch (request.FormType) 不同通过率
            return OARequestStatus.Pending;
        }
    }

    // LEARN [可选] 公司福利预算池 — 团建/下午茶消耗预算点，见 plan §6.4
    public sealed class WelfareBudgetPool
    {
        // public int Remaining { get; private set; } = 10;
        // public bool TrySpend(int amount) { ... }
    }
}
