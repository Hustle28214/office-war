// =============================================================================
// 课 02 | GameEnums | 教案.md · plan.md §三 / §五.2
// 游戏：《明天能上线吗》— 协作 vs 压力方，德式多轨 KPI
// =============================================================================

namespace OfficeWar.Core
{
    /// <summary>场上单位兵种（≠ 玩家职位）</summary>
    public enum JobType
    {
        Unset
        // LEARN: Intern, Engineer, Manager, HR
    }

    /// <summary>战场阵营：项目组 vs 压力方</summary>
    public enum Faction
    {
        Unset
        // LEARN: Team, Crisis
    }

    public enum BoardRow
    {
        Unset
        // LEARN: TeamBack, Frontline, CrisisBack
    }

    /// <summary>工作日阶段（1 回合 = 1 工作日）</summary>
    public enum TurnPhase
    {
        Unset
        // LEARN: MorningStandup, Main, DayEnd
    }

    public enum GameResult
    {
        Unset
        // LEARN: Ongoing, TeamWin, TeamLose
    }

    /// <summary>玩家职位大类</summary>
    public enum PlayerRole
    {
        Unset
        // LEARN: PM, Dev, Design, QA, Scrum
    }

    /// <summary>子路线（绑定特色卡包）</summary>
    public enum RoleArchetype
    {
        Unset
        // LEARN: PmBusiness, PmPlatform, PmGrowth,
        // LEARN: DevFrontend, DevBackend, DevClient, DevInfra,
        // LEARN: DesignVisual, DesignUX, DesignMotion,
        // LEARN: QaFunctional, QaAutomation, QaPerfSec,
        // LEARN: ScrumAgile, ScrumTechLead
    }

    /// <summary>德式 KPI 计分轨</summary>
    public enum KpiTrack
    {
        Unset
        // LEARN: Delivery, Quality, UX, Morale, Pressure
    }

    /// <summary>出牌演出类型（钉钉感打击感）</summary>
    public enum CardFeel
    {
        Unset
        // LEARN: ThrowDoc, SendMessage, StampBug, MergePR, AlignMeeting
    }

    public enum PlayerActionMode
    {
        Unset
        // LEARN: None, DeployCard, SelectUnit, HRHeal
    }
}
