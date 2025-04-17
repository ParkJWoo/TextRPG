using System.ComponentModel.Design;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using static TextRPG.Program;
using Newtonsoft.Json;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.Serialization.Formatters.Binary;                          //  데이터 저장용
using TextRPG;
using static TextRPG.TextRPG_Item;
using static TextRPG.TextRPG_Enum;

namespace TextRPG
{
    internal class Program
    {

        static void Main(string[] args)
        {
            TextRPG_Player player = TextRPG_DataSet.LoadData();

            TextRPG_StartScene.StartScene(player);
        }
    }
}
