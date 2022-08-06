using AutoMapper;
using EdPlatform.Business.Models;
using EdPlatform.Data;
using EdPlatform.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdPlatform.Business.Services
{
    public class CourseService : ICourseService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IImageService _imageService;
        public CourseService(IImageService imageService)
        {
            _unitOfWork = new();
            _imageService = imageService;
        }

        public async Task<bool> CreateCourse(CourseModel course)
        {
            Category? category = (await _unitOfWork.CategoryRepository.Get(course.Category.CategoryId));
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
                ImageName = fileName
            }
            );
            await _unitOfWork.Save();
            return true;
        }

        public async Task<IEnumerable<CourseModel>> GetAllFromAuthor(int authorId)
        {
            var mapper = CreateCourseModelMapper();

            List<CourseModel> courseModels = new List<CourseModel>();
            foreach (var course in await _unitOfWork.CourseRepository.Find(x => x.AuthorId == authorId))
            {
                courseModels.Add(mapper.Map<Course, CourseModel>(course));
            }

            return courseModels;
        }
        public async Task<IEnumerable<CourseModel>> GetAll()
        {
            var mapper = CreateCourseModelMapper();

            List<CourseModel> courseModels = new List<CourseModel>();
            foreach (var course in await _unitOfWork.CourseRepository.GetAll())
            {
                courseModels.Add(mapper.Map<Course, CourseModel>(course));
            }

            return courseModels;
        }

        public async Task<CourseModel> GetById(int id)
        {
            var mapper = CreateCourseModelMapper();
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

            Category? category = (await _unitOfWork.CategoryRepository.Get(course.Category.CategoryId));
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

        private static IMapper CreateCourseModelMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Course, CourseModel>();
                cfg.CreateMap<Category, CategoryModel>();
                cfg.CreateMap<Module, ModuleModel>();
                cfg.CreateMap<Lesson, LessonModel>();
            });
            return config.CreateMapper();
        }
    }
}
