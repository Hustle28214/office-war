# 参考答案（/Reference）

本目录在 **Assets 外**，Unity **不会编译**，避免与 `Assets/_Project/Scripts/` 重名冲突。

## 使用规则

1. **先按 [`教案.md`](../教案.md) 自己写**  
2. 卡关再看同路径文件  
3. 不要整文件复制到 Assets  

## 协作改版说明（重要）

`plan.md` / 教案已改为 **《明天能上线吗》协作局**（Team vs Crisis · 多轨 KPI · 工作日）。

| 状态 | 内容 |
|---|---|
| **已同步** | `Reference/Core/GameEnums.cs`、`GameConstants.cs` |
| **部分过时** | `Battle/`、`AI/SimpleEnemyAI`、HUD 等仍可能是旧「Player/Enemy + HQ 归零」流程 |
| **怎么用** | 学 **模块怎么拆、事件怎么订**；胜负与命名以教案 + Assets 骨架 `LEARN:` 为准 |
| **待补** | 完整协作版 `BattleManager` / `KpiBoardState` / `SimpleCrisisAI` 答案将随课程迭代补齐 |

## 目录对应

| 你写的 | 参考答案 |
|---|---|
| `Assets/_Project/Scripts/Core/GameEnums.cs` | `Reference/Core/GameEnums.cs` |
| `Assets/_Project/Scripts/Battle/BattleManager.cs` | `Reference/Battle/BattleManager.cs`（结构参考） |

Perks / OA 无完整参考，按教案 + `plan.md` §三.7 / §三.8 实现。
