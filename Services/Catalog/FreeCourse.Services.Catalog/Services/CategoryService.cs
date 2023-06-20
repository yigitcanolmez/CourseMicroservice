using AutoMapper;
using FreeCourse.Services.Catalog.Constant;
using FreeCourse.Services.Catalog.DTOs;
using FreeCourse.Services.Catalog.Models;
using FreeCourse.Services.Catalog.Settings;
using FreeCourse.Shared.DTOs;
using MongoDB.Driver;

namespace FreeCourse.Services.Catalog.Services
{

    public class CategoryService
    {
        private readonly IMongoCollection<Category> _categoriesCollection;
        private readonly IMapper _mapper;
        private readonly ILogger<CategoryService> _logger;

        public CategoryService(IMapper mapper, ILogger<CategoryService> logger, IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _categoriesCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);

            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Response<List<CategoryDTO>>> GetAllAsync()
        {
            var categories = await _categoriesCollection.Find(category => true).ToListAsync();

            var getMappedListDto = _mapper.Map<List<CategoryDTO>>(categories);

            return Response<List<CategoryDTO>>.Success(getMappedListDto, StatusCodes.Status200OK);
        }

        public async Task<Response<CategoryDTO>> CreateAsync(Category category)
        {

            await _categoriesCollection.InsertOneAsync(category);
            var mapData = _mapper.Map<CategoryDTO>(category);

            return Response<CategoryDTO>.Success(mapData, StatusCodes.Status200OK);
        }
        public async Task<Response<CategoryDTO>> GetByIdAsync(string id)
        {

            var data = await _categoriesCollection.Find<Category>(x => x.Id == id).FirstOrDefaultAsync();

            if (data == null)
            {
                return Response<CategoryDTO>.Fail(ResponseMessages.CATEGORY_NOT_FOUND, StatusCodes.Status404NotFound);
            }
            var mapData = _mapper.Map<CategoryDTO>(data);

            return Response<CategoryDTO>.Success(mapData, StatusCodes.Status200OK);
        }


    }
}
