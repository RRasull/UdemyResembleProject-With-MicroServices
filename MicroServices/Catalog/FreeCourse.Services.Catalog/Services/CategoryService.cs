using AutoMapper;
using FreeCourse.Services.Catalog.DTOs;
using FreeCourse.Services.Catalog.Entities;
using FreeCourse.Services.Catalog.Settings;
using FreeCourse.Shared.DTOs;
using MongoDB.Driver;

namespace FreeCourse.Services.Catalog.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IMapper _mapper;
        private readonly IMongoCollection<Category> _categoryCollection;

        public CategoryService(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
            _mapper = mapper;
        }

        public async Task<Response<List<CategoryDto>>> GetAllAsync()
        {
            List<Category> categories = await _categoryCollection.Find(category => true).ToListAsync();

            return Response<List<CategoryDto>>.Succeeded(_mapper.Map<List<CategoryDto>>(categories), 200);
        }

        public async Task<Response<CategoryDto>> GetByIdAsync(string id)
        {
            var category = _categoryCollection.Find(x => x.Id == id).FirstOrDefault();

            if (category is null)
            {
                return Response<CategoryDto>.Failed("Category not found", 404);
            }

            return Response<CategoryDto>.Succeeded(_mapper.Map<CategoryDto>(category), 200);
        }

        public async Task<Response<CategoryDto>> CreateAsync(Category category)
        {
            if (category is null)
            {
                return Response<CategoryDto>.Failed("Category not found", 404);

            }

            await _categoryCollection.InsertOneAsync(category);
            return Response<CategoryDto>.Succeeded(_mapper.Map<CategoryDto>(category), 200);
        }

    }
}
