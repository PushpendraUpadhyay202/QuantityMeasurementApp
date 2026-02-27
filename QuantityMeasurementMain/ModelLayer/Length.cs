namespace QuantityMeasurementMain.ModelLayer
{
    //generic class for single responsibility prociple
    public class Length
    {
        public double Value{get;}
        public LengthUnit Unit{get;}
        //enum for length units
        public enum LengthUnit
        {
            FEET,
            INCHES
        }

        public Length(double value,LengthUnit unit)
        {
            Value=value;
            Unit=unit;
        }

        public static double ConversionFactor(LengthUnit unit)
        {
            return unit switch
            {
                LengthUnit.FEET=>12.0,
                LengthUnit.INCHES=>1.0,
                _=>throw new Exception("invalid Length unit")
            };
        }
        //method for conversion to base unit inch
        public double ConvertToBaseUnit()
        {
            return Value * ConversionFactor(Unit);
        }
        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if(obj is not Length second)
            {
                return false;
            }
            return ConvertToBaseUnit().CompareTo(second.ConvertToBaseUnit())==0;
        }
        public override int GetHashCode()
        {
            return ConvertToBaseUnit().GetHashCode();
        }
    } 
}