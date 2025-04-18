﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    partial class TextRPG_Player    //  플레이어 클래스
    {
        //  플레이어 클래스 변수 
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

        //  플레이어 인벤토리 → 리스트로 관리
        public List<TextRPG_Item> lstInventory { get; set; }

        // 플레이어가 착용한 장비 리스트 
        public List<TextRPG_Item> lstEquip { get; set; }
        public List<TextRPG_Item> lstEquipArmor { get; set; }           //   유저가 장착한 방어구 리스트
        public List<TextRPG_Item> lstEquipWeapon { get; set; }          //   유저가 장착한 무기 리스트

        public TextRPG_Player()
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
            lstInventory = new List<TextRPG_Item>();
            lstEquipArmor = new List<TextRPG_Item>();
            lstEquipWeapon = new List<TextRPG_Item>();
            lstEquip = new List<TextRPG_Item>();

            fAttackSum = 0.0f;
            fDefenseSum = 0.0f;
            fHpSum = 0.0f;
        }


        public TextRPG_Player(byte level, string job, string name, float attack, float defense, float hp, int gold)      //  캐릭터 별 상태 정보를 원하는 수치로 입력, 입력한 값으로 초기화한다.
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
            lstInventory = new List<TextRPG_Item>();
            lstEquipArmor = new List<TextRPG_Item>();
            lstEquipWeapon = new List<TextRPG_Item>();
            lstEquip = new List<TextRPG_Item>();

            fAttackSum = 0.0f;
            fDefenseSum = 0.0f;
            fHpSum = 0.0f;
        }

        public void EquipItem(TextRPG_Item item)                        //  플레이어 장비 아이템 착용 메서드 → 인벤토리에서 아이템 장착을 선택할 시, 선택한 장비 아이템이 방어구/무기 아이템인지 확인 후, 해당 리스트에 추가한 후, 능력치를 올린다.
        {
            if (lstEquipArmor.Count == 0 && item.eType == TextRPG_Enum.ITEMTYPE.ITEM_ARMOR)
            {
                lstEquipArmor.Add(item);

                //  아이템 장착 시, 해당 아이템의 추가 능력치만큼 캐릭터의 능력치가 올라간다.
                fDefense += item.fDefense;
                fHp += item.fHp;

                //  아이템을 장착했을 때, 상태 창에 추가된 능력치의 값을 보여주기 위한 작업
                fDefenseSum += item.fDefense;
                fHpSum += item.fHp;
            }

            else if (lstEquipArmor.Count != 0 && item.eType == TextRPG_Enum.ITEMTYPE.ITEM_ARMOR)
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

            if (lstEquipWeapon.Count == 0 && item.eType == TextRPG_Enum.ITEMTYPE.ITEM_WEAPON)
            {
                lstEquipWeapon.Add(item);

                //  아이템 장착 시, 해당 아이템의 추가 능력치만큼 캐릭터의 능력치가 올라간다.
                fAttack += item.fAttack;

                //  아이템을 장착했을 때, 상태 창에 추가된 능력치의 값을 보여주기 위한 작업
                fAttackSum += item.fAttack;
            }

            else if (lstEquipWeapon.Count != 0 && item.eType == TextRPG_Enum.ITEMTYPE.ITEM_WEAPON)
            {
                lstEquipWeapon.Clear();

                fAttackSum = 0;

                lstEquipWeapon.Add(item);

                fAttack += item.fAttack;
                fAttackSum += item.fAttack;
            }
        }

        public void UnequipItem(TextRPG_Item item)      //  장비 아이템 장착 해제 메서드 → 장착 해제한 아이템의 능력치만큼 플레이어 능력치를 감소시킨다. 
        {
            fAttack -= item.fAttack;
            fDefense -= item.fDefense;
            fHp -= item.fHp;

            item.bIsWear = false;
        }

        public void LevelUP(int exp)                    //  레벨 업을 관리하는 메서드 → 던전을 클리어 한 후, 해당 던전의 보상 경험치를 넘겨받아 계산. 일정 경험치를 획득하면 레벨 업 및 경험치를 0으로 초기화한다.
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

            else if (bLevel == 3 && iExp == 30)
            {
                bLevel = 4;

                fAttack += 0.5f;
                fDefense += 1.0f;
                fHp = fMaxHp;
                iExp = 0;
            }

            else if (bLevel == 4 && iExp == 40)
            {
                bLevel = 5;

                fAttack += 0.5f;
                fDefense += 1.0f;
                fHp = fMaxHp;
                iExp = 0;
            }
        }

        public float SetDamage(float fAttack)           // 데미지 계산 메서드 → 던전에서 전투 후, 던전에 설정된 데미지를 넘겨받아 계산. 넘겨받은 데미지만큼 플레이어의 체력을 감소시킨다.
        {
            fHp -= fAttack;

            return fHp;
        }

        public int SetRewardGold(int gold)             //  던전 클리어 골드 보상 메서드 → 던전을 클리어한 후, 던전에 설정된 골드 보상값을 넘겨받아 계산. 넘겨받은 골드만큼 플레이어의 보유 골드를 증가시킨다.
        {
            iGold += gold;

            return iGold;
        }
    }
}