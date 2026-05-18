namespace RpgGameC

{

    internal static class Balance

    {

        public const int StartingGold = 100;
        public const int PlayerInitialHp = 100;
        public const int BaseMaxPlayerHp = 100;
        public const int InitialPotionCount = 1;
        public const int PotionHealPercentOfMaxHp = 40;

        public static int PotHeal(int effectiveMaxHp)
        {

            if (effectiveMaxHp <= 0)
            {
                return 0;
            }

            int heal = effectiveMaxHp * PotionHealPercentOfMaxHp / 100;
            return heal < 1 ? 1 : heal;
        }

        public const int BaseEliteHp = 80;
        public const int Map2EliteHpMultiplier = 2;
        public const int BaseEnemyDamagePerHit = 12;
        public const int Map2EnemyDamageMultiplier = 2;
        public const int PlayerNormalAttackDamage = 10;
        public const int PlayerSpecialAttackDamage = 20;
        public const int BaseSpecialAttackSuccessPercent = 50;
        public const int MysteryElixirSpecialAttackBonusPercent = 30;

        public static int EliteHp(int mapNumber) => mapNumber == 1 ? BaseEliteHp : BaseEliteHp * Map2EliteHpMultiplier;

        public static int EliteAtk(int mapNumber) => mapNumber == 1 ? BaseEnemyDamagePerHit : BaseEnemyDamagePerHit * Map2EnemyDamageMultiplier;

        public const int BaseGrassMonsterHp = 30;
        public const int Map2GrassMonsterMultiplier = 2;
        public const int BaseGrassMonsterDamage = 5;
        public const int GrassBattleGoldRewardMap1 = 50;
        public const int Map2GrassGoldRewardMultiplier = 2;
        public const int GrassTreasureGoldAmount = 100;

        private static readonly Dictionary<int, int> GrassGoldByMap = new()
        {
            [1] = GrassBattleGoldRewardMap1,
            [2] = GrassBattleGoldRewardMap1 * Map2GrassGoldRewardMultiplier,
        };

        public const int GrassTreasureChancePercent = 5;
        public const int GrassBattleChancePercent = 30;
        public const int GrassEncounterMoveCooldown = 5;

        public static int GrassHp(int mapNumber) => mapNumber == 1 ? BaseGrassMonsterHp : BaseGrassMonsterHp * Map2GrassMonsterMultiplier;

        public static int GrassAtk(int mapNumber) => mapNumber == 1 ? BaseGrassMonsterDamage : BaseGrassMonsterDamage * Map2GrassMonsterMultiplier;

        public static int GrassGold(int mapNumber) => GrassGoldByMap[mapNumber];
    }
}

