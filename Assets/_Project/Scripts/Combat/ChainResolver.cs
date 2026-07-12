// =============================================================================
// 课 11 | ChainResolver | 教案.md · plan.md §三.5
// =============================================================================
// 连锁窗口：部署/击杀后入队，依次播放，单日上限 MaxChainPerDay。
// =============================================================================

using System;
using OfficeWar.Core;

namespace OfficeWar.Combat
{
    public sealed class ChainResolver
    {
        public int ChainsToday { get; private set; }

        public event Action<string> OnChainStep;

        // LEARN: void BeginWindow(string reason);
        // LEARN: bool TryEnqueue(string stepId); // 超上限返回 false，「群已禁言」
        // LEARN: void ResetDay();
        // LEARN: 与跨职标签 OnPlay / OnAdvance / OnKill 挂钩（可先 Log）
    }
}
