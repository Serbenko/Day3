using System;
using System.Collections;
using NUnit.Framework;
using Polynomial;

namespace PolynomialTest
{
    [TestFixture]
    public class PolynomialClassTest
    {
        [Test]
        [TestCase(null, ExpectedException = typeof(ArgumentNullException))]
        public void PolynomialClassConstructorTest(double[] array)
        {
            new PolynomialClass(array);
        }

        [Test]
        [TestCaseSource(typeof(DataClass), "ToNormalArrayRepresentationData")]
        public bool ToNormalArrayRepresentationTest(double[] a, double[] b)
        {
            return (new PolynomialClass(a) == new PolynomialClass(b));
        }

        #region Overrided Methods' Tests
        [Test]
        [TestCaseSource(typeof(DataClass), "EqualsData")]
        public bool EqualsTest(PolynomialClass a, PolynomialClass b)
        {
            return a.Equals(b);
        }

        [Test]
        [TestCaseSource(typeof(DataClass), "ToStringData")]
        public String ToStringTest(PolynomialClass a)
        {
            return a.ToString();
        }

        [Test]
        [TestCaseSource(typeof(DataClass), "HashCodeData")]
        public bool GetHashCodeTest(PolynomialClass a, PolynomialClass b)
        {
            return (a.GetHashCode() == b.GetHashCode());
        }
        #endregion

        #region Operators' Tests
        [Test]
        [TestCaseSource(typeof(DataClass), "AddData")]
        public String AddTest(PolynomialClass a, PolynomialClass b)
        {
            return (a + b).ToString();
        }

        [Test]
        [TestCaseSource(typeof(DataClass), "SubtractionData")]
        public String SubtractionTest(PolynomialClass a, PolynomialClass b)
        {
            return (a - b).ToString();
        }

        [Test]
        [TestCaseSource(typeof(DataClass), "MultiplyData")]
        public String MultiplyTest(PolynomialClass a, PolynomialClass b)
        {
            return (a * b).ToString();
        }

        [Test]
        [TestCaseSource(typeof(DataClass), "EqualsData")]
        public bool EqualsOperatorTest(PolynomialClass a, PolynomialClass b)
        {
            return a == b;
        }

        [Test]
        [TestCaseSource(typeof(DataClass), "EqualsData")]
        public bool NotEqualsOperatorTest(PolynomialClass a, PolynomialClass b)
        {
            return !(a != b);
        }

        #endregion

        public class DataClass
        {
            public static IEnumerable ToNormalArrayRepresentationData
            {
                get
                {
                    yield return new TestCaseData(new double[] { 0.0, 0.0, 15.0, 0.0, 0.0 }, new double[] { 0.0, 0.0, 15.0}).Returns(true);
                    yield return new TestCaseData(new double[] { 0.0, 0.0, 15.0 }, new double[] { 15.0 }).Returns(false);
                }
            }

            public static IEnumerable EqualsData
            {
                get
                {
                    yield return new TestCaseData(new PolynomialClass(new double[] { 10.0, 0.0, 15.0}),new PolynomialClass (new double[] { 10.0, 0.0, 15.0 })).Returns(true);
                    yield return new TestCaseData(new PolynomialClass(new double[] { 10.0, 0.0, 15.0 }), new PolynomialClass(new double[] { 0.0, 10.0, 15.0 })).Returns(false);
                    yield return new TestCaseData(new PolynomialClass (new double[] { 18.0, 7.9, 15.1 }), new PolynomialClass(new double[] { 18.1, 19.0, 7.9 })).Returns(false);
                    yield return new TestCaseData(new PolynomialClass(new double[] { 10.0, 0.0, 15.0 }), null).Returns(false);
                }
            }

            public static IEnumerable ToStringData
            {
                get
                {
                    yield return new TestCaseData(new PolynomialClass(new double[] { 10.0, 0.0, 15.0 })).Returns("15x^2 + 10");
                    yield return new TestCaseData(new PolynomialClass(new double[] { 10.0, -18.0, 15.0 })).Returns("15x^2 - 18x^1 + 10");
                    yield return new TestCaseData(new PolynomialClass(new double[] { 18.0, 7.9, -15.1 })).Returns("-15.1x^2 + 7.9x^1 + 18");
                    yield return new TestCaseData(new PolynomialClass(new double[] { 0.0})).Returns("0");
                }
            }

            public static IEnumerable HashCodeData
            {
                get
                {
                    yield return new TestCaseData(new PolynomialClass(new double[] { 10.0, 0.0, 15.0 }), new PolynomialClass(new double[] { 10.0, 0.0, 15.0 })).Returns(true);
                    yield return new TestCaseData(new PolynomialClass(new double[] { 10.0, 0.0, 15.0 }), new PolynomialClass(new double[] { 0.0, 10.0, 15.0 })).Returns(false);
                    yield return new TestCaseData(new PolynomialClass(new double[] { 18.0, 7.9, 15.1 }), new PolynomialClass(new double[] { 18.1, 19.0, 7.9 })).Returns(false);
                }
            }

            public static IEnumerable AddData
            {
                get
                {
                    yield return new TestCaseData(new PolynomialClass(new double[] { 10.0, 0.0, 15.0 }), new PolynomialClass(new double[] { 18.0, -3.0, 15.0, -20.5, 36.9 })).Returns("36.9x^4 - 20.5x^3 + 30x^2 - 3x^1 + 28");
                    yield return new TestCaseData(new PolynomialClass(new double[] { 10.0, 73.0, -15.0 }), new PolynomialClass(new double[] { 0.0 })).Returns("-15x^2 + 73x^1 + 10");
                    yield return new TestCaseData(new PolynomialClass(new double[] { 10.0, 73.0, -15.0 }), new PolynomialClass(new double[] { 20.0 })).Returns("-15x^2 + 73x^1 + 30");
                    yield return new TestCaseData(new PolynomialClass(new double[] { 0.0 }), new PolynomialClass(new double[] { 10.0, 73.0, -15.0 })).Returns("-15x^2 + 73x^1 + 10");
                }
            }

            public static IEnumerable SubtractionData
            {
                get
                {
                    yield return new TestCaseData(new PolynomialClass(new double[] { 10.0, 0.0, 15.0 }), new PolynomialClass(new double[] { 18.0, -3.0, 15.0, -20.5, 36.9 })).Returns("-36.9x^4 + 20.5x^3 + 3x^1 - 8");
                    yield return new TestCaseData(new PolynomialClass(new double[] { 10.0, 73.0, -15.0 }), new PolynomialClass(new double[] { 0.0 })).Returns("-15x^2 + 73x^1 + 10");
                    yield return new TestCaseData(new PolynomialClass(new double[] { 10.0, 73.0, -15.0 }), new PolynomialClass(new double[] { 20.0 })).Returns("-15x^2 + 73x^1 - 10");
                    yield return new TestCaseData(new PolynomialClass(new double[] { 0.0 }), new PolynomialClass(new double[] { 10.0, 73.0, -15.0 })).Returns("15x^2 - 73x^1 - 10");
                }
            }

            public static IEnumerable MultiplyData
            {
                get
                {
                    yield return new TestCaseData(new PolynomialClass(new double[] { 10.0, 0.0, 15.0 }), new PolynomialClass(new double[] { 1.0 })).Returns("15x^2 + 10");
                    yield return new TestCaseData(new PolynomialClass(new double[] { 10.0, 3.0, -15.0 }), new PolynomialClass(new double[] { 10.0 })).Returns("-150x^2 + 30x^1 + 100");
                    yield return new TestCaseData(new PolynomialClass(new double[] { 10.0, 3.0, -15.0 }), new PolynomialClass(new double[] { 1.0, 5.0 })).Returns("-75x^3 + 53x^1 + 10");
                    yield return new TestCaseData(new PolynomialClass(new double[] { 0.0 }), new PolynomialClass(new double[] { 10.0, 73.0, -15.0 })).Returns("0");
                    yield return new TestCaseData(new PolynomialClass(new double[] { 10.0, 73.0, -15.0 }), new PolynomialClass(new double[] { 0.0 })).Returns("0");
                }
            }
        }
    }
}
