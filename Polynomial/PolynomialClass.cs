﻿using System;
using System.Linq;
using Configuration;

namespace Polynomial
{
    public sealed class PolynomialClass
    {
        private static double epsilon;

        static PolynomialClass()
        {
            epsilon = 0.00001;
            //epsilon = double.Parse(ConfigurationManager.AppSettings["epsilon"]);
        }
        #region Private Fields
        private int _degree;
        private readonly double[] _cofficientArray;

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
                if(Math.Abs(array[i - 1]) > epsilon)
                {
                    length = i;
                    break;
                }
            double[] noramlArray = new double[length];
            for (int i = 0; i < length; i++)
                noramlArray[i] = array[i];
            return noramlArray;
        }

        private PolynomialClass Multiply(double cofficient, int degree)
        {
            int tmpDegree = this._degree + degree;
            double[] array = new double[tmpDegree + 1];
            for (int i = 0; i < degree; i++)
                array[i] = 0;
            for (int i = degree, j = 0; i <= tmpDegree; i++, j++)
                array[i] = cofficient * this[j];
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

        public PolynomialClass(PolynomialClass obj) : this(obj._cofficientArray) { }
        #endregion

        #region Overrided methods
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;
            if (ReferenceEquals(null, obj))
                return false;
            if (this.GetType() == obj.GetType())
                return this.Equals((PolynomialClass)obj);
            return false;
        }

        public bool Equals(PolynomialClass obj)
        {
            if (ReferenceEquals(this, obj))
                return true;
            if (ReferenceEquals(null, obj))
                return false;
            if(this._degree == obj._degree)
                return (this._cofficientArray.SequenceEqual(((PolynomialClass)obj)._cofficientArray));
            return false;
        }

        public override string ToString()
        {
            if (this._degree == 0)
                return this[0].ToString();
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
            for(int i = 0; i <= _degree; i++)
                hashCode += this[i].GetHashCode() * i.GetHashCode();
            hashCode *= _degree.GetHashCode();
            return hashCode;

        }
        #endregion

        #region Public Methods
        public static PolynomialClass Add(PolynomialClass a, PolynomialClass b)
        {
            if (a == null || b == null)
                throw new ArgumentNullException();
            if (a._degree == 0 && a[0] == 0)
                return b;
            if (b._degree == 0 && b[0] == 0)
                return a;
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
            for (int i = maxArray.Length - 1; i >= minArray.Length; i--)
                array[i] = maxArray[i];
            for (int i = 0; i < minArray.Length; i++)
                array[i] = minArray[i] + maxArray[i];
            return new PolynomialClass(array);
        }

        public static PolynomialClass Multiply(PolynomialClass a, PolynomialClass b)
        {
            if (a == null || b == null)
                throw new ArgumentNullException();
            if ((a._degree == 0 && a[0] == 0) || (b._degree == 0 && b[0] == 0))
                return new PolynomialClass(new double[] { 0.0 });
            PolynomialClass polynomial = new PolynomialClass(new double[0]);
            for (int i = 0; i <= a._degree; i++)
                polynomial += b.Multiply(a[i], i);
            return polynomial;
        }       

        public static PolynomialClass Subtraction(PolynomialClass a, PolynomialClass b)
        {
            if (a == null || b == null)
                throw new ArgumentNullException();
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
            if (ReferenceEquals(a, null) || ReferenceEquals(null, b))
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
