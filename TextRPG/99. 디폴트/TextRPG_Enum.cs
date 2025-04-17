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
        //  시작 화면의 선택지 상태
        public enum MAIN_STATE                              
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

        //  던전 타입
        public enum DUNGEON                                 
        {
            DUNGEON_EASY = 1,
            DUNGEON_NORMAL,
            DUNGEON_HARD
        }

        //  아이템 타입 (방어구, 무기)
        public enum ITEMTYPE                                
        {
            ITEM_WEAPON = 0,
            ITEM_ARMOR
        }
    }
}
