using RpgGameC.ItemSystem;



namespace RpgGameC

{

    internal static class ShopUi

    {

        private static readonly List<Func<Item>> ShopCatalog = new()

        {

            () => new Weapon("검", 200),

            () => new NonEquipItem("포션", 300, ItemType.Potion),

            () => new Armor("방어구", 200),

            () => new MysteryElixir("신비의물약", 500)

        };



        public static void Run(RunState run)

        {

            int shopSel = 0;

            int invSel = 0;

            int panel = 0;

            string status = string.Empty;



            while (true)

            {

                Draw(run, shopSel, invSel, panel, status);

                status = string.Empty;



                ConsoleKey key = Input.GetInput();



                if (key == ConsoleKey.Escape)

                {

                    return;

                }



                if (key == ConsoleKey.LeftArrow)

                {

                    panel = 0;

                }

                else if (key == ConsoleKey.RightArrow)

                {

                    panel = 1;

                }

                else if (key == ConsoleKey.UpArrow)

                {

                    if (panel == 0)

                    {

                        shopSel--;



                        if (shopSel < 0)

                        {

                            shopSel = ShopCatalog.Count - 1;

                        }

                    }

                    else

                    {

                        invSel--;



                        if (invSel < 0)

                        {

                            invSel = RunState.InvSlots - 1;

                        }

                    }

                }

                else if (key == ConsoleKey.DownArrow)

                {

                    if (panel == 0)

                    {

                        shopSel++;



                        if (shopSel >= ShopCatalog.Count)

                        {

                            shopSel = 0;

                        }

                    }

                    else

                    {

                        invSel++;



                        if (invSel >= RunState.InvSlots)

                        {

                            invSel = 0;

                        }

                    }

                }

                else if (key == ConsoleKey.Enter)

                {

                    if (panel == 0)

                    {

                        TryBuy(run, shopSel, ref status);

                    }

                    else

                    {

                        ToggleEquip(run, invSel, ref status);

                    }

                }

                else if (key == ConsoleKey.Spacebar)

                {

                    if (panel == 1)

                    {

                        TrySell(run, invSel, ref status);

                    }

                }

            }

        }



        static void TryBuy(RunState run, int shopIndex, ref string status)

        {

            const int potionShopIndex = 1;



            if (shopIndex == potionShopIndex)

            {

                Item sample = ShopCatalog[shopIndex]();



                if (run.Gold < sample.Price)

                {

                    status = "골드가 부족하다.";

                    return;

                }



                run.Gold -= sample.Price;

                run.PotionCount++;

                status = "전투 포션이 1개 늘었다!";

                return;

            }



            int slot = run.FreeSlot();



            if (slot < 0)

            {

                status = "가방이 가득 찼다.";

                return;

            }



            Item newItem = ShopCatalog[shopIndex]();



            if (run.Gold < newItem.Price)

            {

                status = "골드가 부족하다.";

                return;

            }



            run.Gold -= newItem.Price;

            run.Inv[slot] = newItem;

            status = $"{newItem.Name} 구매 완료!";

        }



        static void ToggleEquip(RunState run, int slot, ref string status)

        {

            Item? item = run.Inv[slot];



            if (item == null)

            {

                status = "빈 슬롯이다.";

                return;

            }



            if (item is IEquip equippable)

            {

                item.IsEquipped = !item.IsEquipped;



                if (item.IsEquipped)

                {

                    equippable.Equip();

                }

                else

                {

                    equippable.UnEquip();

                }



                if (item is Armor)

                {

                    if (item.IsEquipped)

                    {

                        run.PlayerHp = Math.Min(run.PlayerHp, run.MaxHp());

                    }

                    else

                    {

                        run.PlayerHp = Math.Min(run.PlayerHp, Balance.BaseMaxPlayerHp);

                    }

                }



                if (item is Weapon)

                {

                    status = item.IsEquipped

                        ? $"{item.Name} 장착 — 공격 2배 (15→30, 30→60)"

                        : $"{item.Name} 장착 해제";

                }

                else if (item is Armor)

                {

                    status = item.IsEquipped

                        ? $"{item.Name} 장착 — 최대 HP {run.MaxHp()}"

                        : $"{item.Name} 해제 — 최대 HP {Balance.BaseMaxPlayerHp}";

                }

                else if (item is MysteryElixir)

                {

                    status = item.IsEquipped

                        ? $"{item.Name} 장착 — 강한공격 성공 {run.SplRate()}%"

                        : $"{item.Name} 해제 — 강한공격 성공 {Balance.BaseSpecialAttackSuccessPercent}%";

                }

                else

                {

                    status = item.IsEquipped ? $"{item.Name} 장착 (C)" : $"{item.Name} 장착 해제";

                }

            }

            else

            {

                status = "장착할 수 없는 아이템이다.";

            }

        }



        static void TrySell(RunState run, int slot, ref string status)

        {

            Item? item = run.Inv[slot];



            if (item == null)

            {

                status = "팔 아이템이 없다.";

                return;

            }



            int sellPrice = (int)(item.Price * 0.8);

            string name = item.Name;

            bool hadArmorEquipped = item is Armor && item.IsEquipped;

            run.Gold += sellPrice;

            run.Inv[slot] = null;



            if (hadArmorEquipped)

            {

                run.PlayerHp = Math.Min(run.PlayerHp, Balance.BaseMaxPlayerHp);

            }



            status = $"{name} 판매 완료 (+{sellPrice} Gold)";

        }



        static void Draw(RunState run, int shopSel, int invSel, int panel, string status)

        {

            Console.Clear();



            int w = Console.WindowWidth;

            int h = Console.WindowHeight;

            int splitX = w / 2;



            Console.ForegroundColor = ConsoleColor.Red;

            Console.SetCursorPosition(2, 1);

            Console.Write("(되돌아가기) ESC");

            Console.ResetColor();



            Console.SetCursorPosition(w / 2 - 8, 2);

            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.Write($"{run.Gold} Gold");

            Console.ResetColor();



            int top = 4;

            int leftInner = 4;

            int rightInner = splitX + 4;

            int innerW = Math.Max(18, splitX - leftInner - 2);

            int boxH = Math.Max(8, h - top - 6);



            DrawPanelFrame(leftInner - 2, top - 1, innerW + 4, boxH);

            DrawPanelFrame(rightInner - 2, top - 1, innerW + 4, boxH);



            Console.SetCursorPosition(leftInner + innerW / 2 - 4, top);

            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.Write("[ 상 점 ]");

            Console.ResetColor();



            Console.SetCursorPosition(rightInner + innerW / 2 - 5, top);

            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.Write("[인벤토리]");

            Console.ResetColor();



            int row = top + 2;



            for (int i = 0; i < ShopCatalog.Count; i++)

            {

                Item preview = ShopCatalog[i]();

                Console.SetCursorPosition(leftInner, row + i);



                if (panel == 0 && shopSel == i)

                {

                    Console.ForegroundColor = ConsoleColor.Yellow;

                    Console.Write($"> {i + 1}. {preview.Name,-8} {preview.Price,4} G");

                    Console.ResetColor();

                }

                else

                {

                    Console.Write($"  {i + 1}. {preview.Name,-8} {preview.Price,4} G");

                }

            }



            row = top + 2;



            for (int i = 0; i < RunState.InvSlots; i++)

            {

                Console.SetCursorPosition(rightInner, row + i);



                Item? it = run.Inv[i];

                string label;



                if (it == null)

                {

                    label = "(비어 있음)";

                }

                else

                {

                    label = it.Name + (it.IsEquipped ? "(C)" : string.Empty);

                }



                if (panel == 1 && invSel == i)

                {

                    Console.ForegroundColor = ConsoleColor.Yellow;

                    Console.Write($"> {i + 1}. {label}");

                    Console.ResetColor();

                }

                else

                {

                    Console.Write($"  {i + 1}. {label}");

                }

            }



            int foot = h - 5;

            Console.SetCursorPosition(2, foot);

            Console.Write("←→ : 상점 / 인벤토리   ↑↓ : 항목   Enter : 구매·장착/해제   Space : 판매(가격의 80%)");



            Console.SetCursorPosition(2, foot + 1);



            if (!string.IsNullOrEmpty(status))

            {

                Console.ForegroundColor = ConsoleColor.Green;

                Console.Write(status);

                Console.ResetColor();

            }

        }



        static void DrawPanelFrame(int x, int y, int width, int height)

        {

            if (width < 4 || height < 4)

            {

                return;

            }



            Console.ForegroundColor = ConsoleColor.DarkGray;

            Console.SetCursorPosition(x, y);

            Console.Write("┌");



            for (int i = 0; i < width - 2; i++)

            {

                Console.Write("─");

            }



            Console.Write("┐");



            for (int r = 1; r < height - 1; r++)

            {

                Console.SetCursorPosition(x, y + r);

                Console.Write("│");

                Console.SetCursorPosition(x + width - 1, y + r);

                Console.Write("│");

            }



            Console.SetCursorPosition(x, y + height - 1);

            Console.Write("└");



            for (int i = 0; i < width - 2; i++)

            {

                Console.Write("─");

            }



            Console.Write("┘");

            Console.ResetColor();

        }

    }

}

