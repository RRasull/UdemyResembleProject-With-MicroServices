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

        public async Task<Response<CourseDto>> CreateAsync(CourseCreateDto courseCreateDto)
        {
            if (courseCreateDto is null)
            {
                return Response<CourseDto>.Failed("Course has not found", 404);
            }

            Course course = _mapper.Map<Course>(courseCreateDto);
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

        public async Task<Response<NoContent>> UpdateAsync(CourseUpdateDto courseUpdateDto)
        {
            if (courseUpdateDto is null)
            {
                return Response<NoContent>.Failed("Course has not found", 404);
            }

            Course updateCourse = _mapper.Map<Course>(courseUpdateDto);
            var result = await _courseCollection.FindOneAndReplaceAsync(x => x.Id == updateCourse.Id, updateCourse);

            if (result is null)
            {
                return Response<NoContent>.Failed("Course has not found", 404);
            }

            return Response<NoContent>.Succeeded(204);

        }

        public async Task<Response<NoContent>> DeleteAsync(string id)
        {
            var course = await _courseCollection.FindAsync(x => x.Id == id);
            if (course is null)
            {
                return Response<NoContent>.Failed("Course has not found", 404);
            }

            var result = await _courseCollection.DeleteOneAsync(x => x.Id == id);

            //to make sure course is deleted
            if (result.DeletedCount > 0)
            {
                //if the count is more than 0, then the course is deleted successfully
                return Response<NoContent>.Succeeded(204);
            }
            else
            {
                //the course couldn't be found in the database
                return Response<NoContent>.Failed("Course not found", 404);
            }
            return Response<NoContent>.Succeeded(204);

        }
    }
}
