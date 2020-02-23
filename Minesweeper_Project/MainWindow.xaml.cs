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
using System.Windows.Threading;

namespace Minesweeper_Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Start_Game_Click(object sender, RoutedEventArgs e)
        {
            Board bd = new Board(Difficulty.SelectedIndex);
            bd.SeedBoard();
            bd.SetLiveNeighbors();
            bd.SetButtons(grid);

            DispatcherTimer timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        int increment = 0;

        private void Timer_Tick(object sender, EventArgs e)
        {
            increment++;
            Timer.Content = increment.ToString("00:00");
        }
    }
}
