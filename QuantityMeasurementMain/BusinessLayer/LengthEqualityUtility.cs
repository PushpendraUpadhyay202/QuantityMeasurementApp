using QuantityMeasurementMain.ModelLayer;

namespace QuantityMeasurementMain.BusinessLayer
{
    public class LengthEqualityUtility
    {
        public  static bool  CheckEquality(double input1, Length.LengthUnit unit1,double input2, Length.LengthUnit unit2)
        {
            Length length1 = new Length(input1, unit1);
            Length length2 = new Length(input2, unit2);

            return length1.Equals(length2);
        }
    }
}