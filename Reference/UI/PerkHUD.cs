// ============================================================================
// LEARN [阶段8] PerkHUD — 福利按钮与状态展示
// ============================================================================
// 参考：UI/BattleHUD.cs（订阅 BattleManager 事件刷新）
//
// 练习：
//   1. Setup(BattleManager battle, PerkController perks) 或只拿 BattleManager（内含 Perk）
//   2. 按钮：下午茶、抢零食、偷前台 → 调用 TryActivatePerk
//   3. Text：团建进度 1/2、冷却回合、窗口期高亮
//   4. 在 BattleDemoBootstrap.CreateHud 末尾挂载（仿 HandUI 创建方式）
//
// MonoBehaviour 只负责显示与点击，逻辑在 PerkController。
// ============================================================================

using UnityEngine;

namespace OfficeWar.UI
{
    public sealed class PerkHUD : MonoBehaviour
    {
        // LEARN: [SerializeField] Button _teaBreakButton; 等

        public void Initialize(/* BattleManager battle, PerkController perks */)
        {
            // LEARN: 绑定按钮 onClick → battle.TryActivatePerk(...)
            // LEARN: battle.OnStateChanged += Refresh;
        }

        void Refresh()
        {
            // LEARN: 更新按钮 interactable（回合、咖啡、冷却、窗口）
        }
    }
}
