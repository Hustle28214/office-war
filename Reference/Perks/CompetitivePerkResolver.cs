// ============================================================================
// LEARN [阶段8] 竞争类福利 — 抢零食 / 抢订下午茶
// ============================================================================
// plan.md：先花 1 咖啡者赢；同回合双方抢 → 平局各 -1 咖啡
// 练习：
//   1. 记录本窗口是否已被抢、谁先提交（Faction?）
//   2. ResolveSnackRush(Faction actor) → Win / Lose / Tie
//   3. 在 PerkController.TryActivatePerk 里调用本类
// ============================================================================

using OfficeWar.Core;

namespace OfficeWar.Perks
{
    public static class CompetitivePerkResolver
    {
        public enum CompetitiveResult
        {
            Won,
            Lost,
            Tie,
            WindowClosed,
        }

        public static CompetitiveResult TryClaimSnack(Faction faction, int currentTurn)
        {
            // LEARN: 检查 turn 是否为 3,7,11；检查是否已被抢
            return CompetitiveResult.WindowClosed;
        }

        public static void ResetWindow(int currentTurn)
        {
            // LEARN: 新窗口开始时清空状态
        }
    }
}
