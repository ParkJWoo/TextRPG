using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG;

namespace TextRPG
{
    partial class TextRPG_Item
    {

        public TextRPG_Enum.ITEMTYPE eType { get; set; }
        public string strName { get; set; }
        public float fAttack { get; set; }
        public float fDefense { get; set; }
        public float fHp { get; set; }
        public float fMaxHp { get; set; }
        public int iPrice { get; set; }
        public bool bIsPurchase { get; set; }
        public bool bIsWear { get; set; }

        public TextRPG_Item(TextRPG_Enum.ITEMTYPE type, string name, float attack, float defense, float hp, int gold, bool purchase)
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
}
