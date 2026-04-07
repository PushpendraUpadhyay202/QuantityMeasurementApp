namespace RepoLayer.Interfaces
{
    /// <summary>
    /// Defines operations for saving quantity calculations to the database.
    /// </summary>
    public interface IQuantityRepository
    {
        void SaveMeasurement(
            string category,
            string operation,
            double value1,
            string unit1,
            double? value2,
            string unit2,
            double resultValue,
            string resultUnit);
    }
}