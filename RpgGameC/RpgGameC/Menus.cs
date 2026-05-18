namespace RpgGameC

{

    internal static class Menus

    {

        public static void DrawFramedMenu(string titleLine, string[] options, int selectedIndex)

        {

            Console.Clear();



            int startX = Console.WindowWidth / 2 - 16;

            int startY = Console.WindowHeight / 2 - 5;



            Console.ForegroundColor = ConsoleColor.Cyan;



            Console.SetCursorPosition(startX, startY);

            Console.WriteLine("================================");



            Console.SetCursorPosition(startX, startY + 1);

            Console.WriteLine(titleLine);



            Console.SetCursorPosition(startX, startY + 2);

            Console.WriteLine("================================");



            Console.ResetColor();



            for (int i = 0; i < options.Length; i++)

            {

                Console.SetCursorPosition(startX, startY + 4 + i);



                if (i == selectedIndex)

                {

                    Console.ForegroundColor = ConsoleColor.Yellow;

                    Console.WriteLine($"> {options[i]}");

                    Console.ResetColor();

                }

                else

                {

                    Console.WriteLine($"  {options[i]}");

                }

            }



            Console.SetCursorPosition(startX, startY + 8);

            Console.WriteLine("↑ ↓ 방향키로 선택 / Enter 입력");

        }



        public static int RunGameOverMenu()

        {

            string[] menu =

            {

                "다시 도전",

                "게임 종료"

            };



            int select = 0;



            while (true)

            {

                DrawFramedMenu("           GAME OVER", menu, select);



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

                    return select;

                }

            }

        }

    }

}

