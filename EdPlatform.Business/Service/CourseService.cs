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
            Category? category = (await _unitOfWork.CategoryRepository.Find(x => x.CategoryName == course.Category.CategoryName)).SingleOrDefault();
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
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Course, CourseModel>();
                cfg.CreateMap<Category, CategoryModel>();
            });
            var mapper = config.CreateMapper();

            List<CourseModel> courseModels = new List<CourseModel>();
            foreach(var course in await _unitOfWork.CourseRepository.Find(x => x.AuthorId == authorId))
            {
                courseModels.Add(mapper.Map<Course, CourseModel>(course));
            }

            return courseModels;
        }
        public async Task<IEnumerable<CourseModel>> GetAll()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Course, CourseModel>();
                cfg.CreateMap<Category, CategoryModel>();
            });
            var mapper = config.CreateMapper();

            List<CourseModel> courseModels = new List<CourseModel>();
            foreach(var course in await _unitOfWork.CourseRepository.GetAll())
            {
                courseModels.Add(mapper.Map<Course, CourseModel>(course));
            }

            return courseModels;
        }
    }
}
