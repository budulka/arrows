using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows;
using System.Runtime.CompilerServices;

namespace arrows
{
    
    abstract class DefaultCell : UserControl
    {
        public const int CellSize = 50;
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            drawingContext.DrawRectangle(Brushes.White, new Pen(Brushes.Black, 1), new Rect(0, 0, CellSize, CellSize));
        }
    }
}
