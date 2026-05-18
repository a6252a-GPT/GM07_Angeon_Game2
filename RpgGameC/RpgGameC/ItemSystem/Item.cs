namespace RpgGameC.ItemSystem
{
    internal abstract class Item
    {
        public string Name { get; protected set; }

        public int Price { get; protected set; }

        public bool IsEquipped { get; set; }

        public ItemType Type { get; protected set; }

        protected Item(string name, int price, ItemType type)
        {
            Name = name;
            Price = price;
            Type = type;
            IsEquipped = false;
        }

        public abstract void ShowInfo();

        public virtual int GetAtkBonus()
        {
            return 0;
        }

        public virtual int GetDefBonus()
        {
            return 0;
        }
    }

    internal sealed class NonEquipItem : Item
    {
        public NonEquipItem(string name, int price, ItemType type)
            : base(name, price, type)
        {
        }

        public override void ShowInfo()
        {
            Console.WriteLine($"[{Type}] {Name} | 가격 : {Price} Gold");
        }
    }
}
