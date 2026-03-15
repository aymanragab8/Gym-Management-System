using GymSystem.Application.Dtos.Payment;
using GymSystem.Application.Interfaces;
using GymSystem.Domain.AppMeteData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Gym_Management_System.Controllers
{
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _ipaymentService;

        public PaymentController(IPaymentService ipaymentService)
        {
            _ipaymentService = ipaymentService;
        }
        [HttpGet(Router.Payment.GetAllPayments)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllPayments(int pageNumber = 1, int pageSize = 10)
        {
            var payments = await _ipaymentService.GetAllPaymentsAsync(pageNumber, pageSize);
            return Ok(payments);
        }
        [HttpGet(Router.Payment.GetAllMemberPayments)]
        [Authorize(Roles = "Admin,Member")]
        public async Task<IActionResult> GetAllMemberPayments(int memberId)
        {
            string currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string role = User.FindFirstValue(ClaimTypes.Role);
            var payments = await _ipaymentService.GetMemberPaymentsAsync(memberId, currentUserId, role);
            return Ok(payments);
        }
        [HttpGet(Router.Payment.GetPaymentById)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetPaymentById(int paymentId)
        {
            var payment = await _ipaymentService.GetPaymentByIdAsync(paymentId);
            return Ok(payment);
        }
        [HttpPost(Router.Payment.ProcessPayment)]
        [Authorize(Roles = "Member")]
        public async Task<IActionResult> ProcessPayment(CreatePaymentDto dto)
        {
            string currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _ipaymentService.ProcessPaymentAsync(currentUserId, dto);
            return Ok(result);
        }
    }
}
