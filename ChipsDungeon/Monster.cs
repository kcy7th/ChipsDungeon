public class Monster
{
    public string Name { get; set; }
    public int Health { get; set; }
    public int Attack { get; set; }
    public bool HasFireEffect { get; set; }

    public Monster(string name, int health, int attack, bool hasFireEffect = false)
    {
        Name = name;
        Health = health;
        Attack = attack;
        HasFireEffect = hasFireEffect;
    }
}