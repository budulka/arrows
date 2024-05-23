

using System;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using static arrows.ArrowCell;


namespace arrows
{
    public class GameArea : Canvas
    {
        protected const int CellSize = 50;
        public MatrixClass FieldMatrix;
        public int Size;
        private ArrowCell[][] Arrows;
        internal NumberCell[][] NumberCells;
        internal ArrowCell[][] GetArrowCells => Arrows;
        public MatrixClass _currentField;
        private bool _isGameWon = false;
        public bool IsWon { get {  return _isGameWon; } }
        private NumberCell _highlighted = null;
        public GameArea(int size)
        {
            Size = size;

            int[][] field = new int[Size][];

            for (int i = 0; i < Size; i++)
            {
                field[i] = new int[Size];
                for (int j = 0; j < Size; j++)
                {
                    field[i][j] = 0;
                }
            }
            FieldMatrix = new MatrixClass(field);
            NumberCells = new NumberCell[Size][];
            for (int i = 0; i < Size; i++)
            {
                NumberCells[i] = new NumberCell[Size];
            }
            InitializeArrowCells();
            GetNumberField();
            
            _currentField = (MatrixClass)FieldMatrix.Clone();
            
        }
            

        private int[][] InitializeArrowArrays()
        {
            Random random = new Random();
            int[][] ArrowMatrix = new int[4][];
            for (int j = 0; j < 4; j++)
            {
                ArrowMatrix[j] = new int[Size];
                ArrowMatrix[j][0] = random.Next(1, 3);
                do
                {
                    ArrowMatrix[j][ArrowMatrix[j].Length - 1] = random.Next(1, 4);
                } while (ArrowMatrix[j][ArrowMatrix[j].Length - 1] == 2);
                for (int i = 1; i < ArrowMatrix[j].Length - 1; i++)
                {
                    ArrowMatrix[j][i] = random.Next(1, 4);
                }
            }
            return ArrowMatrix;
        }

        public void InitializeArrowCells()
        {
            Arrows = new ArrowCell[4][];
            for (int i = 0; i < Arrows.Length; i++)
            {
                Arrows[i] = new ArrowCell[Size];
            }
            for (int i = 1; i < Size - 1; i++)
            {
                Arrows[0][i] = new ArrowCell(LogicType.Center, 0, i, this);
          
                Arrows[1][i] = new ArrowCell(LogicType.Center, 1, i, this);
           
                Arrows[2][i] = new ArrowCell(LogicType.Center, 2, i, this);
           
                Arrows[3][i] = new ArrowCell(LogicType.Center, 3, i, this);
            }
            Arrows[0][0] = new ArrowCell(LogicType.Left, 0, 0, this);
            Arrows[0][Size - 1] = new ArrowCell(LogicType.Right, 0, Size - 1, this);
            Arrows[1][0] = new ArrowCell(LogicType.Left, 1, 0, this);
            Arrows[1][Size - 1] = new ArrowCell(LogicType.Right, 1, Size - 1, this);
            Arrows[2][0] = new ArrowCell(LogicType.Left, 2, 0, this);
            Arrows[2][Size - 1] = new ArrowCell(LogicType.Right, 2, Size - 1, this);
            Arrows[3][0] = new ArrowCell(LogicType.Left, 3, 0, this);
            Arrows[3][Size - 1] = new ArrowCell(LogicType.Right, 3, Size - 1, this);
        }

        public void UpdateCurrentField(int from, MatrixClass.VectorDirection direction, int pos, MatrixClass.VectorDirection previous)
        {
            if (previous != MatrixClass.VectorDirection.NoDirection)
            {
                int[][] prev = FieldMatrix.CreateMatrixFromVector(from, previous, pos, Size);
                _currentField = _currentField.AddMatrices(prev);
            }
            if (direction != MatrixClass.VectorDirection.NoDirection)
            {
                int[][] VectorMatrix = FieldMatrix.CreateMatrixFromVector(from, direction, pos, Size);
                _currentField = _currentField.SubtractMatrices(VectorMatrix);
            }
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if (_currentField[i, j] == 0)
                    {
                       NumberCells[i][j].text.Foreground = Brushes.Green;
                       NumberCells[i][j].text.FontWeight = FontWeights.Bold;
                    }
                    else if (_currentField[i, j] < 0)
                    {
                        NumberCells[i][j].text.Foreground = Brushes.Red;
                        NumberCells[i][j].text.FontWeight = FontWeights.Bold;
                    }
                    else
                    {
                        NumberCells[i][j].text.Foreground= Brushes.Black;
                        NumberCells[i][j].text.FontWeight= FontWeights.Normal;
                    }
                }
            }
        }

        internal void HighLightCells(int x, int y, NumberCell cell)
        {
            if (_highlighted != null)
            {
                Dispatcher.Invoke(() => { 
                    _highlighted.RectanglePen = new Pen(Brushes.Black, 1);
                    SetZIndex(_highlighted, 0);
                });
            }
            foreach(var t in Arrows)
            {
                foreach(var i in t)
                {
                    Disable(i);
                }
            }
            _highlighted = cell;
            Dispatcher.Invoke(() => { 
                cell.RectanglePen = new Pen(Brushes.PaleTurquoise, 3);
                SetZIndex(cell, 1);
            });
            HighLight(Arrows[0][y]);
            HighLight(Arrows[1][x]);
            HighLight(Arrows[2][Size - y - 1]);
            HighLight(Arrows[3][Size - x - 1]);

            if (y - x - 1 >= 0)
            {
                HighLight(Arrows[0][y - x - 1]);
            }
            if (y + x + 1 < Size)
            {
                HighLight(Arrows[0][y + x + 1]);
            }
            if (x - Size + y >= 0)
            {
                HighLight(Arrows[1][x - (Size - y)]);
            }
            if (x + Size - y < Size)
            {
                HighLight(Arrows[1][x + Size - y]);
            }
            if (x - y - 1 >= 0)
            {
                HighLight(Arrows[2][x - y - 1]);
            }
            if (2 * Size - 1 - y - x < Size)
            {
                HighLight(Arrows[2][2 * Size - 1 - y - x]);
            }
            if (Size - 2 - x - y >= 0)
            {
                HighLight(Arrows[3][Size - 2 - x - y]);
            }
            if (Size - (x - y) < Size)
            {
                HighLight(Arrows[3][Size - (x - y)]);
            }
        }

        private void HighLight(ArrowCell cell)
        {
            Dispatcher.Invoke(() => {
                cell.RectangleBrush = Brushes.PaleTurquoise;
            });
        }
        private void Disable(ArrowCell cell)
        {
            Dispatcher.Invoke(() => {
                cell.RectangleBrush = Brushes.White;
            });
        }
        

        public bool CheckWin()
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if (_currentField[i,j] != 0)
                    { return false; }
                }
            }
            _isGameWon = true;
            return true;
        }



        private void GetNumberField()
        {
            int[][] ArrowMatrix = InitializeArrowArrays();
            for (int i = 0; i < ArrowMatrix.Length; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    int field = ArrowMatrix[i][j];
                    MatrixClass.VectorDirection direction = MatrixClass.VectorDirection.Vertical;
                    
                    if (field == 2)
                    {
                        direction = MatrixClass.VectorDirection.DiagonalRight;
                    }
                    else if (field == 3)
                    {
                        direction = MatrixClass.VectorDirection.DiagonalLeft;
                    }
                    Arrows[i][j].original = direction;
                    int[][] vectorMatrix = FieldMatrix.CreateMatrixFromVector(i, direction, j, Size);
                    FieldMatrix = FieldMatrix.AddMatrices(vectorMatrix);
                }
            }
        }
    }

    public class Hint
    {
        private GameArea area;
        private int Size;

        public Hint(GameArea area, int s)
        {
            this.area = area;
            Size = s;
        }

        public void GetHint()
        {
            Random randomX = new Random();
            Random randomY = new Random();
            int x, y;
            do {
                x = randomX.Next(0, 4);
                y = randomY.Next(0, Size);
            } while (area.GetArrowCells[x][y].IsDisabled || area.GetArrowCells[x][y].IsCorrect);
            area.GetArrowCells[x][y].IsDisabled = true;
            area.GetArrowCells[x][y].MarkAsHinted();
        }
    }



    public class MatrixClass : ICloneable
    {

        public enum VectorDirection
        {
                Vertical,
                DiagonalRight,
                DiagonalLeft,
                NoDirection
        }
        private int[][] MatrixArray;
        public MatrixClass(int[][] matrix)
        {
                MatrixArray = matrix;
        }
            public object Clone() => new MatrixClass(MatrixArray); 
            public MatrixClass AddMatrices(int[][] matrix2)
            {
                int rows = MatrixArray.Length;
                int cols = MatrixArray[0].Length;

                int[][] result = new int[rows][];

                for (int i = 0; i < rows; i++)
                {
                    result[i] = new int[cols];
                    for (int j = 0; j < cols; j++)
                    {

                        result[i][j] = MatrixArray[i][j] + matrix2[i][j];
                    }
                }

                return new MatrixClass(result);
            }

            public MatrixClass SubtractMatrices(int[][] matrix2)
            {
                int rows = MatrixArray.Length;
                int cols = MatrixArray[0].Length;

                int[][] result = new int[rows][];

                for (int i = 0; i < rows; i++)
                {
                    result[i] = new int[cols];
                    for (int j = 0; j < cols; j++)
                    {

                        result[i][j] = MatrixArray[i][j] - matrix2[i][j];
                    }
                }

                return new MatrixClass(result);
            }

            public int[][] CreateMatrixFromVector(int from, VectorDirection direction, int pos, int len)
            {

                int[][] matrix = new int[len][];
                for (int i = 0; i < len; i++)
                {
                    matrix[i] = new int[len];
                    for (int j = 0; j < len; j++)
                    {
                        matrix[i][j] = 0;
                    }
                }
                switch (direction)
                {
                    case VectorDirection.Vertical:
                        ApplyVertical(pos, matrix);
                        break;
                    case VectorDirection.DiagonalLeft:
                        ApplyDiagonalCounterClockwise(pos, matrix);
                        break;
                    case VectorDirection.DiagonalRight:
                        ApplyDiagonalClockwise(pos, matrix);
                        break;
                }
                for (int j = 0; j < from; j++)
                { 
                    RotateClockwise(matrix);
                }

                return matrix;
            }
            private void ApplyVertical(int pos, int[][] mat)
            {
                for (int i = 0; i < mat.Length; i++)
                {
                    mat[i][pos] += 1;
                } 
            }

            private void ApplyDiagonalClockwise(int pos, int[][] mat)
            {
                int row = 0;
                int col = pos + 1;

                while (row < mat.Length && col < mat[row].Length)
                {
                    mat[row][col] += 1;
                    row++;
                    col++;
                }
            }

            private void ApplyDiagonalCounterClockwise(int pos, int[][] mat)
            {
                int row = 0;
                int col = pos - 1;

                while (row < mat.Length && col >= 0)
                {
                    mat[row][col] += 1;
                    row++;
                    col--;
                }
            }
            private void RotateClockwise(int[][] mat)
            {
                int n = mat.Length;
                int[][] rotatedMatrix = new int[n][];

                for (int i = 0; i < n; i++)
                {
                    rotatedMatrix[i] = new int[n];
                    for (int j = 0; j < n; j++)
                    {
                        rotatedMatrix[i][j] = mat[n - 1 - j][i]; 
                    }
                }

                
                for (int i = 0; i < n; i++)
                {
                    Array.Copy(rotatedMatrix[i], mat[i], n);
                }

            }

           
            public int this[int i, int j]
            {
                get
                {

                    if (i < 0 || i >= MatrixArray.Length || j < 0 || j >= MatrixArray[i].Length)
                    {
                        throw new IndexOutOfRangeException("Index out of range");
                    }

                    return MatrixArray[i][j];
                }
            }
        }
    }
/*
  * - if i can get the name of the cell by clicking
  * - add current state 
  * - func to add/subtract an action from cur state
  * - handle click(add old, subtract new)
  * - win/lose
  * - restart
  * - minmax
  */


