namespace OfficeWar.Core
{
    public static class GameConstants
    {
        public const int DefaultSprintDays = 10;
        public const int MinSprintDays = 8;
        public const int MaxSprintDays = 15;

        public const int InitialCoffeeMax = 1;
        public const int MaxCoffeeMax = 12;
        public const int CardsPerDayDraw = 1;
        public const int OpeningHandSize = 3;
        public const int MaxHandSize = 10;
        public const int CorePackSize = 14;
        public const int ArchetypePackSize = 10;

        public const int InitialCrisisBudget = 20;

        public const int DeliveryTarget = 12;
        public const int QualityTarget = 8;
        public const int UxTarget = 6;
        public const int MoraleTarget = 4;
        public const int MoraleFailAt = 0;
        public const int PressureTargetMax = 6;
        public const int PressureFailAt = 12;
        public const int InitialDelivery = 0;
        public const int InitialQuality = 0;
        public const int InitialUx = 0;
        public const int InitialMorale = 6;
        public const int InitialPressure = 4;

        public const int MaxChainPerDay = 5;
        public const float ChainWindowSeconds = 1.2f;

        // 兼容旧 Demo 命名
        public const int DeckSize = 20;
        public const int CardsPerTurnDraw = CardsPerDayDraw;
    }
}
