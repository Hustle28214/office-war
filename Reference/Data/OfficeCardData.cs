using OfficeWar.Core;
using UnityEngine;

namespace OfficeWar.Data
{
    [CreateAssetMenu(fileName = "NewOfficeCard", menuName = "OfficeWar/Card")]
    public class OfficeCardData : ScriptableObject
    {
        public string cardId;
        public string displayName;
        public JobType job;
        public int hireCost = 1;
        public int actionCost = 1;
        public int maxMorale = 2;
        public int kpi = 1;
        public Sprite icon;
        public bool canAdvance = true;
        public bool isSupport;
        [TextArea] public string description;
    }
}
