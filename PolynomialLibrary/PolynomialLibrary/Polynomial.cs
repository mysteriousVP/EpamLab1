using System;
using System.Text;

namespace PolynomialLibrary
{
    public class Polynomial
    {
        private double[] values { get; set; }
        private int size { get; set; }

        public int Size
        {
            get
            {
                return size;
            }
        }

        public Polynomial(int polynomialLenght)
        {
            if (polynomialLenght <= 0)
            {
                throw new PolynomialException("You cannot create a polynomial with empty coefficients.");
            }

            this.size = polynomialLenght;
            this.values = new double[polynomialLenght];
        }

        public Polynomial(double[] koefsOfPolynomial)
        {
            if (koefsOfPolynomial == null || koefsOfPolynomial.Length == 0)
            {
                throw new PolynomialException("You cannot create a polynomial with empty coefficients.");
            }

            this.size = koefsOfPolynomial.Length;
            this.values = koefsOfPolynomial;
        }

        public double this[int index]
        {
            get
            {
                if (index >= values.Length || index < 0)
                {
                    throw new IndexOutOfRangeException();
                }

                return values[index];
            }

            set
            {
                if (index > values.Length - 1 || index < 0)
                {
                    throw new IndexOutOfRangeException();
                }

                values[index] = value;
            }
        }

        public static Polynomial operator +(Polynomial object1, Polynomial object2)
        {
            if (object1 == null || object2 == null)
            {
                throw new NullReferenceException();
            }

            int size = (object1.size > object2.size) ? object1.size : object2.size;

            Polynomial polynomial = new Polynomial(size);

            for (int i = 0; i < size; i++)
            {
                if (object1.size > i)
                {
                    polynomial[i] += object1[i];
                }

                if (object2.size > i)
                {
                    polynomial[i] += object2[i];
                }
            }

            return polynomial;
        }

        public static Polynomial operator -(Polynomial object1, Polynomial object2)
        {
            if (object1 == null || object2 == null)
            {
                throw new NullReferenceException();
            }

            int size = (object1.size > object2.size) ? object1.size: object2.size;

            Polynomial polynomial = new Polynomial(size);

            for (int j = 0; j < size; j++)
            {
                if (object1.Size == object2.Size)
                {
                    for (int i = object1.Size - 1; i >= 0; i--)
                    {
                        polynomial[i] = object1[i] - object2[i];
                    }
                }

                if (object1.Size > object2.Size)
                {
                    for (int i = object1.Size - 1; i > object2.Size - 1; i--)
                    {
                        polynomial[i] = object1[i];
                    }
                    for (int i = object2.Size - 1; i >= 0; i--)
                    {
                        polynomial[i] = object1[i] - object2[i];
                    }
                }

                if (object1.Size < object2.Size)
                {
                    for (int i = object2.Size - 1; i > object1.Size - 1; i--)
                    {
                        polynomial[i] = -object2[i];
                    }
                    for (int i = object1.Size - 1; i >= 0; i--)
                    {
                        polynomial[i] = object1[i] - object2[i];
                    }
                }
            }

            return polynomial;
        }

        public static Polynomial operator *(Polynomial object1, Polynomial object2)
        {
            if (object1 == null || object2 == null)
            {
                throw new NullReferenceException();
            }

            int size = object1.size + object2.size - 1;

            Polynomial polynomial = new Polynomial(size);

            for (int i = 0; i < object1.size; i++)
            {
                for (int j = 0; j < object2.size; j++)
                {
                    polynomial[i + j] += object1.values[i] * object2.values[j];
                }
            }

            return polynomial;
        }

        public static Polynomial operator *(Polynomial obj, double koef)
        {
            if (obj == null)
            {
                throw new NullReferenceException();
            }

            Polynomial polynomial = new Polynomial(obj.size);

            for (int i = 0; i < obj.size; i++)
            {
                polynomial[i] = obj[i] * koef;
            }

            return polynomial;
        }

        public static Polynomial operator *(double koef, Polynomial obj) 
        {
            if (obj == null)
            {
                throw new NullReferenceException();
            }

            Polynomial polynomial = new Polynomial(obj.size);

            for (int i = 0; i < obj.size; i++)
            {
                polynomial[i] = obj[i] * koef;
            }

            return polynomial;
        }

        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Polynomial polynomial = (Polynomial)obj; 
                return this.ToString() == polynomial.ToString();
            } 
        }

        public override int GetHashCode()
        {
            return values.GetHashCode() + size.GetHashCode();
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = size - 1; i >= 1; i--)
            {
                stringBuilder.Append(string.Format($"{(values[i] >= 0 ? "+" + values[i].ToString() : values[i].ToString())}x^{i.ToString()}"));
            }
            stringBuilder.Append(string.Format($"{(values[0] >= 0 ? "+" + values[0].ToString() : values[0].ToString())}"));

            return stringBuilder.ToString();
        }
    }
}
