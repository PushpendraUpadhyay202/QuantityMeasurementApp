using QuantityMeasurementMain.BusinessLayer;
using QuantityMeasurementMain.ModelLayer;

namespace QuantityMeasurementTest
{
    [TestClass]
    [TestCategory("UnitConversion")]
    public class UnitConversionMsTest
    {
        [TestMethod]
        public void TestConversion_FeetToInches()
        {
            var length=new Length(1.0, Length.LengthUnit.FEET);
            Length result = LengthEqualityUtility.ConvertToOtherUnit(length, Length.LengthUnit.INCHES);
            Assert.AreEqual(12.0, result.Value, 1e-6);
        }

        [TestMethod]
        public void TestConversion_CentimetersToInches()
        {
            var length=new Length(2.54,Length.LengthUnit.CENTIMETER);
            var result =LengthEqualityUtility.ConvertToOtherUnit(length, Length.LengthUnit.INCHES);
            Assert.AreEqual(1.0, result.Value, 1e-6);
        }

        [TestMethod]
        public void TestConversion_RoundTrip_PreservesValue()
        {
            double original = 5.5;
            Length length1=new Length(original,Length.LengthUnit.YARDS);
            var feet =LengthEqualityUtility.ConvertToOtherUnit(length1, Length.LengthUnit.FEET);
            var yards =LengthEqualityUtility.ConvertToOtherUnit(feet, Length.LengthUnit.YARDS);
            Assert.AreEqual(original, yards.Value, 1e-6);
        }
        [TestMethod]
        public void TestConversion_ZeroValue()
        {
            Length length=new Length(0.0,Length.LengthUnit.FEET);
            var result=LengthEqualityUtility.ConvertToOtherUnit(length,Length.LengthUnit.INCHES);
            Assert.AreEqual(0.0,result.Value,1e-6);
        }
        [TestMethod]
        public void TestConversion_NegativeValue()
        {
            Length length=new Length(-1.0,Length.LengthUnit.FEET);
            var result=LengthEqualityUtility.ConvertToOtherUnit(length,Length.LengthUnit.INCHES);
            Assert.AreEqual(-12.0,result.Value,1e-6);
        }
    }
}