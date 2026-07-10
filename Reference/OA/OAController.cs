// ============================================================================
// LEARN [阶段9] OAController — 审批系统核心
// ============================================================================
//
// 【公开 API】
//   IReadOnlyList<OARequest> Inbox(Faction faction)
//   bool TrySubmit(Faction faction, OAFormType form, PerkType? linkedPerk)
//   bool TryUrgent(Faction faction, string requestId)   // 花 2 咖啡，本回合结算
//   void TickApprovals(int turnNumber)                // BeginTurn 时调用
//   int GetPendingCount(Faction faction)
//
// 【与 Perk 联动】
//   PerkController.TryActivatePerk 发现 requiresOA → 改调 TrySubmit
//   TickApprovals 里 Approved → 回调 PerkController.TryActivatePerk（或专用 OnOAApproved）
//
// 【MVP 审批模拟】
//   见 LocalSimOABackend.cs — 主管 70% 通过等
//
// 【行政带宽】plan：每回合每方最多 2 张 Pending
//
// ============================================================================

using System.Collections.Generic;
using OfficeWar.Core;
using OfficeWar.Perks;

namespace OfficeWar.OA
{
    public sealed class OAController
    {
        public event System.Action<string> OnLog;

        public IReadOnlyList<OARequest> Inbox(Faction faction)
        {
            // LEARN: 返回该部门所有非 Applied 的单据
            return new List<OARequest>();
        }

        public bool TrySubmit(Faction faction, OAFormType form, PerkType linkedPerk)
        {
            OnLog?.Invoke("[OAController] 尚未实现 — 请完成 LEARNING.md 练习 9.1");
            return false;
        }

        public bool TryUrgent(Faction faction, string requestId)
        {
            // LEARN: 扣 2 咖啡；将 ResolveTurn 设为当前回合；立即 RollApproval
            return false;
        }

        public void TickApprovals(int turnNumber)
        {
            // LEARN: ResolveTurn <= turnNumber 的 Pending 单 → LocalSimOABackend → Approved/Rejected
        }
    }
}
