using Microsoft.AspNetCore.Mvc;
using ModelLayer.DTOs;
using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace WebApiLayer.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/api/quantitymeasurement")]
    public class QuantityMeasurementController : ControllerBase
    {
        private IQuantityMeasurementService _service;

        public QuantityMeasurementController(IQuantityMeasurementService service)
        {
            _service = service;
        }

        [HttpPost("add")]
        public IActionResult Add(AddRequestDto request)
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var result = _service.Add(request, userId);
                return Ok(result);
            }
            catch (Exception e)
            {
                var msg = e.Message;
                return BadRequest(msg);
            }
        }

        [HttpPost("subtract")]
        public IActionResult Subtract(SubtractRequestDto request)
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var result = _service.Subtract(request, userId);
                return Ok(result);
            }
            catch (Exception e)
            {
                var msg = e.Message;
                return BadRequest(msg);
            }
        }

        [HttpPost("divide")]
        public IActionResult Divide(DivideRequestDto request)
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var result = _service.Divide(request, userId);
                return Ok(result);
            }
            catch (Exception e)
            {
                var msg = e.Message;
                return BadRequest(msg);
            }
        }

        [HttpPost("compare")]
        public IActionResult Compare(ComparisonRequestDto request)
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var result = _service.Compare(request, userId);
                return Ok(result);
            }
            catch (Exception e)
            {
                var msg = e.Message;
                return BadRequest(msg);
            }
        }

        [HttpPost("convert")]
        public IActionResult Convert(ConversionRequestDto request)
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var result = _service.Convert(request, userId);
                return Ok(result);
            }
            catch (Exception e)
            {
                var msg = e.Message;
                return BadRequest(msg);
            }
        }

        [HttpPost("history")]
        public IActionResult GetHistory()
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var result = _service.GetMeasurmentsHistory(userId);
                return Ok(result);
            }
            catch (Exception e)
            {
                var msg = e.Message;
                return BadRequest(msg);
            }
        }

        
    }
}