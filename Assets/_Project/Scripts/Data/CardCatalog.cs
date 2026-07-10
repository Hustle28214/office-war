// =============================================================================
// 课 03 | CardCatalog | 教案.md
// =============================================================================
// 步骤：BuildCache 8 种牌；CreateDemoDeck 凑 20 张
// 参考：Reference/Data/CardCatalog.cs
// =============================================================================

using System.Collections.Generic;

namespace OfficeWar.Data
{
    public static class CardCatalog
    {
        public static IReadOnlyList<OfficeCardData> AllCards
        {
            get
            {
                // LEARN: 懒加载 _cache
                return new List<OfficeCardData>();
            }
        }

        public static List<OfficeCardData> CreateDemoDeck()
        {
            // LEARN: 返回 20 张 RuntimeCard 用的 OfficeCardData 列表
            return new List<OfficeCardData>();
        }

        public static OfficeCardData Find(string cardId)
        {
            // LEARN: 按 cardId 查找
            return null;
        }
    }
}
