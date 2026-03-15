using GymSystem.Application.Dtos.Payment;

namespace GymSystem.Application.Interfaces
{
    public interface IPaymentService
    {
        Task<string> ProcessPaymentAsync(string currentUserId, CreatePaymentDto dto);
        Task<List<PaymentResponseDto>> GetMemberPaymentsAsync(int memberId, string CurrentUserId, string role);
        Task<List<PaymentResponseDto>> GetAllPaymentsAsync(int pageNumber, int pageSize);
        Task<PaymentResponseDto> GetPaymentByIdAsync(int paymentId);
    }
}
