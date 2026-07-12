// =============================================================================
// 课 03 | CardPackData | 教案.md · plan.md §三.0.1
// =============================================================================
// 结构：Role → Archetype → Pack（8~12 张 + 路线被动）
// =============================================================================

using OfficeWar.Core;
using UnityEngine;

namespace OfficeWar.Data
{
    [CreateAssetMenu(fileName = "NewCardPack", menuName = "明天能上线吗/CardPack")]
    public class CardPackData : ScriptableObject
    {
        // LEARN: string packId;
        // LEARN: string displayName;
        // LEARN: PlayerRole role;
        // LEARN: RoleArchetype archetype;
        // LEARN: bool isStarter;
        // LEARN: OfficeCardData[] cards;
        // LEARN: string routePassiveId; // 路线被动配置键，后期接 Perk/被动表
    }
}
