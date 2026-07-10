using System.Collections.Generic;
using OfficeWar.Battle;
using OfficeWar.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace OfficeWar.UI
{
    public sealed class BattleHUD : MonoBehaviour
    {
        TextMeshProUGUI _playerCoffeeText;
        TextMeshProUGUI _enemyCoffeeText;
        TextMeshProUGUI _playerHqText;
        TextMeshProUGUI _enemyHqText;
        TextMeshProUGUI _turnText;
        TextMeshProUGUI _modeText;
        TextMeshProUGUI _logText;
        Button _endTurnButton;
        GameObject _resultPanel;
        TextMeshProUGUI _resultText;
        Button _restartButton;

        readonly Queue<string> _logLines = new();
        BattleManager _battle;

        public Button EndTurnButton => _endTurnButton;

        public void Setup(
            TextMeshProUGUI playerCoffeeText,
            TextMeshProUGUI enemyCoffeeText,
            TextMeshProUGUI playerHqText,
            TextMeshProUGUI enemyHqText,
            TextMeshProUGUI turnText,
            TextMeshProUGUI modeText,
            TextMeshProUGUI logText,
            Button endTurnButton,
            GameObject resultPanel,
            TextMeshProUGUI resultText,
            Button restartButton)
        {
            _playerCoffeeText = playerCoffeeText;
            _enemyCoffeeText = enemyCoffeeText;
            _playerHqText = playerHqText;
            _enemyHqText = enemyHqText;
            _turnText = turnText;
            _modeText = modeText;
            _logText = logText;
            _endTurnButton = endTurnButton;
            _resultPanel = resultPanel;
            _resultText = resultText;
            _restartButton = restartButton;
        }

        public void Initialize(BattleManager battle)
        {
            _battle = battle;
            _battle.OnStateChanged += Refresh;
            _battle.OnMessage += AppendLog;
            _battle.OnGameEnded += ShowResult;
            _endTurnButton.onClick.AddListener(() =>
            {
                if (_battle.Result == GameResult.Ongoing)
                    _battle.EndTurn(Faction.Player);
            });
            _restartButton.onClick.AddListener(() =>
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(
                    UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
            });
            _resultPanel.SetActive(false);
            Refresh();
        }

        public void SetModeText(string text) => _modeText.text = text;

        void Refresh()
        {
            var p = _battle.PlayerDepartment;
            var e = _battle.EnemyDepartment;
            _playerCoffeeText.text = $"咖啡 {p.CoffeeCurrent}/{p.CoffeeMax}";
            _enemyCoffeeText.text = $"咖啡 {e.CoffeeCurrent}/{e.CoffeeMax}";
            _playerHqText.text = $"研发 HQ {p.HqBudget}";
            _enemyHqText.text = $"产品 HQ {e.HqBudget}";

            var active = _battle.Turn.ActiveFaction == Faction.Player ? "玩家" : "电脑";
            _turnText.text = $"第 {_battle.Turn.TurnNumber} 回合 · {active} 行动";
            _endTurnButton.interactable =
                _battle.Result == GameResult.Ongoing && _battle.Turn.ActiveFaction == Faction.Player;
        }

        void AppendLog(string message)
        {
            _logLines.Enqueue(message);
            while (_logLines.Count > 6)
                _logLines.Dequeue();

            _logText.text = string.Join("\n", _logLines);
        }

        void ShowResult(GameResult result)
        {
            _resultPanel.SetActive(true);
            _resultText.text = result == GameResult.PlayerWin
                ? "胜利！\n你抢到了 S 级年终奖！"
                : "失败…\n部门预算归零。";
            _endTurnButton.interactable = false;
        }

        void OnDestroy()
        {
            if (_battle == null)
                return;
            _battle.OnStateChanged -= Refresh;
            _battle.OnMessage -= AppendLog;
            _battle.OnGameEnded -= ShowResult;
        }
    }
}
