// =============================================================================
// 课 10~12 | BattleManager | 教案.md · plan.md §二 / §三
// =============================================================================
// 协作局 Facade：Team 出牌清阻塞、推 KPI；Crisis 由 AI 抬压力。
// =============================================================================

using System;
using OfficeWar.Battlefield;
using OfficeWar.Cards;
using OfficeWar.Combat;
using OfficeWar.Core;
using OfficeWar.Resources;
using OfficeWar.Turn;

namespace OfficeWar.Battle
{
    public sealed class BattleManager
    {
        readonly BoardController _board = new();
        readonly TurnManager _turn = new();
        readonly Hand _teamHand = new();
        readonly ChainResolver _chain = new();

        public DepartmentState TeamDepartment { get; } = new(Faction.Unset);
        public DepartmentState CrisisDepartment { get; } = new(Faction.Unset);
        public KpiBoardState Kpi { get; } = new();
        public Hand TeamHand => _teamHand;
        public TurnManager Turn => _turn;
        public BoardController Board => _board;
        public ChainResolver Chain => _chain;
        public PlayerRole LocalRole { get; private set; } = PlayerRole.Unset;
        public RoleArchetype LocalArchetype { get; private set; } = RoleArchetype.Unset;
        public GameResult Result { get; private set; } = GameResult.Unset;

        public event Action OnStateChanged;
        public event Action<string> OnMessage;
        public event Action<GameResult> OnGameEnded;

        public void StartBattle() =>
            OnMessage?.Invoke("课 10：实现 StartBattle（选角 Starter 包、洗牌、抽 3、StartSprint）");

        // LEARN 课10: TryPlayCardFromHand → TeamBack 空位，扣 Hire Cost，播 CardFeel
        public bool TryPlayCardFromHand(RuntimeCard card, Faction actingFaction) => false;

        // LEARN 课11: TryAdvanceUnit / TryAttackUnit / TryAttackCrisis
        public bool TryAdvanceUnit(UnitEntity unit, Faction actingFaction) => false;
        public bool TryAttackUnit(UnitEntity a, UnitEntity d, Faction f) => false;
        public bool TryAttackCrisis(UnitEntity attacker, Faction actingFaction) => false;

        // LEARN 课12: TryHrHeal / EndDay / CheckVictory（Kpi.MeetsAllTargets / HasFailed / Sprint 到期）
        public bool TryHrHeal(UnitEntity hr, UnitEntity target, Faction actingFaction) => false;
        public void EndDay(Faction actingFaction) { }
        public bool TryActivatePerk(Faction actingFaction) => false;
    }
}
