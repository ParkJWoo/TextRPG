using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG;

namespace TextRPG
{
    partial class TextRPG_Enum
    {
        public enum MAIN_STATE                              //  시작 화면의 선택지 상태
        {
            SCENE_QUIT = 0,
            SCENE_STATUS,
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
    }
}
