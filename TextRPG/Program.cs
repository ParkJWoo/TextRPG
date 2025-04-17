using System.ComponentModel.Design;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using static TextRPG.Program;
using Newtonsoft.Json;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.Serialization.Formatters.Binary;                          //  데이터 저장용

namespace TextRPG
{
    internal class Program
    {
        public static string path = $"..\\TextRPG\\TextRPG\\bin\\Debug\\net8.0\\JSon";            //  데이터 저장 파일 위치

        public enum MAIN_STATE                              //  시작 화면의 선택지 상태
        {
            SCENE_STATUS = 1,
            SCENE_INVENTORY,
            SCENE_SHOP,
            SCENE_BATTLE,
            SCENE_REST,
            SCENE_SAVEDATA,
            SCENE_LOADDATA
        }
        public enum DUNGEON                                 //  던전 타입
        {
            DUNGEON_EASY = 1,
            DUNGEON_NORMAL,
            DUNGEON_HARD
        }
        public enum ITEMTYPE                                //  아이템 타입 (방어구, 무기)
        {
            ITEM_WEAPON = 0,
            ITEM_ARMOR
        }

        public class Item
        {
            public ITEMTYPE eType { get; set; }
            public string strName { get; set; }
            public float fAttack { get; set; }
            public float fDefense { get; set; }
            public float fHp { get; set; }
            public float fMaxHp { get; set; }
            public int iPrice { get; set; }
            public bool bIsPurchase { get; set; }
            public bool bIsWear { get; set; }

            public Item(ITEMTYPE type,string name, float attack, float defense, float hp, int gold, bool purchase)
            {
                eType = type;
                strName = name;
                fAttack = attack;
                fDefense = defense;
                fHp = hp;
                fMaxHp = 100.0f;
                iPrice = gold;
                bIsPurchase = purchase;
                bIsWear = false;
            }
        }

        public class Player
        {
            //  플레이어 클래스

            public byte bLevel { get; set; }
            public string strJob { get; set; }
            public string strName { get; set; }
            public float fAttack { get; set; }
            public float fAttackSum { get; set; }
            public float fDefense { get; set; }
            public float fDefenseSum { get; set; }
            public float fHp { get; set; }
            public float fMaxHp { get; set; }
            public float fHpSum { get; set; }
            public int iExp { get; set; }
            public int iGold { get; set; }
            public List<Item> lstInventory { get; set; }
            public List<Item> lstEquip { get; set; }
            public List<Item> lstEquipArmor { get; set; }
            public List<Item> lstEquipWeapon { get; set; }

            public Player()
            {
                bLevel = 0;
                strJob = string.Empty;
                strName = string.Empty;
                fAttack = 0.0f;
                fDefense = 0.0f;
                fHp = 0.0f;
                fMaxHp = 100.0f;
                iExp = 0;
                iGold = 0;
                lstInventory = new List<Item>();
                lstEquipArmor = new List<Item>();
                lstEquipWeapon = new List<Item>();
                lstEquip = new List<Item>();

                fAttackSum = 0.0f;
                fDefenseSum = 0.0f;
                fHpSum = 0.0f;
            }


            public Player(byte level, string job, string name, float attack, float defense, float hp, int gold)      //  캐릭터 별 상태 정보를 원하는 수치로 입력, 입력한 값으로 초기화한다.
            {
                bLevel = level;
                strJob = job;
                strName = name;
                fAttack = attack;
                fDefense = defense;
                fHp = hp;
                fMaxHp = 100.0f;
                iExp = 0;
                iGold = gold;
                lstInventory = new List<Item>();
                lstEquipArmor = new List<Item>();
                lstEquipWeapon = new List<Item>();
                lstEquip = new List<Item>();

                fAttackSum = 0.0f;
                fDefenseSum = 0.0f;
                fHpSum = 0.0f;
            }

            public void EquipItem(Item item)
            {
                if(lstEquipArmor.Count == 0 && item.eType == ITEMTYPE.ITEM_ARMOR)
                {
                    lstEquipArmor.Add(item);

                    //  아이템 장착 시, 해당 아이템의 추가 능력치만큼 캐릭터의 능력치가 올라간다.
                    fDefense += item.fDefense;
                    fHp += item.fHp;

                    //  아이템을 장착했을 때, 상태 창에 추가된 능력치의 값을 보여주기 위한 작업
                    fDefenseSum += item.fDefense;
                    fHpSum += item.fHp;
                }

                else if(lstEquipArmor.Count != 0 && item.eType == ITEMTYPE.ITEM_ARMOR)
                {
                    //  기존에 있는 방어구 인벤토리를 비우고
                    lstEquipArmor.Clear();

                    fDefenseSum = 0;
                    fHpSum = 0;

                    //  선택한 아이템을 배치한다.
                    lstEquipArmor.Add(item);

                    //  아이템 장착 시, 해당 아이템의 추가 능력치만큼 캐릭터의 능력치가 올라간다.
                    fDefense += item.fDefense;
                    fHp += item.fHp;

                    //  아이템을 장착했을 때, 상태 창에 추가된 능력치의 값을 보여주기 위한 작업
                    fDefenseSum += item.fDefense;
                    fHpSum += item.fHp;
                }

                if (lstEquipWeapon.Count == 0 && item.eType == ITEMTYPE.ITEM_WEAPON)
                {
                    lstEquipWeapon.Add(item);

                    //  아이템 장착 시, 해당 아이템의 추가 능력치만큼 캐릭터의 능력치가 올라간다.
                    fAttack += item.fAttack;

                    //  아이템을 장착했을 때, 상태 창에 추가된 능력치의 값을 보여주기 위한 작업
                    fAttackSum += item.fAttack;
                }

                else if(lstEquipWeapon.Count != 0 && item.eType == ITEMTYPE.ITEM_WEAPON)
                {
                    lstEquipWeapon.Clear();

                    fAttackSum = 0;

                    lstEquipWeapon.Add(item);

                    fAttack += item.fAttack;
                    fAttackSum += item.fAttack;
                }
            }

            public void UnequipItem(Item item)
            {
                fAttack -= item.fAttack;
                fDefense -= item.fDefense;
                fHp -= item.fHp;

                item.bIsWear = false;
            }

            public void LevelUP(int exp)
            {
                iExp += exp;

                if (bLevel == 1 && iExp == 10)
                {

                    bLevel = 2;

                    fAttack += 0.5f;
                    fDefense += 1.0f;
                    fHp = fMaxHp;
                    iExp = 0;
                }

                else if (bLevel == 2 && iExp == 20)
                {
                    bLevel = 3;

                    fAttack += 0.5f;
                    fDefense += 1.0f;
                    fHp = fMaxHp;
                    iExp = 0;
                }

                else if(bLevel == 3 && iExp == 30)
                {
                    bLevel = 4;

                    fAttack += 0.5f;
                    fDefense += 1.0f;
                    fHp = fMaxHp;
                    iExp = 0;
                }

                else if(bLevel == 4 && iExp == 40)
                {
                    bLevel = 5;

                    fAttack += 0.5f;
                    fDefense += 1.0f;
                    fHp = fMaxHp;
                    iExp = 0;
                }
            }

            public float SetDamage(float fAttack)
            {
                fHp -= fAttack;

                return fHp;
            }

            public int SetRewardGold(int gold)
            {
                iGold += gold;

                return iGold;
            }
        }

        static public void StatInfoScene(Player player)            //  플레이어의 스탯 정보를 명시하는 정보 창
        {
            if (player.lstInventory.Count == 0)
            {
                Console.WriteLine("===============[플레이어 스탯창]===============");
                Console.WriteLine($"Lv. {player.bLevel}");
                Console.Write($"이름: {player.strName}");
                Console.WriteLine($" | 직업: {player.strJob}");
                Console.WriteLine($"공격력: {player.fAttack}");
                Console.WriteLine($"방어력: {player.fDefense}");
                Console.Write($"체력: {player.fHp}");
                Console.WriteLine($" | 최대 체력: {player.fMaxHp}");
                Console.WriteLine($"보유 골드: {player.iGold}");
                Console.WriteLine("===============================================");

                Console.WriteLine();
            }

            else
            {
                Console.WriteLine("===============[플레이어 스탯창]===============");
                Console.WriteLine($"Lv. {player.bLevel}");
                Console.Write($"이름: {player.strName}");
                Console.WriteLine($" | 직업: {player.strJob}");
                Console.WriteLine($"공격력: {player.fAttack} (+ {player.fAttackSum})");
                Console.WriteLine($"방어력: {player.fDefense} (+ {player.fDefenseSum})");
                Console.WriteLine($"체력: {player.fHp} (+ {player.fHpSum})");
                Console.WriteLine($"보유 골드: {player.iGold}");
                Console.WriteLine("===============================================");

                Console.WriteLine();
            }
        }


        static public void InventoryScene(Player player)            //  플레이어의 인벤토리를 명시하는 정보 창
        {
            Console.Clear();

            Console.WriteLine("===============[인벤토리 창]===============");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다");
            Console.WriteLine();
            Console.WriteLine("아이템 목록");

            Console.WriteLine("1. 장착 관리");
            Console.WriteLine("0. 메인 화면으로 이동");
            Console.WriteLine();


            if (player.lstInventory.Count == 0)
            {
                Console.WriteLine("===============================================");
                Console.WriteLine();
                Console.WriteLine("인벤토리에 아무런 아이템이 없습니다!");
                Console.WriteLine();
                Console.WriteLine("===============================================");
            }

            else
            {
                Console.WriteLine("=============== [보유 중인 아이템 목록] ===============");

                Console.WriteLine();

                int i = 1;

                foreach (var item in player.lstInventory)
                {
                    if(player.lstEquipArmor.Count == 0 || player.lstEquipWeapon.Count == 0)
                    {
                        if (!item.bIsWear)
                        {
                            Console.WriteLine($" {i}. 아이템 이름: {item.strName} | 공격력: {item.fAttack} 증가 | 방어력: {item.fDefense} 증가 |" +
                                              $" 체력: {item.fHp} 증가");

                            player.lstEquipArmor.Add(item);
                            player.lstEquipWeapon.Add(item);

                            i++;
                            Console.WriteLine();
                        }

                        else if(item.bIsWear)
                        {
                            Console.WriteLine($" {i}. 아이템 이름: {item.strName} | 공격력: {item.fAttack} 증가 | 방어력: {item.fDefense} 증가 |" +
                                              $" 체력: {item.fHp} 증가");

                            i++;
                            Console.WriteLine();
                        }
                    }

                    else if(player.lstEquipArmor.Count != 0)
                    {
                        if (!item.bIsWear)
                        {
                            Console.WriteLine($" {i}. 아이템 이름: {item.strName} | 공격력: {item.fAttack} 증가 | 방어력: {item.fDefense} 증가 |" +
                                              $" 체력: {item.fHp} 증가");

                            player.lstEquipArmor.Add(item);

                            i++;
                            Console.WriteLine();
                        }

                        else if (item.bIsWear)
                        {
                            Console.WriteLine($" {i}. [E] 아이템 이름: {item.strName} | 공격력: {item.fAttack} 증가 | 방어력: {item.fDefense} 증가 |" +
                                         $" 체력: {item.fHp} 증가");

                            i++;
                            Console.WriteLine();
                        }
                    }
                }
            }

            Console.WriteLine("========================================================");

            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int invenInput))
                {
                    if (invenInput == 0)
                    {
                        StartScene(player);
                        break;
                    }

                    else if (invenInput == 1)
                    {
                        WearScene(player);
                        break;
                    }

                    else
                    {
                        Console.WriteLine("잘못된 입력값입니다. 숫자만 입력해주세요!");
                    }
                }
            }
        }

        static public void WearScene(Player player)
        {
            Console.Clear();

            Console.WriteLine("===============[인벤토리 창]===============");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다");
            Console.WriteLine();
            Console.WriteLine("아이템 목록");
          
            if (player.lstInventory.Count == 0)
            {
                Console.WriteLine("===============================================");
                Console.WriteLine();
                Console.WriteLine("인벤토리에 아무런 아이템이 없습니다!");
                Console.WriteLine();
                Console.WriteLine("===============================================");
            }

            else
            {
                Console.WriteLine("=============== [보유 중인 아이템 목록] ===============");

                Console.WriteLine();

                int i = 1;

                foreach (var item in player.lstInventory)
                {
                    Console.WriteLine($"{i}. 아이템 이름: {item.strName} | 공격력: {item.fAttack} 증가 | 방어력: {item.fDefense} 증가 |" +
                    $" 체력: {item.fHp} 증가");

                    i++;

                    Console.WriteLine();
                }

                Console.WriteLine("========================================================");

                Console.WriteLine("장착하고자 하는 아이템을 선택하세요");

                while (true)
                {
                    if (int.TryParse(Console.ReadLine(), out int wearInput) && wearInput >= 0 && wearInput <= player.lstInventory.Count)
                    {
                        if (wearInput == 0)                      //  0을 눌렀을 경우, 이전 씬(상점 씬)으로 이동
                        {
                            InventoryScene(player);
                            break;
                        }

                        else                                    //  0이 아닌 숫자를 눌렀을 경우, 해당 숫자의 아이템을 장착
                        {
                            Item wearableItem = player.lstInventory[wearInput - 1];

                            if (wearableItem.bIsWear)
                            {
                                Console.WriteLine("해당 아이템은 이미 장착 중입니다!");
                            }

                            else if(!wearableItem.bIsWear)
                            {
                                Console.WriteLine("해당 아이템을 장착했습니다!");
                                player.EquipItem(wearableItem);
                                wearableItem.bIsWear = true;
                            }

                            Console.WriteLine("0. 인벤토리 창으로 이동");
                            Console.ReadKey();
                        }
                    }
                }
            }
        }

        static public void ShopScene(Player player)            //  플레이어의 인벤토리를 명시하는 정보 창
        {
            Console.Clear();

            Console.WriteLine("================= [상점 창] =================");
            Console.WriteLine();
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다");
            Console.WriteLine();
            Console.WriteLine($"[보유 골드] : {player.iGold}");
            Console.WriteLine();
            Console.WriteLine("=============================================");
            Console.WriteLine();

            List<Item> itemList = new List<Item>()
            {
                new Item(ITEMTYPE.ITEM_ARMOR, "수련자 갑옷", 0.0f, 5.0f, 0.0f,1000,false),
                new Item(ITEMTYPE.ITEM_ARMOR, "무쇠 갑옷", 0.0f, 9.0f, 0.0f,1000,false),
                new Item(ITEMTYPE.ITEM_ARMOR, "스파르타의 갑옷", 0.0f, 15.0f, 0.0f,3500,false),
                new Item(ITEMTYPE.ITEM_WEAPON, "낡은 검", 2.0f, 0.0f, 0.0f,600,false),
                new Item(ITEMTYPE.ITEM_WEAPON,"청동 도끼", 5.0f, 0.0f, 0.0f,1500,false),
                new Item(ITEMTYPE.ITEM_WEAPON,"스파르타의 창", 7.0f, 0.0f, 0.0f,3000,false),
                new Item(ITEMTYPE.ITEM_WEAPON, "이세계 용사의 꿰뚫는 창", 20.0f, 0.0f, 0.0f,15000,false)
            };

            Console.WriteLine("[아이템 목록]");
            Console.WriteLine();

            for (int i = 0; i < itemList.Count; i++)
            {
                Console.WriteLine($"{i + 1}. 아이템 이름: {itemList[i].strName} | 가격: {itemList[i].iPrice} | 공격력: {itemList[i].fAttack} 증가 | 방어력: {itemList[i].fDefense} 증가 |" +
                    $" 체력: {itemList[i].fHp} 증가 |  구매 여부: {itemList[i].bIsPurchase}");

                Console.WriteLine();
            }

            Console.WriteLine("===============================================");

            foreach (var item in itemList)
            {
                item.bIsPurchase = false;
            }

            Console.WriteLine();
            Console.WriteLine("1. 아이템 구매");
            Console.WriteLine("2. 아이템 판매");
            Console.WriteLine("0. 메인 화면으로 이동");
            Console.WriteLine();
            Console.WriteLine("===============================================");

            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int shopInput))
                {
                    if (shopInput == 0)
                    {
                        StartScene(player);
                        break;
                    }

                    else if (shopInput == 1)
                    {
                        PurchaseItem(itemList, player);
                        break;
                    }

                    else if(shopInput == 2)
                    {
                        SellScene(player);
                        break;
                    }

                    else
                    {
                        Console.WriteLine("잘못된 입력값입니다. 숫자만 입력해주세요!");
                    }
                }
            }
        }

        static void PurchaseItem(List<Item> itemList, Player player)
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("================= [아이템 구매 창] =================");
                Console.WriteLine();
                Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다");
                Console.WriteLine();
                Console.WriteLine($"[보유 골드] : {player.iGold}");
                Console.WriteLine();
                Console.WriteLine("====================================================");
                Console.WriteLine();

                Console.WriteLine("[아이템 목록]");

                for(int i = 0; i < itemList.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. 아이템 이름: {itemList[i].strName} | 가격: {itemList[i].iPrice} | 공격력: {itemList[i].fAttack} 증가 | 방어력: {itemList[i].fDefense} 증가 |" +
                     $" 체력: {itemList[i].fHp} 증가 |  구매 여부: {itemList[i].bIsPurchase}");

                    Console.WriteLine();
                }

                Console.WriteLine("====================================================");

                Console.WriteLine();
                Console.WriteLine("구매하고자 하는 아이템의 숫자를 입력하세요!");
                Console.WriteLine("0. 메인 화면으로 이동");
                Console.WriteLine();
                Console.WriteLine("====================================================");


                if (int.TryParse(Console.ReadLine(),out int itemInput) && itemInput >= 0 && itemInput <= itemList.Count)
                {
                    if(itemInput == 0)                      //  0을 눌렀을 경우, 이전 씬(상점 씬)으로 이동
                    {
                        ShopScene(player);
                        break;
                    }

                    else                                    //  0이 아닌 숫자를 눌렀을 경우, 해당 숫자의 아이템을 구매
                    {
                        Item puschaseItem = itemList[itemInput - 1];

                        if(puschaseItem.bIsPurchase)
                        {
                            Console.WriteLine("이미 구매한 아이템입니다!");
                        }

                        else if (player.iGold >= puschaseItem.iPrice)
                        {
                            Console.WriteLine("구매를 완료했습니다!");
                            player.iGold -= puschaseItem.iPrice;
                            puschaseItem.bIsPurchase = true;
                            player.lstInventory.Add(puschaseItem);
                        }

                        else
                        {
                            Console.WriteLine("골드가 부족합니다");
                        }
                    }

                    Console.WriteLine("아무 키나 누르면 돌아갑니다");
                    Console.ReadKey();
                }

                else
                {
                    Console.WriteLine("잘못된 입력입니다. 아무 키나 누르면 돌아갑니다");
                    Console.ReadKey();
                }
            }
        }

        static public void SellScene(Player player)
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("================= [아이템 판매 창] =================");
                Console.WriteLine();
                Console.WriteLine("아이템을 판매할 수 있습니다.");
                Console.WriteLine();
                Console.WriteLine($"[보유 골드] : {player.iGold}");
                Console.WriteLine();
                Console.WriteLine("====================================================");
                Console.WriteLine();

                Console.WriteLine("[보유 아이템 목록]");
                Console.WriteLine();

                for (int i = 0; i < player.lstInventory.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. 아이템 이름: {player.lstInventory[i].strName} | 가격: {player.lstInventory[i].iPrice} | 공격력: {player.lstInventory[i].fAttack} 증가 | 방어력: {player.lstInventory[i].fDefense} 증가 |" +
                     $" 체력: {player.lstInventory[i].fHp} 증가 |  구매 여부: {player.lstInventory[i].bIsPurchase}");

                    Console.WriteLine();
                }

                Console.WriteLine("====================================================");

                Console.WriteLine();
                Console.WriteLine("판매하고자 하는 아이템의 숫자를 입력하세요!");
                Console.WriteLine("0. 메인 화면으로 이동");
                Console.WriteLine();
                Console.WriteLine("====================================================");


                if (int.TryParse(Console.ReadLine(), out int itemInput) && itemInput >= 0 && itemInput <= player.lstInventory.Count)
                {
                    if (itemInput == 0)                                        //  0을 눌렀을 경우, 이전 씬(상점 씬)으로 이동
                    {
                        ShopScene(player);
                        break;
                    }

                    else if(itemInput != 0)                                    //  0이 아닌 숫자를 눌렀을 경우, 해당 숫자의 아이템을 판매
                    {
                        Item sellItem = player.lstInventory[itemInput - 1];

                        Console.WriteLine("판매를 완료했습니다!");
                        player.iGold += (int)(sellItem.iPrice * 0.85f);
                        sellItem.bIsPurchase = false;
                        player.lstInventory.Remove(sellItem);
                        player.lstEquip.Remove(sellItem);

                        if(sellItem.bIsWear == true)
                        {
                            player.UnequipItem(sellItem);
                        }
                       
                    }

                    else
                    {
                        Console.WriteLine("아무 키나 누르면 돌아갑니다");
                        Console.ReadKey();
                    }
                        
                }

                else
                {
                    Console.WriteLine("잘못된 입력입니다. 아무 키나 누르면 돌아갑니다");
                    Console.ReadKey();
                }
            }
        }

        static public void EnterDungeonScene(Player player)
        {
            while(true)
            {
                Console.Clear();

                Console.WriteLine("================= [던전 입장] =================");
                Console.WriteLine();
                Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");
                Console.WriteLine();
                Console.WriteLine("1. 쉬운 던전 | 방어력 5 이상 권장");
                Console.WriteLine("2. 보통 던전 | 방어력 11 이상 권장");
                Console.WriteLine("3. 어려운 던전 | 방어력 17 이상 권장");
                Console.WriteLine("0. 나가기");
                Console.WriteLine();
                Console.WriteLine("====================================================");
                Console.WriteLine();

                if (int.TryParse(Console.ReadLine(), out int dungeonInput) && dungeonInput >= 0 && dungeonInput <= 4)
                {
                    BattleStage(player, dungeonInput);
                    break;
                }

                else
                {
                    Console.WriteLine("숫자를 잘못 입력하셨습니다. 다시 입력해주세요!");
                    Console.ReadKey();
                }
            }
        }

        static public void BattleStage(Player player, int dungeonChoice)
        {
            float fRecommendDef = 0.0f;                         //  던전 당 권장 방어력
            float fRandomDamage = 0.0f;                         //  대미지 범위
            int iRewardGold = 0;                                //  골드 보상
            int iBonusRewardGold = 0;                           //  골드 추가 보상
            float fBonusRewardPercent = 0.0f;                   //  골드 추가 보상 확률
            int iClearRatio = 0;                                //  클리어 확률
            int iRewardExp = 0;                                 //  경험치 보상

            while(true)
            {
                if (dungeonChoice == 1)
                {
                    fRecommendDef = 5.0f;
                    iRewardGold = 1000;
                    iRewardExp = 10;

                    fRandomDamage = new Random().Next(20 + (int)(fRecommendDef - player.fDefense), 35 + (int)(fRecommendDef - player.fDefense));    //  랜덤 대미지 설정
                    fBonusRewardPercent = new Random().Next((int)player.fAttack, (int)player.fAttack * 2);

                    iClearRatio = new Random().Next(1, 11);


                    iRewardGold = iRewardGold + iBonusRewardGold;

                    if (player.fDefense >= fRecommendDef)                   //  플레이어의 방어력이 권장 방어력보다 높거나 같을 때
                    {
                        iBonusRewardGold += (int)(iRewardGold * fBonusRewardPercent);

                        player.LevelUP(iRewardExp);

                        Console.Clear();

                        Console.WriteLine("던전 클리어!");
                        Console.WriteLine("쉬운 던전을 클리어하였습니다!");
                        Console.WriteLine();

                        Console.WriteLine("[탐험 결과]");
                        Console.WriteLine($"플레이어 체력: {player.fHp} →  {player.SetDamage(fRandomDamage)}");
                        Console.WriteLine($"획득 골드: {player.iGold} →  {player.SetRewardGold(iRewardGold)}");

                        break;
                    }

                    else                                                      //  플레이어의 방어력이 권장 방어력보다 낮을 때
                    {
                        fRandomDamage = new Random().Next(20 + (int)(fRecommendDef - player.fDefense), 35 + (int)(fRecommendDef - player.fDefense));

                        if (iClearRatio < 4)
                        {
                            Console.WriteLine("클리어 실패...");
                            Console.WriteLine();

                            Console.WriteLine($"플레이어 체력: {player.fHp} →  {player.SetDamage(fRandomDamage)}");
                            break;
                        }

                        else if (iClearRatio >= 6)
                        {
                            iBonusRewardGold += (int)(iRewardGold * fBonusRewardPercent);

                            player.LevelUP(iRewardExp);

                            Console.Clear();

                            Console.WriteLine("던전 클리어!");
                            Console.WriteLine("쉬운 던전을 클리어하였습니다!");
                            Console.WriteLine();

                            Console.WriteLine("[탐험 결과]");
                            Console.WriteLine($"플레이어 체력: {player.fHp} →  {player.SetDamage(fRandomDamage)}");
                            Console.WriteLine($"획득 골드: {player.iGold} →  {player.SetRewardGold(iRewardGold)}");
                            break;
                        }

                        break;
                    }
                }

                else if (dungeonChoice == 2)
                {
                    fRecommendDef = 11.0f;
                    iRewardGold = 1700;
                    iRewardExp = 10;

                    fRandomDamage = new Random().Next(20 + (int)(fRecommendDef - player.fDefense), 35 + (int)(fRecommendDef - player.fDefense));    //  랜덤 대미지 설정
                    fBonusRewardPercent = new Random().Next((int)player.fAttack, (int)player.fAttack * 2);

                    iClearRatio = new Random().Next(1, 11);

                    iRewardGold = iRewardGold + iBonusRewardGold;

                    if (player.fDefense >= fRecommendDef)
                    {
                        player.LevelUP(iRewardExp);

                        Console.Clear();

                        Console.WriteLine("던전 클리어!");
                        Console.WriteLine("보통 던전을 클리어하였습니다!");
                        Console.WriteLine();

                        Console.WriteLine("[탐험 결과]");
                        Console.WriteLine($"플레이어 체력: {player.fHp} →  {player.SetDamage(fRandomDamage)}");
                        Console.WriteLine($"획득 골드: {player.iGold} →  {player.SetRewardGold(iRewardGold)}");
                    }

                    else
                    {
                        fRandomDamage = new Random().Next(20 + (int)(fRecommendDef - player.fDefense), 35 + (int)(fRecommendDef - player.fDefense));

                        if (iClearRatio < 4)
                        {
                            Console.WriteLine("클리어 실패...");
                            Console.WriteLine();

                            Console.WriteLine($"플레이어 체력: {player.fHp} →  {player.SetDamage(fRandomDamage)}");
                            break;
                        }

                        else if (iClearRatio >= 6)
                        {
                            iBonusRewardGold += (int)(iRewardGold * fBonusRewardPercent);

                            player.LevelUP(iRewardExp);

                            Console.Clear();

                            Console.WriteLine("던전 클리어!");
                            Console.WriteLine("보통 던전을 클리어하였습니다!");
                            Console.WriteLine();

                            Console.WriteLine("[탐험 결과]");
                            Console.WriteLine($"플레이어 체력: {player.fHp} →  {player.SetDamage(fRandomDamage)}");
                            Console.WriteLine($"획득 골드: {player.iGold} →  {player.SetRewardGold(iRewardGold)}");
                            break;
                        }
                    }

                }

                else if (dungeonChoice == 3)
                {
                    fRecommendDef = 17.0f;
                    iRewardGold = 2500;
                    iRewardExp = 10;

                    fRandomDamage = new Random().Next(20 + (int)(fRecommendDef - player.fDefense), 35 + (int)(fRecommendDef - player.fDefense));    //  랜덤 대미지 설정
                    fBonusRewardPercent = new Random().Next((int)player.fAttack, (int)player.fAttack * 2);

                    iClearRatio = new Random().Next(1,11);

                    iRewardGold = iRewardGold + iBonusRewardGold;

                    if (player.fDefense >= fRecommendDef)
                    {
                        Console.Clear();

                        Console.WriteLine("던전 클리어!");
                        Console.WriteLine("어려운 던전을 클리어하였습니다!");
                        Console.WriteLine();

                        Console.WriteLine("[탐험 결과]");
                        Console.WriteLine($"플레이어 체력: {player.fHp} →  {player.SetDamage(20.0f)}");
                        Console.WriteLine($"획득 골드: {player.iGold} →  {player.SetRewardGold(iRewardGold)}");
                    }

                    else
                    {
                        if (iClearRatio < 4)
                        {
                            Console.WriteLine("클리어 실패...");
                            Console.WriteLine();

                            Console.WriteLine($"플레이어 체력: {player.fHp} →  {player.SetDamage(fRandomDamage)}");
                            break;
                        }

                        else if (iClearRatio >= 6)
                        {
                            iBonusRewardGold += (int)(iRewardGold * fBonusRewardPercent);

                            player.LevelUP(iRewardExp);

                            Console.Clear();

                            Console.WriteLine("던전 클리어!");
                            Console.WriteLine("어려운 던전을 클리어하였습니다!");
                            Console.WriteLine();

                            Console.WriteLine("[탐험 결과]");
                            Console.WriteLine($"플레이어 체력: {player.fHp} →  {player.SetDamage(fRandomDamage)}");
                            Console.WriteLine($"획득 골드: {player.iGold} →  {player.SetRewardGold(iRewardGold)}");
                            break;
                        }
                    }

                }

                else
                {
                    EnterDungeonScene(player);
                }
            }
        }

        static public void RestScene(Player player)
        {
            int iNeedGold = 500;

            Console.Clear();

            Console.WriteLine("================= [휴식하기] =================");
            Console.WriteLine();
            Console.Write($"{iNeedGold} 골드를 내면 체력을 회복할 수 있습니다.");
            Console.WriteLine($"현재 보유 골드: {player.iGold} G");
            Console.WriteLine();
            Console.WriteLine("=============================================");
            Console.WriteLine();

            Console.WriteLine();
            Console.WriteLine("1. 휴식하기");
            Console.WriteLine("0. 메인 화면으로 이동");
            Console.WriteLine();
            Console.WriteLine("===============================================");


            while(true)
            {
                if (int.TryParse(Console.ReadLine(), out int restInput))
                {
                    if (restInput == 0)                                         //  0을 눌렀을 경우, 이전 씬(시작 씬)으로 이동
                    {
                        StartScene(player);
                    }

                    else if(restInput == 1)                                    //  0이 아닌 숫자를 눌렀을 경우, 해당 숫자의 아이템을 구매
                    {
                        if (player.iGold >= iNeedGold)
                        {
                            Console.WriteLine("휴식을 완료했습니다!");
                            player.iGold -= iNeedGold;
                            player.fHp = player.fMaxHp;

                            Console.WriteLine("0. 돌아가기");
                        }

                        else
                        {
                            Console.WriteLine("골드가 부족합니다");
                        }
                    }

                    else
                    {
                        Console.ReadKey();
                    }
                }

                else
                {
                    Console.WriteLine("잘못된 입력입니다. 아무 키나 누르면 돌아갑니다");
                    Thread.Sleep(2000);
                    //Console.ReadKey();
                }   
            }
            
        }

        static public void StartScene(Player player)
        {
            while (true)
            {
                Thread.Sleep(3000);                     //  콘솔창에서 다음 내용 실행 전 딜레이를 주는 함수 → 3초 후에 실행
                Console.Clear();

                Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다");
                Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다");

                Console.WriteLine($"1. 상태 확인");
                Console.WriteLine($"2. 인벤토리 확인");
                Console.WriteLine($"3. 상점 가기");
                Console.WriteLine($"4. 던전 입장");
                Console.WriteLine($"5. 휴식하기");
                Console.WriteLine($"6. 저장하기");
                Console.WriteLine($"7. 불러오기");

                Console.Write("원하시는 행동을 입력해주세요! >> ");

                if (int.TryParse(Console.ReadLine(), out int choiceInput))
                {
                    switch (choiceInput)
                    {
                        case (byte)MAIN_STATE.SCENE_STATUS:
                            Console.Clear();
                            StatInfoScene(player);
                            BackStage(player);
                            break;
                        case (byte)MAIN_STATE.SCENE_INVENTORY:
                            Console.Clear();
                            InventoryScene(player);
                            break;
                        case (byte)MAIN_STATE.SCENE_SHOP:
                            ShopScene(player);
                            break;
                        case (byte)MAIN_STATE.SCENE_BATTLE:
                            EnterDungeonScene(player);
                            break;
                        case (byte)MAIN_STATE.SCENE_REST:
                            RestScene(player);
                            break;
                        case (byte)MAIN_STATE.SCENE_SAVEDATA:
                            SaveData(player);
                            break;
                        case (byte)MAIN_STATE.SCENE_LOADDATA:
                            LoadData(player);
                            break;
                        default:
                            {
                                Console.Clear();
                                Console.WriteLine("잘못된 입력값입니다. 숫자를 다시 입력해주세요!");
                                return;
                            }
                    }
                }
            }
        }

        static public void BackStage(Player player)
        {
           while(true)
            {
                Console.WriteLine("0. 나가기");

                if(int.TryParse(Console.ReadLine(), out int userInput))
                {
                    if(userInput == 0)
                    {
                        StartScene(player);
                        break;
                    }

                    else
                    {
                        Console.WriteLine("잘못된 입력값입니다. 숫자만 입력해주세요!");
                    }
                }
            }
        }

        public static void SaveData(Player player)
        {
            string playerData = "../../../PlayerData.json ";                       //  데이터 저장 파일 경로 설정

            try
            {
                string json = JsonConvert.SerializeObject(player, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(playerData, json);

                Console.WriteLine("데이터가 저장되었습니다!");
                Console.WriteLine(playerData);
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error Saving Data: {ex.Message}");
            }
        }

        public static void LoadData(Player player)
        {
            string playerData = "../../../PlayerData.json ";

            try
            {
                if(File.Exists(playerData))                                         //  playerData가 존재하면 해당 파일을 불러온다
                {
                    string json = File.ReadAllText(playerData);
                    player =  JsonConvert.DeserializeObject<Player>(json);

                    Console.WriteLine("playerData를 성공적으로 불러왔습니다!");
                }

                else                                                                // playerData가 존재하지 않는다면, 기본 데이터로 설정함
                {
                    player =  new Player(1, "전사", "이세계 용사", 10.0f, 1.0f, 100.0f, 10000);
                }
            }

            catch(Exception ex)
            {
                Console.WriteLine($"Error Load Data: {ex.Message}");

                player = new Player(1, "전사", "이세계 용사", 10.0f, 1.0f, 100.0f, 10000);
            }
        }

        static void Main(string[] args)
        {
            //Player player = new Player(1, "전사", "이세계 용사", 10.0f, 1.0f, 100.0f, 10000);

            Player player = new Player();

            StartScene(player);
        }
    }
}
