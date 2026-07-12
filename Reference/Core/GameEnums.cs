// =============================================================================
// Reference · Core/GameEnums — 与协作版 plan / 教案课 02 对齐
// =============================================================================

namespace OfficeWar.Core
{
    public enum JobType
    {
        Intern,
        Engineer,
        Manager,
        HR
    }

    public enum Faction
    {
        Team,
        Crisis
    }

    public enum BoardRow
    {
        TeamBack,
        Frontline,
        CrisisBack
    }

    public enum TurnPhase
    {
        MorningStandup,
        Main,
        DayEnd
    }

    public enum GameResult
    {
        Ongoing,
        TeamWin,
        TeamLose
    }

    public enum PlayerRole
    {
        PM,
        Dev,
        Design,
        QA,
        Scrum
    }

    public enum RoleArchetype
    {
        PmBusiness,
        PmPlatform,
        PmGrowth,
        DevFrontend,
        DevBackend,
        DevClient,
        DevInfra,
        DesignVisual,
        DesignUX,
        DesignMotion,
        QaFunctional,
        QaAutomation,
        QaPerfSec,
        ScrumAgile,
        ScrumTechLead
    }

    public enum KpiTrack
    {
        Delivery,
        Quality,
        UX,
        Morale,
        Pressure
    }

    public enum CardFeel
    {
        ThrowDoc,
        SendMessage,
        StampBug,
        MergePR,
        AlignMeeting
    }

    public enum PlayerActionMode
    {
        None,
        DeployCard,
        SelectUnit,
        HRHeal
    }
}
