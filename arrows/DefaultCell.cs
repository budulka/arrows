
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows;


namespace arrows
{
    
    abstract class DefaultCell : UserControl
    {
        public const int CellSize = 50;

        private Pen _rectanglePen = new Pen(Brushes.Black, 1);

        public Pen RectanglePen
        {
            get { return _rectanglePen; }
            set
            {
                if (_rectanglePen != value)
                {
                    _rectanglePen = value;
                    InvalidateVisual(); 
                }
            }
        }

        internal DefaultCell()
        {
            Width = CellSize;
            Height = CellSize;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            drawingContext.DrawRectangle(Brushes.White, _rectanglePen, new Rect(0, 0, CellSize, CellSize));
        }

    }
}
