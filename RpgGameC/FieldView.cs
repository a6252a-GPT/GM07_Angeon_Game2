namespace RpgGameC

{

    internal static class FieldView

    {

        public static void DrawField(Map map, RunState run, int playerX, int playerY, int portraitFrame)

        {

            int mapW = map.Width;

            int mapH = map.Height;



            int spriteRows = PlayerArt.IdleFrames[0].Length;

            int portraitX = 4;

            int portraitY = Console.WindowHeight - spriteRows - 2;



            int mapOx = Math.Max(2, (Console.WindowWidth - mapW) / 2);

            int mapOy = 2;



            int maxMapBottom = portraitY - 15;

            if (mapOy + mapH - 1 > maxMapBottom)

            {

                mapOy = Math.Max(2, maxMapBottom - mapH + 1);

            }



            int goldX = mapOx + mapW + 3;

            int goldY = mapOy;



            bool statsBesideMap = goldX + 14 < Console.WindowWidth;



            if (!statsBesideMap)

            {

                const int statsRows = 5;

                int minMapTop = statsRows + 1;



                if (mapOy < minMapTop)

                {

                    mapOy = minMapTop;



                    if (mapOy + mapH - 1 > maxMapBottom)

                    {

                        mapOy = Math.Max(minMapTop, maxMapBottom - mapH + 1);

                    }

                }



                goldY = mapOy;

            }



            if (statsBesideMap)

            {

                DrawPlayerStatsPanel(run, goldX, goldY);

            }

            else

            {

                DrawPlayerStatsPanel(run, 2, 0);

            }



            map.Draw(mapOx, mapOy, playerX, playerY, 1);



            int legendX = mapOx;

            int legendY = mapOy + mapH + 2;

            DrawMapLegend(legendX, legendY);



            PlayerArt.Draw(portraitX, portraitY, portraitFrame);

        }



        private static void DrawPlayerStatsPanel(RunState run, int x, int topY)

        {

            int mult = run.AtkMult();

            int normalDmg = Balance.PlayerNormalAttackDamage * mult;

            int specialDmg = Balance.PlayerSpecialAttackDamage * mult;

            int maxHp = run.MaxHp();



            Console.ResetColor();



            Console.SetCursorPosition(x, topY);

            Console.Write($"Gold : {run.Gold}");



            Console.SetCursorPosition(x, topY + 1);

            Console.Write($"일반공격 : {normalDmg}");



            Console.SetCursorPosition(x, topY + 2);

            Console.Write($"강한공격 : {specialDmg}");



            Console.SetCursorPosition(x, topY + 3);

            Console.Write($"체력 : {run.PlayerHp} / {maxHp}");



            Console.SetCursorPosition(x, topY + 4);

            Console.Write($"포션 : {run.PotionCount}개");

        }



        public static void DrawMapLegend(int legendX, int legendY)

        {

            Console.ResetColor();



            int y = legendY;



            Console.SetCursorPosition(legendX, y++);

            Console.Write("방향키 이동 / ESC 메뉴로 돌아가기");



            Console.SetCursorPosition(legendX, y++);

            Console.Write("풀숲에 접근시 몬스터 출현합니다.");



            Console.SetCursorPosition(legendX, y++);

            Console.Write("1 : 상점·인벤토리");



            Console.SetCursorPosition(legendX, y++);

            Console.Write("2 : 포션사용 (40% 체력회복)");



            y++;



            Console.SetCursorPosition(legendX, y++);

            Console.Write("■ : 이동 불가");



            Console.SetCursorPosition(legendX, y++);

            Console.Write("· : 길");



            Console.SetCursorPosition(legendX, y++);

            Console.Write("♠ : 풀숲");



            Console.SetCursorPosition(legendX, y++);

            Console.Write("● : 플레이어");



            Console.SetCursorPosition(legendX, y++);

            Console.Write("$ : 정예 몬스터");



            Console.SetCursorPosition(legendX, y++);

            Console.Write("◇ : 비활성화 포탈");



            Console.SetCursorPosition(legendX, y++);

            Console.Write("◆ : 활성 포탈");

        }

    }

}

