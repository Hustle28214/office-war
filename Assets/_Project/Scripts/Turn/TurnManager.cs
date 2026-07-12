// =============================================================================
// 课 09 | TurnManager | 教案.md · plan.md §二.1
// =============================================================================
// TurnNumber = 当前工作日（第几天）。Sprint 默认 10 天。
// =============================================================================

using System;
using OfficeWar.Core;

namespace OfficeWar.Turn
{
    public sealed class TurnManager
    {
        public Faction ActiveFaction { get; private set; } = Faction.Unset;
        /// <summary>当前工作日（从 1 起）</summary>
        public int WorkDay { get; private set; } = 1;
        public TurnPhase Phase { get; private set; } = TurnPhase.Unset;
        public int SprintDays { get; private set; }

        public event Action<Faction> OnDayStarted;
        public event Action<Faction> OnDayEnded;

        // LEARN: StartSprint(int sprintDays = DefaultSprintDays) — ActiveFaction=Team, WorkDay=1
        // LEARN: EndDay() — Team↔Crisis 切换；回到 Team 时 WorkDay++
        // LEARN: bool IsSprintOver => WorkDay > SprintDays
        // LEARN: 触发 OnDayStarted / OnDayEnded（可兼容旧名 OnTurnStarted）
    }
}
