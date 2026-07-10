# 疯狂周一：办公室战争 — Demo

基于策划书的 Unity MVP Demo，使用 **Cube + uGUI** 跑通核心卡牌战斗循环。

## 快速开始

1. 用 **Unity 2022.3 LTS**（或更新版本）打开本目录作为项目根目录
2. 首次打开等待 Package 导入（TextMeshPro、UGUI）
3. 打开场景 `Assets/_Project/Scenes/Battle.unity`
4. 点击 **Play**

若场景未配置，也可在 Unity 菜单栏选择：**Office War → Setup Demo Scene**

## 操作说明

| 操作 | 方式 |
|---|---|
| 部署员工 | 点击手牌 → 点击「己方后方」格子 |
| 推进前线 | 点击己方单位 → 点击「中央阵线」 |
| 攻击单位 | 点击己方单位 → 点击敌方 Cube |
| 攻击敌方 HQ | 点击己方单位 → 点击「敌方后方」区域 |
| HR 支援 | 点击 HR 单位 → 点击友军 |
| 结束回合 | 点击「结束回合」按钮 |

## Demo 已实现功能

- 咖啡资源系统（入职费 / 沟通费，回合回满与上限成长）
- 三方战场（己方后方 / 前线 / 敌方后方）
- 8 种内置卡牌（无需 ScriptableObject 资产）
- 四类岗位：实习生、研发、管理（远程免反击）、HR（治疗/加攻）
- 单位部署、推进、互殴、HQ 伤害
- 胜负判定（HQ 预算归零）
- 简单敌方 AI
- 战斗日志与胜负面板

## 项目结构

```
Assets/_Project/Scripts/
├── Core/           GameEnums, GameConstants
├── Data/           OfficeCardData, CardCatalog（Demo 内置牌库）
├── Cards/          Deck, Hand, RuntimeCard
├── Resources/      DepartmentState（咖啡 + HQ）
├── Battlefield/    Board, Unit, Frontline
├── Combat/         CombatResolver
├── Turn/           TurnManager
├── Battle/         BattleManager（核心逻辑）
├── Input/          PlayerInputController
├── UI/             HUD, Hand, Unit/Slot/Card View
├── AI/             SimpleEnemyAI
└── Demo/           BattleDemoBootstrap（一键搭场景）
```

## 开发说明

- 逻辑与表现分离：`BattleManager` 纯 C#，View 层只负责显示与输入
- 扩展卡牌：编辑 `CardCatalog.cs` 或在 Unity 中创建 `OfficeCardData` 资产
- 完整策划与后续路线图见根目录 `plan.md`

## 已知限制（Demo 阶段）

- 每行仅 1 个格子（未做多列）
- 美术为占位 Cube
- 卡组固定 20 张，无构筑界面
- 无音效与存档
# office-war
