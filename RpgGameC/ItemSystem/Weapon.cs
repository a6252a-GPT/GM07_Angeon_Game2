namespace RpgGameC.ItemSystem
{
    internal sealed class Weapon : Item, IEquip
    {
        public Weapon(string name, int price)
            : base(name, price, ItemType.Weapon)
        {
        }

        public override void ShowInfo()
        {
            Console.WriteLine($"[무기] {Name} | 착용 시 일반·특수 공격 2배 | 가격 : {Price} Gold");
        }

        public override int GetAtkBonus()
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
