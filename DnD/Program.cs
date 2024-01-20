using MySql.Data.MySqlClient;
using System.Diagnostics.CodeAnalysis;
using static DnD.GameLogic;


namespace DnD
{
    public static class Program
    {

        static void Main(string[] args)
        {

            /*int Strength = 17;
            int Constitution = 16;
            int Dexterity = 13;

            int[] attacking_Weapon = { 2, 6 }; //{Количество костей, вид кости (цифра, для к6 - 6, к20 - 20), модификатор
            Console.WriteLine($"Нанесённый урон - {Dice(1, 6) + Modifier(Strength)  }"); */
            //------------------------------------------------Тест базы данных----------------------------------------------------------
            var dbCon = DBConnection.Instance();

            var armorNamesList = new List<string>();
            foreach (var pair in GetArmorDict(dbCon))
            {
                armorNamesList.Append(pair.Value);
                Console.WriteLine(pair.Value);
            }

            //-------------------------------------------------Тест базы данных---------------------------------------------------------
        }
        public static Dictionary<int, string> GetArmorDict(DBConnection dbCon)
        {
            var GetArmorDict = new Dictionary<int, string>();
            int armorId;
            string armorName;
            if (dbCon.IsConnect())
            {

                //suppose col0 and col1 are defined as VARCHAR in the DB
                string query = "select id, name from armor";
                var cmd = new MySqlCommand(query, dbCon.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    armorId = reader.GetInt32(0);
                    armorName = reader.GetString(1);
                    GetArmorDict.Add(armorId, armorName);
                }
                dbCon.Close();
                dbCon.Update();

            }
            return GetArmorDict;
        }



    }
        }