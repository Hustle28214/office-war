// =============================================================================
// 课 02 | GameEnums | 教案.md
// =============================================================================

namespace OfficeWar.Core
{
    public enum JobType { Unset /* LEARN: Intern, Engineer, Manager, HR */ }
    public enum Faction { Unset /* LEARN: Player, Enemy */ }
    public enum BoardRow { Unset /* LEARN: PlayerBack, Frontline, EnemyBack */ }
    public enum TurnPhase { Unset /* LEARN: Draw, Main, End */ }
    public enum GameResult { Unset /* LEARN: Ongoing, PlayerWin, EnemyWin */ }
    public enum PlayerActionMode { Unset /* LEARN: None, DeployCard, SelectUnit, HRHeal */ }
}
