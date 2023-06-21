using FreeCourse.Services.Catalog.Models;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace FreeCourse.Services.Catalog.DTOs
{
    public class CategoryDTO
    {
        public string Name { get; set; }
    }
}
