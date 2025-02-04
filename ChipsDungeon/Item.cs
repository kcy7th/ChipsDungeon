public class Item
{
    public string Name { get; set; }
    public string Effect { get; set; }
    public string Description { get; set; }
    public int Price { get; set; }
    public bool IsEquipped { get; set; }
    public Action<Player> SpecialEffect { get; set; }  // 특수 효과 추가

    public Item(string name, string effect, string description, int price, Action<Player> specialEffect = null)
    {
        Name = name;
        Effect = effect;
        Description = description;
        Price = price;
        IsEquipped = false;
        SpecialEffect = specialEffect;
    }
}
