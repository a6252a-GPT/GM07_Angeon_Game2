namespace RpgGameC

{

    internal static class BattleUi

    {

        public static void DrawBattleActionMenu(int selectedIndex, int specialSuccessPercent)

        {

            int baseX = Console.WindowWidth / 2 - 8;

            int baseY = Console.WindowHeight - 10;



            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.SetCursorPosition(baseX - 2, baseY);

            Console.WriteLine("【 MY TURN 】");

            Console.ResetColor();



            string[] labels =

            {

                "일반공격",

                $"강한공격 (성공 확률 {specialSuccessPercent}%)",

                "포션 사용",

                "도망가기"

            };



            for (int i = 0; i < labels.Length; i++)

            {

                Console.SetCursorPosition(baseX, baseY + 2 + i);



                if (i == selectedIndex)

                {

                    Console.ForegroundColor = ConsoleColor.Yellow;

                    Console.WriteLine($"> {labels[i]}");

                    Console.ResetColor();

                }

                else

                {

                    Console.WriteLine($"  {labels[i]}");

                }

            }



            Console.SetCursorPosition(baseX - 4, baseY + 7);

            Console.WriteLine("↑ ↓ 선택 / Enter");

        }



        public static void DrawBattleScreen(

            int playerHp,

            int enemyHp,

            int potionCount,

            bool isPlayerTurn,

            int playerIdleFrame = 0,

            int enemyIdleFrame = 0,

            int playerOffsetX = 0,

            int playerOffsetY = 0,

            int enemyOffsetX = 0,

            int enemyOffsetY = 0,

            string[]? playerSpriteOverride = null,

            string[]? enemySpriteOverride = null,

            string[][]? enemyIdleFrames = null,

            ConsoleColor enemyColor = ConsoleColor.Red)

        {

            string[][] frames = enemyIdleFrames ?? EnemyArt.EnemyIdleFrames;



            int centerX = Console.WindowWidth / 2;

            int centerY = Console.WindowHeight / 2;



            int playerBoxX = centerX - 45;

            int enemyBoxX = centerX + 25;

            int boxY = centerY - 8;



            if (isPlayerTurn)

            {

                DrawBox(playerBoxX, boxY, 22, 12, ConsoleColor.Yellow);

                DrawBox(enemyBoxX, boxY, 22, 12, ConsoleColor.Red);

            }

            else

            {

                DrawBox(playerBoxX, boxY, 22, 12, ConsoleColor.Cyan);

                DrawBox(enemyBoxX, boxY, 22, 12, ConsoleColor.Yellow);



                Console.ForegroundColor = ConsoleColor.Red;

                Console.SetCursorPosition(enemyBoxX + 6, boxY - 2);

                Console.WriteLine("【 ENEMY 】");

            }



            string[] playerSprite = playerSpriteOverride

                ?? PlayerArt.IdleFrames[playerIdleFrame % PlayerArt.IdleFrames.Length];



            string[] enemySprite = enemySpriteOverride

                ?? frames[enemyIdleFrame % frames.Length];



            DrawPlayerDot(playerBoxX + 8 + playerOffsetX, boxY + 3 + playerOffsetY, playerSprite);

            DrawEnemyDot(enemyBoxX + 6 + enemyOffsetX, boxY + 2 + enemyOffsetY, enemySprite, enemyColor);



            Console.ResetColor();



            Console.SetCursorPosition(playerBoxX + 2, boxY + 13);

            Console.WriteLine($"플레이어 HP : {playerHp}");



            Console.SetCursorPosition(playerBoxX + 2, boxY + 14);

            Console.WriteLine($"포션 : {potionCount}개");



            Console.SetCursorPosition(enemyBoxX + 2, boxY + 13);

            Console.WriteLine($"적 HP : {enemyHp}");

        }



        public static void DrawPlayerDot(int x, int y, string[] player)

        {

            Console.ForegroundColor = ConsoleColor.Cyan;



            for (int i = 0; i < player.Length; i++)

            {

                Console.SetCursorPosition(x, y + i);

                Console.Write(player[i]);

            }



            Console.ResetColor();

        }



        public static void DrawEnemyDot(int x, int y, string[] enemy, ConsoleColor color = ConsoleColor.Red)

        {

            Console.ForegroundColor = color;



            for (int i = 0; i < enemy.Length; i++)

            {

                Console.SetCursorPosition(x, y + i);

                Console.Write(enemy[i]);

            }



            Console.ResetColor();

        }



        public static void DrawBox(

            int x,

            int y,

            int width,

            int height,

            ConsoleColor color)

        {

            Console.ForegroundColor = color;



            Console.SetCursorPosition(x, y);

            Console.Write("┌");



            for (int i = 0; i < width; i++)

            {

                Console.Write("─");

            }



            Console.Write("┐");



            for (int i = 1; i <= height; i++)

            {

                Console.SetCursorPosition(x, y + i);

                Console.Write("│");



                Console.SetCursorPosition(x + width + 1, y + i);

                Console.Write("│");

            }



            Console.SetCursorPosition(x, y + height + 1);

            Console.Write("└");



            for (int i = 0; i < width; i++)

            {

                Console.Write("─");

            }



            Console.Write("┘");



            Console.ResetColor();

        }

    }

}

