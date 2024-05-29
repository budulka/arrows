using System;
using System.Linq;
using System.Transactions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Threading;
namespace arrows
{
    public partial class CustomGame : Page
    {
        public GameArea area;
        int gridSize;
        Menu menu;
        private int _selected = -1;
        Dictionary<string, int> keyValuePairs = new Dictionary<string, int>() {
            { "Small", 3 },
            { "Medium", 6},
            { "Large", 8}
        };
        public CustomGame(Menu menu)
        {
            InitializeComponent();
            this.menu = menu;
            RowsBox.SelectionChanged += ComboBox_SelectionChanged;

        }
        public void InitializeGameArea()
        {
            area = new GameArea(gridSize)
            {
                Height = 400,
                Width = 400
            };
            area.CreateCustomArea(gridSize);
            for (int i = 0; i < gridSize; i++)
            {
                ArrowCell arrowCell = area.GetArrowCells[0][i];
                Canvas.SetLeft(arrowCell, (i + 1) * DefaultCell.CellSize);
                Canvas.SetTop(arrowCell, 0);
                area.Children.Add(arrowCell);
            }
            for (int i = 0; i < gridSize; i++)
            {
                ArrowCell arrowCell = area.GetArrowCells[1][i];
                Canvas.SetLeft(arrowCell, (gridSize + 1) * DefaultCell.CellSize);
                Canvas.SetTop(arrowCell, (i + 1) * DefaultCell.CellSize);
                area.Children.Add(arrowCell);
            }
            for (int i = 0; i < gridSize; i++)
            {
                ArrowCell arrowCell = area.GetArrowCells[2][i];
                Canvas.SetLeft(arrowCell, gridSize * DefaultCell.CellSize - (i * DefaultCell.CellSize));
                Canvas.SetTop(arrowCell, (gridSize + 1) * DefaultCell.CellSize);
                area.Children.Add(arrowCell);
            }
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
                    CustomCell cell = new CustomCell(row, col, area);
                    area.CustomCells[row][col] = cell;
                    Canvas.SetLeft(cell, (col + 1) * DefaultCell.CellSize);
                    Canvas.SetTop(cell, (row + 1) * DefaultCell.CellSize);
                    area.Children.Add(cell);
                }
            }
            CustomGameField.Content = area;
        }


        private void BackToMenu(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(menu);
        }


        private void CheckIfSolvable(object sender, RoutedEventArgs e)
        {
            
            int curVal;
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    string? text = area.CustomCells[i][j].TextBox.Text;
                    if (string.IsNullOrWhiteSpace(text))
                    {
                        MessageBox.Show($"Cell at ({i}, {j}) is empty or invalid.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    if (int.TryParse(text, out curVal))
                    {
                        area.NumberCells[i][j] = new NumberCell(curVal, i, j, area);
                        area._currentField[i,j] = curVal;
                    }
                    else
                    {
                        return;
                    }
                }
            }
            if (IsGoalState())
            {
                MessageBox.Show($"It seems like this does not have a solution", "Failure", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if(Solve(0, 0, MatrixClass.VectorDirection.NoDirection, area._currentField))
            {
                MessageBox.Show($"Solution  found", "Success", MessageBoxButton.OK, MessageBoxImage.Warning);
                foreach(var ar in area.GetArrowCells)
                {
                    foreach(var cell in ar)
                    {
                        cell.Direction = cell.GetDirectionAsANumber(cell.original);
                        cell.textBlock.Text = cell.GetArrowDirection();
                    }
                }
            }
            else
            {
                MessageBox.Show($"It seems like this does not have a solution", "Failure", MessageBoxButton.OK, MessageBoxImage.Warning);
            };
        }
        public bool Solve(int from, int pos, MatrixClass.VectorDirection previous, MatrixClass original)
        {
            if (from == 3 && pos == gridSize)
            {
                return true;
            }
            if (pos == gridSize)
            {
                pos = 0;
                from++;
                if (from == 4)
                {
                    return false;
                }
            }
            if (area.GetArrowCells[from][pos].Direction == 0)
            {
                original = (MatrixClass)area._currentField.Clone();
            }
           
            previous = MatrixClass.VectorDirection.NoDirection;
            if (IsGoalState())
            {
                return true;
            }
            List<MatrixClass.VectorDirection> poss = area.GetArrowCells[from][pos].PossibleDirection();
            foreach (MatrixClass.VectorDirection direction in poss)
            {
                
                area._currentField = area._currentField;
                if (IsSafe(from, direction, pos, previous))
                {
                    if (previous != MatrixClass.VectorDirection.NoDirection)
                    {
                        area.UpdateCurrentField(from, direction, pos, previous);
                        
                    }
                    else
                    {
                        area.SubractArrow(from, direction, pos);
;                    }
                    if (Solve(from, pos + 1, direction, original))
                    {
                        
                        if (IsGoalState())
                        {
                            area.GetArrowCells[from][pos].original = direction;
                            return true;
                        }
                    }
                    previous = direction;
                }                
            }
            area._currentField = original;
            return false;
        }


        private bool IsSafe(int from, MatrixClass.VectorDirection vectorDirection, int pos, MatrixClass.VectorDirection previous)
        {
            area.UpdateCurrentField(from, vectorDirection, pos, previous);
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    if (area._currentField[i, j] < 0)
                    {
                        area.UpdateCurrentField(from, previous, pos, vectorDirection);
                        return false;
                    }
                }
            }
            area.UpdateCurrentField(from, previous,pos, vectorDirection);
            return true;
        }

        private bool IsGoalState()
        {

            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    if (area._currentField[i, j] != 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem item = (ComboBoxItem)RowsBox.SelectedItem;
            if (item != null)
            {
                _selected = keyValuePairs[item.Content.ToString()];
            }
            gridSize = _selected;
        }

        private void GenerateCustom(object sender, RoutedEventArgs e)
        {
            if (_selected == -1)
            {
                return;
            }
            Button but = (Button)sender;
            but.Content = "Restart";
            InitializeGameArea();
        }
    }

    internal class CustomCell : DefaultCell
    {
        public TextBox TextBox { get; private set; }
        public int X { get; }
        public int Y { get; }
        GameArea Area;

        public CustomCell(int x, int y, GameArea area) : base()
        {
            this.Area = area;
            this.X = x;
            this.Y = y;

            TextBox = new TextBox
            {
                FontSize = 20,
                Foreground = Brushes.Black,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Background = Brushes.Transparent,
                BorderBrush = Brushes.Transparent,
                Width = Width,
                TextAlignment = TextAlignment.Center,
            };

            TextBox.PreviewTextInput += TextBox_PreviewTextInput;
            TextBox.TextChanged += TextBox_TextChanged;

            Grid grid = new Grid();
            grid.Children.Add(TextBox);
            Content = grid;
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!IsTextAllowed(e.Text) || TextBox.Text.Length >= 1)
            {
                e.Handled = true;
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (!string.IsNullOrEmpty(textBox.Text))
            {
                string newText = new string(textBox.Text.Where(ch => IsTextAllowed(ch.ToString())).ToArray());
                if (newText.Length > 1)
                {
                    newText = newText.Substring(0, 1);
                }
                if (textBox.Text != newText)
                {
                    textBox.Text = newText;
                    textBox.CaretIndex = newText.Length;
                }
            }
        }

        private bool IsTextAllowed(string text)
        {
            return text.Length == 1 && int.TryParse(text, out int result) && result >= 0 && result <= 8;
        }
    }
}
