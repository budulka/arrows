
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace arrows
{

    public partial class MainWindow : Window
    {
        Dictionary<string, int> keyValuePairs = new Dictionary<string, int>() {
            { "Small", 3 },
            { "Medium", 5},
            { "Large", 9}
        };
        public MainWindow()
        {
            InitializeComponent();
            RowsBox.SelectionChanged += ComboBox_SelectionChanged;
            

        }

        private void InitializeGameArea(int gridSize)
        {
            GameArea gameArea = new GameArea();
            gameArea.Height = 400;
            gameArea.Width = 400;
            for (int row = 0; row < gridSize; row++)
            {
                for (int col = 0; col < gridSize; col++)
                {
                    NumberCell cell = new NumberCell(row * gridSize + col + 1);
                    Canvas.SetLeft(cell, col * NumberCell.CellSize);
                    Canvas.SetTop(cell, row * NumberCell.CellSize);
                    gameArea.Children.Add(cell);
                }
            }
            GameAreaContainer.Content = gameArea;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


            ComboBoxItem item = (ComboBoxItem)RowsBox.SelectedItem;
            if (item != null)
            {
                int sel = keyValuePairs[item.Content.ToString()];
                InitializeGameArea(sel);

            }
        }
     
    }
}
