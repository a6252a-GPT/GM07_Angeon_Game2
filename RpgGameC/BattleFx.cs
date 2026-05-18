namespace RpgGameC

{

    internal static class BattleFx

    {

        public static void PlayPlayerAttack(

            int playerHp,

            int enemyHp,

            int potionCount,

            bool heavy,

            string[][]? enemyIdleFrames = null,

            ConsoleColor enemyColor = ConsoleColor.Red)

        {

            string[][] frames = enemyIdleFrames ?? EnemyArt.EnemyIdleFrames;



            int[] offsets = heavy

                ? new[] { 0, 1, 2, 3, 4, 5, 6, 6, 5, 4, 3, 2, 1, 0 }

                : new[] { 0, 1, 2, 3, 4, 3, 2, 1, 0 };



            for (int step = 0; step < offsets.Length; step++)

            {

                int ox = offsets[step];

                int maxO = offsets.Max();

                bool atPeak = ox == maxO;



                Console.Clear();



                int walkFrame = step % 2;



                BattleUi.DrawBattleScreen(

                    playerHp,

                    enemyHp,

                    potionCount,

                    true,

                    walkFrame,

                    walkFrame,

                    playerOffsetX: ox,

                    playerOffsetY: 0,

                    enemyOffsetX: 0,

                    enemyOffsetY: 0,

                    playerSpriteOverride: atPeak ? EnemyArt.PlayerAttackPose : null,

                    enemySpriteOverride: null,

                    enemyIdleFrames: frames,

                    enemyColor: enemyColor);



                Thread.Sleep(heavy ? 22 : 24);

            }

        }



        public static void PlayPlayerStagger(

            int playerHp,

            int enemyHp,

            int potionCount,

            string[][]? enemyIdleFrames = null,

            ConsoleColor enemyColor = ConsoleColor.Red)

        {

            string[][] frames = enemyIdleFrames ?? EnemyArt.EnemyIdleFrames;

            int[] shakeX = { 0, -1, 1, -2, 2, -1, 1, 0 };



            for (int step = 0; step < shakeX.Length; step++)

            {

                Console.Clear();



                BattleUi.DrawBattleScreen(

                    playerHp,

                    enemyHp,

                    potionCount,

                    true,

                    step % 2,

                    0,

                    playerOffsetX: shakeX[step],

                    playerOffsetY: 0,

                    enemyOffsetX: 0,

                    enemyOffsetY: 0,

                    enemyIdleFrames: frames,

                    enemyColor: enemyColor);



                Thread.Sleep(26);

            }

        }



        public static void PlayEnemyStrike(

            int playerHp,

            int enemyHp,

            int potionCount,

            string[][]? enemyIdleFrames = null,

            ConsoleColor enemyColor = ConsoleColor.Red)

        {

            string[][] frames = enemyIdleFrames ?? EnemyArt.EnemyIdleFrames;

            int[] offsets = { 0, -1, -2, -3, -4, -5, -5, -4, -3, -2, -1, 0 };



            for (int step = 0; step < offsets.Length; step++)

            {

                int ex = offsets[step];

                int minO = offsets.Min();

                bool atPeak = ex == minO;



                Console.Clear();



                int walkFrame = step % 2;



                BattleUi.DrawBattleScreen(

                    playerHp,

                    enemyHp,

                    potionCount,

                    true,

                    walkFrame,

                    walkFrame,

                    playerOffsetX: 0,

                    playerOffsetY: 0,

                    enemyOffsetX: ex,

                    enemyOffsetY: 0,

                    playerSpriteOverride: null,

                    enemySpriteOverride: atPeak ? frames[1] : null,

                    enemyIdleFrames: frames,

                    enemyColor: enemyColor);



                Thread.Sleep(22);

            }

        }

    }

}

