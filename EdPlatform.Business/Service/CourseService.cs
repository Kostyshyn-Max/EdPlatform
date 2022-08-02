using AutoMapper;
using EdPlatform.Business.Models;
using EdPlatform.Data;
using EdPlatform.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdPlatform.Business.Service
{
    public class CourseService : ICourseService
    {
        private readonly UnitOfWork _unitOfWork;
        public CourseService()
        {
            _unitOfWork = new();
        }

        public async Task CreateCourse(CourseModel course)
        {
            Category? category = (await _unitOfWork.CategoryRepository.Get(course.Category.CategoryId));
            if (category == null)
                return;

            await _unitOfWork.CourseRepository.Add(new Course() 
                { 
                    AuthorId = course.AuthorId,
                    CourseName = course.CourseName,
                    Description = course.Description,
                    Category = category,
                }
            );
            await _unitOfWork.Save();
        }

        public async Task<IEnumerable<CourseModel>> GetAllFromAuthor(int authorId)
        {
            var mapper = CreateCourseModelMapper();

            List<CourseModel> courseModels = new List<CourseModel>();
            foreach(var course in await _unitOfWork.CourseRepository.Find(x => x.AuthorId == authorId))
            {
                courseModels.Add(mapper.Map<Course, CourseModel>(course));
            }

            return courseModels;
        }
        public async Task<IEnumerable<CourseModel>> GetAll()
        {
            var mapper = CreateCourseModelMapper();

            List<CourseModel> courseModels = new List<CourseModel>();
            foreach(var course in await _unitOfWork.CourseRepository.GetAll())
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

        public async Task EditCourse(CourseModel course)
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
                return;

            var courseData = mapper.Map<CourseModel, Course>(course);
            courseData.Category = category;

            _unitOfWork.CourseRepository.Update(courseData);
            await _unitOfWork.Save();
        }

        private static IMapper CreateCourseModelMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Course, CourseModel>();
                cfg.CreateMap<Category, CategoryModel>();
                cfg.CreateMap<Module, ModuleModel>();
            });
            return config.CreateMapper();
        }
    }
}
