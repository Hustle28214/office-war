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
        Player,
        Enemy
    }

    public enum BoardRow
    {
        PlayerBack,
        Frontline,
        EnemyBack
    }

    public enum TurnPhase
    {
        Draw,
        Main,
        End
    }

    public enum GameResult
    {
        Ongoing,
        PlayerWin,
        EnemyWin
    }

    public enum PlayerActionMode
    {
        None,
        DeployCard,
        SelectUnit,
        HRHeal
    }
}
