using System.ComponentModel.Design;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using static TextRPG.Program;
using Newtonsoft.Json;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.Serialization.Formatters.Binary;                          //  데이터 저장용

namespace TextRPG
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TextRPG_Player player = TextRPG_DataSet.LoadData();         //  저장되어 있는 플레이어 데이터를 불러온다.

            TextRPG_StartScene.StartScene(player);                      //  불러온 데이터를 기반을 메인 씬에 입장
        }
    }
}