using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementMain.ModelLayer;

namespace QuantityMeasurementTest
{
    [TestClass]
    [TestCategory("LengthEquality")]
    public class LengthEqualityMsTest
    {
        [TestMethod]
        public void TestEquality_FeetToFeet_SameValue()
        {
            var q1 = new Length(1.0, Length.LengthUnit.FEET);
            var q2 = new Length(1.0, Length.LengthUnit.FEET);

            Assert.IsTrue(q1.Equals(q2));
        }

        [TestMethod]
        public void TestEquality_INCHESToINCHES_SameValue()
        {
            var q1 = new Length(1.0, Length.LengthUnit.INCHES);
            var q2 = new Length(1.0, Length.LengthUnit.INCHES);

            Assert.IsTrue(q1.Equals(q2));
        }

        [TestMethod]
        public void TestEquality_FeetToINCHES_EquivalentValue()
        {
            var q1 = new Length(1.0, Length.LengthUnit.FEET);
            var q2 = new Length(12.0, Length.LengthUnit.INCHES);

            Assert.IsTrue(q1.Equals(q2));
        }

        [TestMethod]
        public void TestEquality_INCHESToFeet_EquivalentValue()
        {
            var q1 = new Length(12.0, Length.LengthUnit.INCHES);
            var q2 = new Length(1.0, Length.LengthUnit.FEET);

            Assert.IsTrue(q1.Equals(q2));
        }

        [TestMethod]
        public void TestEquality_FeetToFeet_DifferentValue()
        {
            var q1 = new Length(1.0, Length.LengthUnit.FEET);
            var q2 = new Length(2.0, Length.LengthUnit.FEET);

            Assert.IsFalse(q1.Equals(q2));
        }

        [TestMethod]
        public void TestEquality_INCHESToINCHES_DifferentValue()
        {
            var q1 = new Length(1.0, Length.LengthUnit.INCHES);
            var q2 = new Length(2.0, Length.LengthUnit.INCHES);

            Assert.IsFalse(q1.Equals(q2));
        }

        [TestMethod]
        public void TestEquality_NullComparison()
        {
            var q1 = new Length(1.0, Length.LengthUnit.FEET);

            Assert.IsFalse(q1.Equals(null));
        }

        [TestMethod]
        public void TestEquality_SameReference()
        {
            var q1 = new Length(1.0, Length.LengthUnit.FEET);

            Assert.IsTrue(q1.Equals(q1));
        }
        //test methods for new units (yard and centimeter)
        [TestMethod]
        public void TestEquality_YardToInch()
        {
            var q1=new Length(1.0,Length.LengthUnit.YARDS);
            var q2=new Length(36.0,Length.LengthUnit.INCHES);
            Assert.IsTrue(q1.Equals(q2));
        }
        [TestMethod]
        public void TestEquality_CentimeterToInch()
        {
            var q1=new Length(100,Length.LengthUnit.CENTIMETER);
            var q2=new Length(39.3701,Length.LengthUnit.INCHES);
            Assert.IsTrue(q1.Equals(q2));
        }
        [TestMethod]
        public void TestEquality_FeetToYard()
        {
            var q1=new Length(3.0,Length.LengthUnit.FEET);
            var q2=new Length(1.0,Length.LengthUnit.YARDS);
            Assert.IsTrue(q1.Equals(q2));
        }
        [TestMethod]
        public void TestEquality_CentimeterToFeet()
        {
            var q1=new Length(30.48,Length.LengthUnit.CENTIMETER);
            var q2=new Length(1.0,Length.LengthUnit.FEET);
            Assert.IsTrue(q1.Equals(q2));
        }
        [TestMethod]
        public void TestEquality_YardNotToInch()
        {
            var q1=new Length(1.0,Length.LengthUnit.YARDS);
            var q2=new Length(38.0,Length.LengthUnit.INCHES);
            Assert.IsFalse(q1.Equals(q2));
        }
        [TestMethod]
        public void TestEquality_MultiUnit_TransitiveProperty()
        {
            var yard = new Length(1.0, Length.LengthUnit.YARDS);
            var feet = new Length(3.0, Length.LengthUnit.FEET);
            var inches = new Length(36.0, Length.LengthUnit.INCHES);
            Assert.IsTrue(yard.Equals(feet));
            Assert.IsTrue(feet.Equals(inches));
            Assert.IsTrue(yard.Equals(inches));
        }
        [TestMethod]
        public void TestEquality_AllUnits_ComplexScenario()
        {
            var yards = new Length(2.0, Length.LengthUnit.YARDS);
            var feet = new Length(6.0, Length.LengthUnit.FEET);
            var inches = new Length(72.0, Length.LengthUnit.INCHES);
            Assert.IsTrue(yards.Equals(feet));
            Assert.IsTrue(feet.Equals(inches));
            Assert.IsTrue(yards.Equals(inches));
        }
    }
}