public class Item
{
    public string Name { get; set; }
    public string Effect { get; set; }
    public string Description { get; set; }
    public int Price { get; set; }
    public bool IsEquipped { get; set; }  // 추가된 부분

    public Item(string name, string effect, string description, int price)
    {
        Name = name;
        Effect = effect;
        Description = description;
        Price = price;
        IsEquipped = false;  // 기본값 설정
    }
}
