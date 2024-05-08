

using System.Windows;
using System.Windows.Controls;


namespace arrows
{

    public partial class MainWindow : Window
    {
        Dictionary<string, int> keyValuePairs = new Dictionary<string, int>() {
            { "Small", 3 },
            { "Medium", 5},
            { "Large", 7}
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
            for (int col = 0; col < gridSize; col++) {
                ArrowCell arrow = new ArrowCell();
                Canvas.SetLeft(arrow, DefaultCell.CellSize +col * DefaultCell.CellSize);
                Canvas.SetTop(arrow, 0);
                gameArea.Children.Add(arrow);
            }
            for (int row = 0; row < gridSize; row++)
            {
                ArrowCell arrow = new ArrowCell(   );
                Canvas.SetLeft(arrow, 0);
                Canvas.SetTop(arrow, 50 + row * DefaultCell.CellSize);
                gameArea.Children.Add(arrow);
                for (int col = 0; col < gridSize; col++)
                {
                    
                    NumberCell cell = new NumberCell(row * gridSize + col + 1);
                    Canvas.SetLeft(cell, DefaultCell.CellSize + col * DefaultCell.CellSize);
                    Canvas.SetTop(cell, DefaultCell.CellSize + row * DefaultCell.CellSize);
                    gameArea.Children.Add(cell);
                }
                ArrowCell arrow2 = new ArrowCell();
                Canvas.SetLeft(arrow2, DefaultCell.CellSize * (gridSize+ 1)) ;
                Canvas.SetTop(arrow2, DefaultCell.CellSize + row * DefaultCell.CellSize);
                gameArea.Children.Add(arrow2);
            }
            for (int col = 0; col < gridSize; col++)
            {
                ArrowCell arrow = new ArrowCell();
                Canvas.SetLeft(arrow, DefaultCell.CellSize + col * DefaultCell.CellSize);
                Canvas.SetTop(arrow, (gridSize + 1) * DefaultCell.CellSize);
                gameArea.Children.Add(arrow);
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
