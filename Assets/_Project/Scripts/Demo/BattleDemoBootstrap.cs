// =============================================================================
// 课 17 | BattleDemoBootstrap | 教案.md
// =============================================================================
// 完成课 10~16 后再实现。之前 Play 只会提示进度。
// =============================================================================

using UnityEngine;

namespace OfficeWar.Demo
{
    public sealed class BattleDemoBootstrap : MonoBehaviour
    {
        void Awake()
        {
            Debug.Log(
                "[明天能上线吗] 学习模式：请打开根目录 教案.md，从课 02 开始实现。\n" +
                "协作局：项目组 vs 压力方 · 德式 KPI · 工作日 Sprint。\n" +
                "课 17 完成后可一键 Play。参考答案：Reference/（旧 PvP 结构参考，枚举以教案为准）");
        }

        // LEARN [课17]: EnsureMaterials, SetupCamera, CreateBoard, CreateHud, CreateHandUi
        // LEARN [课17]: 组合 BattleManager, PlayerInputController, SimpleCrisisAI
        // LEARN [课18]: CreatePerkHud
        // LEARN [课20]: CreateOaPanel
    }
}
