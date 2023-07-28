using System;
using System.Collections.Generic;
using System.Text;

namespace FreeCourse.Shared.Messages.Publisher
{
    public class CreateOrderMessageCommand
    {
        public string BuyerId { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public Address Address { get; set; }


    }
    public class OrderItem
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }
        public Decimal Price { get; set; }
    }
    public class Address
    {
        public string Province { get; private set; }
        public string District { get; private set; }
        public string Street { get; private set; }
        public string ZipCode { get; private set; }
        public string Line { get; private set; }
    }
}
