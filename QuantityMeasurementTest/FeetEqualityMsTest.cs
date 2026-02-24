using System.Runtime.CompilerServices;
using QuantityMeasurementMain.ModelLayer;
namespace QuantityMeasurementTest
{
    [TestClass]
    public class FeetEqualityMsTest
    {
        //Testing same Values
        [TestMethod]
        public void TestEquality_SameValue()
        {
            Feet feet1=new Feet(1.0);
            Feet feet2=new Feet(1.0);
            bool isEqual=feet1.Equals(feet2);
            Assert.IsTrue(isEqual,"value are equal");
        }
        //testing different values
        [TestMethod]
        public void TestEquality_DifferentValue()
        {
            Feet feet1=new Feet(1.0);
            Feet feet2=new Feet(2.0);
            bool isEqual=feet1.Equals(feet2);
            Assert.IsFalse(isEqual,"Values should be  same to be equal");
        }
        //testing value to null
        [TestMethod]
        public void TestEquality_NullComparison()
        {
            Feet feet1=new Feet(1.0);
            Feet feet2=null;
            bool isEqual=feet1.Equals(feet2);
            Assert.IsFalse(isEqual,"value is not equal to null");
        }
        //testing feet type obj to different type
        [TestMethod]
        public void TestEquality_DifferentType()
        {
            Feet feet1=new Feet(1.0);
            bool isEqual=feet1.Equals("1.0");
            Assert.IsFalse(isEqual,"numeric value is not equal to non-numeric value");
        }
        //testing value to itself to check reflexivity
        [TestMethod]
        public void TestEquality_SameReference()
        {
            Feet feet1=new Feet(1.0);
            bool isEqual=feet1.Equals(feet1);
            Assert.IsTrue(isEqual,"value is always equal to itself");
        }
    }
}