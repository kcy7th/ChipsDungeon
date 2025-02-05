using System;
using System.Collections.Generic;

public class Player
{
    public int Level { get; set; }
    public string Name { get; set; }
    public string Job { get; set; }
    public int BaseAttack { get; set; }
    public int BaseDefense { get; set; }
    public int Health { get; set; }
    public int Gold { get; set; }
    public Inventory Inventory { get; set; }
    private List<Item> purchasedItems;  // 구매한 아이템

    // 캐릭터 초기 속성
    public Player(string name, string job)
    {
        Level = 1;
        Name = name;
        Job = job;
        Gold = 1500;
        Inventory = new Inventory();  // 인벤토리 생성
        purchasedItems = new List<Item>();  // 구매한 아이템 목록 초기화

        // 직업에 따른 공격력, 방어력, 체력
        switch (job)
        {
            case "전사":
                BaseAttack = 12;
                BaseDefense = 7;
                Health = 120;
                break;
            case "마법사":
                BaseAttack = 15;
                BaseDefense = 3;
                Health = 80;
                break;
            case "궁수":
                BaseAttack = 14;
                BaseDefense = 5;
                Health = 100;
                break;
            default:
                BaseAttack = 10;
                BaseDefense = 5;
                Health = 100;
                break;
        }
    }

    // 아이템 인벤토리에 추가
    public void AddItem(Item item)
    {
        Inventory.AddItem(item);  // 인벤토리에 아이템 추가
        purchasedItems.Add(item);  // 구매한 아이템 목록에 추가
    }

    // 특정 아이템을 보유하고 있는지 확인
    public bool HasItem(Item item)
    {
        return purchasedItems.Contains(item);
    }

    // 캐릭터 상태 출력
    public void ShowStatus()
    {
        // 인벤토리에서 아이템의 공격력, 방어력 가져오기
        var (attackBonus, defenseBonus) = Inventory.GetEquipmentStats();

        Console.Clear();
        Console.WriteLine("상태 보기");
        Console.WriteLine("캐릭터의 정보가 표시됩니다.\n");
        Console.WriteLine($"Lv. {Level}");
        Console.WriteLine($"{Name} ({Job})");
        Console.WriteLine($"공격력 : {BaseAttack + attackBonus} {(attackBonus > 0 ? $"(+{attackBonus})" : "")}");
        Console.WriteLine($"방어력 : {BaseDefense + defenseBonus} {(defenseBonus > 0 ? $"(+{defenseBonus})" : "")}");
        Console.WriteLine($"체 력 : {Health}");
        Console.WriteLine($"Gold : {Gold} G");
        Console.WriteLine("\n0. 나가기");
        Console.Write("\n원하시는 행동을 입력해주세요.\n>> ");

        ConsoleKeyInfo key = Console.ReadKey();
        if (key.Key == ConsoleKey.D0)
        {
            return;  // "0"을 누르면 상태 보기 종료
        }
    }
}
