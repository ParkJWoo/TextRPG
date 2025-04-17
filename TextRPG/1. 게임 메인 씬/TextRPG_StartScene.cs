using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    partial class TextRPG_StartScene
    {
        static public void StartScene(TextRPG_Player player)
        {
            while (true)
            {
                Thread.Sleep(2000);                                                     //  콘솔창에서 다음 내용 실행 전 딜레이를 주는 함수 → 2초 후에 실행
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
                Console.WriteLine($"0. 나가기");

                Console.Write("원하시는 행동을 입력해주세요! >> ");

                if (int.TryParse(Console.ReadLine(), out int choiceInput))
                {
                    switch (choiceInput)
                    {
                        case (byte)TextRPG_Enum.MAIN_STATE.SCENE_QUIT:
                            Console.WriteLine("게임을 종료합니다!");
                            return;
                        case (byte)TextRPG_Enum.MAIN_STATE.SCENE_STATUS:
                            Console.Clear();
                            TextRPG_StatInfoScene.StatInfoScene(player);
                            BackStage(player);
                            break;
                        case (byte)TextRPG_Enum.MAIN_STATE.SCENE_INVENTORY:
                            Console.Clear();
                            TextRPG_InventoryScene.InventoryScene(player);
                            break;
                        case (byte)TextRPG_Enum.MAIN_STATE.SCENE_SHOP:
                            TextRPG_ShopScene.ShopScene(player);
                            break;
                        case (byte)TextRPG_Enum.MAIN_STATE.SCENE_BATTLE:
                            TextRPG_DungeonScene.EnterDungeonScene(player);
                            break;
                        case (byte)TextRPG_Enum.MAIN_STATE.SCENE_REST:
                            TextRPG_RestScene.RestScene(player);
                            break;
                        case (byte)TextRPG_Enum.MAIN_STATE.SCENE_SAVEDATA:
                            TextRPG_DataSet.SaveData(player);
                            break;
                        case (byte)TextRPG_Enum.MAIN_STATE.SCENE_LOADDATA:
                            TextRPG_DataSet.LoadData();
                            Console.WriteLine(player);
                            break;
                        default:
                            {
                                Console.WriteLine("잘못된 입력값입니다. 숫자를 다시 입력해주세요!");
                                break;
                            }
                    }
                }
            }
        }

        static public void BackStage(TextRPG_Player player)
        {
            while (true)
            {
                Console.WriteLine("0. 나가기");

                if (int.TryParse(Console.ReadLine(), out int userInput))
                {
                    if (userInput == 0)
                    {
                        TextRPG_StartScene.StartScene(player);
                        break;
                    }

                    else
                    {
                        Console.WriteLine("잘못된 입력값입니다. 숫자만 입력해주세요!");
                    }
                }
            }
        }
    }
}
