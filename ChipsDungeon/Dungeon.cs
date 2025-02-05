using System;
using System.Threading;

public class Dungeon
{
    // 던전 정보 구조체
    private struct DungeonInfo
    {
        public string Name;
        public int RecommendedDefense;
        public int BaseReward;
        public Monster Monster;

        // 던전 초기화
        public DungeonInfo(string name, int recommendedDefense, int baseReward, Monster monster)
        {
            Name = name;
            RecommendedDefense = recommendedDefense;
            BaseReward = baseReward;
            Monster = monster;
        }
    }

    // 던전 정보 배열
    private DungeonInfo[] dungeons = new DungeonInfo[]
    {
        new DungeonInfo("바삭바삭 초원", 5, 1000, new Monster("바삭 슬라임", 30, 5)),
        new DungeonInfo("짭짤한 협곡", 11, 1700, new Monster("소금 골렘", 60, 10)),
        new DungeonInfo("지옥의 기름솥", 17, 2500, new Monster("불튀김 골렘", 80, 15, true))
    };

    private Random random = new Random();

    // 던전 입장 후 선택하기
    public void EnterDungeon(Player player)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("던전 입장");
            Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.\n");
            Console.WriteLine("1. 바삭바삭 초원     | 방어력 5 이상 권장");
            Console.WriteLine("2. 짭짤한 협곡     | 방어력 11 이상 권장");
            Console.WriteLine("3. 지옥의 기름솥    | 방어력 17 이상 권장\n");
            Console.WriteLine("0. 나가기\n");
            Console.WriteLine("원하는 메뉴를 선택해 주세요.");
            Console.Write(">> ");

            string input = Console.ReadLine();
            if (input == "0") return;

            if (int.TryParse(input, out int dungeonChoice) && dungeonChoice >= 1 && dungeonChoice <= 3)
            {
                ProcessDungeon(player, dungeons[dungeonChoice - 1]);
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
                Console.ReadKey();
            }
        }
    }

    private void ProcessDungeon(Player player, DungeonInfo dungeon)
    {
        Console.Clear();
        Console.WriteLine($"{dungeon.Name}에 입장합니다!\n");
        Thread.Sleep(1000);

        // 전투 실패
        if (!Battle(player, dungeon.Monster))
        {
            Console.WriteLine("던전 실패!\n보상을 얻지 못했으며, 체력이 절반 감소했습니다.");
            int lostHealth = player.Health / 2;
            player.Health -= lostHealth;
            Console.WriteLine($"체력: {player.Health + lostHealth} -> {player.Health}\n");
            Console.WriteLine("0. 나가기");
            Console.Write(">> ");
            while (Console.ReadLine() != "0") { }
            return;
        }

        // 전투 성공
        // 체력 계산
        int playerDefense = player.BaseDefense + player.Inventory.GetEquipmentStats().defenseBonus;
        int playerAttack = player.BaseAttack + player.Inventory.GetEquipmentStats().attackBonus;

        // 방어력에 따른 체력 계산
        int defenseDiff = dungeon.RecommendedDefense - playerDefense;
        int healthLossMin = 20 + (defenseDiff > 0 ? defenseDiff : defenseDiff);
        int healthLossMax = 35 + (defenseDiff > 0 ? defenseDiff : defenseDiff);
        int healthLoss = random.Next(healthLossMin, healthLossMax + 1);

        int initialHealth = player.Health;
        player.Health -= healthLoss;
        if (player.Health < 0) player.Health = 0;  // 음수 방지

        // 보상 금액 계산
        int bonusPercentage = random.Next(playerAttack, playerAttack * 2 + 1);
        int bonusGold = dungeon.BaseReward * bonusPercentage / 100;
        int totalGold = dungeon.BaseReward + bonusGold;

        player.Gold += totalGold;

        Console.Clear();
        Console.WriteLine("던전 클리어");
        Console.WriteLine("축하합니다!!");
        Console.WriteLine($"{dungeon.Name}을 클리어 하였습니다.\n");
        Console.WriteLine("[탐험 결과]");
        Console.WriteLine($"체력: {initialHealth} -> {player.Health}");
        Console.WriteLine($"Gold: {player.Gold - totalGold} G -> {player.Gold} G\n");

        Console.WriteLine("0. 나가기");
        Console.Write(">> ");
        while (Console.ReadLine() != "0") { }
    }

    // 턴제 TRPG
    private bool Battle(Player player, Monster monster)
    {
        Console.WriteLine($"{monster.Name}이(가) 등장했습니다!\n");
        Thread.Sleep(1000);

        while (player.Health > 0 && monster.Health > 0)
        {
            // 플레이어의 턴
            Console.WriteLine("플레이어의 공격!");
            Thread.Sleep(1000);
            monster.Health -= player.BaseAttack;
            Console.WriteLine($"{monster.Name}의 체력: {monster.Health}");
            Thread.Sleep(1000);
            if (monster.Health <= 0) break;

            // 몬스터의 턴
            Console.WriteLine("몬스터의 반격!");
            Thread.Sleep(1000);
            int damage = monster.Attack;

            // 데미지 있는 몬스터
            if (monster.HasFireEffect)
            {
                Console.WriteLine("불튀김 골렘의 불 데미지! 추가 피해를 입습니다!");
                damage += 5;
                Thread.Sleep(1000);
            }

            player.Health -= damage;
            Console.WriteLine($"플레이어 체력: {player.Health}\n");
            Thread.Sleep(1000);
        }

        return player.Health > 0;
    }
}