using System;
using System.Collections.Generic;

public class Inventory
{
    private List<Item> items;

    public Inventory()
    {
        items = new List<Item>();
    }

    public void AddItem(Item item)
    {
        items.Add(item);
    }

    public (int attackBonus, int defenseBonus) GetEquipmentStats()
    {
        int attackBonus = 0;
        int defenseBonus = 0;

        foreach (Item item in items)
        {
            if (item.IsEquipped)
            {
                if (item.Effect.Contains("공격력"))
                {
                    attackBonus += ExtractStatValue(item.Effect);
                }
                else if (item.Effect.Contains("방어력"))
                {
                    defenseBonus += ExtractStatValue(item.Effect);
                }
            }
        }

        return (attackBonus, defenseBonus);
    }

    public void ShowInventory()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("인벤토리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");
            Console.WriteLine("[아이템 목록]");

            if (items.Count == 0)
            {
                Console.WriteLine("보유 중인 아이템이 없습니다.\n");
            }
            else
            {
                for (int i = 0; i < items.Count; i++)
                {
                    string equipped = items[i].IsEquipped ? "[E] " : "";
                    Console.WriteLine($"- {i + 1}. {equipped}{items[i].Name} | {items[i].Effect} | {items[i].Description}");
                }
            }

            Console.WriteLine("\n1. 장착 관리");
            Console.WriteLine("0. 나가기");
            Console.Write("\n원하시는 행동을 입력해주세요.\n>> ");

            string input = Console.ReadLine();
            if (input == "1")
            {
                ManageEquipment();
            }
            else if (input == "0")
            {
                return;
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
                Console.ReadKey();
            }
        }
    }

    public void ManageEquipment()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("장착 관리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");

            if (items.Count == 0)
            {
                Console.WriteLine("보유 중인 아이템이 없습니다.");
                Console.ReadKey();
                return;
            }

            for (int i = 0; i < items.Count; i++)
            {
                string equipped = items[i].IsEquipped ? "[E] " : "";
                Console.WriteLine($"- {i + 1}. {equipped}{items[i].Name} | {items[i].Effect} | {items[i].Description}");
            }

            Console.WriteLine("\n번호를 입력하여 장착/해제하세요.");
            Console.WriteLine("\n0. 나가기");
            Console.WriteLine("\n>> ");

            if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= items.Count)
            {
                ToggleEquipment(index - 1);
            }
            else if (index == 0)
            {
                return;
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
                Console.ReadKey();
            }
        }
    }

    private void ToggleEquipment(int index)
    {
        items[index].IsEquipped = !items[index].IsEquipped;
        string action = items[index].IsEquipped ? "장착" : "해제";
        Console.WriteLine($"\n{items[index].Name}을(를) {action}했습니다.");
        Console.ReadKey();
    }

    private int ExtractStatValue(string effect)
    {
        string[] parts = effect.Split('+');
        if (parts.Length == 2 && int.TryParse(parts[1].Trim().Split(' ')[0], out int value))
        {
            return value;
        }
        return 0;
    }
}
