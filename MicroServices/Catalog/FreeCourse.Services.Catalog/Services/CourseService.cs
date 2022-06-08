using AutoMapper;
using FreeCourse.Services.Catalog.DTOs;
using FreeCourse.Services.Catalog.Entities;
using FreeCourse.Services.Catalog.Settings;
using FreeCourse.Shared.DTOs;
using MongoDB.Driver;

namespace FreeCourse.Services.Catalog.Services
{
    public class CourseService : ICourseService
    {
        private readonly IMapper _mapper;
        private readonly IMongoCollection<Course> _courseCollection;
        private readonly IMongoCollection<Category> _categoryCollection;


        public CourseService(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var databse = client.GetDatabase(databaseSettings.DatabaseName);
            _courseCollection = databse.GetCollection<Course>(databaseSettings.CourseCollectionName);
            _categoryCollection = databse.GetCollection<Category>(databaseSettings.CategoryCollectionName);
            _mapper = mapper;

        }

        public async Task<Response<List<CourseDto>>> GetAllAsync()
        {
            List<Course> courses = await _courseCollection.Find(courses => true).ToListAsync();

            if (courses.Any())
            {
                foreach (var course in courses)
                {
                    course.Category = await _categoryCollection.Find<Category>(x => x.Id == course.CategoryId).FirstAsync();
                }
            }
            else
            {
                courses = new List<Course>();
            }

            return Response<List<CourseDto>>.Succeeded(_mapper.Map<List<CourseDto>>(courses), 200);
        }

        public async Task<Response<CourseDto>> GetByIdAsync(string id)
        {
            var course = await _courseCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

            if (course == null)
            {
                return Response<CourseDto>.Failed("Course has not found", 404);
            }

            return Response<CourseDto>.Succeeded(_mapper.Map<CourseDto>(course), 200);
        }

        public async Task<Response<CourseDto>> CreateAsync(Course course)
        {
            if (course is null)
            {
                return Response<CourseDto>.Failed("Course has not found", 404);
            }

            await _courseCollection.InsertOneAsync(course);
            return Response<CourseDto>.Succeeded(_mapper.Map<CourseDto>(course), 200);
        }

        public async Task<Response<List<CourseDto>>> GetAllByUserId(string userId)
        {
            var courses = await _courseCollection.Find<Course>(x => x.UserId == userId).ToListAsync();

            if (courses.Any())
            {
                foreach (var course in courses)
                {
                    course.Category = await _categoryCollection.Find<Category>(x => x.Id == course.CategoryId).FirstAsync();
                }
            }
            else
            {
                courses = new List<Course>();
            }

            return Response<List<CourseDto>>.Succeeded(_mapper.Map<List<CourseDto>>(courses), 200);
        }
    }
}
