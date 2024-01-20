using DND_WPF;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using Org.BouncyCastle.Bcpg.OpenPgp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;

namespace DnD
{
    public class Query
    {
        public DBConnection dbCon;
        /// <summary>
        /// Тестовый метод для выполнения запроса в базу данных и возвращения результата в виде строк 
        /// </summary>
        /// <param name="dbCon"></param>
        /// <returns></returns>
        public static List<TextBlock> GetCharDescription(DBConnection dbCon)
        {
            List<TextBlock> rows = new List<TextBlock>();
            if (dbCon.IsConnect())
            {
                //suppose col0 and col1 are defined as VARCHAR in the DB
                string query = "select characters.name, characters.strength, characters.dexterity, characters.constitution, races.name, classes.name, weapon.name, armor.name, characters.HP\r\nfrom characters\r\njoin equipped_weapon_character join weapon join races join classes join equipped_armor_character join armor\r\non characters.race_id = races.id and characters.class_id = classes.id and equipped_weapon_character.weapon_id = weapon.id \r\nand characters.id = equipped_weapon_character.character_id and equipped_armor_character.armor_id = armor.id and\r\ncharacters.id = equipped_armor_character.character_id\r\n;";
                var cmd = new MySqlCommand(query, dbCon.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string charsNames = reader.GetString(0);
                    string strength = reader.GetString(1);
                    string dexterity = reader.GetString(2);
                    string constitution = reader.GetString(3);
                    string race = reader.GetString(4);
                    string classes = reader.GetString(5);
                    string weapon = reader.GetString(6);
                    string armor = reader.GetString(7);
                    string HP = reader.GetString(8);
                    string description = $"{race}, {classes}, Хиты: {HP}\nСила: {strength}\nЛовкость: " + $"{dexterity}\nТелосложение:{constitution}\nОружие: {weapon}\nБроня:{armor}";
                    ToolTip tooltip = new ToolTip();
                    tooltip.Content = description;
                    TextBlock tB = new TextBlock();
                    tB.Text = charsNames;
                    tooltip.FontSize = 18;
                    tB.FontSize = 20;
                    tB.ToolTip = tooltip;
                    rows.Add(tB);

                }
                dbCon.Close();
                dbCon.Update();
            }
            return rows;
        }
        public static List<TextBlock> GetEnemyDescription(DBConnection dbCon)
        {
            var rows = new List<TextBlock>();
            if (dbCon.IsConnect())
            {

                //suppose col0 and col1 are defined as VARCHAR in the DB
                string query = "select e.name, e.strength, e.dexterity, e.constitution, w.name, a.name, e.HP \r\nfrom enemy e join weapon w join armor a join equipped_weapon_enemy ewe join equipped_armor_enemy eae on\r\newe.enemy_id = e.id and ewe.weapon_id = w.id and eae.enemy_id = e.id and eae.armor_id = a.id;";
                var cmd = new MySqlCommand(query, dbCon.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    string enemyName = reader.GetString(0);
                    string strength = reader.GetString(1);
                    string dexterity = reader.GetString(2);
                    string constitution = reader.GetString(3);
                    string weapon = reader.GetString(4);
                    string armor = reader.GetString(5);
                    string HP = reader.GetString(6);
                    string description = $"Хиты: {HP}\nСила: {strength}\nЛовкость: " + $"{dexterity}\nТелосложение:{constitution}\nОружие: {weapon}\nБроня:{armor}";
                    ToolTip tooltip = new ToolTip();
                    tooltip.Content = description;
                    tooltip.FontSize = 18;
                    TextBlock tB = new TextBlock();
                    tB.Text = enemyName;
                    tB.FontSize = 20;
                    tB.ToolTip = tooltip;
                    rows.Add(tB);

                }
                dbCon.Close();
                dbCon.Update();
            }
            return rows;
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
        public static Dictionary<int, string> GetWeaponDict(DBConnection dbCon)
        {
            var GetWeaponDict = new Dictionary<int, string>();
            int weaponId;
            string weaponName;
            if (dbCon.IsConnect())
            {

                //suppose col0 and col1 are defined as VARCHAR in the DB
                string query = "select id, name from weapon";
                var cmd = new MySqlCommand(query, dbCon.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    weaponId = reader.GetInt32(0);
                    weaponName = reader.GetString(1);
                    GetWeaponDict.Add(weaponId, weaponName);
                }
                dbCon.Close();
                dbCon.Update();

            }
            return GetWeaponDict;
        }
        public static Dictionary<int, string> GetClassDict(DBConnection dbCon)
        {
            var GetClassDict = new Dictionary<int, string>();
            int classId;
            string className;
            if (dbCon.IsConnect())
            {

                //suppose col0 and col1 are defined as VARCHAR in the DB
                string query = "select id, name from classes";
                var cmd = new MySqlCommand(query, dbCon.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    classId = reader.GetInt32(0);
                    className = reader.GetString(1);
                    GetClassDict.Add(classId, className);
                }
                dbCon.Close();
                dbCon.Update();

            }
            return GetClassDict;
        }
        public static Dictionary<int, string> GetRaceDict(DBConnection dbCon)
        {
            var GetRaceDict = new Dictionary<int, string>();
            int raceId;
            string className;
            if (dbCon.IsConnect())
            {
                //suppose col0 and col1 are defined as VARCHAR in the DB
                string query = "select id, name from races";
                var cmd = new MySqlCommand(query, dbCon.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    raceId = reader.GetInt32(0);
                    className = reader.GetString(1);
                    GetRaceDict.Add(raceId, className);
                }
                dbCon.Close();
                dbCon.Update();
            }
            return GetRaceDict;
        }
        public static void InsertData(DBConnection dbCon, string query)
        {
            if (dbCon.IsConnect())
            {
                var cmd = new MySqlCommand(query, dbCon.Connection);
                cmd.ExecuteNonQuery();
                dbCon.Close();
                dbCon.Update();
            }

        }
        public static void insertEquippedWeaponChar(DBConnection dbCon, string name, int weapon_id, int armor_id)
        {
            int? id = null;
            if (dbCon.IsConnect())
            {
                var cmd = new MySqlCommand($"select id from characters where name='{name}'", dbCon.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    id = reader.GetInt32(0);
                }
                dbCon.Close();
                dbCon.Update();
                cmd = new MySqlCommand($"insert into equipped_weapon_character(character_id, weapon_id) values ({id}, {weapon_id})", dbCon.Connection);
                cmd.ExecuteNonQuery();
                dbCon.Close();
                dbCon.Update();
                cmd = new MySqlCommand($"insert into equipped_armor_character(character_id, armor_id) values ({id}, {armor_id})", dbCon.Connection);
                cmd.ExecuteNonQuery();
                dbCon.Close();
                dbCon.Update();
            }
        }
        public static void insertEquippedWeaponEnemy(DBConnection dbCon, string name, int weapon_id, int armor_id)
        {
            int? id = null;
            if (dbCon.IsConnect())
            {
                var cmd = new MySqlCommand($"select id from enemy where name='{name}'", dbCon.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    id = reader.GetInt32(0);
                }
                dbCon.Close();
                dbCon.Update();
                cmd = new MySqlCommand($"insert into equipped_weapon_enemy(enemy_id, weapon_id) values ({id}, {weapon_id})", dbCon.Connection);
                cmd.ExecuteNonQuery();
                dbCon.Close();
                dbCon.Update();
                cmd = new MySqlCommand($"insert into equipped_armor_enemy(enemy_id, armor_id) values ({id}, {armor_id})", dbCon.Connection);
                cmd.ExecuteNonQuery();
                dbCon.Close();
                dbCon.Update();
            }
        }
        public static int? getId(DBConnection dbCon, string name, bool isEnemy = false)
        {
            int? id = null;
            if (dbCon.IsConnect())
            {
                MySqlCommand cmd = new MySqlCommand($"select id from characters where name='{name}'", dbCon.Connection);
                if (isEnemy)
                {
                    cmd = new MySqlCommand($"select id from enemy where name='{name}'", dbCon.Connection);
                }
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    id = reader.GetInt32(0);
                }
                dbCon.Close();
                dbCon.Update();
            }
            return id;
        }
        public static void deleteRow(DBConnection dbCon, int? id, bool isEnemy = false)
        {
            if (dbCon.IsConnect())
            {
                MySqlCommand cmd = new MySqlCommand($"delete from characters where id= {id}", dbCon.Connection);
                if (isEnemy)
                {
                    cmd = new MySqlCommand($"delete from enemy where id= {id}", dbCon.Connection);
                }
                cmd.ExecuteNonQuery();
                dbCon.Close();
                dbCon.Update();
            }
        }
        public static Dictionary<string, int> GetAverageValueChar(DBConnection dbCon, string[] names, string table1="characters", string table2 = "character")
        {
            string query = $"select strength, HP, defence, id from {table1} where ";
            var result = new Dictionary<string, int>();
            List<int> id = new List<int>();
            float counter = 0;
            float sumStrength = 0;
            float sumHP = 0;
            float sumDefence = 0;
            float averageDmg = 0;
            int averageDmgInt = 0;
            List<int> weapon_id = new List<int>();

            foreach (string name in names)
            {
                query += $"name = '{name}' or ";
            }
            query = query.Substring(0, query.Length - 3);
            if (dbCon.IsConnect())
            {
                var cmd = new MySqlCommand(query, dbCon.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                { 
                    sumStrength += reader.GetInt32(0);
                    sumHP += reader.GetInt32(1);
                    sumDefence += reader.GetInt32(2);
                    id.Add(reader.GetInt32(3));
                    counter++;
                }
                dbCon.Close();
                dbCon.Update();
                query = $"select weapon_id from equipped_weapon_{table2} where ";
                foreach(int n in id)
                {
                    query += $"{table2}_id = {n} or ";
                }
                query = query.Substring(0, query.Length - 3);
                cmd = new MySqlCommand(query, dbCon.Connection);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    weapon_id.Add(reader.GetInt32(0));
                }
                dbCon.Close();
                dbCon.Update();
                query = "select hit_dice, dice_count from weapon where ";
                foreach (int n in weapon_id)
                {
                    query += $"id = {n} or ";
                }
                query = query.Substring(0, query.Length - 3);
                cmd = new MySqlCommand(query, dbCon.Connection);
                reader = cmd.ExecuteReader();
                int averageStrength = Convert.ToInt32((double)sumStrength / (double)counter);
                while (reader.Read())
                {
                    averageDmg += ( ( (reader.GetInt32(0) + 1) * reader.GetInt32(1)) / 2 + GameLogic.Modifier(averageStrength));
                }
                averageDmgInt = Convert.ToInt32( Math.Round(averageDmg)) + 1;
                dbCon.Close();
                dbCon.Update();
            }
            result.Add("damage", averageDmgInt);
            result.Add("defence", Convert.ToInt32(((double)sumDefence / (double)counter)));
            result.Add("HP", Convert.ToInt32((double)sumHP / (double)counter));
            result.Add("mod", Convert.ToInt32((double)sumStrength / (double)counter));

            return result;
        }
    }
}
    