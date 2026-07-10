using System.Collections;
using OfficeWar.Battle;
using OfficeWar.Battlefield;
using OfficeWar.Cards;
using OfficeWar.Core;
using UnityEngine;

namespace OfficeWar.AI
{
    public sealed class SimpleEnemyAI : MonoBehaviour
    {
        [SerializeField] float actionDelay = 0.6f;

        BattleManager _battle;

        public void Initialize(BattleManager battle)
        {
            _battle = battle;
            _battle.Turn.OnTurnStarted += HandleTurnStarted;
        }

        void OnDestroy()
        {
            if (_battle?.Turn != null)
                _battle.Turn.OnTurnStarted -= HandleTurnStarted;
        }

        void HandleTurnStarted(Faction faction)
        {
            if (faction == Faction.Enemy && _battle.Result == GameResult.Ongoing)
                StartCoroutine(RunTurn());
        }

        IEnumerator RunTurn()
        {
            yield return new WaitForSeconds(actionDelay);

            var deployed = false;
            foreach (var card in _battle.GetHand(Faction.Enemy).Cards)
            {
                if (_battle.TryPlayCardFromHand(card, Faction.Enemy))
                {
                    deployed = true;
                    break;
                }
            }

            if (deployed)
                yield return new WaitForSeconds(actionDelay);

            foreach (var unit in _battle.Board.GetUnits(Faction.Enemy))
            {
                if (unit.Source.Data.canAdvance && unit.IsInBackRow &&
                    _battle.Frontline.CanAdvanceToFrontline(Faction.Enemy))
                {
                    if (_battle.TryAdvanceUnit(unit, Faction.Enemy))
                        yield return new WaitForSeconds(actionDelay);
                }
            }

            foreach (var unit in _battle.Board.GetUnits(Faction.Enemy))
            {
                if (!_battle.CanUnitAttack(unit))
                    continue;

                UnitEntity bestTarget = null;
                var lowestMorale = int.MaxValue;
                foreach (var enemy in _battle.GetAttackableEnemyUnits(unit))
                {
                    if (enemy.CurrentMorale < lowestMorale)
                    {
                        lowestMorale = enemy.CurrentMorale;
                        bestTarget = enemy;
                    }
                }

                if (bestTarget != null)
                {
                    _battle.TryAttackUnit(unit, bestTarget, Faction.Enemy);
                    yield return new WaitForSeconds(actionDelay);
                    continue;
                }

                if (_battle.CanAttackEnemyHq(unit))
                {
                    _battle.TryAttackHq(unit, Faction.Enemy);
                    yield return new WaitForSeconds(actionDelay);
                }
            }

            yield return new WaitForSeconds(actionDelay);
            if (_battle.Result == GameResult.Ongoing && _battle.Turn.ActiveFaction == Faction.Enemy)
                _battle.EndTurn(Faction.Enemy);
        }
    }
}
