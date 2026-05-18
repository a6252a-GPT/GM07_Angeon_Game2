namespace RpgGameC
{
    internal static class BattleIn
    {
        public static int WaitForBattleActionSelection(
            RunState run,
            int enemyHp,
            string[][]? enemyIdleFrames = null,
            ConsoleColor enemyColor = ConsoleColor.Red)
        {
            string[][] frames = enemyIdleFrames ?? EnemyArt.EnemyIdleFrames;

            int selected = 0;

            void RedrawMenu()
            {
                Console.Clear();
                BattleUi.DrawBattleScreen(
                    run.PlayerHp,
                    enemyHp,
                    run.PotionCount,
                    true,
                    0,
                    0,
                    enemyIdleFrames: frames,
                    enemyColor: enemyColor);
                BattleUi.DrawBattleActionMenu(selected, run.SplRate());
            }

            RedrawMenu();

            while (true)
            {
                if (!Console.KeyAvailable)
                {
                    Thread.Sleep(50);
                    continue;
                }

                ConsoleKey key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.UpArrow)
                {
                    selected--;

                    if (selected < 0)
                    {
                        selected = 3;
                    }

                    RedrawMenu();
                }
                else if (key == ConsoleKey.DownArrow)
                {
                    selected++;

                    if (selected > 3)
                    {
                        selected = 0;
                    }

                    RedrawMenu();
                }
                else if (key == ConsoleKey.Enter)
                {
                    return selected;
                }
            }
        }
    }
}
