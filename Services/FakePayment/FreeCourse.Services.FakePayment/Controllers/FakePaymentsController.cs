using FreeCourse.Services.FakePayment.Models;
using FreeCourse.Shared.ControllerBases;
using FreeCourse.Shared.DTOs;
using FreeCourse.Shared.Messages.Publisher;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreeCourse.Services.FakePayment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FakePaymentsController : CustomBaseController
    {
        private readonly ISendEndpointProvider _sendEndpointProvider;

        public FakePaymentsController(ISendEndpointProvider sendEndpointProvider)
        {
            _sendEndpointProvider = sendEndpointProvider;
        }

        [HttpPost]
        public async Task<IActionResult> ReceivePayment(PaymentDto paymentDto)
        {
            var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:created-order-service-queue"));
            var createOrderMessage = new CreateOrderMessageCommand();
            createOrderMessage.BuyerId = paymentDto.Order.BuyerId;
            createOrderMessage.Province = paymentDto.Order.Address.Province;
            createOrderMessage.Street = paymentDto.Order.Address.Street;
            createOrderMessage.Line = paymentDto.Order.Address.Line;
            createOrderMessage.District = paymentDto.Order.Address.District;
            paymentDto.Order.OrderItems.ForEach(x =>
            {
                createOrderMessage.OrderItems.Add(new OrderItem
                {
                    PictureUrl = x.PictureUrl,
                    Price = x.Price,
                    ProductId = x.ProductId,
                    ProductName = x.ProductName
                });
            });
            await sendEndpoint.Send<CreateOrderMessageCommand>(createOrderMessage);
            return CreateActionResultInstance(Shared.DTOs.Response<NoContent>.Success(StatusCodes.Status200OK));
        }
    }
}
