using OfficeWar.Battlefield;
using OfficeWar.Core;
using TMPro;
using UnityEngine;

namespace OfficeWar.UI
{
    public sealed class SlotView : MonoBehaviour
    {
        static readonly Color IdleColor = new(0.35f, 0.35f, 0.38f, 0.5f);
        static readonly Color HighlightColor = new(0.95f, 0.85f, 0.2f, 0.85f);
        static readonly Color FrontlineColor = new(0.55f, 0.45f, 0.2f, 0.6f);

        TextMeshPro _label;
        Renderer _floorRenderer;
        Collider _clickCollider;

        public BoardSlot Slot { get; private set; }
        public bool IsHighlighted { get; private set; }

        public void Setup(TextMeshPro label, Renderer floorRenderer, Collider clickCollider)
        {
            _label = label;
            _floorRenderer = floorRenderer;
            _clickCollider = clickCollider;
        }

        public void Initialize(BoardSlot slot, string title)
        {
            Slot = slot;
            _label.text = title;
            _floorRenderer.material.color = slot.Row == BoardRow.Frontline ? FrontlineColor : IdleColor;
        }

        public void SetHighlighted(bool on)
        {
            IsHighlighted = on;
            if (Slot.Row == BoardRow.Frontline)
                _floorRenderer.material.color = on ? HighlightColor : FrontlineColor;
            else
                _floorRenderer.material.color = on ? HighlightColor : IdleColor;
        }

        public Collider ClickCollider => _clickCollider;
    }
}
