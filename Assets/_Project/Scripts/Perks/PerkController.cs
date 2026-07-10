// ============================================================================
// LEARN [阶段8] PerkController — 福利系统核心（本文件是练习主体）
// ============================================================================
//
// 【你要实现的公开 API】
//   bool TryActivatePerk(Faction faction, PerkType type)
//   int GetActionCostModifier(Faction faction)
//   int GetHireCostModifier(Faction faction, bool isFirstDeployThisBattle)
//   void OnTurnStarted(Faction faction, int turnNumber)
//   void OnFrontlineChanged(Faction? frontlineOwner)
//   void TickEndOfTurn(Faction faction)   // Buff 回合递减、冷却 tick
//
// 【挂钩位置 — 在 BattleManager 中搜索 LEARN [阶段8]】
//   BeginTurn()      → OnTurnStarted
//   TryAdvanceUnit   → 扣费时减去 GetActionCostModifier
//   TryPlayCard      → 减去 GetHireCostModifier
//   占线变化后       → OnFrontlineChanged
//
// 【建议实现顺序】
//   1. 只做下午茶（Manual + 减 actionCost）
//   2. 加 ActivePerk 列表与 duration
//   3. 团建占线计数
//   4. GlobalEventScheduler（第5回合培训）→ 可拆到单独类
//   5. CompetitivePerkResolver / StealReceptionResolver
//
// ============================================================================

using System;
using System.Collections.Generic;
using OfficeWar.Core;

namespace OfficeWar.Perks
{
    /// <summary>
    /// 运行时：某部门当前激活的一条 Buff。
    /// LEARN: 实现 TurnsRemaining、来自哪条 PerkDefinition。
    /// </summary>
    public sealed class ActivePerk
    {
        public PerkDefinition Definition { get; set; }
        public int TurnsRemaining { get; set; }
    }

    public sealed class PerkController
    {
        // LEARN: 按 Faction 分别存储 ActivePerks、冷却、占线 streak 等
        // readonly Dictionary<Faction, List<ActivePerk>> _active = new();

        public event Action<string> OnLog;

        public bool TryActivatePerk(Faction faction, PerkType type)
        {
            // LEARN: 1) Find definition  2) 检查回合/咖啡/冷却  3) 扣费  4) 添加 ActivePerk 或触发即时效果
            OnLog?.Invoke("[PerkController] 尚未实现 — 请完成 LEARNING.md 练习 8.2");
            return false;
        }

        public int GetActionCostModifier(Faction faction)
        {
            // LEARN: 遍历 ActivePerk，累加 actionCostReduction
            return 0;
        }

        public int GetHireCostModifier(Faction faction, bool isFirstDeployThisBattle)
        {
            // LEARN: 弹性办公等（可选）
            return 0;
        }

        public void OnTurnStarted(Faction faction, int turnNumber)
        {
            // LEARN: 全局事件（全员培训）、被动抽牌、竞争窗口开关
        }

        public void OnFrontlineChanged(Faction? owner)
        {
            // LEARN: 更新占线 streak；满 2 触发团建
        }

        public void TickEndOfTurn(Faction faction)
        {
            // LEARN: ActivePerk.TurnsRemaining--；移除过期；冷却--
        }
    }
}
