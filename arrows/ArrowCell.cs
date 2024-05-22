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
        private MatrixClass.VectorDirection previous = MatrixClass.VectorDirection.NoDirection;
        public enum LogicType
        {
            Left, Center, Right
        }
        private static readonly Dictionary<LogicType, Dictionary<int, string>> ArrowMappingsDictionary = new()
        {
            { LogicType.Left, new Dictionary<int, string> { { 0, "?" }, { 1, "🡦" }, { 2, " 🡣" } } },
            { LogicType.Center, new Dictionary<int, string> { { 0, "?" }, { 1, "🡧" }, { 2, "🡣" }, { 3, "🡦" } } },
            { LogicType.Right, new Dictionary<int, string> { { 0, "?" }, { 1, "🡧" }, { 2, "🡣" } } },
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
            log = logic;
            Content = textBlock;
            MouseDown += Cell_MouseDown;
        }

        private void Cell_MouseDown(object sender, MouseEventArgs e)
        {
            if(area.IsWon)
            {
                return;
            }
            direction++;
            if (direction >= ArrowMappings.Count)
            {
                direction = 0;
            }
            textBlock.Text = GetArrowDirection();
            MatrixClass.VectorDirection cur = GetActualDireaction();
            area.UpdateCurrentField((int)Pos.X, cur, (int)Pos.Y, previous);
            previous = cur;
            
            if(area.CheckWin())
            {
                WinDialog winDialog = new();
                winDialog.ShowDialog();
            }
           

        }

        private MatrixClass.VectorDirection GetActualDireaction()
        {
            if (direction == 0)
            {
                return MatrixClass.VectorDirection.NoDirection;
            }
            switch (log) { 
                case LogicType.Center:
                    if (direction == 1) {
                        return MatrixClass.VectorDirection.DiagonalLeft;
                    }
                    if (direction == 2) {
                        return MatrixClass.VectorDirection.Vertical;
                    }
                    return MatrixClass.VectorDirection.DiagonalRight;
                case LogicType.Right:
                    if (direction == 1) {
                        return MatrixClass.VectorDirection.DiagonalLeft;
                    }
                    return MatrixClass.VectorDirection.Vertical;
                case LogicType.Left:
                    if (direction == 1) {
                        return MatrixClass.VectorDirection.DiagonalRight;
                    }
                    return MatrixClass.VectorDirection.Vertical;
                default:
                    return MatrixClass.VectorDirection.NoDirection;
            }
            
        }
        string GetArrowDirection() => ArrowMappings[direction];
    }

    
}



