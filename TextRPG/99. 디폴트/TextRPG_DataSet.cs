using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;                                                             // 데이터 저장, 불러오기를 사용하기 위한 네임 스페이스 선언

namespace TextRPG
{
    partial class TextRPG_DataSet                                                  // 데이터 저장, 불러오기를 관리하는 클래스 
    {
        public static void SaveData(TextRPG_Player player)
        {
            //  데이터 저장 파일 경로 설정 → 상대 경로로 지정
            string playerData = "../../../PlayerData.json ";                       

            try
            {
                string json = JsonConvert.SerializeObject(player, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(playerData, json);

                Console.WriteLine("데이터가 저장되었습니다!");
                Console.WriteLine(playerData);
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error Saving Data: {ex.Message}");
            }
        }

        public static TextRPG_Player LoadData()                                       //  저장된 playerData 데이터 불러오는 함수
        {
            string playerData = "../../../PlayerData.json ";

            try
            {
                //  playerData가 존재하면 해당 파일을 불러온다
                if (File.Exists(playerData))                                        
                {
                    string json = File.ReadAllText(playerData);

                    Console.WriteLine("playerData를 성공적으로 불러왔습니다!");

                    return JsonConvert.DeserializeObject<TextRPG_Player>(json);
                }

                // playerData가 존재하지 않는다면, 임의로 설정한 기본 데이터를 적용.
                else
                {
                    return new TextRPG_Player(1, "전사", "이세계 용사", 10.0f, 1.0f, 100.0f, 10000);
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error Load Data: {ex.Message}");

                return new TextRPG_Player(1, "전사", "이세계 용사", 10.0f, 1.0f, 100.0f, 10000);
            }
        }
    }
}
