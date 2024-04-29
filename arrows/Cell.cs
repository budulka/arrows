using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows;

namespace arrows
{
    internal class Cell : UserControl
    {
        private const int CellSize = 50;
        public int value;
        
        public Cell(int v)
        {
            Rectangle rect = new Rectangle
            {
                Width = CellSize,
                Height = CellSize,
                Fill = Brushes.White,
                Stroke = Brushes.Black,
                StrokeThickness = 1
            };
            value = v;
            TextBlock textBlock = new TextBlock
            {
                Text = v.ToString(), 
                FontSize = 20, 
                Foreground = Brushes.Black, 
                HorizontalAlignment = HorizontalAlignment.Center, 
                VerticalAlignment = VerticalAlignment.Center 
            };
            Content = new Grid();
            ((Grid)Content).Children.Add(rect);
            ((Grid)Content).Children.Add(textBlock);
        }
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            drawingContext.DrawRectangle(Brushes.White, new Pen(Brushes.Black, 1), new Rect(0, 0, CellSize, CellSize));
        }
    }
}
