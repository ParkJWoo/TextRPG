using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    partial class TextRPG_RestScene
    {
        static public void RestScene(TextRPG_Player player)                 //  500 골드를 사용하여 플레이어의 체력을 최대 체력만큼 회복시켜주는 화면
        {
            //  휴식에 소모되는 골드 값
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


            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int restInput))
                {
                    //  0을 눌렀을 경우, 이전 씬(시작 씬)으로 이동
                    if (restInput == 0)                                         
                    {
                        TextRPG_StartScene.StartScene(player);
                    }

                    //  0이 아닌 숫자를 눌렀을 경우, 해당 숫자의 아이템을 구매
                    else if (restInput == 1)                                    
                    {
                        //  플레이어가 보유한 골드가 휴식 시 소모되는 골드보다 많다면, 
                        if (player.iGold >= iNeedGold)
                        {
                            Console.WriteLine("휴식을 완료했습니다!");

                            //  플레이어의 보유 골드를 휴식 시 필요한 골드만큼 차감한다.
                            player.iGold -= iNeedGold;

                            //  이후, 플레이어의 체력을 최대 체력만큼 올려준다.
                            player.fHp = player.fMaxHp;

                            Console.WriteLine("0. 돌아가기");
                        }

                        //  플레이어가 보유한 골드가 휴식 시 소모되는 골드보다 적다면,
                        else
                        {
                            //  체력을 회복하지 않는다.
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
                }
            }
        }
    }
}
