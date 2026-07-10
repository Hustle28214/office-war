using System;
using OfficeWar.Cards;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace OfficeWar.UI
{
    public sealed class CardView : MonoBehaviour
    {
        Button _button;
        TextMeshProUGUI _titleText;
        TextMeshProUGUI _detailText;
        Image _background;

        RuntimeCard _card;
        Action<RuntimeCard> _onClick;

        public RuntimeCard Card => _card;

        public void Setup(Button button, TextMeshProUGUI titleText, TextMeshProUGUI detailText, Image background)
        {
            _button = button;
            _titleText = titleText;
            _detailText = detailText;
            _background = background;
        }

        public void Bind(RuntimeCard card, bool interactable, Action<RuntimeCard> onClick)
        {
            _card = card;
            _onClick = onClick;

            var data = card.Data;
            _titleText.text = data.displayName;
            _detailText.text = $"入职 {data.hireCost} | 沟通 {data.actionCost}\n{data.job} 精{data.maxMorale} KPI{data.kpi}";
            _button.interactable = interactable;
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(() => _onClick?.Invoke(_card));
        }

        public void SetSelected(bool selected)
        {
            _background.color = selected ? new Color(0.95f, 0.85f, 0.35f) : new Color(0.92f, 0.94f, 0.98f);
        }
    }
}
