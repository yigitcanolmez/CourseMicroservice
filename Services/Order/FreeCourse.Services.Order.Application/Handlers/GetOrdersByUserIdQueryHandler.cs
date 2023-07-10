using FreeCourse.Services.Order.Application.DTOs;
using FreeCourse.Services.Order.Application.Mapping;
using FreeCourse.Services.Order.Application.Queries;
using FreeCourse.Services.Order.Infrastructure;
using FreeCourse.Shared.DTOs;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace FreeCourse.Services.Order.Application.Handlers
{
    public class GetOrdersByUserIdQueryHandler : IRequestHandler<GetOrdersByUserIdQuery, Response<List<OrderDTO>>>
    {
        private readonly OrderDbContext _dbContext;

        public GetOrdersByUserIdQueryHandler(OrderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Response<List<OrderDTO>>> Handle(GetOrdersByUserIdQuery request, CancellationToken cancellationToken)
        {
            var orders = await _dbContext.Orders.Include(x => x.OrderItems).Where(x => x.BuyerId == request.UserId).ToListAsync();
            if (!orders.Any())
            {
                return Response<List<OrderDTO>>.Success(new List<OrderDTO>(), StatusCodes.Status200OK);
            }

            var ordersDTO = ObjectMapper.Mapper.Map<List<OrderDTO>>(orders);

            return Response<List<OrderDTO>>.Success(ordersDTO, StatusCodes.Status200OK);
        }
    }
}
