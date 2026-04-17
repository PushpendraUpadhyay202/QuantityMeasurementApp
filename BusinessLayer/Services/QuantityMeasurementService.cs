using System;
using ModelLayer.Models;
using ModelLayer.Enums;
using ModelLayer.Interfaces;
using ModelLayer.DTOs;
using BusinessLayer.Interfaces;
using DataAccessLayer.Data;

namespace BusinessLayer.Services
{
    public class QuantityMeasurementService : IQuantityMeasurementService
    {

        private AppDbContext _context;

        public QuantityMeasurementService(AppDbContext context)
        {
            _context = context;
        }
        public QuantityResultDto Add(AddRequestDto request, int userId)
        {
            switch (request.QuantityType)
            {
                case "Length":
                    return AddInternal<LengthUnit>(request, userId);
                case "Weight":
                    return AddInternal<WeightUnit>(request, userId);
                case "Volume":
                    return AddInternal<VolumeUnit>(request, userId);
                case "Temperature":
                    return AddInternal<TemperatureUnit>(request, userId);
                default:
                    throw new NotSupportedException("Invalid quantity type");
            }
        }

        public QuantityResultDto AddInternal<T>(AddRequestDto request, int userId) where T : struct, Enum
        {
            var converter = ResolveConverter<T>();

            var q1 = new Quantity<T>(request.Value1, Enum.Parse<T>(request.Unit1, true), converter);
            var q2 = new Quantity<T>(request.Value2, Enum.Parse<T>(request.Unit2, true), converter);

            return DemonstrateAddition(q1, q2, userId);
        }

        public QuantityResultDto Subtract(SubtractRequestDto request, int userId)
        {
            switch (request.QuantityType)
            {
                case "Length":
                    return SubtractInternal<LengthUnit>(request, userId);
                case "Weight":
                    return SubtractInternal<WeightUnit>(request, userId);
                case "Volume":
                    return SubtractInternal<VolumeUnit>(request, userId);
                case "Temperature":
                    return SubtractInternal<TemperatureUnit>(request, userId);
                default:
                    throw new NotSupportedException("Invalid quantity type");
            }
        }

        public QuantityResultDto SubtractInternal<T>(SubtractRequestDto request, int userId) where T : struct, Enum
        {
            var converter = ResolveConverter<T>();

            var q1 = new Quantity<T>(request.Value1, Enum.Parse<T>(request.Unit1, true), converter);
            var q2 = new Quantity<T>(request.Value2, Enum.Parse<T>(request.Unit2, true), converter);
            var resultUnit = Enum.Parse<T>(request.ResultUnit);

            return Subtract(q1, q2, resultUnit, userId);
        }

        public DivisionResultDto Divide(DivideRequestDto request, int userId)
        {
            switch (request.QuantityType)
            {
                case "Length":
                    return DivideInternal<LengthUnit>(request, userId);
                case "Weight":
                    return DivideInternal<WeightUnit>(request, userId);
                case "Volume":
                    return DivideInternal<VolumeUnit>(request, userId);
                case "Temperature":
                    return DivideInternal<TemperatureUnit>(request, userId);
                default:
                    throw new NotSupportedException("Invalid quantity type");
            }
        }

        public DivisionResultDto DivideInternal<T>(DivideRequestDto request, int userId) where T : struct, Enum
        {
            var converter = ResolveConverter<T>();

            var q1 = new Quantity<T>(request.Value1, Enum.Parse<T>(request.Unit1, true), converter);
            var q2 = new Quantity<T>(request.Value2, Enum.Parse<T>(request.Unit2, true), converter);

            return Divide(q1, q2, userId);
        }

        public QuantityResultDto Convert(ConversionRequestDto request, int userId)
        {
            switch (request.QuantityType)
            {
                case "Length":
                    return ConvertInteral<LengthUnit>(request, userId);
                case "Weight":
                    return ConvertInteral<WeightUnit>(request, userId);
                case "Volume":
                    return ConvertInteral<VolumeUnit>(request, userId);
                case "Temperature":
                    return ConvertInteral<TemperatureUnit>(request, userId);
                default:
                    throw new NotSupportedException("Invalid quantity type");
            }
        }

        public QuantityResultDto ConvertInteral<T>(ConversionRequestDto request, int userId) where T: struct, Enum
        {
            var converter = ResolveConverter<T>();

            var quantity = new Quantity<T>(request.Value, Enum.Parse<T>(request.SourceUnit, true), converter);

            var desiredUnit = Enum.Parse<T>(request.TargetUnit);

            return DemonstrateConversion(quantity, desiredUnit, userId);
        }

        public ComparisonResultDto Compare(ComparisonRequestDto request, int userId)
        {
            switch (request.QuantityType)
            {
                case "Length":
                    return CompareInternal<LengthUnit>(request, userId);
                case "Weight":
                    return CompareInternal<WeightUnit>(request, userId);
                case "Volume":
                    return CompareInternal<VolumeUnit>(request, userId);
                case "Temperature":
                    return CompareInternal<TemperatureUnit>(request, userId);
                default:
                    throw new NotSupportedException("Invalid quantity type");
            }
        }

        public ComparisonResultDto CompareInternal<T>(ComparisonRequestDto request, int userId) where T : struct, Enum
        {
            var converter = ResolveConverter<T>();

            var q1 = new Quantity<T>(request.Value1, Enum.Parse<T>(request.Unit1, true), converter);
            var q2 = new Quantity<T>(request.Value2, Enum.Parse<T>(request.Unit2, true), converter);

            return Compare(q1, q2, userId);
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

        public ComparisonResultDto Compare<U>(Quantity<U> firstQuantity, Quantity<U> secondQuantity, int userId) where U : struct, Enum
        {
            var areEqual = firstQuantity.Equals(secondQuantity);

            var entity = new QuantityMeasurement
            {
                UserId = userId,
                Category = typeof(U).Name,
                Operation = "Comparison",
                Value1 = firstQuantity.Value,
                Unit1 = firstQuantity.Unit.ToString(),
                Value2 = secondQuantity.Value,
                Unit2 = secondQuantity.Unit.ToString(),
                ResultValue = areEqual ? 1 : 0,
                ResultUnit = "Boolean",
                CreatedAt = DateTime.Now
            };

            _context.QuantityMeasurements.Add(entity);
            _context.SaveChanges();

            return new ComparisonResultDto
            {
                AreEqual = areEqual
            };
        }

        public QuantityResultDto DemonstrateConversion<U>(Quantity<U> originalQuantity, U desiredUnit, int userId) where U : struct, Enum
        {
            var result = originalQuantity.ConvertTo(desiredUnit);

            var entity = new QuantityMeasurement
            {
                UserId = userId,
                Category = typeof(U).Name,
                Operation = "Conversion",
                Value1 = originalQuantity.Value,
                Unit1 = originalQuantity.Unit.ToString(),
                Value2 = null,
                Unit2 = null,
                ResultValue = result.Value,
                ResultUnit = result.Unit.ToString(),
                CreatedAt = DateTime.Now
            };

            _context.QuantityMeasurements.Add(entity);
            _context.SaveChanges();

            return MapQuantityToDto(result);
        }

        public QuantityResultDto DemonstrateAddition<U>(Quantity<U> leftOperand, Quantity<U> rightOperand, int userId) where U : struct, Enum
        {
            var result = leftOperand.Add(rightOperand);

            var entity = new QuantityMeasurement
            {
                UserId = userId,
                Category = typeof(U).Name,
                Operation = "Addition",
                Value1 = leftOperand.Value,
                Unit1 = leftOperand.Unit.ToString(),
                Value2 = rightOperand.Value,
                Unit2 = rightOperand.Unit.ToString(),
                ResultValue = result.Value,
                ResultUnit = result.Unit.ToString(),
                CreatedAt = DateTime.Now
            };

            _context.QuantityMeasurements.Add(entity);
            _context.SaveChanges();

            return MapQuantityToDto(result);
        }

        public QuantityResultDto Subtract<U>(Quantity<U> firstValue, Quantity<U> secondValue, U resultUnit, int userId) where U : struct, Enum
        {
            var result = firstValue.Subtract(secondValue, resultUnit);

            var entity = new QuantityMeasurement
            {
                UserId = userId,
                Category = typeof(U).Name,
                Operation = "Subtraction",
                Value1 = firstValue.Value,
                Unit1 = firstValue.Unit.ToString(),
                Value2 = secondValue.Value,
                Unit2 = secondValue.Unit.ToString(),
                ResultValue = result.Value,
                ResultUnit = result.Unit.ToString(),
                CreatedAt = DateTime.Now
            };

            _context.QuantityMeasurements.Add(entity);
            _context.SaveChanges();

            return MapQuantityToDto(result);
        }

        public DivisionResultDto Divide<T>(Quantity<T> dividend, Quantity<T> divisor, int userId) where T : struct, Enum
        {
            double result = dividend.Divide(divisor);

            var entity = new QuantityMeasurement
            {
                UserId = userId,
                Category = typeof(T).Name,
                Operation = "Division",
                Value1 = dividend.Value,
                Unit1 = dividend.Unit.ToString(),
                Value2 = divisor.Value,
                Unit2 = divisor.Unit.ToString(),
                ResultValue = result,
                ResultUnit = "ratio",
                CreatedAt = DateTime.Now
            };

            _context.QuantityMeasurements.Add(entity);
            _context.SaveChanges();

            return new DivisionResultDto
            {
                Ratio = result
            };
        }

        public QuantityResultDto DemonstrateConversion<U>(double numericValue, U sourceType, U targetType, int userId)
    where U : struct, Enum
        {
            var converter = ResolveConverter<U>();

            Quantity<U> quantity = new Quantity<U>(numericValue, sourceType, converter);

            Quantity<U> converted = quantity.ConvertTo(targetType);

            var entity = new QuantityMeasurement
            {
                UserId = userId,
                Category = typeof(U).Name,
                Operation = "Conversion",
                Value1 = numericValue,
                Unit1 = sourceType.ToString(),
                Value2 = null,
                Unit2 = null,
                ResultValue = converted.Value,
                ResultUnit = converted.Unit.ToString(),
                CreatedAt = DateTime.Now
            };

            _context.QuantityMeasurements.Add(entity);
            _context.SaveChanges();

            return MapQuantityToDto(converted);
        }

        public QuantityResultDto DemonstrateAddition<U>(Quantity<U> leftOperand, Quantity<U> rightOperand, U resultUnit, int userId)
    where U : struct, Enum
        {
            Quantity<U> result = leftOperand.Add(rightOperand, resultUnit);

            var entity = new QuantityMeasurement
            {
                UserId = userId,
                Category = typeof(U).Name,
                Operation = "Addition",
                Value1 = leftOperand.Value,
                Unit1 = leftOperand.Unit.ToString(),
                Value2 = rightOperand.Value,
                Unit2 = rightOperand.Unit.ToString(),
                ResultValue = result.Value,
                ResultUnit = result.Unit.ToString(),
                CreatedAt = DateTime.Now
            };

            _context.QuantityMeasurements.Add(entity);
            _context.SaveChanges();

            return MapQuantityToDto(result);
        }
    
        public QuantityMeasurementHistoryDto GetMeasurmentsHistory(int userId)
        {
            var measurements = _context.QuantityMeasurements
                               .Where(q => q.UserId == userId)
                               .Select(q => new QuantityMeasurementDto
                               {
                                   Id = q.Id,
                                   Category = q.Category,
                                   Operation = q.Operation,
                                   Value1 = q.Value1,
                                   Unit1 = q.Unit1,
                                   Value2 = q.Value2,
                                   Unit2 = q.Unit2,
                                   ResultValue = q.ResultValue,
                                   ResultUnit = q.ResultUnit,
                                   CreatedAt = q.CreatedAt
                               })
                               .ToList();

            return new QuantityMeasurementHistoryDto
            {
                Measurements = measurements
            };
        }
    }
}