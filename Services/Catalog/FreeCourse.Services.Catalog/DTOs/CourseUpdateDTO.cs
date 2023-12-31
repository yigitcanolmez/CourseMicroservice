﻿namespace FreeCourse.Services.Catalog.DTOs
{
    public class CourseUpdateDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Picture { get; set; }
        public string UserId { get; set; }
        public string CategoryId { get; set; }
        public FeatureDTO Feature { get; set; }
    }
}
