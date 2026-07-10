using System.Collections.Generic;
using OfficeWar.Core;

namespace OfficeWar.Cards
{
    public sealed class Hand
    {
        readonly List<RuntimeCard> _cards = new();

        public IReadOnlyList<RuntimeCard> Cards => _cards;

        public bool IsFull => _cards.Count >= GameConstants.MaxHandSize;

        public void Add(RuntimeCard card)
        {
            if (card == null || IsFull)
                return;
            _cards.Add(card);
        }

        public bool Remove(RuntimeCard card) => _cards.Remove(card);

        public void Clear() => _cards.Clear();
    }
}
