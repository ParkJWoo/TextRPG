using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    partial class TextRPG_StatInfoScene
    {
        static public void StatInfoScene(TextRPG_Player player)            //  플레이어의 스탯 정보를 명시하는 정보 창
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
    }
}
