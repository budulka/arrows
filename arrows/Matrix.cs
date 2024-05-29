using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace arrows
{
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
        public MatrixClass(int i)
        {
            MatrixArray = new int[i][];
            for (int j = 0; j < i; j++)
            {
                MatrixArray[j] = new int[i];
            }
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
            set
            {
                if (i < 0 || i >= MatrixArray.Length || j < 0 || j >= MatrixArray[i].Length)
                {
                    throw new IndexOutOfRangeException("Index out of range");
                }
                MatrixArray[i][j] = value;
            }
        }
    }
}
