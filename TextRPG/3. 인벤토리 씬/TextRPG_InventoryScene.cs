using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    partial class TextRPG_InventoryScene
    {
        static public void InventoryScene(TextRPG_Player player)            //  플레이어의 인벤토리를 명시하는 정보 창
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
                    if (player.lstEquipArmor.Count == 0 || player.lstEquipWeapon.Count == 0)
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

                        else if (item.bIsWear)
                        {
                            Console.WriteLine($" {i}. 아이템 이름: {item.strName} | 공격력: {item.fAttack} 증가 | 방어력: {item.fDefense} 증가 |" +
                                              $" 체력: {item.fHp} 증가");

                            i++;
                            Console.WriteLine();
                        }
                    }

                    else if (player.lstEquipArmor.Count != 0)
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
                        TextRPG_StartScene.StartScene(player);
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

        static public void WearScene(TextRPG_Player player)
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
                            TextRPG_Item wearableItem = player.lstInventory[wearInput - 1];

                            if (wearableItem.bIsWear)
                            {
                                Console.WriteLine("해당 아이템은 이미 장착 중입니다!");
                            }

                            else if (!wearableItem.bIsWear)
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
    }
}
