using System.Collections.Generic;
using OfficeWar.AI;
using OfficeWar.Battle;
using OfficeWar.Battlefield;
using OfficeWar.Core;
using OfficeWar.Input;
using OfficeWar.UI;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace OfficeWar.Demo
{
    /// <summary>
    /// 将空场景一键搭成可玩的 Demo。挂到任意 GameObject 上即可 Play。
    /// </summary>
    public sealed class BattleDemoBootstrap : MonoBehaviour
    {
        static Material _slotMaterial;
        static Material _unitMaterial;

        BattleManager _battle;

        void Awake()
        {
            EnsureMaterials();
            var camera = SetupCamera();
            EnsureEventSystem();
            _battle = new BattleManager();

            var slotViews = CreateBoard();
            var input = gameObject.AddComponent<PlayerInputController>();
            var hud = CreateHud();
            var handUi = CreateHandUi(hud.transform, input);
            input.Initialize(_battle, hud, handUi, slotViews, camera);

            var ai = gameObject.AddComponent<SimpleEnemyAI>();
            ai.Initialize(_battle);

            _battle.StartBattle();
        }

        // LEARN [阶段8]: 在 CreateHud() 末尾调用 CreatePerkHud(hud.transform, input)
        // LEARN [阶段9]: 在 CreateHud() 末尾调用 CreateOaPanel(hud.transform)

        static void EnsureMaterials()
        {
            var shader = Shader.Find("Universal Render Pipeline/Lit") ?? Shader.Find("Standard");

            if (_slotMaterial == null)
                _slotMaterial = new Material(shader);

            if (_unitMaterial == null)
                _unitMaterial = new Material(shader);
        }

        static Camera SetupCamera()
        {
            var camGo = new GameObject("Main Camera");
            var cam = camGo.AddComponent<Camera>();
            cam.tag = "MainCamera";
            cam.transform.position = new Vector3(0f, 12f, -6f);
            cam.transform.rotation = Quaternion.Euler(55f, 0f, 0f);
            cam.backgroundColor = new Color(0.12f, 0.14f, 0.18f);
            return cam;
        }

        static void EnsureEventSystem()
        {
            if (FindObjectOfType<EventSystem>() != null)
                return;

            var es = new GameObject("EventSystem");
            es.AddComponent<EventSystem>();
            es.AddComponent<StandaloneInputModule>();
        }

        List<SlotView> CreateBoard()
        {
            var root = new GameObject("Board");
            var slots = new List<SlotView>();

            _battle.Board.Initialize(1);

            slots.Add(CreateSlot(root.transform, _battle.Board.GetBackSlot(Faction.Enemy), new Vector3(0f, 0f, 5f),
                "敌方后方 · 产品 HQ"));
            slots.Add(CreateSlot(root.transform, _battle.Board.FrontlineSlot, new Vector3(0f, 0f, 0f),
                "中央阵线 · 老板视线"));
            slots.Add(CreateSlot(root.transform, _battle.Board.GetBackSlot(Faction.Player), new Vector3(0f, 0f, -5f),
                "己方后方 · 研发 HQ"));

            return slots;
        }

        SlotView CreateSlot(Transform parent, BoardSlot slot, Vector3 position, string title)
        {
            var go = GameObject.CreatePrimitive(PrimitiveType.Cube);
            go.name = title;
            go.transform.SetParent(parent);
            go.transform.position = position;
            go.transform.localScale = new Vector3(4f, 0.15f, 2.5f);

            var renderer = go.GetComponent<Renderer>();
            renderer.sharedMaterial = _slotMaterial;

            var labelGo = new GameObject("Label");
            labelGo.transform.SetParent(go.transform);
            labelGo.transform.localPosition = new Vector3(0f, 0.6f, 0f);
            labelGo.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
            var label = labelGo.AddComponent<TextMeshPro>();
            label.fontSize = 3;
            label.alignment = TextAlignmentOptions.Center;
            label.color = Color.white;

            var slotView = go.AddComponent<SlotView>();
            slotView.Setup(label, renderer, go.GetComponent<Collider>());
            slotView.Initialize(slot, title);
            return slotView;
        }

        BattleHUD CreateHud()
        {
            var canvasGo = new GameObject("Canvas");
            var canvas = canvasGo.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            var scaler = canvasGo.AddComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1920, 1080);
            canvasGo.AddComponent<GraphicRaycaster>();

            var hudGo = new GameObject("BattleHUD");
            hudGo.transform.SetParent(canvasGo.transform, false);
            var hud = hudGo.AddComponent<BattleHUD>();

            var panel = CreatePanel(hudGo.transform, new Vector2(0f, 1f), new Vector2(0f, 1f), new Vector2(0f, 1f),
                new Vector2(20f, -20f), new Vector2(420f, 220f), new Color(0f, 0f, 0f, 0.55f));

            var playerCoffee = CreateText(panel.transform, "PlayerCoffee", new Vector2(10f, -10f), 22);
            var enemyCoffee = CreateText(panel.transform, "EnemyCoffee", new Vector2(10f, -45f), 22);
            var playerHq = CreateText(panel.transform, "PlayerHQ", new Vector2(10f, -80f), 22);
            var enemyHq = CreateText(panel.transform, "EnemyHQ", new Vector2(10f, -115f), 22);
            var turn = CreateText(panel.transform, "Turn", new Vector2(10f, -150f), 20);
            var mode = CreateText(panel.transform, "Mode", new Vector2(10f, -185f), 18);

            var logPanel = CreatePanel(hudGo.transform, new Vector2(1f, 0f), new Vector2(1f, 0f), new Vector2(1f, 0f),
                new Vector2(-20f, 20f), new Vector2(520f, 200f), new Color(0f, 0f, 0f, 0.55f));
            var logText = CreateText(logPanel.transform, "Log", new Vector2(10f, -10f), 16);
            logText.rectTransform.sizeDelta = new Vector2(500f, 180f);
            logText.alignment = TextAlignmentOptions.TopLeft;

            var endTurnBtn = CreateButton(hudGo.transform, "结束回合", new Vector2(0.5f, 0f), new Vector2(-120f, 30f),
                new Vector2(240f, 56f));

            var resultPanel = CreatePanel(hudGo.transform, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f),
                new Vector2(0.5f, 0.5f), Vector2.zero, new Vector2(520f, 260f), new Color(0.05f, 0.08f, 0.12f, 0.92f));
            var resultText = CreateText(resultPanel.transform, "Result", new Vector2(0f, -30f), 28);
            resultText.alignment = TextAlignmentOptions.Center;
            resultText.rectTransform.anchorMin = new Vector2(0f, 0.5f);
            resultText.rectTransform.anchorMax = new Vector2(1f, 1f);
            resultText.rectTransform.offsetMin = new Vector2(20f, 0f);
            resultText.rectTransform.offsetMax = new Vector2(-20f, -20f);
            var restartBtn = CreateButton(resultPanel.transform, "再来一局", new Vector2(0.5f, 0f), new Vector2(0f, 30f),
                new Vector2(220f, 52f));

            hud.Setup(playerCoffee, enemyCoffee, playerHq, enemyHq, turn, mode, logText, endTurnBtn, resultPanel.gameObject,
                resultText, restartBtn);
            hud.Initialize(_battle);
            return hud;
        }

        HandUI CreateHandUi(Transform canvas, PlayerInputController input)
        {
            var handRoot = CreatePanel(canvas, new Vector2(0.5f, 0f), new Vector2(0.5f, 0f), new Vector2(0.5f, 0f),
                new Vector2(0f, 110f), new Vector2(900f, 170f), new Color(0f, 0f, 0f, 0.35f));

            var cardRootGo = new GameObject("CardRoot");
            cardRootGo.transform.SetParent(handRoot, false);
            var layout = cardRootGo.AddComponent<HorizontalLayoutGroup>();
            layout.spacing = 12f;
            layout.padding = new RectOffset(12, 12, 12, 12);
            layout.childAlignment = TextAnchor.MiddleCenter;
            layout.childForceExpandWidth = false;
            cardRootGo.AddComponent<ContentSizeFitter>().horizontalFit = ContentSizeFitter.FitMode.PreferredSize;

            var rt = cardRootGo.GetComponent<RectTransform>();
            rt.anchorMin = Vector2.zero;
            rt.anchorMax = Vector2.one;
            rt.offsetMin = Vector2.zero;
            rt.offsetMax = Vector2.zero;

            var cardPrefab = CreateCardPrefab();
            var handGo = new GameObject("HandUI");
            handGo.transform.SetParent(canvas, false);
            var handUi = handGo.AddComponent<HandUI>();
            handUi.Setup(cardRootGo.transform, cardPrefab);
            handUi.Initialize(_battle, input.OnCardClicked);
            return handUi;
        }

        CardView CreateCardPrefab()
        {
            var go = new GameObject("CardPrefab");
            go.SetActive(false);
            DontDestroyOnLoad(go);

            var rt = go.AddComponent<RectTransform>();
            rt.sizeDelta = new Vector2(180f, 130f);

            var bg = go.AddComponent<Image>();
            bg.color = new Color(0.92f, 0.94f, 0.98f);

            var button = go.AddComponent<Button>();
            var colors = button.colors;
            colors.highlightedColor = new Color(0.95f, 0.85f, 0.35f);
            button.colors = colors;

            var title = CreateText(go.transform, "Title", new Vector2(10f, -8f), 18);
            title.fontStyle = FontStyles.Bold;
            var detail = CreateText(go.transform, "Detail", new Vector2(10f, -42f), 14);
            detail.rectTransform.sizeDelta = new Vector2(160f, 80f);

            var cardView = go.AddComponent<CardView>();
            cardView.Setup(button, title, detail, bg);
            return cardView;
        }

        static RectTransform CreatePanel(Transform parent, Vector2 anchorMin, Vector2 anchorMax, Vector2 pivot,
            Vector2 anchoredPos, Vector2 size, Color color)
        {
            var go = new GameObject("Panel");
            go.transform.SetParent(parent, false);
            var rt = go.AddComponent<RectTransform>();
            rt.anchorMin = anchorMin;
            rt.anchorMax = anchorMax;
            rt.pivot = pivot;
            rt.anchoredPosition = anchoredPos;
            rt.sizeDelta = size;
            var img = go.AddComponent<Image>();
            img.color = color;
            return rt;
        }

        static TextMeshProUGUI CreateText(Transform parent, string name, Vector2 anchoredPos, float fontSize)
        {
            var go = new GameObject(name);
            go.transform.SetParent(parent, false);
            var rt = go.AddComponent<RectTransform>();
            rt.anchorMin = new Vector2(0f, 1f);
            rt.anchorMax = new Vector2(0f, 1f);
            rt.pivot = new Vector2(0f, 1f);
            rt.anchoredPosition = anchoredPos;
            rt.sizeDelta = new Vector2(400f, 30f);
            var text = go.AddComponent<TextMeshProUGUI>();
            text.fontSize = fontSize;
            text.color = Color.white;
            return text;
        }

        static Button CreateButton(Transform parent, string label, Vector2 anchor, Vector2 pos, Vector2 size)
        {
            var go = new GameObject(label);
            go.transform.SetParent(parent, false);
            var rt = go.AddComponent<RectTransform>();
            rt.anchorMin = anchor;
            rt.anchorMax = anchor;
            rt.pivot = new Vector2(0.5f, 0f);
            rt.anchoredPosition = pos;
            rt.sizeDelta = size;

            var img = go.AddComponent<Image>();
            img.color = new Color(0.2f, 0.55f, 0.95f);
            var btn = go.AddComponent<Button>();

            var text = CreateText(go.transform, "Text", Vector2.zero, 22);
            text.text = label;
            text.alignment = TextAlignmentOptions.Center;
            text.rectTransform.anchorMin = Vector2.zero;
            text.rectTransform.anchorMax = Vector2.one;
            text.rectTransform.offsetMin = Vector2.zero;
            text.rectTransform.offsetMax = Vector2.zero;
            return btn;
        }
    }

    public static class BattleDemoRuntime
    {
        static Material _unitMaterial;

        public static UnitView CreateUnitView(UnitEntity entity)
        {
            if (_unitMaterial == null)
            {
                var shader = Shader.Find("Universal Render Pipeline/Lit") ?? Shader.Find("Standard");
                _unitMaterial = new Material(shader);
            }

            var pos = RowPosition(entity.Slot.Row);
            var go = GameObject.CreatePrimitive(PrimitiveType.Cube);
            go.name = entity.Source.Data.displayName;
            go.transform.position = pos + Vector3.up * 0.6f;
            go.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);

            var renderer = go.GetComponent<Renderer>();
            renderer.sharedMaterial = _unitMaterial;

            var labelGo = new GameObject("Label");
            labelGo.transform.SetParent(go.transform);
            labelGo.transform.localPosition = new Vector3(0f, 0.75f, 0f);
            labelGo.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
            var label = labelGo.AddComponent<TextMeshPro>();
            label.fontSize = 2.5f;
            label.alignment = TextAlignmentOptions.Center;
            label.color = Color.white;

            var view = go.AddComponent<UnitView>();
            view.Setup(label, renderer);
            view.Bind(entity);
            return view;
        }

        static Vector3 RowPosition(BoardRow row) => GetRowPosition(row);

        public static Vector3 GetRowPosition(BoardRow row) => row switch
        {
            BoardRow.EnemyBack => new Vector3(0f, 0f, 5f),
            BoardRow.Frontline => new Vector3(0f, 0f, 0f),
            BoardRow.PlayerBack => new Vector3(0f, 0f, -5f),
            _ => Vector3.zero
        };
    }
}
