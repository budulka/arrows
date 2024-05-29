
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows;


namespace arrows
{
    
    internal abstract class DefaultCell : UserControl
    {
        public const int CellSize = 50;
        internal TextBlock text;

        private Brush _rectangleBrush = Brushes.Transparent;
        private Pen _rectanglePen = new Pen(Brushes.Black, 1);

        public Brush RectangleBrush
        {
            get { return _rectangleBrush; }
            set
            {
                if (_rectangleBrush != value)
                {
                    _rectangleBrush = value;
                    InvalidateVisual(); 
                }
            }
        }
        public Pen RectanglePen
        {
            get { return _rectanglePen; }
            set
            {
                if ( _rectanglePen != value)
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
            drawingContext.DrawRectangle(_rectangleBrush, _rectanglePen, new Rect(0, 0, CellSize, CellSize));
        }

    }
}
