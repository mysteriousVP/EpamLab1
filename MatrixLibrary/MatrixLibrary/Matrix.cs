using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace MatrixLibrary
{
    [Serializable]
    public class Matrix
    {
        public int n { get; set; }

        public int m { get; set; }

        private double[,] values;

        public Matrix(int n, int m)
        {
            if (n <= 0 || m <= 0)
            {
                throw new MatrixException("You cannot create a matrix with sizes less or equal zero.");
            }

            this.n = n;
            this.m = m;
            values = new double[n, m];
        }

        public Matrix(double[,] matrix)
        {
            this.n = matrix.GetLength(0);
            this.m = matrix.GetLength(1);
            values = matrix;
        }

        public double this[int indexN, int indexM]
        {
            get
            {
                if (indexN >= n || indexM >= m || indexN < 0 || indexM < 0)
                {
                    throw new IndexOutOfRangeException();
                }
                else
                {
                    return values[indexN, indexM];
                }
            }

            set
            {
                if (indexN >= n || indexM >= m || indexN < 0 || indexM < 0)
                {
                    throw new IndexOutOfRangeException();
                }
                else
                {
                    values[indexN, indexM] = value;
                }
            }
        }

        public static Matrix operator +(Matrix object1, Matrix object2)
        {
            if ((object)object1 == null || (object)object2 == null)
            {
                throw new NullReferenceException();
            }

            if (object1.n != object2.n || object2.m != object2.m)
            {
                throw new MatrixException("Addition is not possible with different matrix dimensions.");
            }

            Matrix matrix = new Matrix(object1.n, object1.m);

            for (int i = 0; i < object1.n; i++)
            {
                for (int j = 0; j < object1.m; j++)
                {
                    matrix[i, j] = object1[i, j] + object2[i, j];
                }
            }

            return matrix;
        }

        public static Matrix operator -(Matrix object1, Matrix object2)
        {
            if ((object)object1 == null || (object)object2 == null)
            {
                throw new NullReferenceException();
            }

            if (object1.n != object2.n || object2.m != object2.m)
            {
                throw new MatrixException("Subtraction is not possible with different matrix dimensions.");
            }

            Matrix matrix = new Matrix(object1.n, object1.m);

            for (int i = 0; i < object1.n; i++)
            {
                for (int j = 0; j < object1.m; j++)
                {
                    matrix[i, j] = object1[i, j] - object2[i, j];
                }
            }

            return matrix;
        }

        public static Matrix operator *(Matrix object1, Matrix object2)
        {
            if ((object)object1 == null || (object)object2 == null)
            {
                throw new NullReferenceException();
            }

            if (object1.m != object2.n)
            {
                throw new MatrixException("Matrix multiplication is not possible. Pay attention on sizes of matrix.");
            }

            Matrix matrix = new Matrix(object1.n, object2.m);

            for (int i = 0; i < object1.n; i++)
            {
                for (int j = 0; j < object2.m; j++)
                {
                    for (int k = 0; k < object2.n; k++)
                    {
                        matrix[i, j] += object1[i, k] * object2[k, j];
                    }
                }
            }

            return matrix;
        }


        public static Matrix operator *(Matrix obj, double koef)
        {
            if (obj == null)
            {
                throw new NullReferenceException();
            }

            Matrix matrix = new Matrix(obj.n, obj.m);

            for (int i = 0; i < obj.n; i++)
            {
                for (int j = 0; j < obj.m; j++)
                {
                    matrix[i, j] = obj[i, j] * koef;
                }
            }

            return matrix;
        }


        public static Matrix operator *(double koef, Matrix obj)
        {
            if (obj == null)
            {
                throw new NullReferenceException();
            }

            Matrix matrix = new Matrix(obj.n, obj.m);

            for (int i = 0; i < obj.n; i++)
            {
                for (int j = 0; j < obj.m; j++)
                {
                    matrix[i, j] = obj[i, j] * koef;
                }
            }

            return matrix;
        }

        public static bool operator ==(Matrix object1, Matrix object2)
        {
            if ((object)object1 == null || (object)object2 == null)
            {
                return false;
            }

            if (object1.n != object2.n || object2.m != object2.m)
            {
                return false;
            }

            for (int i = 0; i < object1.n; i++)
            {
                for (int j = 0; j < object1.m; j++)
                {
                    if (object1[i, j] != object2[i, j])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public static bool operator !=(Matrix object1, Matrix object2)
        {
            return !(object1 == object2);
        }

        public override int GetHashCode()
        {
            return values.GetHashCode() ^ n.GetHashCode() ^ m.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Matrix matrix = (Matrix)obj;
                return this.ToString() == matrix.ToString();
            }
        }

        public override string ToString()
        {
            StringBuilder outputString = new StringBuilder();

            for (int i = 0; i < this.n; i++)
            {
                for (int j = 0; j < this.m; j++)
                {
                    outputString.Append(string.Format($"{values[i, j]} "));
                }
                outputString.Append("\n");
            }

            return outputString.ToString();
        }

        public void WriteToJSON(string path)
        {
            using (StreamWriter sw = new StreamWriter(path, false, Encoding.UTF8))
            {
                string output = JsonConvert.SerializeObject(this.values, Formatting.Indented);
                sw.WriteLine(output);
            }
        }

        public static Matrix ReadFromJSON(string path)
        {
            using (StreamReader sr = new StreamReader(path))
            {
                string input = sr.ReadToEnd();
                double[,] deserialized = JsonConvert.DeserializeObject<double[,]>(input);

                return new Matrix(deserialized);
            }
        }
    }
}
