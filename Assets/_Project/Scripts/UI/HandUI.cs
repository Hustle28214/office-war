using System.Collections.Generic;
using OfficeWar.Battle;
using OfficeWar.Cards;
using OfficeWar.Core;
using UnityEngine;

namespace OfficeWar.UI
{
    public sealed class HandUI : MonoBehaviour
    {
        Transform _cardRoot;
        CardView _cardPrefab;

        readonly List<CardView> _views = new();
        BattleManager _battle;
        System.Action<RuntimeCard> _onCardClicked;

        public void Setup(Transform cardRoot, CardView cardPrefab)
        {
            _cardRoot = cardRoot;
            _cardPrefab = cardPrefab;
        }

        public void Initialize(BattleManager battle, System.Action<RuntimeCard> onCardClicked)
        {
            _battle = battle;
            _onCardClicked = onCardClicked;
            _battle.OnStateChanged += Refresh;
            Refresh();
        }

        public void SetSelectedCard(RuntimeCard card)
        {
            foreach (var view in _views)
                view.SetSelected(view.Card == card);
        }

        void Refresh()
        {
            foreach (var view in _views)
                Destroy(view.gameObject);
            _views.Clear();

            var canPlay = _battle.Result == GameResult.Ongoing &&
                          _battle.Turn.ActiveFaction == Faction.Player;

            foreach (var card in _battle.PlayerHand.Cards)
            {
                var view = Instantiate(_cardPrefab, _cardRoot);
                view.gameObject.SetActive(true);
                view.Bind(card, canPlay, _onCardClicked);
                _views.Add(view);
            }
        }

        void OnDestroy()
        {
            if (_battle != null)
                _battle.OnStateChanged -= Refresh;
        }
    }
}
