using AutoMapper;
using FreeCourse.Services.Catalog.Constant;
using FreeCourse.Services.Catalog.DTOs;
using FreeCourse.Services.Catalog.Models;
using FreeCourse.Services.Catalog.Services.CategoryServices;
using FreeCourse.Services.Catalog.Settings;
using FreeCourse.Shared.DTOs;
using MongoDB.Driver;

namespace FreeCourse.Services.Catalog.Services.CourseServices
{
    public class CourseService : ICourseService
    {
        private readonly IMongoCollection<Course> _coursesCollection;
        private readonly IMapper _mapper;
        private readonly ILogger<CourseService> _logger;
        private readonly ICategoryService _categoryService;
        public CourseService(IMapper mapper, ILogger<CourseService> logger, IDatabaseSettings databaseSettings, ICategoryService categoryService)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _coursesCollection = database.GetCollection<Course>(databaseSettings.CourseCollectionName);

            _mapper = mapper;
            _logger = logger;
            _categoryService = categoryService;
        }
        public async Task<Response<List<CourseDTO>>> GetAllAsync()
        {
            var data = await _coursesCollection.Find<Course>(p => true).ToListAsync();
            if (!data.Any())
            {
                _logger.LogInformation("No datas");

                return Response<List<CourseDTO>>.Fail(ResponseMessages.COURSE_NOT_FOUND, StatusCodes.Status404NotFound);


            }

            foreach (var course in data)
            {
                var category = await _categoryService.GetByIdAsync(course.Id);

                if (category.StatusCode == StatusCodes.Status200OK)
                {
                    course.Category = _mapper.Map<Category>(category.Data);
                }
            }



            _logger.LogInformation("Successfuly list of data returned!");
            return Response<List<CourseDTO>>.Success(_mapper.Map<List<CourseDTO>>(data), StatusCodes.Status200OK);

        }

        public async Task<Response<CourseDTO>> GetByIdAsync(string id)
        {
            var data = await _coursesCollection.Find<Course>(p => p.Id == id).FirstOrDefaultAsync();
            if (data == null)
            {
                _logger.LogInformation("No data");

                return Response<CourseDTO>.Fail(ResponseMessages.COURSE_NOT_FOUND, StatusCodes.Status404NotFound);


            }


            var category = await _categoryService.GetByIdAsync(id);

            if (category.StatusCode == StatusCodes.Status200OK)
            {
                data.Category = _mapper.Map<Category>(category.Data);
            }




            _logger.LogInformation("Successfuly list of data returned!");
            return Response<CourseDTO>.Success(_mapper.Map<CourseDTO>(data), StatusCodes.Status200OK);

        }
        public async Task<Response<List<CourseDTO>>> GetAllByUserId(string id)
        {
            var data = await _coursesCollection.Find<Course>(p => p.UserId == id).ToListAsync();
            if (!data.Any())
            {
                _logger.LogInformation("No datas");

                return Response<List<CourseDTO>>.Fail(ResponseMessages.COURSE_NOT_FOUND, StatusCodes.Status404NotFound);


            }

            foreach (var course in data)
            {
                var category = await _categoryService.GetByIdAsync(course.Id);

                if (category.StatusCode == StatusCodes.Status200OK)
                {
                    course.Category = _mapper.Map<Category>(category.Data);
                }
            }


            _logger.LogInformation("Successfuly list of data returned!");
            return Response<List<CourseDTO>>.Success(_mapper.Map<List<CourseDTO>>(data), StatusCodes.Status200OK);

        }
        public async Task<Response<CourseDTO>> CreateAsync(CourseCreateDTO courseDto)
        {
            var course = _mapper.Map<Course>(courseDto);
            course.CreatedTime = DateTime.Now;

            await _coursesCollection.InsertOneAsync(course);

            return Response<CourseDTO>.Success(_mapper.Map<CourseDTO>(course), StatusCodes.Status200OK);

        }
        public async Task<Response<NoContent>> UpdateAsync(CourseUpdateDTO courseDto)
        {
            var course = _mapper.Map<Course>(courseDto);
            var result = await _coursesCollection.FindOneAndReplaceAsync(x => x.Id == courseDto.Id, course);

            if (result == null)
            {
                _logger.LogInformation("No data");

                return Response<NoContent>.Fail(ResponseMessages.COURSE_NOT_FOUND, StatusCodes.Status404NotFound);

            }
            return Response<NoContent>.Success(StatusCodes.Status404NotFound);

        }
        public async Task<Response<NoContent>> DeleteAsync(string Id)
        {
             
            var result = await _coursesCollection.DeleteOneAsync(x => x.Id == Id);

            if (result == null)
            {
                _logger.LogInformation("No data");

                return Response<NoContent>.Fail(ResponseMessages.COURSE_NOT_FOUND, StatusCodes.Status404NotFound);

            }
            return Response<NoContent>.Success(StatusCodes.Status404NotFound);

        }
    }
}
