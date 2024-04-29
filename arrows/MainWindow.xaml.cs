
using System.Windows;

namespace arrows
{
    
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitializeGameArea();
        }

        private void InitializeGameArea()
        {
            GameArea gameArea = new GameArea();
            gameArea.Height = 400;
            gameArea.Width = 400;
            GameAreaContainer.Content = gameArea;
        }


    }
}