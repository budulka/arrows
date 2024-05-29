using System.Runtime.ConstrainedExecution;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;


namespace arrows
{
    internal class ArrowCell : DefaultCell
    {
        private int direction;
        private GameArea area;
        public LogicType log;
        public int Direction { get {return direction; } set { direction = value; }}
        public readonly Point Pos;
        private MatrixClass.VectorDirection previous = MatrixClass.VectorDirection.NoDirection;
        public MatrixClass.VectorDirection original = MatrixClass.VectorDirection.NoDirection;
        public bool IsDisabled;
        public bool IsCorrect;
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
        public TextBlock textBlock;
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
            if (area.IsWon || IsDisabled)
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
            IsCorrect = false;
            if (original == cur)
            {
                IsCorrect = true;
            }
            area.UpdateCurrentField((int)Pos.X, cur, (int)Pos.Y, previous);
            area.Paint();
            previous = cur;
            if (area.CheckWin())
            {
                WinDialog winDialog = new();
                winDialog.ShowDialog();
            }
        }

        public MatrixClass.VectorDirection GetActualDireaction()
        {
            if (direction == 0)
            {
                return MatrixClass.VectorDirection.NoDirection;
            }
            switch (log)
            {
                case LogicType.Center:
                    if (direction == 1)
                    {
                        return MatrixClass.VectorDirection.DiagonalLeft;
                    }
                    if (direction == 2)
                    {
                        return MatrixClass.VectorDirection.Vertical;
                    }
                    return MatrixClass.VectorDirection.DiagonalRight;
                case LogicType.Right:
                    if (direction == 1)
                    {
                        return MatrixClass.VectorDirection.DiagonalLeft;
                    }
                    return MatrixClass.VectorDirection.Vertical;
                case LogicType.Left:
                    if (direction == 1)
                    {
                        return MatrixClass.VectorDirection.DiagonalRight;
                    }
                    return MatrixClass.VectorDirection.Vertical;
                default:
                    return MatrixClass.VectorDirection.NoDirection;
            }
        }

        public List<MatrixClass.VectorDirection> PossibleDirection()
        {
            List<MatrixClass.VectorDirection> possible = new List<MatrixClass.VectorDirection>();
            switch (log)
            {
                case LogicType.Center:
                    possible.Add(MatrixClass.VectorDirection.Vertical);
                    possible.Add(MatrixClass.VectorDirection.DiagonalLeft);
                    possible.Add(MatrixClass.VectorDirection.DiagonalRight);
                    break;
                case LogicType.Right:
                    possible.Add(MatrixClass.VectorDirection.Vertical);
                    possible.Add(MatrixClass.VectorDirection.DiagonalLeft);
                    break;
                case LogicType.Left:
                    possible.Add(MatrixClass.VectorDirection.Vertical);
                    possible.Add(MatrixClass.VectorDirection.DiagonalRight);
                    break;
            }
            return possible;

        }
        public void NextMove()
        {
            direction++;
            if (direction >= ArrowMappings.Count)
            {
                direction = 0;
            }
            textBlock.Text = GetArrowDirection();
        }
        
        public void MarkAsHinted()
        {
            IsDisabled = true;
            textBlock.Foreground = Brushes.Blue;
            area.UpdateCurrentField((int)Pos.X, original, (int)Pos.Y, previous);
            area.Paint();
            direction = GetDirectionAsANumber(original);
            textBlock.Text = GetArrowDirection();
            if (area.CheckWin())
            {
                WinDialog winDialog = new();
                winDialog.ShowDialog();
            }
        }

        public int GetDirectionAsANumber(MatrixClass.VectorDirection dir)
        {
            switch (log)
            {
                case LogicType.Center:
                    if (dir == MatrixClass.VectorDirection.DiagonalLeft)
                    {
                        return 1;
                    }
                    if (dir == MatrixClass.VectorDirection.Vertical)
                    {
                        return 2;
                    }
                    return 3;
                case LogicType.Right:
                    if (dir == MatrixClass.VectorDirection.DiagonalLeft)
                    {
                        return 1;
                    }
                    return 2;
                case LogicType.Left:
                    if (dir == MatrixClass.VectorDirection.DiagonalRight)
                    {
                        return 1;
                    }
                    return 2;
                default:
                    return 0;
            }
        }
       public  string GetArrowDirection() => ArrowMappings[direction];
    }
}
