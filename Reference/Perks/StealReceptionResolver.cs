// ============================================================================
// LEARN [阶段8] 擦边福利 — 偷前台东西
// ============================================================================
// plan.md：60% 成功 +2 咖啡；40% 失败 HQ-2；敌方占前线 -20% 成功率；10% 大成功抽牌
// 练习：
//   1. 定义 StealResult { Fail, Success, Crit }
//   2. RollSuccess(enemyControlsFrontline) → bool
//   3. 在 PerkController 里应用结果（改 DepartmentState）
// ============================================================================

namespace OfficeWar.Perks
{
    public enum StealResult
    {
        Fail,
        Success,
        Crit,
    }

    public static class StealReceptionResolver
    {
        public static StealResult Resolve(bool enemyControlsFrontline)
        {
            // LEARN: UnityEngine.Random 或 System.Random
            //       baseSuccess = 0.6f; if (enemyControlsFrontline) baseSuccess -= 0.2f;
            //       crit 额外 10%
            return StealResult.Fail;
        }
    }
}
