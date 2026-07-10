using System.Collections.Generic;
using OfficeWar.Core;
using UnityEngine;

namespace OfficeWar.Data
{
    /// <summary>
    /// Demo 内置卡牌库，无需手动创建 ScriptableObject 资产即可运行。
    /// </summary>
    public static class CardCatalog
    {
        static List<OfficeCardData> _cache;

        public static IReadOnlyList<OfficeCardData> AllCards
        {
            get
            {
                if (_cache == null)
                    BuildCache();
                return _cache;
            }
        }

        public static List<OfficeCardData> CreateDemoDeck()
        {
            var deck = new List<OfficeCardData>();
            AddMany(deck, Find("intern_rookie"), 4);
            AddMany(deck, Find("intern_veteran"), 2);
            AddMany(deck, Find("engineer_mid"), 3);
            AddMany(deck, Find("engineer_senior"), 2);
            AddMany(deck, Find("manager_ppt"), 3);
            AddMany(deck, Find("manager_director"), 2);
            AddMany(deck, Find("hr_recruit"), 2);
            AddMany(deck, Find("hr_wellness"), 2);
            return deck;
        }

        static void BuildCache()
        {
            _cache = new List<OfficeCardData>
            {
                Create("intern_rookie", "外包老哥", JobType.Intern, hire: 1, action: 1, morale: 2, kpi: 1,
                    "便宜占线，适合挡枪。"),
                Create("intern_veteran", "老油条实习生", JobType.Intern, hire: 2, action: 1, morale: 3, kpi: 2,
                    "比外包老哥更能熬。"),
                Create("engineer_mid", "全栈小王", JobType.Engineer, hire: 3, action: 2, morale: 5, kpi: 3,
                    "血厚攻高，沟通成本也不低。"),
                Create("engineer_senior", "架构师老张", JobType.Engineer, hire: 4, action: 3, morale: 7, kpi: 4,
                    "团队核心，行动费很贵。"),
                Create("manager_ppt", "PPT战神", JobType.Manager, hire: 3, action: 2, morale: 3, kpi: 3,
                    "后方远程输出，不受反击。"),
                Create("manager_director", "画饼总监", JobType.Manager, hire: 4, action: 2, morale: 4, kpi: 4,
                    "汇报伤害更高。"),
                Create("hr_recruit", "招聘专员", JobType.HR, hire: 2, action: 1, morale: 3, kpi: 0,
                    "部署后点击友军恢复 3 点精神值。", support: true, canAdvance: false),
                Create("hr_wellness", "行政小姐姐", JobType.HR, hire: 3, action: 1, morale: 3, kpi: 0,
                    "部署后点击友军本回合 KPI +2。", support: true, canAdvance: false)
            };
        }

        static OfficeCardData Create(
            string id,
            string name,
            JobType job,
            int hire,
            int action,
            int morale,
            int kpi,
            string desc,
            bool support = false,
            bool canAdvance = true)
        {
            var card = ScriptableObject.CreateInstance<OfficeCardData>();
            card.cardId = id;
            card.displayName = name;
            card.job = job;
            card.hireCost = hire;
            card.actionCost = action;
            card.maxMorale = morale;
            card.kpi = kpi;
            card.description = desc;
            card.isSupport = support;
            card.canAdvance = canAdvance;
            return card;
        }

        static void AddMany(List<OfficeCardData> deck, OfficeCardData card, int count)
        {
            for (var i = 0; i < count; i++)
                deck.Add(card);
        }

        public static OfficeCardData Find(string cardId)
        {
            foreach (var card in AllCards)
            {
                if (card.cardId == cardId)
                    return card;
            }

            return null;
        }
    }
}
