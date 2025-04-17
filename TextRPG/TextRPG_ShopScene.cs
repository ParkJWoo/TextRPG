using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    partial class TextRPG_ShopScene
    {
        static public void ShopScene(TextRPG_Player player)            //  플레이어의 인벤토리를 명시하는 정보 창
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

            List<TextRPG_Item> itemList = new List<TextRPG_Item>()
            {
                new TextRPG_Item(TextRPG_Enum.ITEMTYPE.ITEM_ARMOR, "수련자 갑옷", 0.0f, 5.0f, 0.0f,1000,false),
                new TextRPG_Item(TextRPG_Enum.ITEMTYPE.ITEM_ARMOR, "무쇠 갑옷", 0.0f, 9.0f, 0.0f,1000,false),
                new TextRPG_Item(TextRPG_Enum.ITEMTYPE.ITEM_ARMOR, "스파르타의 갑옷", 0.0f, 15.0f, 0.0f,3500,false),
                new TextRPG_Item(TextRPG_Enum.ITEMTYPE.ITEM_WEAPON, "낡은 검", 2.0f, 0.0f, 0.0f,600,false),
                new TextRPG_Item(TextRPG_Enum.ITEMTYPE.ITEM_WEAPON,"청동 도끼", 5.0f, 0.0f, 0.0f,1500,false),
                new TextRPG_Item(TextRPG_Enum.ITEMTYPE.ITEM_WEAPON,"스파르타의 창", 7.0f, 0.0f, 0.0f,3000,false),
                new TextRPG_Item(TextRPG_Enum.ITEMTYPE.ITEM_WEAPON, "이세계 용사의 꿰뚫는 창", 20.0f, 0.0f, 0.0f,15000,false)
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
                        TextRPG_StartScene.StartScene(player);
                        break;
                    }

                    else if (shopInput == 1)
                    {
                        PurchaseItem(itemList, player);
                        break;
                    }

                    else if (shopInput == 2)
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

        static void PurchaseItem(List<TextRPG_Item> itemList, TextRPG_Player player)
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

                for (int i = 0; i < itemList.Count; i++)
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


                if (int.TryParse(Console.ReadLine(), out int itemInput) && itemInput >= 0 && itemInput <= itemList.Count)
                {
                    if (itemInput == 0)                      //  0을 눌렀을 경우, 이전 씬(상점 씬)으로 이동
                    {
                        ShopScene(player);
                        break;
                    }

                    else                                    //  0이 아닌 숫자를 눌렀을 경우, 해당 숫자의 아이템을 구매
                    {
                        TextRPG_Item puschaseItem = itemList[itemInput - 1];

                        if (puschaseItem.bIsPurchase)
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

        static public void SellScene(TextRPG_Player player)
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

                    else if (itemInput != 0)                                    //  0이 아닌 숫자를 눌렀을 경우, 해당 숫자의 아이템을 판매
                    {
                        TextRPG_Item sellItem = player.lstInventory[itemInput - 1];

                        Console.WriteLine("판매를 완료했습니다!");
                        player.iGold += (int)(sellItem.iPrice * 0.85f);
                        sellItem.bIsPurchase = false;
                        player.lstInventory.Remove(sellItem);
                        player.lstEquip.Remove(sellItem);

                        if (sellItem.bIsWear == true)
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
    }
}
