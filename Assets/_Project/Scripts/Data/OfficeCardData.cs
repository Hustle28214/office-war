// =============================================================================
// 课 03 | OfficeCardData | 教案.md · plan.md §三
// =============================================================================

using OfficeWar.Core;
using UnityEngine;

namespace OfficeWar.Data
{
    [CreateAssetMenu(fileName = "NewOfficeCard", menuName = "明天能上线吗/Card")]
    public class OfficeCardData : ScriptableObject
    {
        // LEARN: cardId, displayName, job, hireCost, actionCost, maxMorale, kpi
        // LEARN: canAdvance, isSupport, description
        // LEARN: CardFeel feel;           // 出牌演出
        // LEARN: KpiTrack? kpiOnPlay;     // 打出时可选推一条 KPI（可先不做）
        // LEARN: PlayerRole? exclusiveRole; // 专属大类，null=公共核心包
    }
}
