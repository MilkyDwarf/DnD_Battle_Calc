using DnD;
using Org.BouncyCastle.Bcpg.OpenPgp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DND_WPF
{
    /// <summary>
    /// Interaction logic for Add_Character.xaml
    /// </summary>
    public partial class Add_Character : Window
    {

        public static Dictionary<int, string> ArmorDict { get; set; }
        public static Dictionary<int, string> WeaponDict { get; set; }
        public static Dictionary<int, string> ClassDict { get; set; }
        public static Dictionary<int, string> RaceDict { get; set; }
        public static DBConnection dbCon { get; set; }
        public Add_Character()
        {
            InitializeComponent();
            dbCon = DBConnection.Instance();
            ArmorDict = Query.GetArmorDict(dbCon);
            WeaponDict = Query.GetWeaponDict(dbCon);
            ClassDict = Query.GetClassDict(dbCon);
            RaceDict = Query.GetRaceDict(dbCon);
            addArmor();
            addWeapon();
            addClass();
            addRace();
            DataContext = this;
            
        }
        private void addArmor()
        {
            List<string> armorNamesList = new List<string>();

            
            armorNamesList.AddRange(Query.GetArmorDict(dbCon).Values);
            armorCB.ItemsSource = armorNamesList;
        }
        private void addWeapon()
        {
            List<string> weaponNamesList = new List<string>();
            weaponNamesList.AddRange(Query.GetWeaponDict(dbCon).Values);
            weaponCB.ItemsSource = weaponNamesList;
        }
        private void addClass()
        {
            List<string> classNamesList = new List<string>();
            classNamesList.AddRange(Query.GetClassDict(dbCon).Values);
            classCB.ItemsSource = classNamesList;
        }
        private void addRace()
        {
            List<string> raceNamesList = new List<string>();
            raceNamesList.AddRange(Query.GetRaceDict(dbCon).Values);
            raceCB.ItemsSource = raceNamesList;
        }

        public void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (checkValidaton())
            {
                int raceId = GetKeyByValue(RaceDict, raceCB.SelectedItem.ToString());
                int classId = GetKeyByValue(ClassDict, classCB.SelectedItem.ToString());
                int weaponId = GetKeyByValue(WeaponDict, weaponCB.SelectedItem.ToString());
                int armorId = GetKeyByValue(ArmorDict, armorCB.SelectedItem.ToString());
                string name = charNameTB.Text.ToString();
                int strength = Convert.ToInt16(charStrengthTB.Text);
                int dexterity = Convert.ToInt16(charDexterityTB.Text);
                int constitution = Convert.ToInt16(charConstitutionTB.Text);
                int HP = Convert.ToInt16(charHpTB.Text);
                int DC = Convert.ToInt16(charDcTB.Text);
                string addRowToCharactersDB = $"INSERT INTO characters (name, strength, dexterity, constitution, race_id, class_id, HP, defence) Values ('{name}', {strength}, {dexterity}, {constitution}, {raceId},{classId}, {HP}, {DC})";
                Query.InsertData(dbCon, addRowToCharactersDB);
                Query.insertEquippedWeaponChar(dbCon, name, weaponId, armorId);
                Close();
            }

        }
        private bool checkValidaton()
        {
        bool flag = true;    
            if (charNameTB.Text == "")
            {
                showMsgBox("Неверно введено имя персонажа", ref flag);
                return flag;
            }

            if (raceCB.SelectedItem == null)
            {
                showMsgBox("Не выбрана раса", ref flag);
                return flag;
            }
            if (classCB.SelectedItem == null)
            {
                showMsgBox("Не выбран класс", ref flag);
                return flag;
            }
            if (weaponCB.SelectedItem == null)
            {
                showMsgBox("Не выбрано оружие", ref flag);
                return flag;
            }
            if (armorCB.SelectedItem == null)
            {
                showMsgBox("Не выбран доспех", ref flag);
                return flag;
            }
            if (charStrengthTB.Text == "" || int.TryParse(charStrengthTB.Text.ToString(), out int n) == false || n < 0)
            {
                
                showMsgBox("Не введено значение силы, либо введено не целочисленное значение", ref flag);
                return flag;
            }
            if (charConstitutionTB.Text == "" || int.TryParse(charConstitutionTB.Text.ToString(), out int m) == false || m < 0)
            {
                showMsgBox("Не введено значение телосложения, либо введено не целочисленное значение", ref flag);
                return flag;
            }
            if (charDexterityTB.Text == "" || int.TryParse(charDexterityTB.Text.ToString(), out int k) == false || k < 0)
            {
                showMsgBox("Не введено значение Ловкости, либо введено не целочисленное значение", ref flag);
                return flag;
            }
            if (charHpTB.Text == "" || int.TryParse(charHpTB.Text.ToString(), out int l) == false || l < 0)
            {
                showMsgBox("Не введено количество хитов, либо введено не целочисленное значение", ref flag);
                return flag;
            }
            if (charDcTB.Text == "" || int.TryParse(charDcTB.Text.ToString(), out int u) == false || u < 0)
            {
                showMsgBox("Не введён класс доспеха, либо введено не целочисленное значение", ref flag);
                return flag;
            }
            return true;

        }

        //Метод для создания всплывающих подсказок. Пусть аргументом будет текст и фсё
        private void showMsgBox(string message, ref bool flag, string caption="Ошибка при вводе данных")
        {
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult result;
            flag = false;
            result = MessageBox.Show(message, caption, button, icon, MessageBoxResult.Yes);
        }

        static int GetKeyByValue(Dictionary<int, string> dictionary, string value)
        {
            foreach (var pair in dictionary)
            {
                if (pair.Value == value)
                {
                    return pair.Key;
                }
            }

            return 0;
        }

        private void clearButton_Click(object sender, RoutedEventArgs e)
        {
            charNameTB.Clear();
            raceCB.SelectedItem = null;
            classCB.SelectedItem = null;
            armorCB.SelectedItem = null;
            weaponCB.SelectedItem = null;
            charStrengthTB.Clear();
            charDexterityTB.Clear();
            charConstitutionTB.Clear();
            charHpTB.Clear();
            charDcTB.Clear();
        }
    }
    }
