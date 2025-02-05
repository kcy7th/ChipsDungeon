using System;
using System.Collections.Generic;

public class Shop
{
    // 아이템 목록
    private List<Item> shopItems;

    // 판매 아이템 초기화
    public Shop()
    {
        shopItems = new List<Item>
        {
            new Item("바삭한 갑옷", "방어력 +5", "튀김처럼 바삭하고 내구성이 뛰어난 갑옷입니다.", 1000, p => p.BaseDefense += 5),
            new Item("매운 감자튀김 세트", "공격력 +3, 일정 확률로 적에게 화상", "아주 맵습니다!", 1200, p => p.BaseAttack += 3),
            new Item("감자튀김 방패", "방어력 +7", "바삭함과 방어력을 동시에!", 2000, p => p.BaseDefense += 7),
            new Item("감자 망토", "방어력 +10", "포근한 감자로 만든 망토.", 3000, p => p.BaseDefense += 10),
            new Item("전기 감자", "공격력 +5, 일정 확률 추가 타격", "전기가 찌릿찌릿 흐른다.", 2500, p => p.BaseAttack += 5),
            new Item("썩은 감자의 저주", "공격력 +8, 하지만 체력 지속 감소", "위험하지만 강력한 힘.", 3500, p => { p.BaseAttack += 8; p.Health -= 5; }),
        };
    }

    // 상점 화면
    public void ShowShop(Player player)
    {
        Console.Clear();
        Console.WriteLine("상점");
        Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");
        Console.WriteLine($"[보유 골드]\n{player.Gold} G\n");
        Console.WriteLine("[아이템 목록]");

        // 판매 중인 아이템
        for (int i = 0; i < shopItems.Count; i++)
        {
            var item = shopItems[i];
            // 구매 완료 시 '구매 완료'
            string status = player.HasItem(item) ? "구매 완료" : $"{item.Price} G";
            Console.WriteLine($"- {item.Name} | {item.Effect} | {item.Description} | {status}");
        }

        Console.WriteLine("\n1. 아이템 구매");
        Console.WriteLine("0. 나가기");
        Console.Write("\n원하시는 행동을 입력해주세요.\n>> ");

        string input = Console.ReadLine();

        switch (input)
        {
            case "1":
                BuyItem(player);
                break;
            case "0":
                return;
            default:
                Console.WriteLine("잘못된 입력입니다.");
                Console.ReadKey();
                ShowShop(player);
                break;
        }
    }

    // 아이템 구매 화면
    private void BuyItem(Player player)
    {
        Console.Clear();
        Console.WriteLine("상점 - 아이템 구매");
        Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");
        Console.WriteLine("[보유 골드]");
        Console.WriteLine($"{player.Gold} G\n");

        Console.WriteLine("[아이템 목록]");
        // 판매된 아이템 목록
        for (int i = 0; i < shopItems.Count; i++)
        {
            var item = shopItems[i];
            string status = player.HasItem(item) ? "구매 완료" : $"{item.Price} G";
            Console.WriteLine($"{i + 1}. {item.Name} | {item.Effect} | {item.Description} | {status}");
        }

        Console.WriteLine("\n0. 나가기");
        Console.Write("\n원하시는 행동을 입력해주세요.\n>> ");
        string choice = Console.ReadLine();
        PurchaseItem(choice, player);
    }

    // 아이템 구매 로직
    private void PurchaseItem(string choice, Player player)
    {
        if (choice == "0")
        {
            ShowShop(player);
            return;
        }

        if (!int.TryParse(choice, out int itemNumber) || itemNumber < 1 || itemNumber > shopItems.Count)
        {
            Console.WriteLine("잘못된 입력입니다.");
            Console.ReadKey();
            BuyItem(player);
            return;
        }

        Item selectedItem = shopItems[itemNumber - 1];

        // 이미 소지한 아이템일 경우
        if (player.HasItem(selectedItem))
        {
            Console.WriteLine("이미 구매한 아이템입니다.");
            Console.ReadKey();
            BuyItem(player);
            return;
        }

        if (player.Gold >= selectedItem.Price)
        {
            Console.WriteLine("구매를 완료했습니다.");
            player.Gold -= selectedItem.Price;
            player.AddItem(selectedItem);
            selectedItem.SpecialEffect?.Invoke(player); // 특수 효과 적용
            Console.ReadKey();
        }
        else
        {
            Console.WriteLine("Gold가 부족합니다.");
            Console.ReadKey();
        }

        BuyItem(player);
    }
}
