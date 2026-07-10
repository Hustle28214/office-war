using System;
using System.Collections.Generic;
using OfficeWar.Battlefield;
using OfficeWar.Cards;
using OfficeWar.Combat;
using OfficeWar.Core;
using OfficeWar.Data;
using OfficeWar.Resources;
using OfficeWar.Turn;

namespace OfficeWar.Battle
{
    public sealed class BattleManager
    {
        readonly BoardController _board = new();
        readonly FrontlineController _frontline;
        readonly TurnManager _turn = new();
        readonly Deck _playerDeck = new();
        readonly Deck _enemyDeck = new();
        readonly Hand _playerHand = new();
        readonly Hand _enemyHand = new();
        readonly List<UnitEntity> _units = new();

        // LEARN [阶段8]: 取消注释 — using OfficeWar.Perks;
        // readonly PerkController _perks = new();

        // LEARN [阶段9]: 取消注释 — using OfficeWar.OA;
        // readonly OAController _oa = new();

        public DepartmentState PlayerDepartment { get; } = new(Faction.Player);
        public DepartmentState EnemyDepartment { get; } = new(Faction.Enemy);
        public Hand PlayerHand => _playerHand;
        public TurnManager Turn => _turn;
        public BoardController Board => _board;
        public FrontlineController Frontline => _frontline;
        public GameResult Result { get; private set; } = GameResult.Ongoing;

        public event Action OnStateChanged;
        public event Action<string> OnMessage;
        public event Action<GameResult> OnGameEnded;

        public BattleManager()
        {
            _frontline = new FrontlineController(_board);
        }

        public void StartBattle()
        {
            Result = GameResult.Ongoing;
            _board.Initialize(1);
            _units.Clear();
            _playerHand.Clear();
            _enemyHand.Clear();

            _playerDeck.LoadFromCatalog(CardCatalog.CreateDemoDeck());
            _enemyDeck.LoadFromCatalog(CardCatalog.CreateDemoDeck());

            for (var i = 0; i < GameConstants.OpeningHandSize; i++)
            {
                DrawToHand(Faction.Player);
                DrawToHand(Faction.Enemy);
            }

            _turn.StartBattle();
            BeginTurn(Faction.Player, increaseMax: false, drawCard: false);
            Notify("战斗开始！产品经理部 vs 研发程序员部");
        }

        void BeginTurn(Faction faction, bool increaseMax = true, bool drawCard = true)
        {
            if (increaseMax)
                GetDepartment(faction).IncreaseCoffeeMax();

            GetDepartment(faction).RefillCoffee();
            _board.ResetAllActedFlags(faction);

            foreach (var unit in _board.GetUnits(faction))
                unit.SupportUsedThisTurn = false;

            if (drawCard)
                DrawToHand(faction);

            // LEARN [阶段8]: _perks.OnTurnStarted(faction, _turn.TurnNumber);
            // LEARN [阶段9]: _oa.TickApprovals(_turn.TurnNumber);  // 审批结算 → 通过后激活 Perk

            Notify($"{FactionLabel(faction)} 回合开始 — 咖啡 {GetDepartment(faction).CoffeeCurrent}/{GetDepartment(faction).CoffeeMax}");
            NotifyChanged();
        }

        void DrawToHand(Faction faction)
        {
            var hand = faction == Faction.Player ? _playerHand : _enemyHand;
            var deck = faction == Faction.Player ? _playerDeck : _enemyDeck;

            if (hand.IsFull)
                return;

            var card = deck.Draw();
            if (card != null)
                hand.Add(card);
        }

        public bool TryPlayCardFromHand(RuntimeCard card, Faction actingFaction)
        {
            if (!CanAct(actingFaction))
                return false;

            var hand = actingFaction == Faction.Player ? _playerHand : _enemyHand;
            if (!hand.Cards.Contains(card))
                return Fail("手牌中没有这张卡。");

            var slot = _board.GetBackSlot(actingFaction);
            if (!slot.IsEmpty)
                return Fail("后方已满，无法部署。");

            var dept = GetDepartment(actingFaction);
            // LEARN [阶段8]: var hireCost = Math.Max(0, card.Data.hireCost - _perks.GetHireCostModifier(actingFaction, isFirstDeploy));
            if (!dept.TrySpendCoffee(card.Data.hireCost))
                return Fail("咖啡不足，无法部署。");

            var unit = UnitFactory.Spawn(card, actingFaction, slot);
            WireUnit(unit);
            hand.Remove(card);

            Notify($"{FactionLabel(actingFaction)} 部署了 {card.Data.displayName}（-{card.Data.hireCost} 咖啡）");
            NotifyChanged();
            return true;
        }

        public bool TryAdvanceUnit(UnitEntity unit, Faction actingFaction)
        {
            if (!CanAct(actingFaction))
                return false;

            if (unit.Faction != actingFaction)
                return Fail("不能操作敌方单位。");

            if (!unit.Source.Data.canAdvance)
                return Fail("该单位无法推进到前线。");

            if (!unit.IsInBackRow)
                return Fail("该单位已经在前线。");

            if (!_frontline.CanAdvanceToFrontline(actingFaction))
                return Fail("前线被敌方占据，无法推进。");

            var dept = GetDepartment(actingFaction);
            // LEARN [阶段8]: var actionCost = Math.Max(0, unit.Source.Data.actionCost - _perks.GetActionCostModifier(actingFaction));
            if (!dept.TrySpendCoffee(unit.Source.Data.actionCost))
                return Fail("沟通咖啡不足，员工选择摸鱼。");

            var front = _board.FrontlineSlot;
            unit.Slot.Occupant = null;
            unit.Slot = front;
            front.Occupant = unit;
            unit.HasActedThisTurn = true;

            // LEARN [阶段8]: _perks.OnFrontlineChanged(_frontline.GetOwner());

            Notify($"{unit.Source.Data.displayName} 推进到前线（-{unit.Source.Data.actionCost} 咖啡）");
            NotifyChanged();
            return true;
        }

        public bool TryAttackUnit(UnitEntity attacker, UnitEntity defender, Faction actingFaction)
        {
            if (!CanAct(actingFaction))
                return false;

            if (attacker.Faction != actingFaction)
                return Fail("不能操作敌方单位。");

            if (defender.Faction == actingFaction)
                return Fail("不能攻击友军。");

            if (!CanUnitAttack(attacker))
                return Fail("该单位当前无法发起攻击。");

            var dept = GetDepartment(actingFaction);
            if (!dept.TrySpendCoffee(attacker.Source.Data.actionCost))
                return Fail("沟通咖啡不足，无法撕逼。");

            if (attacker.Source.Data.job == JobType.Manager && attacker.IsInBackRow)
                CombatResolver.ResolveRangedAttack(attacker, defender, true);
            else
                CombatResolver.ResolveUnitVsUnit(attacker, defender);

            attacker.HasActedThisTurn = true;
            Notify($"{attacker.Source.Data.displayName} 攻击 {defender.Source.Data.displayName}！");
            CleanupDeadUnits();
            CheckVictory();
            NotifyChanged();
            return true;
        }

        public bool TryAttackHq(UnitEntity attacker, Faction actingFaction)
        {
            if (!CanAct(actingFaction))
                return false;

            if (attacker.Faction != actingFaction)
                return Fail("不能操作敌方单位。");

            if (!CanUnitAttack(attacker))
                return Fail("该单位当前无法攻击 HQ。");

            if (!CanAttackEnemyHq(attacker))
                return Fail("未满足攻击敌方主管办公室的条件。");

            var dept = GetDepartment(actingFaction);
            if (!dept.TrySpendCoffee(attacker.Source.Data.actionCost))
                return Fail("沟通咖啡不足，无法汇报甩锅。");

            var targetHq = actingFaction == Faction.Player ? EnemyDepartment : PlayerDepartment;

            if (attacker.Source.Data.job == JobType.Manager && attacker.IsInBackRow)
                CombatResolver.ResolveRangedAttackOnHq(attacker, targetHq);
            else
                CombatResolver.ResolveUnitVsHq(attacker, targetHq);

            attacker.HasActedThisTurn = true;
            Notify($"{attacker.Source.Data.displayName} 向敌方 HQ 发起汇报！造成 {attacker.EffectiveKpi} 点预算伤害");
            CheckVictory();
            NotifyChanged();
            return true;
        }

        public bool TryHrHeal(UnitEntity hr, UnitEntity target, Faction actingFaction)
        {
            if (!CanAct(actingFaction))
                return false;

            if (hr.Source.Data.job != JobType.HR || !hr.Source.Data.isSupport)
                return Fail("只有 HR 单位可以使用支援。");

            if (hr.Faction != actingFaction || target.Faction != actingFaction)
                return Fail("只能治疗友军。");

            if (hr.SupportUsedThisTurn)
                return Fail("该 HR 本回合已使用过支援。");

            var dept = GetDepartment(actingFaction);
            if (!dept.TrySpendCoffee(hr.Source.Data.actionCost))
                return Fail("沟通咖啡不足。");

            hr.SupportUsedThisTurn = true;
            hr.HasActedThisTurn = true;

            if (hr.Source.Data.cardId == "hr_wellness")
            {
                target.ApplyKpiBuff(2);
                Notify($"{hr.Source.Data.displayName} 给 {target.Source.Data.displayName} 打鸡血，KPI +2");
            }
            else
            {
                target.Heal(3);
                Notify($"{hr.Source.Data.displayName} 为 {target.Source.Data.displayName} 恢复 3 点精神值");
            }

            NotifyChanged();
            return true;
        }

        public void EndTurn(Faction actingFaction)
        {
            if (!CanAct(actingFaction))
                return;

            // LEARN [阶段8]: _perks.TickEndOfTurn(actingFaction);

            _turn.EndTurn();
            BeginTurn(_turn.ActiveFaction);
        }

        // LEARN [阶段8]: 供 PerkHUD 调用；requiresOA 时改走 OA（阶段9）
        public bool TryActivatePerk(/* PerkType type, */ Faction actingFaction)
        {
            Notify("福利系统练习：见 LEARNING.md 阶段8 与 Scripts/Perks/");
            return false;
        }

        public bool CanUnitAttack(UnitEntity unit)
        {
            if (unit.HasActedThisTurn)
                return false;

            if (unit.Source.Data.isSupport)
                return false;

            if (unit.IsOnFrontline)
                return true;

            if (unit.Source.Data.job == JobType.Manager && unit.IsInBackRow && _frontline.IsControlledBy(unit.Faction))
                return true;

            return false;
        }

        public bool CanAttackEnemyHq(UnitEntity attacker)
        {
            var enemy = attacker.Faction == Faction.Player ? Faction.Enemy : Faction.Player;

            if (attacker.IsOnFrontline)
                return true;

            if (attacker.Source.Data.job == JobType.Manager && attacker.IsInBackRow &&
                _frontline.IsControlledBy(attacker.Faction))
                return true;

            if (attacker.IsInBackRow && _frontline.IsControlledBy(enemy))
                return attacker.Source.Data.job != JobType.Manager;

            return false;
        }

        public IEnumerable<UnitEntity> GetAttackableEnemyUnits(UnitEntity attacker)
        {
            var enemy = attacker.Faction == Faction.Player ? Faction.Enemy : Faction.Player;
            foreach (var unit in _board.GetUnits(enemy))
            {
                if (attacker.IsOnFrontline)
                    yield return unit;
                else if (attacker.Source.Data.job == JobType.Manager && attacker.IsInBackRow &&
                         _frontline.IsControlledBy(attacker.Faction))
                    yield return unit;
            }
        }

        public DepartmentState GetDepartment(Faction faction) =>
            faction == Faction.Player ? PlayerDepartment : EnemyDepartment;

        public Hand GetHand(Faction faction) =>
            faction == Faction.Player ? _playerHand : _enemyHand;

        void WireUnit(UnitEntity unit)
        {
            _units.Add(unit);
            unit.OnDied += HandleUnitDied;
        }

        void HandleUnitDied(UnitEntity unit)
        {
            if (unit.Slot != null)
                unit.Slot.Occupant = null;
            _units.Remove(unit);
            Notify($"{unit.Source.Data.displayName} 精神崩溃，离场！");
        }

        void CleanupDeadUnits()
        {
            for (var i = _units.Count - 1; i >= 0; i--)
            {
                if (_units[i].IsDead)
                    HandleUnitDied(_units[i]);
            }
        }

        void CheckVictory()
        {
            if (Result != GameResult.Ongoing)
                return;

            if (EnemyDepartment.HqBudget <= 0)
            {
                Result = GameResult.PlayerWin;
                OnGameEnded?.Invoke(Result);
                Notify("胜利！你抢到了 S 级年终奖！");
                return;
            }

            if (PlayerDepartment.HqBudget <= 0)
            {
                Result = GameResult.EnemyWin;
                OnGameEnded?.Invoke(Result);
                Notify("失败… 你的部门预算被砍到零。");
            }
        }

        bool CanAct(Faction faction)
        {
            if (Result != GameResult.Ongoing)
                return Fail("战斗已结束。");

            if (_turn.ActiveFaction != faction)
                return Fail("还没轮到你。");

            return true;
        }

        bool Fail(string reason)
        {
            Notify(reason);
            return false;
        }

        void Notify(string msg) => OnMessage?.Invoke(msg);
        void NotifyChanged() => OnStateChanged?.Invoke();

        static string FactionLabel(Faction faction) =>
            faction == Faction.Player ? "玩家" : "电脑";
    }
}
