using AutoMapper;
using FreeCourse.Services.Catalog.DTOs;
using FreeCourse.Services.Catalog.Models;

namespace FreeCourse.Services.Catalog.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            #region Category
            CreateMap<Category, CategoryDTO>().ReverseMap();

            #endregion

            #region Feature
            CreateMap<Feature, FeatureDTO>().ReverseMap();

            #endregion

            #region Course
            CreateMap<Course, CourseCreateDTO>().ReverseMap();
            CreateMap<Course, CourseUpdateDTO>().ReverseMap();
            CreateMap<Course, CourseDTO>().ReverseMap();

            #endregion
        }
    }
}
