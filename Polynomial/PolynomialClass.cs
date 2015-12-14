using System;
using System.Linq;

namespace Polynomial
{
    public sealed class PolynomialClass
    {
        #region Private Fields
        private int _degree;
        private double[] _cofficientArray;

        private double this[int i]
        {
            get
            {
                if (i < 0 || i > _cofficientArray.Length - 1)
                    throw new ArgumentOutOfRangeException();
                return _cofficientArray[i];
            }
        }
        #endregion

        #region Private Methods
        private static double[] ToNormalArrayRepresentation(double[] array)
        {
            int length = 0;
            for(int i = array.Length; i > 0; i--)
                if(Math.Abs(array[i - 1]) > 0)
                {
                    length = i;
                    break;
                }
            double[] noramlArray = new double[length];
            for (int i = 0; i < length; i++)
                noramlArray[i] = array[i];
            return noramlArray;
        }

        private static PolynomialClass Multiply(double cofficient, int degree, PolynomialClass obj)
        {
            int tmpDegree = obj._degree + degree;
            double[] array = new double[tmpDegree + 1];
            for (int i = 0; i < degree; i++)
                array[i] = 0;
            for (int i = degree, j = 0; i <= tmpDegree; i++, j++)
                array[i] = cofficient * obj[j];
            return new PolynomialClass(array);
        }
        #endregion

        #region Constructors
        public PolynomialClass(double[] array)
        {
            if (array == null)
                throw new ArgumentNullException();
            double[] tmpArray = ToNormalArrayRepresentation(array);
            if(tmpArray.Length == 0)
            {
                _degree = 0;
                _cofficientArray = new double[] { 0.0 };
            }
            else
            {
                _cofficientArray = new double[tmpArray.Length];
                Array.Copy(tmpArray, _cofficientArray, tmpArray.Length);
                _degree = _cofficientArray.Length - 1;

            }
        }

        public PolynomialClass(PolynomialClass obj)
        {
             new PolynomialClass(obj._cofficientArray);
        }
        #endregion

        #region Overrided methods
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;
            if (ReferenceEquals(null, obj))
                return false;
            return (this.GetType() == obj.GetType()) && (this._cofficientArray.SequenceEqual(((PolynomialClass)obj)._cofficientArray));
        }

        public bool Equals(PolynomialClass obj)
        {
            if (ReferenceEquals(this, obj))
                return true;
            if (ReferenceEquals(null, obj))
                return false;
            return (this._cofficientArray.SequenceEqual(((PolynomialClass)obj)._cofficientArray));
        }

        public override string ToString()
        {
            String polynomial = this[_degree] + "x^" + _degree;
            for(int i = _degree - 1; i > 0; i--)
            {
                 if(this[i] < 0)
                 {
                     polynomial += " - " + Math.Abs(this[i]) + "x^" + i;
                 }
                 if (this[i] > 0)
                 {
                     polynomial += " + " + this[i] + "x^" + i;
                 }
            }
            if (this[0] > 0)
                polynomial += " + " + this[0];
            if (this[0] < 0)
                polynomial += " - " + Math.Abs(this[0]);
            return polynomial;
        }

        public override int GetHashCode()
        {
            int hashCode = 17;
            foreach (double cof in _cofficientArray)
                hashCode += cof.GetHashCode();
            hashCode *= _degree.GetHashCode();
            return hashCode;

        }
        #endregion

        #region Public Methods
        public static PolynomialClass Add(PolynomialClass a, PolynomialClass b)
        {
            double[] maxArray, minArray;
            if (a._degree > b._degree)
            {
                maxArray = a._cofficientArray;
                minArray = b._cofficientArray;
            }               
            else
            {
                minArray = a._cofficientArray;
                maxArray = b._cofficientArray;
            }
            double[] array = new double[maxArray.Length];
            for (int i = maxArray.Length - 1; i > maxArray.Length - minArray.Length; i--)
                array[i] = maxArray[i];
            for (int i = 0; i < minArray.Length; i++)
                array[i] = minArray[i] + maxArray[i];
            return new PolynomialClass(array);
        }

        public static PolynomialClass Multiply(PolynomialClass a, PolynomialClass b)
        {
            PolynomialClass polynomial = new PolynomialClass(b);
            for (int i = 0; i <= a._degree; i++)
                if(a[i] != 0)
                    polynomial = Multiply(a[i], i, polynomial);
            return polynomial;
        }       

        public static PolynomialClass Subtraction(PolynomialClass a, PolynomialClass b)
        {
            double[] tmpArray = new double[b._degree + 1];
            for (int i = 0; i <= b._degree; i++)
               tmpArray[i] = b._cofficientArray[i] / (-1);
            return Add(a, new PolynomialClass(tmpArray));
        }
        #endregion

        #region Operators
        public static PolynomialClass operator+(PolynomialClass a, PolynomialClass b)
        {
            return Add(a, b);
        }

        public static PolynomialClass operator -(PolynomialClass a, PolynomialClass b)
        {
            return Subtraction(a, b);
        }

        public static PolynomialClass operator *(PolynomialClass a, PolynomialClass b)
        {
            return Multiply(a, b);
        }

        public static bool operator ==(PolynomialClass a, PolynomialClass b)
        {
            if (ReferenceEquals(a, b))
                return true;
            if ((object)a == null || (object)b == null)
                return false;
            return (a._cofficientArray.SequenceEqual(b._cofficientArray));
        }

        public static bool operator !=(PolynomialClass a, PolynomialClass b)
        {
            return !(a == b);
        }
        #endregion
    }
}
