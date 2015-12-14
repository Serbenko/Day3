using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Polynomial;

namespace PolynomialTest
{
    [TestFixture]
    public class PolynomialClassTest
    {
        [Test]
        [TestCaseSource(typeof(DataClass), "PositiveData")]
        public bool ToNormalArrayRepresentationTest(double[] a, double[] b)
        {
            return (new PolynomialClass(a) == new PolynomialClass(b));
        }

        public class DataClass
        {
            public static IEnumerable PositiveData
            {
                get
                {
                    yield return new TestCaseData(new double[] { 0.0, 0.0, 15.0, 0.0, 0.0 }, new double[] { 0.0, 0.0, 15.0}).Returns(true);
                    yield return new TestCaseData(new double[] { 0.0, 0.0, 15.0 }, new double[] { 15.0 }).Returns(false);
                    yield return new TestCaseData(new double[] { 11.0, 12.0, 0.0, 18.0 }, new double[] { 11.0, 12.0, 0.0, 18.0 }).Returns(true);
                    yield return new TestCaseData(new double[] { 11.0, 12.0, 0.0, 18.0 }, new double[] { 12.0, 11.0, 0.0, 18.0 }).Returns(false);
                }
            }
        }
    }
}
