using System;
using System.Collections.Generic;
using OfficeWar.Data;

namespace OfficeWar.Cards
{
    public sealed class Deck
    {
        readonly List<RuntimeCard> _cards = new();

        public int Count => _cards.Count;

        public void LoadFromCatalog(IEnumerable<OfficeCardData> source)
        {
            _cards.Clear();
            foreach (var data in source)
                _cards.Add(new RuntimeCard(data));
            Shuffle();
        }

        public void Shuffle()
        {
            var rng = new Random();
            for (var i = _cards.Count - 1; i > 0; i--)
            {
                var j = rng.Next(i + 1);
                (_cards[i], _cards[j]) = (_cards[j], _cards[i]);
            }
        }

        public RuntimeCard Draw()
        {
            if (_cards.Count == 0)
                return null;

            var card = _cards[0];
            _cards.RemoveAt(0);
            return card;
        }
    }
}
