using System.Collections.Generic;
using OfficeWar.Battle;
using OfficeWar.Battlefield;
using OfficeWar.Cards;
using OfficeWar.Core;
using OfficeWar.Demo;
using OfficeWar.UI;
using UnityEngine;

namespace OfficeWar.Input
{
    public sealed class PlayerInputController : MonoBehaviour
    {
        BattleManager _battle;
        BattleHUD _hud;
        HandUI _handUi;
        readonly Dictionary<BoardSlot, SlotView> _slotViews = new();
        readonly Dictionary<UnitEntity, UnitView> _unitViews = new();

        PlayerActionMode _mode = PlayerActionMode.None;
        RuntimeCard _selectedCard;
        UnitEntity _selectedUnit;

        Camera _camera;

        public void Initialize(
            BattleManager battle,
            BattleHUD hud,
            HandUI handUi,
            IEnumerable<SlotView> slotViews,
            Camera camera)
        {
            _battle = battle;
            _hud = hud;
            _handUi = handUi;
            _camera = camera;

            foreach (var slotView in slotViews)
                _slotViews[slotView.Slot] = slotView;

            _battle.OnStateChanged += HandleStateChanged;
            UpdateModeText();
        }

        void OnDestroy()
        {
            if (_battle != null)
                _battle.OnStateChanged -= HandleStateChanged;
        }

        void HandleStateChanged()
        {
            SyncUnitViews();
            if (_battle.Turn.ActiveFaction != Faction.Player)
                ClearSelection();
        }

        void SyncUnitViews()
        {
            var alive = new HashSet<UnitEntity>();
            foreach (var unit in _battle.Board.GetAllUnits())
            {
                alive.Add(unit);
                if (!_unitViews.ContainsKey(unit))
                {
                    var view = BattleDemoRuntime.CreateUnitView(unit);
                    _unitViews[unit] = view;
                }
                else
                {
                    _unitViews[unit].Refresh();
                }
            }

            var toRemove = new List<UnitEntity>();
            foreach (var pair in _unitViews)
            {
                if (!alive.Contains(pair.Key))
                    toRemove.Add(pair.Key);
            }

            foreach (var unit in toRemove)
                _unitViews.Remove(unit);
        }

        void Update()
        {
            if (_battle.Result != GameResult.Ongoing)
                return;

            if (_battle.Turn.ActiveFaction != Faction.Player)
                return;

            if (!UnityEngine.Input.GetMouseButtonDown(0))
                return;

            var ray = _camera.ScreenPointToRay(UnityEngine.Input.mousePosition);
            if (!Physics.Raycast(ray, out var hit))
                return;

            if (hit.collider.TryGetComponent<UnitView>(out var unitView))
            {
                HandleUnitClick(unitView.Entity);
                return;
            }

            if (hit.collider.TryGetComponent<SlotView>(out var slotView))
                HandleSlotClick(slotView);
        }

        public void OnCardClicked(RuntimeCard card)
        {
            if (_battle.Turn.ActiveFaction != Faction.Player)
                return;

            _selectedCard = card;
            _selectedUnit = null;
            _mode = PlayerActionMode.DeployCard;
            HighlightDeploySlots();
            _handUi.SetSelectedCard(card);
            UpdateModeText();
        }

        void HandleUnitClick(UnitEntity unit)
        {
            if (_mode == PlayerActionMode.HRHeal && _selectedUnit != null)
            {
                if (unit.Faction == Faction.Player && unit != _selectedUnit)
                    _battle.TryHrHeal(_selectedUnit, unit, Faction.Player);
                ClearSelection();
                return;
            }

            if (unit.Faction != Faction.Player)
            {
                if (_selectedUnit != null && _battle.CanUnitAttack(_selectedUnit))
                    _battle.TryAttackUnit(_selectedUnit, unit, Faction.Player);
                ClearSelection();
                return;
            }

            _selectedUnit = unit;
            _selectedCard = null;
            _handUi.SetSelectedCard(null);

            if (unit.Source.Data.isSupport && !unit.SupportUsedThisTurn)
            {
                _mode = PlayerActionMode.HRHeal;
                UpdateModeText();
                return;
            }

            _mode = PlayerActionMode.SelectUnit;
            HighlightActionTargets(unit);
            UpdateModeText();
        }

        void HandleSlotClick(SlotView slotView)
        {
            if (_mode == PlayerActionMode.DeployCard && _selectedCard != null &&
                slotView.Slot.Row == BoardRow.PlayerBack)
            {
                if (_battle.TryPlayCardFromHand(_selectedCard, Faction.Player))
                    ClearSelection();
                return;
            }

            if (_mode == PlayerActionMode.SelectUnit && _selectedUnit != null)
            {
                if (slotView.Slot.Row == BoardRow.Frontline && _selectedUnit.IsInBackRow)
                {
                    if (_battle.TryAdvanceUnit(_selectedUnit, Faction.Player))
                        ClearSelection();
                    return;
                }

                if (slotView.Slot.Row == BoardRow.EnemyBack &&
                    _battle.CanAttackEnemyHq(_selectedUnit))
                {
                    if (_battle.TryAttackHq(_selectedUnit, Faction.Player))
                        ClearSelection();
                }
            }
        }

        void HighlightDeploySlots()
        {
            ClearHighlights();
            foreach (var pair in _slotViews)
            {
                if (pair.Key.Row == BoardRow.PlayerBack && pair.Key.IsEmpty)
                    pair.Value.SetHighlighted(true);
            }
        }

        void HighlightActionTargets(UnitEntity unit)
        {
            ClearHighlights();

            if (unit.IsInBackRow && unit.Source.Data.canAdvance &&
                _battle.Frontline.CanAdvanceToFrontline(Faction.Player))
                _slotViews[_battle.Board.FrontlineSlot].SetHighlighted(true);

            if (_battle.CanAttackEnemyHq(unit))
                _slotViews[_battle.Board.GetBackSlot(Faction.Enemy)].SetHighlighted(true);

            foreach (var enemy in _battle.GetAttackableEnemyUnits(unit))
            {
                if (_unitViews.TryGetValue(enemy, out var view))
                    view.gameObject.transform.localScale = Vector3.one * 1.15f;
            }
        }

        void ClearHighlights()
        {
            foreach (var pair in _slotViews)
                pair.Value.SetHighlighted(false);

            foreach (var view in _unitViews.Values)
                view.transform.localScale = Vector3.one;
        }

        void ClearSelection()
        {
            _mode = PlayerActionMode.None;
            _selectedCard = null;
            _selectedUnit = null;
            _handUi.SetSelectedCard(null);
            ClearHighlights();
            UpdateModeText();
        }

        void UpdateModeText()
        {
            var text = _mode switch
            {
                PlayerActionMode.DeployCard => "部署模式：点击己方后方格子",
                PlayerActionMode.SelectUnit when _selectedUnit != null =>
                    $"行动模式：{_selectedUnit.Source.Data.displayName} — 点击前线推进、敌人或敌方 HQ",
                PlayerActionMode.HRHeal => "HR 支援：点击一名友军",
                _ => "点击手牌部署，或点击己方单位行动"
            };
            _hud.SetModeText(text);
        }
    }
}
