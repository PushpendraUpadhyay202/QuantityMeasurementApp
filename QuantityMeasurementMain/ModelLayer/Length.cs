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
            INCHES,
            YARDS,//Added more  Units
            CENTIMETER
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
                LengthUnit.YARDS=>36.0,//conversion factor for new units
                LengthUnit.CENTIMETER=>0.393701,
                _=>throw new Exception("invalid Length unit")
            };
        }
        //method for conversion to base unit inch
        public double ConvertToBaseUnit()
        {
            double result=Value * ConversionFactor(Unit);
            return result;
        }
        private const double Tolerence=0.0001;//for floating comparison
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
            return Math.Abs(ConvertToBaseUnit()-second.ConvertToBaseUnit())<Tolerence;
        }
        public override int GetHashCode()
        {
            return ConvertToBaseUnit().GetHashCode();
        }
    } 
}