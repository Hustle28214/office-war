using OfficeWar.Battlefield;
using OfficeWar.Core;
using OfficeWar.Resources;

namespace OfficeWar.Combat
{
    public static class CombatResolver
    {
        public static void ResolveUnitVsUnit(UnitEntity attacker, UnitEntity defender)
        {
            defender.TakeDamage(attacker.EffectiveKpi);
            attacker.TakeDamage(defender.EffectiveKpi);
        }

        public static void ResolveUnitVsHq(UnitEntity attacker, DepartmentState targetHq)
        {
            targetHq.TakeHqDamage(attacker.EffectiveKpi);
        }

        public static void ResolveRangedAttack(UnitEntity attacker, UnitEntity defender, bool attackerInBackRow)
        {
            defender.TakeDamage(attacker.EffectiveKpi);
            if (!attackerInBackRow || attacker.Source.Data.job != JobType.Manager)
                attacker.TakeDamage(defender.EffectiveKpi);
        }

        public static void ResolveRangedAttackOnHq(UnitEntity attacker, DepartmentState targetHq)
        {
            targetHq.TakeHqDamage(attacker.EffectiveKpi);
        }
    }
}
