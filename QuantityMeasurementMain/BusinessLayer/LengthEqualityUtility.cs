using QuantityMeasurementMain.ModelLayer;

namespace QuantityMeasurementMain.BusinessLayer
{
    public class LengthEqualityUtility
    {
        public  static bool  CheckLengthEquality(double input1, Length.LengthUnit unit1,double input2, Length.LengthUnit unit2)
        {
            Length length1 = new Length(input1, unit1);
            Length length2 = new Length(input2, unit2);

            return length1.Equals(length2);
        }
        //method for conversion to other unit
        public static Length ConvertToOtherUnit(Length length,Length.LengthUnit targetUnit)
        {
            if (double.IsNaN(length.Value) || double.IsInfinity(length.Value))
            {
                throw new Exception("invalid value");     
            }    
            double newValue=Length.ConvertToBaseUnit(length);
            newValue=newValue/Length.ConversionFactor(targetUnit);
            return new Length(newValue,targetUnit);
        }
    }
}