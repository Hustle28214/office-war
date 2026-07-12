// =============================================================================
// 课 02 | GameConstants | 教案.md · plan.md §二.1 / §二.3
// =============================================================================
// 参考：Reference/Core/GameConstants.cs
// =============================================================================

namespace OfficeWar.Core
{
    public static class GameConstants
    {
        // —— Sprint / 工作日 ——
        // LEARN: public const int DefaultSprintDays = 10;
        // LEARN: public const int MinSprintDays = 8;
        // LEARN: public const int MaxSprintDays = 15;

        // —— 咖啡（全队共享池 MVP）——
        // LEARN: public const int InitialCoffeeMax = 1;
        // LEARN: public const int MaxCoffeeMax = 12;
        // LEARN: public const int CardsPerDayDraw = 1;
        // LEARN: public const int OpeningHandSize = 3;
        // LEARN: public const int MaxHandSize = 10;
        // LEARN: public const int CorePackSize = 14;
        // LEARN: public const int ArchetypePackSize = 10;

        // —— Crisis 压力池（空间战层，类旧 HQ，用于清阻塞数值）——
        // LEARN: public const int InitialCrisisBudget = 20;

        // —— KPI 达标线（plan §二.3）——
        // LEARN: public const int DeliveryTarget = 12;
        // LEARN: public const int QualityTarget = 8;
        // LEARN: public const int UxTarget = 6;
        // LEARN: public const int MoraleTarget = 4;
        // LEARN: public const int MoraleFailAt = 0;
        // LEARN: public const int PressureTargetMax = 6;
        // LEARN: public const int PressureFailAt = 12;
        // LEARN: public const int InitialDelivery = 0;
        // LEARN: public const int InitialQuality = 0;
        // LEARN: public const int InitialUx = 0;
        // LEARN: public const int InitialMorale = 6;
        // LEARN: public const int InitialPressure = 4;

        // —— 连锁 ——
        // LEARN: public const int MaxChainPerDay = 5;
        // LEARN: public const float ChainWindowSeconds = 1.2f;
    }
}
