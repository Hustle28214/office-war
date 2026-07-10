using System;
using OfficeWar.Core;

namespace OfficeWar.Resources
{
    public sealed class DepartmentState
    {
        public Faction Faction { get; }
        public int HqBudget { get; private set; }
        public int CoffeeMax { get; private set; }
        public int CoffeeCurrent { get; private set; }

        public event Action<DepartmentState> OnChanged;

        public DepartmentState(Faction faction)
        {
            Faction = faction;
            HqBudget = GameConstants.InitialHqBudget;
            CoffeeMax = GameConstants.InitialCoffeeMax;
            CoffeeCurrent = CoffeeMax;
        }

        public bool TrySpendCoffee(int amount)
        {
            if (amount < 0 || CoffeeCurrent < amount)
                return false;

            CoffeeCurrent -= amount;
            Notify();
            return true;
        }

        public void RefillCoffee()
        {
            CoffeeCurrent = CoffeeMax;
            Notify();
        }

        public void IncreaseCoffeeMax()
        {
            if (CoffeeMax >= GameConstants.MaxCoffeeMax)
                return;

            CoffeeMax++;
            Notify();
        }

        public void TakeHqDamage(int amount)
        {
            if (amount <= 0)
                return;

            HqBudget = Math.Max(0, HqBudget - amount);
            Notify();
        }

        void Notify() => OnChanged?.Invoke(this);
    }
}
