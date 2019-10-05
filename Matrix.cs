using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Lab2
{
    public class Matrix
    {
        double[][] data;

        public Matrix(int n)
        {
            Random r = new Random();
            data = new double[n][];
            for (int i = 0; i < n; i++)
            {
                data[i] = new double[n];
            }

        }

        public Matrix(int n, bool empty)
        {
            Random r = new Random();
            data = new double[n][];
            for (int i = 0; i < n; i++)
            {
                data[i] = new double[n];
            }
            if (!empty)
            {
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        data[i][j] = (double)(r.NextDouble() * 10.7);
                    }
                }
            }
        }

        public Matrix(int nRows, int nCols)
        {
            data = new double[nRows][];

            for (int i = 0; i < nRows; i++)
            {
                data[i] = new double[nCols];

            }
        }

        public Matrix(int nRows, int nCols, bool empty)
        {
            data = new double[nRows][];
            Random r = new Random();

            for (int i = 0; i < nRows; i++)
            {
                data[i] = new double[nCols];

                if (!empty)
                {
                    for (int j = 0; j < nCols; j++)
                    {
                        data[i][j] = (double)(r.NextDouble() * 10.3);
                    }
                }
            }
        }

        public Matrix(double[] initData)
        {
            data = new double[initData.Length][];
            for (int i = 0; i < initData.Length; i++)
            {
                data[i] = new double[1];
                for (int j = 0; j < initData.Length; j++)
                {
                    data[i][j] = initData[j];

                }
            }
        }

        public Matrix(double[,] initData)
        {
            data = new double[initData.Length][];
            for (int i = 0; i < initData.Length; i++)
            {
                data[i] = new double[initData.Length];
                for (int j = 0; j < initData.Length; j++) //!!!!
                {
                    data[i][j] = initData[i, j];

                }
            }

        }

        public Matrix(double[][] initData)
        {
            //data = initData;
            int maxCols = 0;
            for (int i = 0; i < initData.Length; i++)
            {
                if (maxCols < initData[i].Length) maxCols = initData[i].Length;
            }
            data = new double[initData.Length][];


            for (int i = 0; i < initData.Length; i++)
            {
                data[i] = new double[maxCols];

                for (int j = 0; j < maxCols; j++)
                {
                    if (j<=initData[i].Length-1) data[i][j] = initData[i][j];
                    else data[i][j] = 0;
                }
            }
        

        }

        public double this[int i, int j]
        {
            get
            {
                return data[i][j];
            }
            set
            {
                data[i][j] = value;
            }
        }

        public int Rows
        {

            get { return data.Length; }
        }
        public int Columns
        {
            get { return data[0].Length; }
        }
        // размер квадратной матрицы
        public int? Size
        {

            get
            {
                if (data.Length == data[0].Length) return data.Length;
                else return null;
            }
        }

        // Является ли матрица квадратной
        public bool IsSquared
        {
            get { return Size != null; }
        }

        public bool IsEmpty // Является ли матрица нулевой
        {
            get
            {
                for (int i = 0; i < Rows; i++)
                {
                    for (int j = 0; j < Columns; j++)
                    {

                        if (data[i][j] != 0)
                        {
                            return false;

                        }
                    }
                }
                return true;
            }
        }

        public bool IsUnity // Является ли матрица единичной
        {
            get
            {
                if (this.IsDiagonal == false) return false;
                {
                    for (int i = 0; i < Size; i++)
                    {
                        if (data[i][i] != 1)
                        {
                            return false;

                        }
                    }
                    return true;
                }

            }
        }

        public bool IsDiagonal // Является ли матрица диагональной !!!!!!!!!проверить на квадрат
        {
            get
            {
                if (!IsSquared) return false;
                for (int i = 0; i < Size; i++)
                {
                    if (data[i][i] == 0)
                    {
                        return false;

                    }
                    for (int j = 0; j < Size; j++)
                    {
                        if (i != j)
                        {
                            if (data[i][j] != 0)
                            {
                                return false;

                            }

                        }
                    }

                }
                return true;
            }

        }

        public bool IsSymmetric
        {
            get
            {
                if (!IsSquared) return false;
                for (int i = 0; i < Size; i++)
                {

                    for (int j = 0; j < Size; j++)
                    {

                        if (data[i][j] != data[j][i])
                        {
                            return false;

                        }

                    }
                }

                return true;
            }

        } // Является ли матрица симметричной

        public static Matrix operator +(Matrix m1, Matrix m2)
        { if(m1.Rows!=m2.Rows || m1.Columns!=m2.Columns) {
                Console.WriteLine("Матрицы разного размера и не были сложены!");
               return null;
            }
            Matrix m3 = new Matrix(m1.data);
            for (int i = 0; i < m3.Rows; i++)
            {

                for (int j = 0; j < m3.Columns; j++)
                {
                    m3.data[i][j] += m2.data[i][j];
                }
            }
            return m3;

        }
        public static Matrix operator -(Matrix m1, Matrix m2)
        {
            if (m1.Rows != m2.Rows || m1.Columns != m2.Columns)
            {
                Console.WriteLine("Матрицы разного размера и операция не была произведена!");
                return null;
            }
            Matrix m3 = new Matrix(m1.data);
            for (int i = 0; i < m3.Rows; i++)
            {

                for (int j = 0; j < m3.Columns; j++)
                {
                    m3.data[i][j] -= m2.data[i][j];
                }
            }
            return m3;

        }
        public static Matrix operator *(Matrix m1, double d)
        {

            Matrix m3 = new Matrix(m1.data);
            for (int i = 0; i < m3.Rows; i++)
            {
                for (int j = 0; j < m3.Columns; j++)
                {
                    m3.data[i][j] *= d;
                }
            }
            return m3;

        }
        public static Matrix operator *(Matrix m1, Matrix m2)
        {
            if (m1.Rows != m2.Columns)
            {
                Console.WriteLine("Столбцы матрицы А не совпадают со строками матрицы Б. Операция не была произведена!");
                return null;
            }
            Matrix m3 = new Matrix(m1.Rows, m2.Columns);

            for (int i = 0; i < m1.Rows; i++)
            {
                for (int j = 0; j < m2.Columns; j++)
                {
                    for (int k = 0; k < m2.Columns; k++)
                    {
                        m3[i, j] += m1[i, k] * m2[k, j];
                    }
                }
            }
            return m3;
        }

        public static explicit operator Matrix(double[,] arr)
        {
            return new Matrix(arr);
        }

        public static explicit operator Matrix(double[][] arr)
        {
            return new Matrix(arr);
        }

        public Matrix Transpose()
        {
            Matrix tmp = new Matrix(Columns, Rows);
            for (int i = 0; i < tmp.Rows; i++)
            {
                for (int j = 0; j < tmp.Columns; j++)
                {
                    tmp.data[i][j] = data[j][i];//строка на столбец
                }
            }
            this.data = tmp.data;
            return this;
        }

        public double Trace()
        {
            double tmp = 0;
            for (int i = 0; i < Size; i++)
            {
                tmp += data[i][i];
            }
            return tmp;
        }

        public override string ToString() {
            String[] forPrint = new String[Rows];

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    forPrint[i] += data[i][j].ToString()+"  ";//строка на столбец
                }

            }
            string result = String.Join("\n", forPrint);
            return "\n"+result+ "\n";

        }//Реализуйте переопределение метода ToString для преобразования матрицы в строку:

        public static Matrix GetUnity(int Size)
        {
            Matrix unity = new Matrix(Size);
            for (int i = 0; i < unity.Size; i++)
            {
                unity.data[i][i] = 1;
            }
            return unity;
        }
       
        public static Matrix GetEmpty(int Size)
        {
            Matrix empty = new Matrix(Size);
            return empty;
        }

        public static Matrix Parse(string s) {
            string[] rows = Regex.Split(s, @";\s?");
            double[][] tmp= new double[rows.Length][];
            for (int i = 0; i < rows.Length; i++)
                tmp[i] = Array.ConvertAll(Regex.Split(rows[i], @"\s+"), double.Parse);
            Matrix c = (Matrix)tmp;
            return c;
        }
       
        public static bool TryParse(string s, out Matrix m) {
            try
            {
                m=Parse(s);
                return true;
            }
            catch (FormatException) { m=null; return false; }
        }

    

    }
}