using System.CodeDom;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;

namespace arrows
{
    internal class ArrowCell : DefaultCell
    {
        private int direction;
        private GameArea area;
        public LogicType log;
        public int Direction => direction;
        public readonly Point Pos;
        private MatrixClass.VectorDirection VectorDirection = new MatrixClass.VectorDirection();
        public enum LogicType
        {
            Left, Center, Right
        }
        private static readonly Dictionary<LogicType, Dictionary<int, string>> ArrowMappingsDictionary = new()
        {
            { LogicType.Left, new Dictionary<int, string> { { 0, "?" }, { 1, "🡦" }, { 2, " 🡣" } } },
            { LogicType.Center, new Dictionary<int, string> { { 0, "?" }, { 1, "🡧" }, { 2, "🡣" }, { 3, "🡦" } } },
            { LogicType.Right, new Dictionary<int, string> { { 0, "?" }, { 1, " 🡣" }, { 2, "🡧" } } }, 
        };



        private TextBlock textBlock;
        private Dictionary<int, string> ArrowMappings;



        public ArrowCell(LogicType logic, int x, int y, GameArea area) : base()
        {

            direction = 0;
            Pos.X = x;
            Pos.Y = y;
            ArrowMappings = ArrowMappingsDictionary[logic];
            this.area = area;
            textBlock = new TextBlock
            {
                Text = GetArrowDirection(),
                FontSize = 20,
                Foreground = Brushes.Black,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                LayoutTransform = new RotateTransform(x * 90)

            };

            Content = textBlock;
            MouseDown += Cell_MouseDown;
        }

        private void Cell_MouseDown(object sender, MouseEventArgs e)
        {
            direction++;
            if (direction >= ArrowMappings.Count)
            {
                direction = 0;
            }
            textBlock.Text = GetArrowDirection();
            if (direction != 0)
            {
                area.UpdateCurrentField((int)Pos.X, GetActualDireaction(), (int)Pos.Y);
            }

        }

        private MatrixClass.VectorDirection GetActualDireaction()
        {
            switch (log) { 
            
            }
            
        }
        string GetArrowDirection() => ArrowMappings[direction];
    }

    
}



