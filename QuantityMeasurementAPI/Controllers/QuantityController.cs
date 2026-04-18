using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using QuantityMeasurementAPI.DTOs;
using QuantityMeasurementModel.DTOs;
using QuantityMeasurementModel.Models;
using QuantityMeasurementServices.Interfaces;

namespace QuantityMeasurementAPI.Controllers
{
    [ApiController]
    [Route("api/quantities")]
    public class QuantityController : ControllerBase
    {
        private readonly IQuantityMeasurementService _service;

        public QuantityController(IQuantityMeasurementService service)
        {
            _service = service;
        }
        private string? GetUserId()
        {
            if (!User.Identity?.IsAuthenticated ?? true) return null;
            return User.Claims.FirstOrDefault(c =>
                c.Type == ClaimTypes.NameIdentifier ||
                c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"
            )?.Value;
        }

        [AllowAnonymous]
        [HttpPost("compare")]
        public IActionResult Compare([FromBody] CompareRequest request)
        {
            var userId = GetUserId();
            var result = _service.Compare(request.First, request.Second, userId);
            return Ok(new
            {
                result,
                first = request.First,
                second = request.Second,
                message = result ? "quantities are equal" : "quantities are not equal"
            });
        }

        [AllowAnonymous]
        [HttpPost("add")]
        public IActionResult Add([FromBody] QuantityRequest request)
        {
            var result = _service.Add(request.First, request.Second, request.TargetUnit, GetUserId());
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("subtract")]
        public IActionResult Subtract([FromBody] QuantityRequest request)
        {
            var result = _service.Subtract(request.First, request.Second, request.TargetUnit, GetUserId());
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("divide")]
        public IActionResult Divide([FromBody] DivideRequest request)
        {
            var result = _service.Divide(request.First, request.Second, GetUserId());
            return Ok(new
            {
                BaseValue = result,
                Unit = "ratio",
                Category = request.First.Category
            });
        }

        [AllowAnonymous]
        [HttpPost("convert")]
        public IActionResult Convert([FromBody] ConvertRequest request)
        {
            var result = _service.Convert(request.Input, request.TargetUnit);
            return Ok(result);
        }

        [Authorize]
        [HttpGet("history")]
        public IActionResult History()
        {
            var userId = GetUserId();
            if (userId == null)
                return Unauthorized(new { message = "User not found in token" });
            var result = _service.GetHistoryByUser(userId);
            return Ok(result);
        }

        [Authorize]
        [HttpDelete("clear")]
        public IActionResult DeleteAll()
        {
            var userId = GetUserId();
            if (userId == null)
                return Unauthorized(new { message = "User not found in token" });
            _service.DeleteRecordsByUser(userId);
            return Ok("Deleted");
        }
    }
}