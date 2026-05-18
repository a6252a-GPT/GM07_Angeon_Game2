namespace RpgGameC

{

    internal enum GrassRoll

    {

        None,

        Treasure,

        Battle,

    }



    internal static class GrassEvt

    {

        public static GrassRoll RollGrassEvent()

        {

            int roll = Random.Shared.Next(1, 101);



            if (roll <= Balance.GrassTreasureChancePercent)

            {

                return GrassRoll.Treasure;

            }



            if (roll <= Balance.GrassTreasureChancePercent + Balance.GrassBattleChancePercent)

            {

                return GrassRoll.Battle;

            }



            return GrassRoll.None;

        }



        public static void ShowTreasureFound(RunState run)

        {

            run.Gold += Balance.GrassTreasureGoldAmount;



            Console.Clear();



            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.SetCursorPosition(Console.WindowWidth / 2 - 14, Console.WindowHeight / 2);

            Console.WriteLine($"보물 발견! Gold +{Balance.GrassTreasureGoldAmount}");

            Console.ResetColor();



            Thread.Sleep(1500);

        }



        public static BattleRes RunGrassBattle(int mapNumber, RunState run)

        {

            GrassArt.Variant variant = GrassArt.NextVariant();



            return Battle.Run(

                run,

                new BattleCfg

                {

                    EnemyMaxHp = Balance.GrassHp(mapNumber),

                    EnemyDamagePerHit = Balance.GrassAtk(mapNumber),

                    EnemyIdleFrames = variant.IdleFrames,

                    EnemyColor = variant.Color,

                    EnemyAttackMessage = "풀숲 몬스터가 {0} 데미지를 입혔다!",

                    VictoryGoldReward = Balance.GrassGold(mapNumber),

                });

        }

    }

}

