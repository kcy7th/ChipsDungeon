using System;

public class StartMenu
{
    private Player player;

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
    }

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
                return "전사";
        }
    }

    public void DisplayMenu()
    {
        Console.Clear();
        Console.WriteLine("감자튀김 마을에 오신 여러분 환영합니다!");
        Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.\n");
        Console.WriteLine("1. 상태 보기");
        Console.WriteLine("2. 인벤토리");
        Console.WriteLine("3. 상점");
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
            default:
                Console.WriteLine("잘못된 입력입니다.");
                break;
        }
    }

    private void ShowStatus()
    {
        player.ShowStatus();
        string choice = Console.ReadLine();

        if (choice == "0")
        {
            Console.Clear();
            DisplayMenu();    // 메인 메뉴로 바로 돌아가기
        }
    }


    private void ShowInventory()
    {
        player.Inventory.ShowInventory();  // 중복된 ShowInventory 메서드 제거
    }

    private void ShowShop()
    {
        Shop shop = new Shop();
        shop.ShowShop(player);
    }

    private void ManageEquipment()
    {
        player.Inventory.ManageEquipment();
    }
}
