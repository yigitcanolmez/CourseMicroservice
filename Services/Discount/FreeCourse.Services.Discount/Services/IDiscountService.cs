using FreeCourse.Services.Discount.Models;
using FreeCourse.Shared.DTOs;

namespace FreeCourse.Services.Discount.Services
{
    public interface IDiscountService
    {
        Task<Response<List<DiscountModel>>> GetAll();
        Task<Response<DiscountModel>> GetById(int id);
        Task<Response<NoContent>> Save(DiscountModel discount);
        Task<Response<NoContent>> Update(DiscountModel discount);
        Task<Response<NoContent>> Delete(int id);
        Task<Response<DiscountModel>> GetByCodeAndUserId(string code, string userId);
    }
}
