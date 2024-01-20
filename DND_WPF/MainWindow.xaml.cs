using DnD;
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
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace DND_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static List<string> selectedCharNames;
        public static List<string> selectedEnemyNames;
        public static DBConnection dbCon;
        public static Add_Character AddCharacter;
        public static Add_Enemy AddEnemy;
        public static Battle battle;
        public MainWindow()
        {
            selectedCharNames = new List<string>();
            selectedEnemyNames = new List<string>();
            dbCon = DBConnection.Instance();
            InitializeComponent();
            loadItems();
            //Для добавления компонентов в ListBox нужно использовать структуру данных List<String>
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        public void loadItems()
        {
            List<TextBlock> list = Query.GetCharDescription(dbCon);
            list.ForEach(r => CharsLb.Items.Add(r));
            list = Query.GetEnemyDescription(dbCon);
            list.ForEach(r => EnemiesLb.Items.Add(r));


        }

        private void AddNewCharacter_Click(object sender, RoutedEventArgs e)
        {
            if (AddCharacter == null || !AddCharacter.IsLoaded)
            {
                AddCharacter = new Add_Character();
                AddCharacter.Closed += (sender, args) => AddCharacter = null;
                AddCharacter.Show();
            }

        }
        private void AddNewEnemy_Click(object sender, RoutedEventArgs e)
        {
            if (AddEnemy == null || !AddEnemy.IsLoaded)
            {
                AddEnemy = new Add_Enemy();
                AddEnemy.Closed += (sender, args) => AddEnemy = null;
                AddEnemy.Show();
            }
        }

        public void updateLists_Click(object sender, RoutedEventArgs e)
        {
            CharsLb.Items.Clear();
            EnemiesLb.Items.Clear();
            loadItems();
        }

        private void addSelectedCharButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (var selectedItem in CharsLb.SelectedItems.Cast<TextBlock>().ToList())
            {
                CharsLb.Items.Remove(selectedItem);

                // Создаем новый TextBlock и копируем свойства
                TextBlock copiedTextBlock = new TextBlock();
                copiedTextBlock.Text = selectedItem.Text;

                // Копируем ToolTip, если он установлен
                if (selectedItem.ToolTip != null)
                {
                    ToolTip newToolTip = new ToolTip();

                    // Копируем свойства ToolTip
                    if (selectedItem.ToolTip is ToolTip originalToolTip)
                    {
                        newToolTip.Content = originalToolTip.Content;
                        // Копируйте другие свойства, если они вам нужны
                    }

                    copiedTextBlock.ToolTip = newToolTip;
                }

                // Добавляем копию в SelectedCharsScrBar
                SelectedCharsScrBar.Children.Add(copiedTextBlock);
            }
        }

        private void addSelectedEnemyButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (var selectedItem in EnemiesLb.SelectedItems.Cast<TextBlock>().ToList())
            {
                EnemiesLb.Items.Remove(selectedItem);

                // Создаем новый TextBlock и копируем свойства
                TextBlock copiedTextBlock = new TextBlock();
                copiedTextBlock.Text = selectedItem.Text;

                // Копируем ToolTip, если он установлен
                if (selectedItem.ToolTip != null)
                {
                    ToolTip newToolTip = new ToolTip();

                    // Копируем свойства ToolTip
                    if (selectedItem.ToolTip is ToolTip originalToolTip)
                    {
                        newToolTip.Content = originalToolTip.Content;
                        // Копируйте другие свойства, если они вам нужны
                    }

                    copiedTextBlock.ToolTip = newToolTip;
                }

                // Добавляем копию в SelectedCharsScrBar
                SelectedEnemiesScrBar.Children.Add(copiedTextBlock);
            }
        }


        public void deleteSelectedCharsButton_Click(object sender, RoutedEventArgs e)
        {
            int? id;
            foreach (var selectedItem in CharsLb.SelectedItems.Cast<TextBlock>().ToList())
            {
                id = Query.getId(dbCon, selectedItem.Text.ToString());
                Query.deleteRow(dbCon, id);
            }
            CharsLb.Items.Clear();
            EnemiesLb.Items.Clear();
            loadItems();
        }

        private void deleteSelectedEnemyButton_Click(object sender, RoutedEventArgs e)
        {
            int? id;
            foreach (var selectedItem in EnemiesLb.SelectedItems.Cast<TextBlock>().ToList())
            {
                id = Query.getId(dbCon, selectedItem.Text.ToString(), true);
                Query.deleteRow(dbCon, id, true);
            }
            CharsLb.Items.Clear();
            EnemiesLb.Items.Clear();
            loadItems();
        }

        private void startBattleButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedEnemiesScrBar.Children.Count != 0 && SelectedCharsScrBar.Children.Count != 0)
            {
                foreach (TextBlock selectedItem in SelectedCharsScrBar.Children)
                {
                    if (selectedItem != null)
                    {
                        selectedCharNames.Add(selectedItem.Text.ToString());
                    }
                }
                foreach (TextBlock selectedItem in SelectedEnemiesScrBar.Children)
                {
                    if (selectedItem != null)
                    {
                        selectedEnemyNames.Add(selectedItem.Text.ToString());
                    }
                }
                if (battle == null || !battle.IsLoaded)
                {
                    battle = new Battle();
                    battle.Closed += (sender, args) => battle = null;
                    battle.Show();
                }
            }
            else showMsgBox("Вы не выбрали персонажей и противников");
        }

        private void clearLists_Click(object sender, RoutedEventArgs e)
        {
            SelectedCharsScrBar.Children.Clear();
            SelectedEnemiesScrBar.Children.Clear();
        }
        private void showMsgBox(string message, string caption = "Ошибка при вводе данных")
        {
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult result;
            result = MessageBox.Show(message, caption, button, icon, MessageBoxResult.Yes);
        }
    }

}
