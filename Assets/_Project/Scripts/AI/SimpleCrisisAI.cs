// =============================================================================
// 课 16 | SimpleCrisisAI | 教案.md · plan.md §二（压力方）
// =============================================================================
// 控 Crisis 阵营：部署阻塞 → 占线 → 抬 Pressure / 砸 Morale → 结束今天。
// 旧名 SimpleEnemyAI 可删或改为转发到本类。
// =============================================================================

using UnityEngine;

namespace OfficeWar.AI
{
    public sealed class SimpleCrisisAI : MonoBehaviour
    {
        // LEARN: Initialize(BattleManager)
        // LEARN: 订阅工作日开始；当 ActiveFaction == Crisis 时 RunDay coroutine
        // LEARN: 顺序：部署 → 推进 → 输出 → EndDay
    }
}
