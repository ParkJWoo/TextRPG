using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TextRPG
{
    partial class TextRPG_DataSet
    {
        public static void SaveData(TextRPG_Player player)
        {
            string playerData = "../../../PlayerData.json ";                       //  데이터 저장 파일 경로 설정

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

        public static TextRPG_Player LoadData()                                             //  저장된 playerData 데이터 불러오는 함수
        {
            string playerData = "../../../PlayerData.json ";

            try
            {
                if (File.Exists(playerData))                                         //  playerData가 존재하면 해당 파일을 불러온다
                {
                    string json = File.ReadAllText(playerData);

                    Console.WriteLine("playerData를 성공적으로 불러왔습니다!");

                    return JsonConvert.DeserializeObject<TextRPG_Player>(json);
                }

                else                                                                // playerData가 존재하지 않는다면, 기본 데이터로 설정함
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
