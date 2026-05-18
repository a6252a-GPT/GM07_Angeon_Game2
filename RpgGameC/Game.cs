

namespace RpgGameC

{

    internal class Game

    {

        static void Main()

        {

            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.CursorVisible = false;



            string[] menu =

            {

                "게임시작",

                "게임종료"

            };



            int select = 0;



            while (true)

            {

                DrawMenu(menu, select);



                ConsoleKey key = Input.GetInput();



                if (key == ConsoleKey.DownArrow)

                {

                    select++;



                    if (select >= menu.Length)

                    {

                        select = 0;

                    }

                }

                else if (key == ConsoleKey.UpArrow)

                {

                    select--;



                    if (select < 0)

                    {

                        select = menu.Length - 1;

                    }

                }

                else if (key == ConsoleKey.Enter)

                {

                    if (select == 0)

                    {

                        StartGame();

                    }

                    else if (select == 1)

                    {

                        Console.Clear();

                        Console.WriteLine("게임을 종료합니다.");

                        break;

                    }

                }

            }

        }



        static void DrawMenu(string[] menu, int select)

        {

            Menus.DrawFramedMenu("          콘솔 RPG 게임", menu, select);

        }



        static void StartGame()

        {

            Map map = new Map();

            RunState runState = new RunState();

            runState.Reset();

            GrassArt.ResetSeq();



            int playerX = 8;

            int playerY = 8;



            int moveParity = 0;

            int movesSinceLastGrassEvent = Balance.GrassEncounterMoveCooldown;



            void RedrawField()

            {

                int frame = moveParity % 2;

                Console.Clear();

                FieldView.DrawField(map, runState, playerX, playerY, frame);

            }



            void ShowOverworldToast(string message)

            {

                int spriteRows = PlayerArt.IdleFrames[0].Length;

                int portraitTopY = Console.WindowHeight - spriteRows - 2;

                int toastY = portraitTopY + spriteRows + 1;



                if (toastY >= Console.WindowHeight)

                {

                    toastY = Console.WindowHeight - 1;

                }



                Console.SetCursorPosition(2, toastY);

                Console.ForegroundColor = ConsoleColor.Yellow;

                Console.Write(message.PadRight(Math.Max(message.Length, 40)));

                Console.ResetColor();

                Thread.Sleep(900);

            }



            RedrawField();



            while (true)

            {

                if (Console.KeyAvailable)

                {

                    ConsoleKey key = Console.ReadKey(true).Key;



                    if (key == ConsoleKey.D1 || key == ConsoleKey.NumPad1)

                    {

                        ShopUi.Run(runState);

                        RedrawField();

                        continue;

                    }



                    if (key == ConsoleKey.D2 || key == ConsoleKey.NumPad2)

                    {

                        RunState.PotResult potionResult = runState.UsePot(out int healAmount);



                        if (potionResult == RunState.PotResult.Success)

                        {

                            ShowOverworldToast($"체력 회복! [+{healAmount}]  남은 포션 : {runState.PotionCount}개");

                        }

                        else if (potionResult == RunState.PotResult.NoPotion)

                        {

                            ShowOverworldToast("포션이 없습니다!");

                        }

                        else

                        {

                            ShowOverworldToast("체력이 이미 가득합니다!");

                        }



                        RedrawField();

                        continue;

                    }



                    if (key == ConsoleKey.Escape)

                    {

                        break;

                    }



                    int nextX = playerX;

                    int nextY = playerY;



                    if (key == ConsoleKey.UpArrow)

                    {

                        nextY--;

                    }

                    else if (key == ConsoleKey.DownArrow)

                    {

                        nextY++;

                    }

                    else if (key == ConsoleKey.LeftArrow)

                    {

                        nextX--;

                    }

                    else if (key == ConsoleKey.RightArrow)

                    {

                        nextX++;

                    }

                    else

                    {

                        continue;

                    }



                    if (map.CanMove(nextX, nextY))

                    {

                        int beforeMoveX = playerX;

                        int beforeMoveY = playerY;



                        playerX = nextX;

                        playerY = nextY;

                        moveParity ^= 1;

                        movesSinceLastGrassEvent++;



                        if (map.IsEliteMonster(playerX, playerY))

                        {

                            BattleRes outcome = Battle.RunElite(map, runState);



                            if (outcome == BattleRes.Win)

                            {

                                map.DefeatEliteMonster(playerX, playerY);

                            }

                            else if (outcome == BattleRes.Lose)

                            {

                                int gameOverChoice = Menus.RunGameOverMenu();



                                if (gameOverChoice == 0)

                                {

                                    map.ResetWorldToMap1();

                                    runState.Reset();

                                    GrassArt.ResetSeq();

                                    playerX = 8;

                                    playerY = 8;

                                    moveParity = 0;

                                    movesSinceLastGrassEvent = Balance.GrassEncounterMoveCooldown;

                                    RedrawField();

                                    continue;

                                }

                                else

                                {

                                    Console.Clear();

                                    Console.WriteLine("게임을 종료합니다.");

                                    Environment.Exit(0);

                                }

                            }

                            else if (outcome == BattleRes.Flee)

                            {

                                playerX = beforeMoveX;

                                playerY = beforeMoveY;

                                moveParity ^= 1;

                                movesSinceLastGrassEvent--;

                            }

                        }

                        else if (map.IsGrassTile(playerX, playerY)

                            && movesSinceLastGrassEvent >= Balance.GrassEncounterMoveCooldown)

                        {

                            movesSinceLastGrassEvent = 0;

                            GrassRoll grassRoll = GrassEvt.RollGrassEvent();



                            if (grassRoll == GrassRoll.Treasure)

                            {

                                GrassEvt.ShowTreasureFound(runState);

                            }

                            else if (grassRoll == GrassRoll.Battle)

                            {

                                BattleRes grassOutcome = GrassEvt.RunGrassBattle(

                                    map.currentMapNumber,

                                    runState);



                                if (grassOutcome == BattleRes.Lose)

                                {

                                    int gameOverChoice = Menus.RunGameOverMenu();



                                    if (gameOverChoice == 0)

                                    {

                                        map.ResetWorldToMap1();

                                        runState.Reset();

                                        GrassArt.ResetSeq();

                                        playerX = 8;

                                        playerY = 8;

                                        moveParity = 0;

                                        movesSinceLastGrassEvent = Balance.GrassEncounterMoveCooldown;

                                        RedrawField();

                                        continue;

                                    }

                                    else

                                    {

                                        Console.Clear();

                                        Console.WriteLine("게임을 종료합니다.");

                                        Environment.Exit(0);

                                    }

                                }

                            }

                        }



                        if (map.IsNextMap(playerX, playerY))

                        {

                            Console.Clear();



                            if (map.currentMapNumber == 1)

                            {

                                map.LoadMap2();



                                playerX = 2;

                                playerY = 1;

                                movesSinceLastGrassEvent = Balance.GrassEncounterMoveCooldown;



                                Console.ForegroundColor = ConsoleColor.Yellow;



                                Console.SetCursorPosition(

                                    Console.WindowWidth / 2 - 5,

                                    Console.WindowHeight / 2);



                                Console.WriteLine("2 CHAPTER");



                                Console.ResetColor();

                                Thread.Sleep(1500);

                            }

                            else if (map.currentMapNumber == 2)

                            {

                                Console.ForegroundColor = ConsoleColor.Magenta;



                                Console.SetCursorPosition(

                                    Console.WindowWidth / 2 - 5,

                                    Console.WindowHeight / 2);



                                Console.WriteLine("GAME CLEAR!");



                                Console.ResetColor();



                                Thread.Sleep(3000);

                                break;

                            }

                        }



                        RedrawField();

                    }



                    continue;

                }



                Thread.Sleep(50);

            }

        }

    }

}

