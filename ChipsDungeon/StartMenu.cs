using System;

public class StartMenu
{
    private Player player;
    private Dungeon dungeon;

    public StartMenu()
    {
        // 플레이어 이름 입력
        Console.Clear();
        Console.WriteLine("캐릭터의 이름을 입력하세요:");
        Console.Write(">> ");
        string playerName = Console.ReadLine();

        // 직업 선택
        string playerJob = ChooseJob();

        // 플레이어 생성
        player = new Player(playerName, playerJob);
        dungeon = new Dungeon();
    }

    // 직업 선택
    private string ChooseJob()
    {
        Console.Clear();
        Console.WriteLine("직업을 선택하세요:");
        Console.WriteLine("1. 전사");
        Console.WriteLine("2. 마법사");
        Console.WriteLine("3. 궁수");
        Console.Write("\n>> ");

        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                return "전사";
            case "2":
                return "마법사";
            case "3":
                return "궁수";
            default:
                Console.WriteLine("\n올바르지 않은 입력! 기본 직업 '전사'로 설정됩니다.");
                Console.ReadKey();
                return "전사";  // 기본 값 - 전사
        }
    }

    // 메인 메뉴
    public void DisplayMenu()
    {
        Console.Clear();
        Console.WriteLine("감자튀김 마을에 오신 여러분 환영합니다!");
        Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.\n");
        Console.WriteLine("1. 상태 보기");
        Console.WriteLine("2. 인벤토리");
        Console.WriteLine("3. 상점");
        Console.WriteLine("4. 던전입장");
        Console.WriteLine("5. 휴식하기");
        Console.WriteLine("\n원하는 메뉴를 선택해 주세요.");
        Console.Write(">> ");

        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                ShowStatus();
                break;
            case "2":
                ShowInventory();
                break;
            case "3":
                ShowShop();
                break;
            case "4":
                EnterDungeon();
                break;
            case "5":
                Rest();
                break;
            default:
                Console.WriteLine("잘못된 입력입니다.");
                break;
        }
    }

    // 1. 상태 보기
    private void ShowStatus()
    {
        player.ShowStatus();
        string choice = Console.ReadLine();

        if (choice == "0")
        {
            Console.Clear();
            DisplayMenu();
        }
    }

    // 2. 인벤토리
    private void ShowInventory()
    {
        player.Inventory.ShowInventory();
    }

    // 3. 상점
    private void ShowShop()
    {
        Shop shop = new Shop();
        shop.ShowShop(player);
    }

    // 4. 던전 입장
    private void EnterDungeon()
    {
        dungeon.EnterDungeon(player);
    }

    // 5. 휴식하기
    private void Rest()
    {
        Console.Clear();
        Console.WriteLine("휴식하기");
        Console.WriteLine("500 G 를 내면 체력을 회복할 수 있습니다.");
        Console.WriteLine($"(보유 골드 : {player.Gold} G)\n");

        Console.WriteLine("1. 휴식하기");
        Console.WriteLine("0. 나가기");
        Console.Write("\n원하시는 행동을 입력해주세요.\n>> ");

        string choice = Console.ReadLine();
        if (choice == "1")
        {
            if (player.Gold >= 500)
            {
                player.Gold -= 500;
                player.Health = 100;
                Console.WriteLine("휴식을 완료했습니다. 체력이 100으로 회복되었습니다!");
            }
            else
            {
                Console.WriteLine("Gold가 부족합니다.");
            }
            Console.ReadKey();
            DisplayMenu();
        }
        else if (choice == "0")
        {
            DisplayMenu();
        }
        else
        {
            Console.WriteLine("잘못된 입력입니다.");
            Console.ReadKey();
            Rest();
        }
    }
}
