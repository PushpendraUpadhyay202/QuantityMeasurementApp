using System.Runtime.CompilerServices;
using QuantityMeasurementMain.ModelLayer;
namespace QuantityMeasurementTest
{
    //Test cases for Inche Measurement Equality
    [TestClass]
    [TestCategory("InchEqualityTest")]
    public class InchEqualityMsTest
    {
        //Testing same Values
        [TestMethod]
        public void TestEquality_SameValue()
        {
            Inch inch1=new Inch(1.0);
            Inch inch2=new Inch(1.0);
            bool isEqual=inch1.Equals(inch2);
            Assert.IsTrue(isEqual,"value are equal");
        }
        //testing different values
        [TestMethod]
        public void TestEquality_DifferentValue()
        {
            Inch inch1=new Inch(1.0);
            Inch inch2=new Inch(2.0);
            bool isEqual=inch1.Equals(inch2);
            Assert.IsFalse(isEqual,"Values should be  same to be equal");
        }
        //testing value to null
        [TestMethod]
        public void TestEquality_NullComparison()
        {
            Inch inch1=new Inch(1.0);
            Inch inch2=null;
            bool isEqual=inch1.Equals(inch2);
            Assert.IsFalse(isEqual,"value is not equal to null");
        }
        //testing Inche type obj to different type
        [TestMethod]
        public void TestEquality_DifferentType()
        {
            Inch inch1=new Inch(1.0);
            bool isEqual=inch1.Equals("1.0");
            Assert.IsFalse(isEqual,"numeric value is not equal to non-numeric value");
        }
        //testing value to itself to check reflexivity
        [TestMethod]
        public void TestEquality_SameReference()
        {
            Inch inch1=new Inch(1.0);
            bool isEqual=inch1.Equals(inch1);
            Assert.IsTrue(isEqual,"value is always equal to itself");
        }
    }
}