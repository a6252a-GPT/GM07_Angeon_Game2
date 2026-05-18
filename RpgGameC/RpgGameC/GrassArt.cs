namespace RpgGameC

{

    internal static class GrassArt

    {

        private static readonly string[][] SlimeIdle =

        {

            new[]

            {

                "    ▄▄▄▄    ",

                "  ▄██████▄  ",

                " ▄████████▄ ",

                " ██████████ ",

                "  ████████  ",

                "   ▀▀▀▀▀▀   ",

                "  ▄█    █▄  ",

                " ▄█      █▄ "

            },

            new[]

            {

                "    ▄▄▄▄    ",

                "  ▄██████▄  ",

                " ▄████████▄ ",

                " ██████████ ",

                "  ████████  ",

                "   ▀▀▀▀▀▀   ",

                "   █▄  ▄█   ",

                "  ▄█▀  ▀█▄  "

            }

        };



        private static readonly string[][] BeetleIdle =

        {

            new[]

            {

                "  ▄▄▄▄▄▄▄▄  ",

                " ██████████ ",

                "██▀▀████▀▀██",

                "██  ████  ██",

                " ▀█▄████▄█▀ ",

                "   ██  ██   ",

                "  ██▀  ▀██  ",

                " ██      ██ "

            },

            new[]

            {

                "  ▄▄▄▄▄▄▄▄  ",

                " ██████████ ",

                "██▀▀████▀▀██",

                "██  ████  ██",

                " ▀█▄████▄█▀ ",

                "  ▀█    █▀  ",

                " ██▀    ▀██ ",

                " ██      ██ "

            }

        };



        private static readonly string[][] MushroomIdle =

        {

            new[]

            {

                "   ▄████▄   ",

                " ▄████████▄ ",

                " ██████████ ",

                "  ▀▀▀██▀▀▀  ",

                "     ██     ",

                "    ████    ",

                "   ██  ██   ",

                "  ██    ██  "

            },

            new[]

            {

                "   ▄████▄   ",

                " ▄████████▄ ",

                " ██████████ ",

                "  ▀▀▀██▀▀▀  ",

                "     ██     ",

                "    ████    ",

                "   ██  ██   ",

                "  ▀▀    ▀▀  "

            }

        };



        private static readonly string[][] SpikyIdle =

        {

            new[]

            {

                "      ▄      ",

                "    ▄██▄    ",

                "   ▄████▄   ",

                "  ▄██████▄  ",

                " ▄████████▄ ",

                "  ▀██████▀  ",

                "   ▀████▀   ",

                "    ▀██▀    "

            },

            new[]

            {

                "     ▄▄     ",

                "    ████    ",

                "   ▄████▄   ",

                "  ▄██████▄  ",

                " ▄████████▄ ",

                "  ▀██████▀  ",

                "   ▀████▀   ",

                "    ▀██▀    "

            }

        };



        private static readonly string[][] WormIdle =

        {

            new[]

            {

                "  ▄▄  ▄▄  ▄▄ ",

                " ██▀██████▀█",

                "  ▀▀  ▀▀  ▀▀ ",

                "    ██████  ",

                "   ██    ██ ",

                "  ██      ██",

                " ██        █",

                "▀▀          "

            },

            new[]

            {

                "  ▄▄  ▄▄  ▄▄ ",

                " ██▀██████▀█",

                "  ▀▀  ▀▀  ▀▀ ",

                "    ██████  ",

                "   ██    ██ ",

                "  ██      ██",

                " ██        █",

                " ▀          "

            }

        };



        internal sealed class Variant

        {

            public required string[][] IdleFrames { get; init; }

            public ConsoleColor Color { get; init; } = ConsoleColor.Green;

        }



        private static readonly List<Variant> Monsters = new()

        {

            new Variant { Color = ConsoleColor.Green, IdleFrames = SlimeIdle },

            new Variant { Color = ConsoleColor.DarkYellow, IdleFrames = BeetleIdle },

            new Variant { Color = ConsoleColor.Magenta, IdleFrames = MushroomIdle },

            new Variant { Color = ConsoleColor.DarkCyan, IdleFrames = SpikyIdle },

            new Variant { Color = ConsoleColor.DarkGreen, IdleFrames = WormIdle },

        };



        private static int _nextVariantIndex;



        public static void ResetSeq()

        {

            _nextVariantIndex = 0;

        }



        public static Variant NextVariant()

        {

            Variant variant = Monsters[_nextVariantIndex % Monsters.Count];

            _nextVariantIndex++;

            return variant;

        }

    }

}

