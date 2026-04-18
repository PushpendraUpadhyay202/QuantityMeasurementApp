using QuantityMeasurementServices.Exceptions;
using QuantityMeasurementServices.Interfaces;
using QuantityMeasurementModel.DTOs;
using QuantityMeasurementModel.Models;
using QuantityMeasurementData.Interfaces;

namespace QuantityMeasurementServices.Services
{
    public class QuantityMeasurementService : IQuantityMeasurementService
    {
        private readonly IQuantityRepository _repository;

        public QuantityMeasurementService(IQuantityRepository repository)
        {
            _repository = repository;
        }

        public bool Compare(QuantityDTO first, QuantityDTO second, string? userId = null)
        {
            Validate(first); Validate(second);
            EnsureSameCategory(first, second, "compare");
            double a = ConvertToBase(first);
            double b = ConvertToBase(second);
            bool result = Math.Abs(a - b) < 1e-6;
            PersistWithOperation(first, second, "Compare", result ? 1 : 0, "Boolean", userId);
            return result;
        }

        public QuantityDTO Convert(QuantityDTO source, string targetUnit)
        {
            Validate(source);
            double baseValue = ConvertToBase(source);
            double convertedValue = ConvertFromBase(baseValue, source.Category, targetUnit);
            Persist(source);
            return new QuantityDTO { Value = convertedValue, Unit = targetUnit, Category = source.Category };
        }

        public QuantityDTO Add(QuantityDTO first, QuantityDTO second, string targetUnit = null, string? userId = null)
        {
            var result = ApplyArithmetic(first, second, "addition", (a, b) => a + b, targetUnit);
            PersistWithOperation(first, second, "Addition", result.Value, result.Unit, userId);
            return result;
        }

        public QuantityDTO Subtract(QuantityDTO first, QuantityDTO second, string targetUnit = null, string? userId = null)
        {
            var result = ApplyArithmetic(first, second, "subtraction", (a, b) => a - b, targetUnit);
            PersistWithOperation(first, second, "Subtraction", result.Value, result.Unit, userId);
            return result;
        }

        public double Divide(QuantityDTO first, QuantityDTO second, string? userId = null)
        {
            Validate(first); Validate(second);
            EnsureSameCategory(first, second, "division");
            EnsureArithmeticAllowed(first, "division");
            double a = ConvertToBase(first);
            double b = ConvertToBase(second);
            if (Math.Abs(b) < 1e-12)
                throw new QuantityMeasurementException("Division by zero is not allowed.");
            double result = a / b;
            PersistWithOperation(first, second, "Division", result, "Ratio", userId);
            return result;
        }

        public HistoryResponse GetFullHistory()
        {
            return new HistoryResponse
            {
                Quantities = _repository.GetAll(),
                Operations = _repository.GetOperations()
            };
        }

        public HistoryResponse GetHistoryByUser(string userId)
        {
            var operations = _repository.GetOperationsByUser(userId).ToList();
            var quantityIds = operations
                .SelectMany(o => new[] { o.FirstQuantityId, o.SecondQuantityId })
                .Distinct().ToList();
            var quantities = _repository.GetAll()
                .Where(q => quantityIds.Contains(q.Id)).ToList();
            return new HistoryResponse { Quantities = quantities, Operations = operations };
        }

        public void DeleteAllRecords() => _repository.DeleteAll();

        public void DeleteRecordsByUser(string userId) => _repository.DeleteByUser(userId);

        // ─── ARITHMETIC HELPER ────────────────────────────
        private QuantityDTO ApplyArithmetic(QuantityDTO first, QuantityDTO second, string operationName, Func<double, double, double> operation, string targetUnit = null)
        {
            Validate(first); Validate(second);
            EnsureSameCategory(first, second, operationName);
            EnsureArithmeticAllowed(first, operationName);

            if (!string.IsNullOrWhiteSpace(targetUnit))
            {
                double a = ConvertToBase(first);
                double b = ConvertToBase(second);
                double resultBase = operation(a, b);
                double converted = ConvertFromBase(resultBase, first.Category, targetUnit);
                return new QuantityDTO { Value = converted, Unit = targetUnit, Category = first.Category };
            }

            if (!first.Unit.Equals(second.Unit, StringComparison.OrdinalIgnoreCase))
                throw new QuantityMeasurementException($"Units must match when no target unit is provided. Got '{first.Unit}' and '{second.Unit}'.");

            return new QuantityDTO { Value = operation(first.Value, second.Value), Unit = first.Unit, Category = first.Category };
        }

        // ─── CONVERSION ───────────────────────────────────
        private double ConvertToBase(QuantityDTO dto)
        {
            string category = dto.Category.Trim().ToLowerInvariant();
            string unit = dto.Unit.Trim().ToLowerInvariant();
            return category switch
            {
                "length" => unit switch
                {
                    "inch" => dto.Value, "feet" => dto.Value * 12.0,
                    "yard" => dto.Value * 36.0, "centimeter" => dto.Value / 2.54,
                    _ => throw new QuantityMeasurementException($"Unsupported length unit: '{dto.Unit}'.")
                },
                "weight" => unit switch
                {
                    "gram" => dto.Value, "kilogram" => dto.Value * 1000.0,
                    "pound" => dto.Value * 453.592,
                    _ => throw new QuantityMeasurementException($"Unsupported weight unit: '{dto.Unit}'.")
                },
                "volume" => unit switch
                {
                    "milliliter" or "millilitre" => dto.Value,
                    "liter" or "litre" => dto.Value * 1000.0,
                    "gallon" => dto.Value * 3785.41,
                    _ => throw new QuantityMeasurementException($"Unsupported volume unit: '{dto.Unit}'.")
                },
                "temperature" => unit switch
                {
                    "celsius" => dto.Value, "fahrenheit" => (dto.Value - 32.0) * 5.0 / 9.0,
                    "kelvin" => dto.Value - 273.15,
                    _ => throw new QuantityMeasurementException($"Unsupported temperature unit: '{dto.Unit}'.")
                },
                _ => throw new QuantityMeasurementException($"Unsupported category: '{dto.Category}'.")
            };
        }

        private double ConvertFromBase(double baseValue, string category, string targetUnit)
        {
            category = category.Trim().ToLowerInvariant();
            targetUnit = targetUnit.Trim().ToLowerInvariant();
            return category switch
            {
                "length" => targetUnit switch
                {
                    "inch" => baseValue, "feet" => baseValue / 12.0,
                    "yard" => baseValue / 36.0, "centimeter" => baseValue * 2.54,
                    _ => throw new QuantityMeasurementException($"Unsupported length unit: '{targetUnit}'.")
                },
                "weight" => targetUnit switch
                {
                    "gram" => baseValue, "kilogram" => baseValue / 1000.0,
                    "pound" => baseValue / 453.592,
                    _ => throw new QuantityMeasurementException($"Unsupported weight unit: '{targetUnit}'.")
                },
                "volume" => targetUnit switch
                {
                    "milliliter" or "millilitre" => baseValue,
                    "liter" or "litre" => baseValue / 1000.0,
                    "gallon" => baseValue / 3785.41,
                    _ => throw new QuantityMeasurementException($"Unsupported volume unit: '{targetUnit}'.")
                },
                "temperature" => targetUnit switch
                {
                    "celsius" => baseValue, "fahrenheit" => (baseValue * 9.0 / 5.0) + 32.0,
                    "kelvin" => baseValue + 273.15,
                    _ => throw new QuantityMeasurementException($"Unsupported temperature unit: '{targetUnit}'.")
                },
                _ => throw new QuantityMeasurementException($"Unsupported category: '{category}'.")
            };
        }

        // ─── GUARDS ───────────────────────────────────────
        private void Validate(QuantityDTO dto)
        {
            if (dto == null) throw new QuantityMeasurementException("Quantity cannot be null.");
            if (string.IsNullOrWhiteSpace(dto.Category)) throw new QuantityMeasurementException("Category is required.");
            if (string.IsNullOrWhiteSpace(dto.Unit)) throw new QuantityMeasurementException("Unit is required.");
        }

        private void EnsureSameCategory(QuantityDTO first, QuantityDTO second, string operation)
        {
            if (!first.Category.Equals(second.Category, StringComparison.OrdinalIgnoreCase))
                throw new QuantityMeasurementException($"Cannot {operation} quantities of different categories ('{first.Category}' vs '{second.Category}').");
        }

        private void EnsureArithmeticAllowed(QuantityDTO dto, string operation)
        {
            if (dto.Category.Trim().Equals("Temperature", StringComparison.OrdinalIgnoreCase))
                throw new QuantityMeasurementException($"Temperature does not support {operation}.");
        }

        // ─── PERSISTENCE ──────────────────────────────────
        private void PersistWithOperation(QuantityDTO first, QuantityDTO second, string operation, double resultValue, string resultUnit, string? userId = null)
        {
            var firstEntity = new QuantityMeasurementEntity { Value = first.Value, Unit = first.Unit, Category = first.Category };
            var secondEntity = new QuantityMeasurementEntity { Value = second.Value, Unit = second.Unit, Category = second.Category };

            _repository.SaveQuantity(firstEntity);
            _repository.SaveQuantity(secondEntity);

            _repository.SaveOperation(new OperationHistoryEntity
            {
                FirstQuantityId = firstEntity.Id,
                SecondQuantityId = secondEntity.Id,
                OperationType = operation,
                ResultValue = resultValue,
                ResultUnit = resultUnit,
                UserId = userId
            });
        }

        private void Persist(QuantityDTO dto)
        {
            _repository.SaveQuantity(new QuantityMeasurementEntity { Value = dto.Value, Unit = dto.Unit, Category = dto.Category });
        }
    }
}