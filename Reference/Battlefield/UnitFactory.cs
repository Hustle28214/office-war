using OfficeWar.Cards;
using OfficeWar.Core;

namespace OfficeWar.Battlefield
{
    public static class UnitFactory
    {
        public static UnitEntity Spawn(RuntimeCard card, Faction faction, BoardSlot slot)
        {
            return new UnitEntity(card, faction, slot);
        }
    }
}
