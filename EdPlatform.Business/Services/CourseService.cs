using AutoMapper;
using EdPlatform.Business.Models;
using EdPlatform.Data;
using EdPlatform.Data.Entities;
using EdPlatform.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdPlatform.Business.Services
{
    public class CourseService : ICourseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IImageService _imageService;
        public CourseService(IUnitOfWork unitOfWork, IImageService imageService)
        {
            _unitOfWork = unitOfWork;
            _imageService = imageService;
        }

        public async Task<bool> CreateCourse(CourseModel course)
        {
            Category? category = (await _unitOfWork.CategoryRepository.Get(course.CategoryId));
            if (category == null)
                return false;

            string fileName = $"course_{Guid.NewGuid()}";
            bool imageUploadSuccess = await _imageService.UploadFileAsync(course.Image, fileName, course.ContentType);

            if (!imageUploadSuccess)
                return false;

            await _unitOfWork.CourseRepository.Add(new()
            {
                AuthorId = course.AuthorId,
                CourseName = course.CourseName,
                Description = course.Description,
                Category = category,
                CategoryId = course.CategoryId,
                ImageName = fileName
            }
            );
            await _unitOfWork.Save();
            return true;
        }

        public async Task<IEnumerable<CourseModel>> GetAllFromAuthor(int authorId)
        {
            var mapper = CreateCourseToCourseModelMapper();

            List<CourseModel> courseModels = new List<CourseModel>();
            foreach (var course in await _unitOfWork.CourseRepository.Find(x => x.AuthorId == authorId))
            {
                courseModels.Add(mapper.Map<Course, CourseModel>(course));
            }

            return courseModels;
        }
        public async Task<IEnumerable<CourseModel>> GetAll()
        {
            var mapper = CreateCourseToCourseModelMapper();

            List<CourseModel> courseModels = new List<CourseModel>();
            foreach (var course in await _unitOfWork.CourseRepository.GetAll())
            {
                courseModels.Add(mapper.Map<Course, CourseModel>(course));
            }

            return courseModels;
        }

        public async Task<CourseModel> GetById(int id)
        {
            var mapper = CreateCourseToCourseModelMapper();
            var course = mapper.Map<Course, CourseModel>(await _unitOfWork.CourseRepository.Get(id));

            return course;
        }

        public async Task<bool> EditCourse(CourseModel course)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CourseModel, Course>();
                cfg.CreateMap<CategoryModel, Category>();
                cfg.CreateMap<ModuleModel, Module>();
            });
            var mapper = config.CreateMapper();

            Category? category = (await _unitOfWork.CategoryRepository.Get(course.CategoryId));
            if (category == null)
                return false;

            if (course.Image != null)
            {
                (byte[]? oldImage, string? oldContentType) = await _imageService.DownloadFileAsync(course.ImageName);
                if (oldImage == null)
                    return false;

                if (!await _imageService.DeleteFileAsync(course.ImageName))
                    return false;

                if (!await _imageService.UploadFileAsync(course.Image, course.ImageName, course.ContentType))
                {
                    await _imageService.UploadFileAsync(oldImage, course.ImageName, oldContentType);
                    return false;
                }
            }
            
            var courseData = mapper.Map<CourseModel, Course>(course);
            courseData.Category = category;

            _unitOfWork.CourseRepository.Update(courseData);
            await _unitOfWork.Save();
            return true;
        }
        public async Task<IEnumerable<CourseModel>> SearchCourses(string searchRequest)
        {
            IMapper mapper = CreateCourseToCourseModelMapper();

            List<CourseModel> searchResults = new List<CourseModel>();

            var courses = await _unitOfWork.CourseRepository.GetAll();

            foreach (var course in courses)
            {
                foreach (var word in searchRequest.Replace(" ", "").Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).ToList())
                {
                    if (course.CourseName.Contains(searchRequest) || course.Category.CategoryName.Contains(word))
                    {
                        searchResults.Add(mapper.Map<Course, CourseModel>(course));
                    }
                }
            }

            return searchResults;
        }

        private static IMapper CreateCourseToCourseModelMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Course, CourseModel>();
                cfg.CreateMap<Category, CategoryModel>();
                cfg.CreateMap<Module, ModuleModel>();
                cfg.CreateMap<Lesson, LessonModel>();
                cfg.CreateMap<Exercise, ExerciseModel>();
            });
            return config.CreateMapper();
        }

        public async Task Delete(int id)
        {
            _unitOfWork.CourseRepository.Remove(id);
            await _unitOfWork.Save();
        }
    }
}
