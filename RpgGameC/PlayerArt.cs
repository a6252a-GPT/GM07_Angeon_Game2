namespace RpgGameC
{
    internal static class PlayerArt
    {
        public static readonly string[][] IdleFrames =
        {
            new[]
            {
                "  ▄██▄  ",
                " ██████ ",
                " ██ ██ ",
                " ██████ ",
                "▄█ ██ █▄",
                "   ██   ",
                "  ████  ",
                " ██  ██ "
            },
            new[]
            {
                "  ▄██▄  ",
                " ██████ ",
                " ██ ██ ",
                " ██████ ",
                " ▄█ ██ ▄",
                "  ███   ",
                "  ████  ",
                " ███ ███"
            }
        };

        public static int SpriteLineCount => IdleFrames[0].Length;

        public static void Draw(int leftX, int topY, int frameIndex, ConsoleColor color = ConsoleColor.Cyan)
        {
            string[] lines = IdleFrames[frameIndex % IdleFrames.Length];

            Console.ForegroundColor = color;

            for (int i = 0; i < lines.Length; i++)
            {
                Console.SetCursorPosition(leftX, topY + i);
                Console.Write(lines[i]);
            }

            Console.ResetColor();
        }
    }
}
