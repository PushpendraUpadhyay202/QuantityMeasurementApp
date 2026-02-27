//QuantityMeasurmentApp - UC1 Feet measurement equality

namespace QuantityMeasurementMain.ModelLayer
{
   // encapsulated class for Feet 
    public class Feet
    {
        private readonly double _Value;
        public Feet(double value){
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
            if(obj is not Feet feet2)
            {
                return false;
            }
            return Value.CompareTo(feet2.Value)==0;
        }
    }
}