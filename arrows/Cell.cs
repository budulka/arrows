
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows;

namespace arrows
{
    internal class NumberCell : DefaultCell
    {
        
        public int value;
      

        public NumberCell(int v) : base()
        {
     
            value = v;
            TextBlock textBlock = new TextBlock
            {
                Text = v.ToString(), 
                FontSize = 20, 
                Foreground = Brushes.Black, 
                HorizontalAlignment = HorizontalAlignment.Center, 
                VerticalAlignment = VerticalAlignment.Center 
            };
            Grid grid = new Grid();
            grid.Children.Add(textBlock);
            Content = grid;

        }
        
    }
}
