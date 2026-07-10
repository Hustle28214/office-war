// ============================================================================
// LEARN [阶段8] PerkCatalog — 福利配置表
// ============================================================================
// 参考：Data/CardCatalog.cs
// 练习：实现 AllPerks 与 Find(PerkType)，至少配置：
//   - tea_break      下午茶      单方  花2咖啡  沟通费-1
//   - team_building  团建        单方  占线2回合  全体+2精神
//   - snack_rush     抢零食      竞争  窗口回合 3,7,11
//   - steal_reception 偷前台     擦边  60%成功+2咖啡 / 40%失败HQ-2
//   - company_training 全员培训  双方  第5,9回合  CoffeeMax+1
// ============================================================================

using System.Collections.Generic;

namespace OfficeWar.Perks
{
    public static class PerkCatalog
    {
        public static IReadOnlyList<PerkDefinition> AllPerks
        {
            get
            {
                // LEARN: 懒加载缓存列表，仿 CardCatalog.BuildCache()
                return new List<PerkDefinition>();
            }
        }

        public static PerkDefinition Find(PerkType type)
        {
            // LEARN: 遍历 AllPerks 按 type 查找
            return null;
        }
    }
}
