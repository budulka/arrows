
using System.Windows;
using System.Windows.Controls;


namespace arrows
{
    public class GameArea : Canvas
    {
        protected const int CellSize = 50;
        private int GridRows;
        private int GridColumns;


        private readonly int[][] FieldMatrix;
        public GameArea()
        {
            Loaded += GameArea_Loaded;
        }

        private void GameArea_Loaded(object sender, RoutedEventArgs e)
        {
            DrawGameArea();
        }

        private void DrawGameArea()
        {


            for (int row = 0; row < GridRows; row++)
            {
                for (int col = 0; col < GridColumns; col++)
                {
                    Random random = new Random();
                    Cell cell = new Cell(random.Next(1, 8));
                    Children.Add(cell);
                    SetLeft(cell, col * CellSize);
                    SetTop(cell, row * CellSize);
                }
            }
        }


    }
}
