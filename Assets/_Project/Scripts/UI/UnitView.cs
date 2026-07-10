using OfficeWar.Battlefield;
using OfficeWar.Core;
using OfficeWar.Demo;
using TMPro;
using UnityEngine;

namespace OfficeWar.UI
{
    public sealed class UnitView : MonoBehaviour
    {
        static readonly Color PlayerColor = new(0.25f, 0.55f, 0.95f);
        static readonly Color EnemyColor = new(0.92f, 0.35f, 0.32f);

        TextMeshPro _label;
        Renderer _bodyRenderer;

        public UnitEntity Entity { get; private set; }

        public void Setup(TextMeshPro label, Renderer bodyRenderer)
        {
            _label = label;
            _bodyRenderer = bodyRenderer;
        }

        public void Bind(UnitEntity entity)
        {
            Entity = entity;
            entity.OnChanged += _ => Refresh();
            entity.OnDied += _ => Destroy(gameObject);
            Refresh();
        }

        public void Refresh()
        {
            if (Entity == null)
                return;

            var data = Entity.Source.Data;
            _label.text =
                $"{data.displayName}\n{JobShort(data.job)} 精{Entity.CurrentMorale}/{data.maxMorale} KPI{Entity.EffectiveKpi}";
            _bodyRenderer.material.color = Entity.Faction == Faction.Player ? PlayerColor : EnemyColor;
            transform.position = BattleDemoRuntime.GetRowPosition(Entity.Slot.Row) + Vector3.up * 0.6f;
        }

        static string JobShort(JobType job) => job switch
        {
            JobType.Intern => "实习",
            JobType.Engineer => "研发",
            JobType.Manager => "管理",
            JobType.HR => "HR",
            _ => "?"
        };
    }
}
