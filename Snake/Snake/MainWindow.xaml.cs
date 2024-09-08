using Snake.Models;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Snake
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Arena Arena;

        public MainWindow()
        {
            InitializeComponent();

            // This hivatkozik arra az osztálypéldányra amiben éppen vagyok, vagyis azt az ablakot küldöm be az Arena példányba
            Arena = new Arena(this);
        }

        private void ButtonStart_Click(object sender, RoutedEventArgs e)
        {
            Arena.Start();
        }

        private void ButtonStop_Click(object sender, RoutedEventArgs e)
        {
            Arena.Stop();
        }

        /// <summary>
        /// Az ablakban leütött billentyű figyelése és szűrőse és továbbítása az arenanak
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left:
                case Key.Up:
                case Key.Right:
                case Key.Down:
                    // Ha Left vagy Right vagy Up vagy Down akkor ez lefut
                    Arena.Keydown(e.Key);

                    // Ezeket a gombokat nem dolgozza fel6
                    e.Handled = true;
                    break;
                default:
                    break;
            }   
        }
    }
}