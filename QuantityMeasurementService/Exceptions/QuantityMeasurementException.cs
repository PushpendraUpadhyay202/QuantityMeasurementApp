using System;

namespace QuantityMeasurementServices.Exceptions
{
    public class QuantityMeasurementException : ApplicationException
    {
        public QuantityMeasurementException(string message) : base(message)
        {
        }
    }
}