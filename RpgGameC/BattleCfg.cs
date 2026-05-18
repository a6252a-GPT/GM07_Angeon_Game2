namespace RpgGameC

{

    internal sealed class BattleCfg

    {

        public required int EnemyMaxHp { get; init; }

        public required int EnemyDamagePerHit { get; init; }

        public required string[][] EnemyIdleFrames { get; init; }

        public required ConsoleColor EnemyColor { get; init; }

        public required string EnemyAttackMessage { get; init; }

        public int VictoryGoldReward { get; init; }

    }

}

