using FreeCourse.Services.Basket.DTOs;
using FreeCourse.Shared.DTOs;
using System.Text.Json;

namespace FreeCourse.Services.Basket.Services
{
    public interface IBasketService
    {
        Task<Response<BasketDto>> GetBasket(string UserId);
        Task<Response<bool>> SaveOrUpdate(BasketDto basketDto);
        Task<Response<bool>> Delete(string UserId);
    }
    public class BasketService : IBasketService
    {
        private readonly RedisService _redisService;

        public BasketService(RedisService redisService)
        {
            _redisService = redisService;
        }

        public async Task<Response<bool>> Delete(string UserId)
        {
            var status = await _redisService.GetDatabase().KeyDeleteAsync(UserId);
            return status ? Response<bool>.Success(StatusCodes.Status204NoContent) : Response<bool>.Fail("Could not delete", StatusCodes.Status500InternalServerError);

        }

        public async Task<Response<BasketDto>> GetBasket(string UserId)
        {
            var existBasket = await _redisService.GetDatabase().StringGetAsync(UserId);

            if (String.IsNullOrEmpty(existBasket))
            {
                return Response<BasketDto>.Fail("Basket Not Found", StatusCodes.Status404NotFound);

            }
            return Response<BasketDto>.Success(JsonSerializer.Deserialize<BasketDto>(existBasket),StatusCodes.Status200OK);
        }

        public async Task<Response<bool>> SaveOrUpdate(BasketDto basketDto)
        {
            var status = await _redisService.GetDatabase().StringSetAsync(basketDto.UserId, JsonSerializer.Serialize(basketDto));

            return status ? Response<bool>.Success(StatusCodes.Status204NoContent) : Response<bool>.Fail("Could not save or update",StatusCodes.Status500InternalServerError);
        }
    }
}
