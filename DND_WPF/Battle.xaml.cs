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
using System.Windows.Shapes;

namespace DND_WPF
{
    /// <summary>
    /// Interaction logic for Battle.xaml
    /// </summary>
    public partial class Battle : Window
    {
        public Battle()
        {
            InitializeComponent();
            Dictionary<string, int> CharCharacteristics = Query.GetAverageValueChar(MainWindow.dbCon, MainWindow.selectedCharNames.ToArray());
            Dictionary<string, int> EnemyCharacteristics = Query.GetAverageValueChar(MainWindow.dbCon, MainWindow.selectedEnemyNames.ToArray(), "enemy", "enemy");
            //int a = characteristics["damage"];
            damageTB.Text = CharCharacteristics["damage"].ToString();
            defenceTB.Text = CharCharacteristics["defence"].ToString();
            HpTB.Text = CharCharacteristics["HP"].ToString();
            modTB.Text = CharCharacteristics["mod"].ToString();
            int winCounter = 0;
            int counter = 0;
            for (int i = 0; i < 500; i ++)
            {
                if (GameLogic.BattleSteps(CharCharacteristics, EnemyCharacteristics) == true)
                {
                    winCounter++;
                }
                counter++;
            }
            double variety =  (double)winCounter / (double)counter;
            BattlesCountTB.Text = counter.ToString();
            WinCountTB.Text = winCounter.ToString();
            WinRateTB.Text = (Math.Round(variety, 4) * 100).ToString() + "%";
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
