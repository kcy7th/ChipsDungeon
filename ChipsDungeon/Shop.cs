using System;
using System.Collections.Generic;

public class Shop
{
    private List<Item> shopItems;

    public Shop()
    {
        shopItems = new List<Item>
        {
            new Item("바삭한 갑옷", "방어력 +5", "튀김처럼 바삭하고 내구성이 뛰어난 갑옷입니다.", 1000),
            new Item("감자껍질 갑옷", "방어력 +9", "단단한 감자 껍질로 만든 갑옷.", 2000),
            new Item("스마일감튀 갑옷", "방어력 +15", "케찹이 묻어도 방어력은 완벽하다!", 3500),
            new Item("썩은 감자", "공격력 +2", "감자의 쓴 맛을 느낄 수 있습니다.", 600),
            new Item("감자칼", "공격력 +5", "뭐든 썰어버립니다. (아닐 수도)", 1500),
            new Item("회오리감자칼", "공격력 +7", "회오리처럼 날카로운 칼입니다.", 2500),
            new Item("황금 감자 창", "공격력 +10", "순금으로 된 전설의 창. 사실은 금이 아니라 흙일 지도...?", 3500)
        };
    }

    // 상점 메인
    public void ShowShop(Player player)
    {
        Console.Clear();
        Console.WriteLine("상점");
        Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");

        Console.WriteLine($"[보유 골드]\n{player.Gold} G\n");
        Console.WriteLine("[아이템 목록]");

        for (int i = 0; i < shopItems.Count; i++)
        {
            var item = shopItems[i];
            string status = player.HasItem(item) ? "구매완료" : $"{item.Price} G";
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
                ShowShop(player);  // 오류 수정: ShowShop으로 돌아가도록 수정
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
        for (int i = 0; i < shopItems.Count; i++)
        {
            var item = shopItems[i];
            string status = player.HasItem(item) ? "구매완료" : $"{item.Price} G";
            Console.WriteLine($"{i + 1}. {item.Name} | {item.Effect} | {item.Description} | {status}");
        }

        Console.WriteLine("\n0. 나가기");

        Console.Write("\n원하시는 행동을 입력해주세요.\n>> ");
        string choice = Console.ReadLine();
        PurchaseItem(choice, player);  // 오류 수정: ShowShopItems -> PurchaseItem
    }

    // 아이템 구매 처리
    private void PurchaseItem(string choice, Player player)
    {
        if (choice == "0")
        {
            ShowShop(player);  // 오류 수정: ShowShop으로 돌아가도록 수정
            return;
        }

        if (!int.TryParse(choice, out int itemNumber) || itemNumber < 1 || itemNumber > shopItems.Count)
        {
            Console.WriteLine("잘못된 입력입니다.");
            Console.ReadKey();
            BuyItem(player);  // 오류 수정: BuyItem으로 돌아가도록 수정
            return;
        }

        Item selectedItem = shopItems[itemNumber - 1];

        if (player.HasItem(selectedItem))
        {
            Console.WriteLine("이미 구매한 아이템입니다.");
            Console.ReadKey();
            BuyItem(player);  // 오류 수정: BuyItem으로 돌아가도록 수정
            return;
        }

        // 골드가 충분한지 확인
        if (player.Gold >= selectedItem.Price)
        {
            Console.WriteLine("구매를 완료했습니다.");
            player.Gold -= selectedItem.Price;  // 골드 차감
            player.AddItem(selectedItem);  // 아이템 추가
            Console.ReadKey();
        }
        else
        {
            Console.WriteLine("Gold가 부족합니다.");
            Console.ReadKey();
        }

        BuyItem(player);  // 구매 후 다시 아이템 구매 화면을 보여줌
    }
}
