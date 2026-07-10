// =============================================================================
// 课 17 | BattleDemoBootstrap | 教案.md · Reference/Demo/BattleDemoBootstrap.cs
// =============================================================================
// 完成课 10~16 后再实现本文件。之前 Play 只会提示进度。
// =============================================================================

using UnityEngine;

namespace OfficeWar.Demo
{
    public sealed class BattleDemoBootstrap : MonoBehaviour
    {
        void Awake()
        {
            Debug.Log(
                "[OfficeWar] 学习模式：请打开根目录 教案.md，从课 02 开始实现。\n" +
                "课 17 完成后可一键 Play 完整 Demo。\n" +
                "参考答案：项目根目录 Reference/");
        }

        // LEARN [课17]: EnsureMaterials, SetupCamera, CreateBoard, CreateHud, CreateHandUi
        // LEARN [课17]: 组合 BattleManager, PlayerInputController, SimpleEnemyAI
        // LEARN [课18]: CreatePerkHud
        // LEARN [课20]: CreateOaPanel
    }
}
