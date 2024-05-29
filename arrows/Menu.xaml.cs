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

namespace arrows
{
    public partial class Menu : Page
    {
        Game game;
        CustomGame custom;

        public Menu()
        {
            InitializeComponent();
            game = new Game(this);
            custom = new CustomGame(this);
        }

        private void GoToGamePage(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(game);
        }

        private void GoToCustomGamePage(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(custom);
        }

        private void Quit(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Close();
        }
    }
}
