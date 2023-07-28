namespace FreeCourse.Services.FakePayment.Models
{
    public class OrderDTO
    {
        public OrderDTO()
        {
            OrderItems = new List<OrderItemDTO>();
        }
        public string BuyerId { get; set; }
        public List<OrderItemDTO> OrderItems { get; set; }
        public AddressDTO Address { get; set; }

    }
    public class AddressDTO
    {
        public string Province { get; private set; }
        public string District { get; private set; }
        public string Street { get; private set; }
        public string ZipCode { get; private set; }
        public string Line { get; private set; }

    }
    public class OrderItemDTO
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }
        public Decimal Price { get; set; }
    }
}
