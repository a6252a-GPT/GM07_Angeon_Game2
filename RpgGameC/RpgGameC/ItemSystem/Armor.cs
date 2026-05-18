namespace RpgGameC.ItemSystem
{
    internal sealed class Armor : Item, IEquip
    {
        public Armor(string name, int price)
            : base(name, price, ItemType.Armor)
        {
        }

        public override void ShowInfo()
        {
            Console.WriteLine($"[방어구] {Name} | 착용 시 최대 HP 2배(200) | 가격 : {Price} Gold");
        }

        public override int GetDefBonus()
        {
            return 0;
        }

        public void Equip()
        {
        }

        public void UnEquip()
        {
        }
    }
}
