﻿using Dapper.Contrib.Extensions;

namespace FreeCourse.Services.Discount.Models
{
    [Table("discount")]
    public class DiscountModel
    {
        public int Id{ get; set; }
        public string UserId { get; set; }
        public int Rate { get; set; }
        public string Code { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
