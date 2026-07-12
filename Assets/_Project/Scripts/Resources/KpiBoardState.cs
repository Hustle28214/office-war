// =============================================================================
// 课 05 | KpiBoardState | 教案.md · plan.md §二.3
// =============================================================================
// 全队共享的多轨 KPI。胜负看达标，不看「把对面 HQ 打到 0」。
// =============================================================================

using System;
using OfficeWar.Core;

namespace OfficeWar.Resources
{
    public sealed class KpiBoardState
    {
        public event Action<KpiBoardState> OnChanged;

        // LEARN: int Get(KpiTrack track) / void Add(KpiTrack track, int delta)
        // LEARN: Delivery, Quality, UX, Morale, Pressure 属性或字典
        // LEARN: bool MeetsAllTargets() — Delivery/Quality/UX/Morale ≥ 目标 且 Pressure ≤ 上限
        // LEARN: bool HasFailed() — Morale≤0 或 Pressure≥爆表
        // LEARN: 变更时触发 OnChanged
    }
}
