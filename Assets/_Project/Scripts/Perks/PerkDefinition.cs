// ============================================================================
// LEARN [阶段8] PerkDefinition — 单条福利的配置
// ============================================================================
// 参考：Data/OfficeCardData.cs（ScriptableObject 配置）
// 练习：
//   1. 补全字段：coffeeCost, actionCostReduction, durationTurns, requiresOA, linkedFormType
//   2. 可选：改为 [CreateAssetMenu] ScriptableObject，或像 CardCatalog 一样静态列表
// ============================================================================

namespace OfficeWar.Perks
{
    public sealed class PerkDefinition
    {
        public PerkType type;
        public PerkScope scope;
        public PerkTrigger trigger;
        public string displayName;
        public string description;

        // LEARN: 主动福利消耗的咖啡（如下午茶 = 2）
        // public int activationCoffeeCost;

        // LEARN: 生效后本回合行动费减免（下午茶 = 1）
        // public int actionCostReduction;

        // LEARN: 持续回合数；0 表示仅本回合
        // public int durationTurns;

        // LEARN: 为 true 时阶段9走 OA，阶段8可一律 false
        // public bool requiresOA;

        // LEARN: 冷却回合数（下午茶 = 2）
        // public int cooldownTurns;
    }
}
