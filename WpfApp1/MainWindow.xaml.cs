using System;
using System.Collections.Generic;
using System.Drawing;
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
using System.Windows.Threading;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int increment = 0;
        public MainWindow()
        {
            InitializeComponent();
            DispatcherTimer dt = new DispatcherTimer();
            dt.Interval = TimeSpan.FromSeconds(0.0166666);
            dt.Tick += onUpdate;
            dt.Start();
        }
        public int iterator = 0;
        private void onUpdate(object sender, EventArgs e) //Аналог метода onUpdate Unity. Здесь будут происходить все операции, требующие постоянного отображения
        {
            iterator++;
            BlueText.Text = Convert.ToString(iterator);
            if (iterator % 2 == 0)
            {
                RedText.Background = new SolidColorBrush(Colors.Red);
            }
            else
                RedText.Background = new SolidColorBrush(Colors.Black);
        }

    }
}
