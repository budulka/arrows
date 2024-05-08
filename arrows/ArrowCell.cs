using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace arrows
{
    internal class ArrowCell : DefaultCell
    {
        public int direction;
        
        private static readonly Dictionary<int, string> ArrowMappings = new Dictionary<int, string>
        {
            { 0, "?" },
            { 1, "🡠" },
            {2, "🡤" },
            {3 , "🡡" },
            {4 , "🡥" },
            {5 , "🡢" },
            {6, " 🡦" },
            {7, " 🡣" },
            {8, "🡧" }
        };

        private TextBlock textBlock;



        public ArrowCell() : base()
        {

            //direction = v;
            textBlock = new TextBlock
            {
                Text = getArrowDirection(),
                FontSize = 20,
                Foreground = Brushes.Black,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center

            };

            Content = textBlock;
            MouseDown += Cell_MouseDown;
        }

        private void Cell_MouseDown(object sender, MouseEventArgs e)
        {
            direction++;
            if (direction > 8) {
                direction = 0;
            }
            textBlock.Text = getArrowDirection();

        }

        string getArrowDirection()
        {
            switch (direction)
            {
                case 1:
                    return ArrowMappings[1];
                case 2:
                    return ArrowMappings[2];
                case 3:
                    return ArrowMappings[3];
                case 4:
                    return ArrowMappings[4];
                case 5:
                    return ArrowMappings[5];
                case 6:
                    return ArrowMappings[6];
                case 7: 
                    return ArrowMappings[7];
                default:
                    return ArrowMappings[0];
            }
        }
       
    }
}
