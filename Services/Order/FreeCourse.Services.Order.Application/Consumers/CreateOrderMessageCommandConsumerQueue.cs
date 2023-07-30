using FreeCourse.Services.Order.Infrastructure;
using FreeCourse.Shared.Messages.Publisher;
using MassTransit;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeCourse.Services.Order.Application.Consumers
{
    public class CreateOrderMessageCommandConsumer: IConsumer<CreateOrderMessageCommand>
    {
        private readonly OrderDbContext _orderDbContext;

        public CreateOrderMessageCommandConsumer(OrderDbContext orderDbContext)
        {
            _orderDbContext = orderDbContext;
        }

        public async Task Consume(ConsumeContext<CreateOrderMessageCommand> context)
        {
            var message = context.Message;
            var newAddress = new Domain.OrderAggregate.Address(message.Province, message.District, message.Street, message.ZipCode, message.Line);

            Domain.OrderAggregate.Order order = new Domain.OrderAggregate.Order(newAddress, message.BuyerId);

            message.OrderItems.ForEach(c =>
            {
                order.AddOrderItem(c.ProductId, c.ProductName, c.Price, c.PictureUrl);
            });

            await _orderDbContext.Orders.AddAsync(order);
            await _orderDbContext.SaveChangesAsync();
        }
    }
}
