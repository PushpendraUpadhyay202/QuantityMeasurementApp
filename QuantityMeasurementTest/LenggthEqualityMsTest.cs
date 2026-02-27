using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementMain.ModelLayer;

namespace QuantityMeasurementTest
{
    [TestClass]
    [TestCategory("LengthEquality")]
    public class LengthEqualityMsTest
    {
        [TestMethod]
        public void testEquality_FeetToFeet_SameValue()
        {
            var q1 = new Length(1.0, Length.LengthUnit.FEET);
            var q2 = new Length(1.0, Length.LengthUnit.FEET);

            Assert.IsTrue(q1.Equals(q2));
        }

        [TestMethod]
        public void testEquality_INCHESToINCHES_SameValue()
        {
            var q1 = new Length(1.0, Length.LengthUnit.INCHES);
            var q2 = new Length(1.0, Length.LengthUnit.INCHES);

            Assert.IsTrue(q1.Equals(q2));
        }

        [TestMethod]
        public void testEquality_FeetToINCHES_EquivalentValue()
        {
            var q1 = new Length(1.0, Length.LengthUnit.FEET);
            var q2 = new Length(12.0, Length.LengthUnit.INCHES);

            Assert.IsTrue(q1.Equals(q2));
        }

        [TestMethod]
        public void testEquality_INCHESToFeet_EquivalentValue()
        {
            var q1 = new Length(12.0, Length.LengthUnit.INCHES);
            var q2 = new Length(1.0, Length.LengthUnit.FEET);

            Assert.IsTrue(q1.Equals(q2));
        }

        [TestMethod]
        public void testEquality_FeetToFeet_DifferentValue()
        {
            var q1 = new Length(1.0, Length.LengthUnit.FEET);
            var q2 = new Length(2.0, Length.LengthUnit.FEET);

            Assert.IsFalse(q1.Equals(q2));
        }

        [TestMethod]
        public void testEquality_INCHESToINCHES_DifferentValue()
        {
            var q1 = new Length(1.0, Length.LengthUnit.INCHES);
            var q2 = new Length(2.0, Length.LengthUnit.INCHES);

            Assert.IsFalse(q1.Equals(q2));
        }

        [TestMethod]
        public void testEquality_NullComparison()
        {
            var q1 = new Length(1.0, Length.LengthUnit.FEET);

            Assert.IsFalse(q1.Equals(null));
        }

        [TestMethod]
        public void testEquality_SameReference()
        {
            var q1 = new Length(1.0, Length.LengthUnit.FEET);

            Assert.IsTrue(q1.Equals(q1));
        }
    }
}