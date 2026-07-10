// ============================================================================
// LEARN [阶段9] OAPanel — 审批待办 UI
// ============================================================================
// 练习：
//   1. 列表展示 Inbox：单号、FormType、Status、预计生效回合
//   2. Pending 项显示「加急」按钮 → OAController.TryUrgent
//   3. Rejected 显示 RejectReason（主管：理由不充分）
//   4. 角标：GetPendingCount
//
// Perk 按钮若 requiresOA，点击后打开本面板预填表单，而非直接激活 Buff。
// ============================================================================

using UnityEngine;

namespace OfficeWar.UI
{
    public sealed class OAPanel : MonoBehaviour
    {
        public void Initialize(/* OAController oa, BattleManager battle */)
        {
            // LEARN: 订阅 oa.OnLog 追加到列表或 BattleHUD 日志
        }
    }
}
