namespace RpgGameC.ItemSystem
{
    internal sealed class MysteryElixir : Item, IEquip
    {
        public MysteryElixir(string name, int price)
            : base(name, price, ItemType.MysteryElixir)
        {
        }

        public override void ShowInfo()
        {
            Console.WriteLine($"[신비] {Name} | 착용 시 강한공격 성공 +30% (총 80%) | 가격 : {Price} Gold");
        }

        public void Equip()
        {
        }

        public void UnEquip()
        {
        }
    }
}
