

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;



namespace arrows
{

    public partial class MainWindow : Window
    {
        GameArea area;
        Dictionary<string, int> keyValuePairs = new Dictionary<string, int>() {
            { "Small", 3 },
            { "Medium", 6},
            { "Large", 8}
        };
        public MainWindow()
        {
            InitializeComponent();
            RowsBox.SelectionChanged += ComboBox_SelectionChanged;
            { }
           
        }

        private void InitializeGameArea(int gridSize)
        {
            area = new GameArea(gridSize)
            {
                Height = 400,
                Width = 400
            };
            //top row
            for (int i = 0; i < gridSize; i++) {
                ArrowCell arrowCell = area.GetArrowCells[0][i];
                Canvas.SetLeft(arrowCell, (i + 1) * DefaultCell.CellSize);
                Canvas.SetTop(arrowCell, 0);
                area.Children.Add(arrowCell);
            }
            //right column
            for (int i = 0; i < gridSize; i++)
            {
                ArrowCell arrowCell = area.GetArrowCells[1][i];
                Canvas.SetLeft(arrowCell, (gridSize+1) * DefaultCell.CellSize);
                Canvas.SetTop(arrowCell, (i+1) * DefaultCell.CellSize);
                area.Children.Add(arrowCell);
            }
            //bottom row
            for (int i = 0; i < gridSize; i++)
            {
                ArrowCell arrowCell = area.GetArrowCells[2][i];
                Canvas.SetLeft(arrowCell, gridSize * DefaultCell.CellSize - (i * DefaultCell.CellSize));
                Canvas.SetTop(arrowCell, (gridSize + 1) * DefaultCell.CellSize);
                area.Children.Add(arrowCell);
            }
            //left row
            for (int i = 0; i < gridSize; i++)
            {
                ArrowCell arrowCell = area.GetArrowCells[3][i];
                Canvas.SetLeft(arrowCell, 0);
                Canvas.SetTop(arrowCell, (gridSize - i) * DefaultCell.CellSize);
                area.Children.Add(arrowCell);
            }

            for (int row = 0; row < gridSize; row++)
            {   
                for (int col = 0; col < gridSize; col++)
                {
                    NumberCell cell = new NumberCell(area.FieldMatrix[row, col], row, col, area);
                    if (area.FieldMatrix[row, col] == 0)
                    {
                        cell.text.Foreground = Brushes.Green;
                        cell.text.FontWeight = FontWeights.Bold;
                    }
                    Canvas.SetLeft(cell, (col+1) * DefaultCell.CellSize);
                    Canvas.SetTop(cell, (row+1) * DefaultCell.CellSize);
                    area.NumberCells[row][col] = cell;
                    area.Children.Add(cell);
                }
            }
            GameAreaContainer.Content = area;
           
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

        private void GetHint(object sender, RoutedEventArgs e)
        {
            Hint hint = new Hint(area, area.Size);
            hint.GetHint();
        }

        private void RevealGame(object sender, RoutedEventArgs e)
        {
            Hint hint = new Hint(area, area.Size); 
        }
    }
}
