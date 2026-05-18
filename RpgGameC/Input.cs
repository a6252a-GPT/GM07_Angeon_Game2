namespace RpgGameC

{

    internal static class Input

    {

        public static ConsoleKey GetInput()

        {

            ConsoleKeyInfo key = Console.ReadKey(true);

            return key.Key;

        }

    }

}

