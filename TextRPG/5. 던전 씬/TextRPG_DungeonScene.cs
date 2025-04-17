using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    partial class TextRPG_DungeonScene                                          //  던전 입장, 던전 기능을 관리하는 클래스
    {
        static public void EnterDungeonScene(TextRPG_Player player)
        {
            while (true)
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

        static public void BattleStage(TextRPG_Player player, int dungeonChoice)
        {
            float fRecommendDef = 0.0f;                         //  던전 당 권장 방어력
            float fRandomDamage = 0.0f;                         //  대미지 범위
            int iRewardGold = 0;                                //  골드 보상
            int iBonusRewardGold = 0;                           //  골드 추가 보상
            float fBonusRewardPercent = 0.0f;                   //  골드 추가 보상 확률
            int iClearRatio = 0;                                //  클리어 확률
            int iRewardExp = 0;                                 //  경험치 보상

            while (true)
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

                    iClearRatio = new Random().Next(1, 11);

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
    }
}
