
namespace RpgGameC
{
    internal class Map
    {
        private static readonly Dictionary<char, ConsoleColor> TileColors = new()
        {
            ['^'] = ConsoleColor.Green,
            ['.'] = ConsoleColor.DarkYellow,
            ['~'] = ConsoleColor.DarkGreen,
        };

        private static readonly Dictionary<char, string> TileGlyphs = new()
        {
            ['^'] = "■",
            ['.'] = "·",
            ['~'] = "♠",
        };

        private static readonly string[] Map1Template =
        {
            "^^^^^^^^^^^^^^^^^^^^",
            "^^^^^^^....^^^^^^^^^",
            "^^^^^^........^^^^^^",
            "^^^.....~~~.....^^^^",
            "^^....~~~~~~....^^^^",
            "^^...~~~~~~^^....^^^",
            "^^....~~~~^^^^..$.^^",
            "^^........^^^^^...^^",
            "^^^^......^^^^^..#^^",
            "^^^^^^^^^^^^^^^^^^^^"
        };

        private static readonly string[] Map2Template =
        {
            "^^^^^^^^^^^^^^^^^^^^",
            "^^................^^",
            "^^..~~~~~~~.......^^",
            "^^^^~~~~~~~~~.....^^",
            "^^^^^^^^^^^~~~....^^",
            "^^^^^^^^^^^~~~....^^",
            "^^^^^^^^^^^~~~....^^",
            "^^#...$..~~~~.....^^",
            "^^................^^",
            "^^^^^^^^^^^^^^^^^^^^"
        };

        public string[] map1;
        public string[] map2;

        public string[] currentMap;

        public int currentMapNumber = 1;
        public bool eliteMonsterDefeated = false;

        public Map()
        {
            map1 = CloneTemplate(Map1Template);
            map2 = CloneTemplate(Map2Template);
            currentMap = map1;
        }

        static string[] CloneTemplate(string[] source)
        {
            return (string[])source.Clone();
        }

        public void ResetWorldToMap1()
        {
            map1 = CloneTemplate(Map1Template);
            map2 = CloneTemplate(Map2Template);
            currentMap = map1;
            currentMapNumber = 1;
            eliteMonsterDefeated = false;
        }

        public int Width
        {
            get { return currentMap[0].Length; }
        }

        public int Height
        {
            get { return currentMap.Length; }
        }

        public bool CanMove(int x, int y)
        {
            if (y < 0 || y >= currentMap.Length)
                return false;

            if (x < 0 || x >= currentMap[y].Length)
                return false;

            char tile = currentMap[y][x];

            if (tile == '^')
                return false;

            if (tile == '#' && eliteMonsterDefeated == false)
                return false;

            return true;
        }

        public bool IsEliteMonster(int x, int y)
        {
            return currentMap[y][x] == '$';
        }

        public bool IsGrassTile(int x, int y)
        {
            return currentMap[y][x] == '~';
        }

        public bool IsNextMap(int x, int y)
        {
            return currentMap[y][x] == '#' && eliteMonsterDefeated == true;
        }

        public void DefeatEliteMonster(int x, int y)
        {
            eliteMonsterDefeated = true;

            char[] line = currentMap[y].ToCharArray();
            line[x] = '.';
            currentMap[y] = new string(line);
        }

        public void LoadMap2()
        {
            currentMap = map2;
            currentMapNumber = 2;
            eliteMonsterDefeated = false;
        }

        public void Draw(int mapOriginX, int mapOriginY, int playerX, int playerY, int cellScale = 1)
        {
            if (cellScale < 1)
            {
                cellScale = 1;
            }

            bool useSingleEntityGlyph = cellScale > 1;

            for (int y = 0; y < currentMap.Length; y++)
            {
                for (int x = 0; x < currentMap[y].Length; x++)
                {
                    char tile = currentMap[y][x];
                    bool isPlayerHere = x == playerX && y == playerY;

                    for (int dy = 0; dy < cellScale; dy++)
                    {
                        for (int dx = 0; dx < cellScale; dx++)
                        {
                            int cx = mapOriginX + x * cellScale + dx;
                            int cy = mapOriginY + y * cellScale + dy;
                            Console.SetCursorPosition(cx, cy);

                            bool primary = dx == 0 && dy == 0;

                            if (isPlayerHere)
                            {
                                if (!useSingleEntityGlyph || primary)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.Write("●");
                                }
                                else
                                {
                                    char ground = tile == '#' || tile == '$' ? '.' : tile;
                                    WriteTerrainGlyph(ground);
                                }

                                continue;
                            }

                            if (tile == '#')
                            {
                                if (!useSingleEntityGlyph || primary)
                                {
                                    if (eliteMonsterDefeated)
                                    {
                                        Console.ForegroundColor = ConsoleColor.Magenta;
                                        Console.Write("◆");
                                    }
                                    else
                                    {
                                        Console.ForegroundColor = ConsoleColor.DarkGray;
                                        Console.Write("◇");
                                    }
                                }
                                else
                                {
                                    WritePathFiller();
                                }

                                continue;
                            }

                            if (tile == '$')
                            {
                                if (!useSingleEntityGlyph || primary)
                                {
                                    Console.ForegroundColor = ConsoleColor.DarkRed;
                                    Console.Write("$");
                                }
                                else
                                {
                                    WritePathFiller();
                                }

                                continue;
                            }

                            WriteTerrainGlyph(tile);
                        }
                    }
                }
            }

            Console.ResetColor();
        }

        private static void WritePathFiller()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("·");
        }

        private static void WriteTerrainGlyph(char tile)
        {
            if (TileColors.TryGetValue(tile, out ConsoleColor color)
                && TileGlyphs.TryGetValue(tile, out string? glyph))
            {
                Console.ForegroundColor = color;
                Console.Write(glyph);
            }
            else
            {
                Console.Write(" ");
            }
        }
    }
}
