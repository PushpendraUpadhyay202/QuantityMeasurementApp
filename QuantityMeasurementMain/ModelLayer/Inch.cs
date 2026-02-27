//QuantityMeasurmentApp - UC2 Inches measurement equality

namespace QuantityMeasurementMain.ModelLayer
{
   // encapsulated class for Inche 
    public class Inch
    {
        private readonly double _Value;
        public Inch(double value){
            _Value=value;
        }
        public double Value
        {
            get
            {
                return _Value;
            }
        }
        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(this,obj))
            {
                return true;
            }
            if(obj is not Inch inch2)
            {
                return false;
            }
            return Value.CompareTo(inch2.Value)==0;
        }
    }
}