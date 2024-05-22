
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows;

namespace arrows
{
    internal class NumberCell : DefaultCell
    {

        public int value;
        public TextBlock text;
        public int x { get; }
        public int y { get; }
        GameArea area;

        public NumberCell(int v, int x, int y, GameArea area) : base()
        {
            this.area = area;
            this.x = x;
            this.y = y;
            value = v;
            TextBlock textBlock = new TextBlock
            {
                Text = v.ToString(), 
                FontSize = 20, 
                Foreground = Brushes.Black, 
                HorizontalAlignment = HorizontalAlignment.Center, 
                VerticalAlignment = VerticalAlignment.Center 
            };
            text = textBlock;
            Grid grid = new Grid();
            grid.Children.Add(textBlock);
            Content = grid;
            MouseDown += On_Click;
        }

        public void On_Click(object sender, RoutedEventArgs e)
        {

        }
        
    }
}
