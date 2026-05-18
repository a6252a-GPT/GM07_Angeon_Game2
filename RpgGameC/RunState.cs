using RpgGameC.ItemSystem;



namespace RpgGameC

{

    internal sealed class RunState

    {

        public const int InvSlots = 10;



        public int PlayerHp { get; set; }



        public int PotionCount { get; set; }



        public int Gold { get; set; }



        public List<Item?> Inv { get; } = Enumerable.Repeat<Item?>(null, InvSlots).ToList();



        public void Reset()

        {

            PlayerHp = Balance.PlayerInitialHp;

            PotionCount = Balance.InitialPotionCount;

            Gold = Balance.StartingGold;



            for (int i = 0; i < Inv.Count; i++)

            {

                Inv[i] = null;

            }

        }



        public bool HasWeapon()

        {

            foreach (Item? it in Inv)

            {

                if (it is Weapon w && w.IsEquipped)

                {

                    return true;

                }

            }



            return false;

        }



        public bool HasArmor()

        {

            foreach (Item? it in Inv)

            {

                if (it is Armor a && a.IsEquipped)

                {

                    return true;

                }

            }



            return false;

        }



        public int MaxHp()

        {

            return HasArmor()

                ? Balance.BaseMaxPlayerHp * 2

                : Balance.BaseMaxPlayerHp;

        }



        public int AtkMult()

        {

            return HasWeapon() ? 2 : 1;

        }



        public int SplRate()

        {

            return HasElixir()

                ? Balance.BaseSpecialAttackSuccessPercent + Balance.MysteryElixirSpecialAttackBonusPercent

                : Balance.BaseSpecialAttackSuccessPercent;

        }



        public bool HasElixir()

        {

            foreach (Item? it in Inv)

            {

                if (it is MysteryElixir me && me.IsEquipped)

                {

                    return true;

                }

            }



            return false;

        }



        public int FreeSlot()

        {

            for (int i = 0; i < Inv.Count; i++)

            {

                if (Inv[i] == null)

                {

                    return i;

                }

            }



            return -1;

        }



        public enum PotResult

        {

            Success,

            NoPotion,

            AlreadyFull,

        }



        public PotResult UsePot(out int healAmount)

        {

            healAmount = 0;



            if (PotionCount <= 0)

            {

                return PotResult.NoPotion;

            }



            int maxHp = MaxHp();



            if (PlayerHp >= maxHp)

            {

                return PotResult.AlreadyFull;

            }



            healAmount = Balance.PotHeal(maxHp);

            PlayerHp += healAmount;



            if (PlayerHp > maxHp)

            {

                PlayerHp = maxHp;

            }



            PotionCount--;

            return PotResult.Success;

        }

    }

}

