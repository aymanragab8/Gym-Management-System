using GymSystem.Application.Dtos.SubscriptionDto;
using GymSystem.Application.Interfaces;
using GymSystem.Domain.AppMeteData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gym_Management_System.Controllers
{
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionService _subscriptionService;

        public SubscriptionController(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }
        [HttpGet(Router.Subscription.GetAllSubscriptions)]
        public async Task<IActionResult> GetAllSubscriptions(int pageNumber = 1, int pageSize = 10)
        {
            var result = await _subscriptionService.GetAllSubscriptionsAsync(pageNumber, pageSize);
            return Ok(result);
        }
        [HttpGet(Router.Subscription.GetById)]
        public async Task<IActionResult> GetSubscription(int id)
        {
            var result = await _subscriptionService.GetSubscriptionByIdAsync(id);
            return Ok(result);
        }
        [HttpPost(Router.Subscription.CreateSubscription)]
        public async Task<IActionResult> CreateSubscription(int memberId, CreateSubscriptionDto dto)
        {
            var result = await _subscriptionService.AddSubscriptionAsync(memberId, dto);
            return Ok(result);
        }
        [HttpPut(Router.Subscription.UpdateSubscription)]
        public async Task<IActionResult> UpdateSubscription(int subId, UpdateSubscriptionDto dto)
        {
            var result = await _subscriptionService.UpdateSubscriptionAsync(subId, dto);
            return Ok(result);
        }
        [HttpDelete(Router.Subscription.DeleteSubscription)]
        public async Task<IActionResult> DeleteSubscription(int subId)
        {
            var result = await _subscriptionService.DeleteSubscriptionAsync(subId);
            return Ok(result);
        }
    }
}
