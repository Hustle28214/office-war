// =============================================================================
// 课 03 | CardCatalog | 教案.md · plan.md §三.0.2
// =============================================================================
// MVP：公共核心包 + 一个 Starter 子路线包（如 DevFrontend）拼成本局牌库。
// =============================================================================

using System.Collections.Generic;
using OfficeWar.Core;

namespace OfficeWar.Data
{
    public static class CardCatalog
    {
        public static IReadOnlyList<OfficeCardData> AllCards
        {
            get
            {
                // LEARN: 懒加载公共牌 + 各 Starter 包牌
                return new List<OfficeCardData>();
            }
        }

        /// <summary>入门局：核心包 + 指定子路线 Starter 包</summary>
        public static List<OfficeCardData> CreateStarterDeck(PlayerRole role, RoleArchetype archetype)
        {
            // LEARN: 核心约 12~16 张 + 子路线 8~10 张；总数可仍约 20~24
            return new List<OfficeCardData>();
        }

        /// <summary>兼容旧 Demo 名：默认 Dev + DevFrontend Starter</summary>
        public static List<OfficeCardData> CreateDemoDeck() =>
            CreateStarterDeck(PlayerRole.Unset, RoleArchetype.Unset);

        public static OfficeCardData Find(string cardId)
        {
            // LEARN: 按 cardId 查找
            return null;
        }
    }
}
