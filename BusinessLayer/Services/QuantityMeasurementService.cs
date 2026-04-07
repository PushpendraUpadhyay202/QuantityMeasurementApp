using System;
using ModelLayer.Models;
using ModelLayer.Enums;
using ModelLayer.Interfaces;
using ModelLayer.DTOs;
using RepoLayer.Interfaces;
using RepoLayer.Repositories;
using BusinessLayer.Interfaces;

namespace BusinessLayer.Services
{
    public class QuantityMeasurementService : IQuantityMeasurementService
    {
        private readonly IQuantityRepository repository;

        public QuantityMeasurementService(string connectionString)
        {
            repository = new QuantityRepository(connectionString);
        }

        private IUnitConverter<T> ResolveConverter<T>() where T : struct, Enum
        {
            if (typeof(T) == typeof(LengthUnit))
                return (IUnitConverter<T>)(object)new LengthUnitConverter();

            if (typeof(T) == typeof(WeightUnit))
                return (IUnitConverter<T>)(object)new WeightUnitConverter();

            if (typeof(T) == typeof(VolumeUnit))
                return (IUnitConverter<T>)(object)new VolumeUnitConverter();

            if (typeof(T) == typeof(TemperatureUnit))
                return (IUnitConverter<T>)(object)new TemperatureUnitConverter();

            throw new NotSupportedException();
        }

        private QuantityResultDto MapQuantityToDto<T>(Quantity<T> quantity) where T : struct, Enum
        {
            return new QuantityResultDto
            {
                Value = quantity.Value,
                UnitSymbol = quantity.ToString().Split(' ')[1]
            };
        }

        public ComparisonResultDto Compare<U>(Quantity<U> firstQuantity, Quantity<U> secondQuantity)
            where U : struct, Enum
        {
            return new ComparisonResultDto
            {
                AreEqual = firstQuantity.Equals(secondQuantity)
            };
        }

        public QuantityResultDto DemonstrateConversion<U>(Quantity<U> originalQuantity, U desiredUnit)
            where U : struct, Enum
        {
            var result = originalQuantity.ConvertTo(desiredUnit);

            repository.SaveMeasurement(
                typeof(U).Name,
                "Conversion",
                originalQuantity.Value,
                originalQuantity.Unit.ToString(),
                null,
                null,
                result.Value,
                result.Unit.ToString());

            return MapQuantityToDto(result);
        }

        public QuantityResultDto DemonstrateAddition<U>(Quantity<U> leftOperand, Quantity<U> rightOperand)
            where U : struct, Enum
        {
            var result = leftOperand.Add(rightOperand);

            repository.SaveMeasurement(
                typeof(U).Name,
                "Addition",
                leftOperand.Value,
                leftOperand.Unit.ToString(),
                rightOperand.Value,
                rightOperand.Unit.ToString(),
                result.Value,
                result.Unit.ToString());

            return MapQuantityToDto(result);
        }

        public QuantityResultDto Subtract<U>(Quantity<U> firstValue, Quantity<U> secondValue, U resultUnit)
            where U : struct, Enum
        {
            var result = firstValue.Subtract(secondValue, resultUnit);

            repository.SaveMeasurement(
                typeof(U).Name,
                "Subtraction",
                firstValue.Value,
                firstValue.Unit.ToString(),
                secondValue.Value,
                secondValue.Unit.ToString(),
                result.Value,
                result.Unit.ToString());

            return MapQuantityToDto(result);
        }

        public DivisionResultDto Divide<T>(double firstValue, T firstUnit, double secondValue, T secondUnit)
            where T : struct, Enum
        {
            var converter = ResolveConverter<T>();

            Quantity<T> dividend = new Quantity<T>(firstValue, firstUnit, converter);
            Quantity<T> divisor = new Quantity<T>(secondValue, secondUnit, converter);

            double result = dividend.Divide(divisor);

            repository.SaveMeasurement(
                typeof(T).Name,
                "Division",
                firstValue,
                firstUnit.ToString(),
                secondValue,
                secondUnit.ToString(),
                result,
                "ratio");

            return new DivisionResultDto
            {
                Ratio = result
            };
        }

        public QuantityResultDto DemonstrateConversion<U>(double numericValue, U sourceType, U targetType)
    where U : struct, Enum
        {
            var converter = ResolveConverter<U>();

            Quantity<U> quantity = new Quantity<U>(numericValue, sourceType, converter);

            Quantity<U> converted = quantity.ConvertTo(targetType);

            repository.SaveMeasurement(
                typeof(U).Name,
                "Conversion",
                numericValue,
                sourceType.ToString(),
                null,
                null,
                converted.Value,
                converted.Unit.ToString());

            return MapQuantityToDto(converted);
        }

        public QuantityResultDto DemonstrateAddition<U>(
    Quantity<U> leftOperand,
    Quantity<U> rightOperand,
    U resultUnit)
    where U : struct, Enum
        {
            Quantity<U> result = leftOperand.Add(rightOperand, resultUnit);

            repository.SaveMeasurement(
                typeof(U).Name,
                "Addition",
                leftOperand.Value,
                leftOperand.Unit.ToString(),
                rightOperand.Value,
                rightOperand.Unit.ToString(),
                result.Value,
                result.Unit.ToString());

            return MapQuantityToDto(result);
        }
    }
}