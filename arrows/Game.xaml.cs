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

namespace arrows
{
    
    public partial class Game : Page
    {
        GameArea area;
        Menu menu;
        private int _selected = -1;
        Dictionary<string, int> keyValuePairs = new Dictionary<string, int>() {
            { "Small", 3 },
            { "Medium", 6},
            { "Large", 8}
        };
        private DispatcherTimer _timer;
        private TimeSpan _timeElapsed;
        public Game(Menu menu)
        {
            InitializeComponent();
            this.menu = menu;
            RowsBox.SelectionChanged += ComboBox_SelectionChanged;
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += Timer_Tick;
        }

        private void InitializeGameArea(int gridSize)
        {
            area = new GameArea(gridSize)
            {
                Height = 400,
                Width = 400
            };
            area.CreateGameArea();
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
                    NumberCell cell = new NumberCell(area.FieldMatrix[row, col], row, col, area);
                    if (area.FieldMatrix[row, col] == 0)
                    {
                        cell.text.Foreground = Brushes.Green;
                        cell.text.FontWeight = FontWeights.Bold;
                    }
                    Canvas.SetLeft(cell, (col + 1) * DefaultCell.CellSize);
                    Canvas.SetTop(cell, (row + 1) * DefaultCell.CellSize);
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
                _selected = keyValuePairs[item.Content.ToString()];
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
            hint.Reveal();
        }

        private void Generate(object sender, RoutedEventArgs e)
        {
            if (_selected == -1)
            {
                return;
            }
            Button but = (Button)sender;
            but.Content = "Restart";
            InitializeGameArea(_selected);
            Hint.Visibility = Visibility.Visible;
            GiveUpButton.Visibility = Visibility.Visible;
            _timeElapsed = TimeSpan.Zero;
            _timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            _timeElapsed = _timeElapsed.Add(TimeSpan.FromSeconds(1));
            TimerTextBlock.Text = _timeElapsed.ToString(@"mm\:ss");
        }

        private void GoToMenu(object sender, RoutedEventArgs e)
        {
            _timer.Stop();
            NavigationService.Navigate(menu);
        }
    }
}

