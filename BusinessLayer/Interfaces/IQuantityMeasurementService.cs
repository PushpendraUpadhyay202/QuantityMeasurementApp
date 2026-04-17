using System;
using ModelLayer.Models;
using ModelLayer.DTOs;

namespace BusinessLayer.Interfaces
{
    /// <summary>
    /// Defines operations for working with Quantity objects.
    /// </summary>
    public interface IQuantityMeasurementService
    {
        QuantityResultDto Add(AddRequestDto request, int userId);
        QuantityResultDto Subtract(SubtractRequestDto request, int userId);
        DivisionResultDto Divide(DivideRequestDto request, int userId);
        ComparisonResultDto Compare(ComparisonRequestDto request, int userId);
        QuantityResultDto Convert(ConversionRequestDto request, int userId);
        ComparisonResultDto Compare<U>(Quantity<U> firstQuantity, Quantity<U> secondQuantity, int userId)
            where U : struct, Enum;

        QuantityResultDto DemonstrateConversion<U>(double numericValue, U sourceType, U targetType, int userId)
            where U : struct, Enum;

        QuantityResultDto DemonstrateConversion<U>(Quantity<U> originalQuantity, U desiredUnit, int userId)
            where U : struct, Enum;

        QuantityResultDto DemonstrateAddition<U>(Quantity<U> leftOperand, Quantity<U> rightOperand, int userId)
            where U : struct, Enum;

        QuantityResultDto DemonstrateAddition<U>(Quantity<U> leftOperand, Quantity<U> rightOperand, U resultUnit, int userId)
            where U : struct, Enum;

        QuantityResultDto Subtract<U>(Quantity<U> firstValue, Quantity<U> secondValue, U resultUnit, int userId)
            where U : struct, Enum;

        DivisionResultDto Divide<T>(Quantity<T> dividend, Quantity<T> divisor, int userId)
            where T : struct, Enum;

        QuantityMeasurementHistoryDto GetMeasurmentsHistory(int userId);
    }
}