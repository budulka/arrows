
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows;


namespace arrows
{
    
    abstract class DefaultCell : UserControl
    {
        public const int CellSize = 50;

        internal DefaultCell()
        {
            Width = CellSize;
            Height = CellSize;
        }
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            drawingContext.DrawRectangle(Brushes.White, new Pen(Brushes.Black, 1), new Rect(0, 0, CellSize, CellSize));
        }

    }
}
