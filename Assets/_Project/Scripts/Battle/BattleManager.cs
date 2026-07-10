// =============================================================================
// 课 10~12 | BattleManager | 教案.md · Reference/Battle/BattleManager.cs
// =============================================================================

using System;
using OfficeWar.Battlefield;
using OfficeWar.Cards;
using OfficeWar.Core;
using OfficeWar.Resources;
using OfficeWar.Turn;

namespace OfficeWar.Battle
{
    public sealed class BattleManager
    {
        readonly BoardController _board = new();
        readonly TurnManager _turn = new();
        readonly Hand _playerHand = new();

        public DepartmentState PlayerDepartment { get; } = new(Faction.Unset);
        public DepartmentState EnemyDepartment { get; } = new(Faction.Unset);
        public Hand PlayerHand => _playerHand;
        public TurnManager Turn => _turn;
        public BoardController Board => _board;
        public GameResult Result { get; private set; } = GameResult.Unset;

        public event Action OnStateChanged;
        public event Action<string> OnMessage;
        public event Action<GameResult> OnGameEnded;

        public void StartBattle() =>
            OnMessage?.Invoke("课 10：请实现 StartBattle（参考 Reference）");

        public bool TryPlayCardFromHand(RuntimeCard card, Faction actingFaction) => false;
        public bool TryAdvanceUnit(UnitEntity unit, Faction actingFaction) => false;
        public bool TryAttackUnit(UnitEntity a, UnitEntity d, Faction f) => false;
        public bool TryAttackHq(UnitEntity attacker, Faction actingFaction) => false;
        public bool TryHrHeal(UnitEntity hr, UnitEntity target, Faction actingFaction) => false;
        public void EndTurn(Faction actingFaction) { }
        public bool TryActivatePerk(Faction actingFaction) => false;
    }
}
