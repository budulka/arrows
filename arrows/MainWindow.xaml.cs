﻿

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
            { }
           
        }

        private void InitializeGameArea(int gridSize)
        {
            GameArea gameArea = new GameArea(gridSize)
            {
                Height = 400,
                Width = 400
            };
            
            for (int i = 0; i < gridSize; i++) {
                ArrowCell arrowCell = gameArea.GetArrowCells[0][i];
                Canvas.SetLeft(arrowCell, (i + 1) * DefaultCell.CellSize);
                Canvas.SetTop(arrowCell, 0);
                gameArea.Children.Add(arrowCell);
            }
            for (int i = 0; i < gridSize; i++)
            {
                ArrowCell arrowCell = gameArea.GetArrowCells[1][i];
                Canvas.SetLeft(arrowCell, (gridSize+1) * DefaultCell.CellSize);
                Canvas.SetTop(arrowCell, (i+1) * DefaultCell.CellSize);
                gameArea.Children.Add(arrowCell);
            }
            for (int i = 0; i < gridSize; i++)
            {
                ArrowCell arrowCell = gameArea.GetArrowCells[2][i];
                Canvas.SetLeft(arrowCell, (i + 1) * DefaultCell.CellSize);
                Canvas.SetTop(arrowCell, (gridSize + 1) * DefaultCell.CellSize);
                gameArea.Children.Add(arrowCell);
            }
            for (int i = 0; i < gridSize; i++)
            {
                ArrowCell arrowCell = gameArea.GetArrowCells[3][i];
                Canvas.SetLeft(arrowCell, 0);
                Canvas.SetTop(arrowCell, (i + 1) * DefaultCell.CellSize);
                gameArea.Children.Add(arrowCell);
            }

            for (int row = 0; row < gridSize; row++)
            {   
                for (int col = 0; col < gridSize; col++)
                {
                    NumberCell cell = new NumberCell(gameArea.FieldMatrix[row, col]);
                    Canvas.SetLeft(cell, (col+1) * DefaultCell.CellSize);
                    Canvas.SetTop(cell, (row+1) * DefaultCell.CellSize);
                    gameArea.NumberCells[row][col] = cell;
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
