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
    public class CourseUserService : ICourseUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CourseUserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateCourseUser(CourseUserModel courseUser)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<CourseUserModel, CourseUser>());
            var mapper = config.CreateMapper();

            var course = await _unitOfWork.CourseRepository.Get(courseUser.CourseId);
            course.UsersJoined++;
            _unitOfWork.CourseRepository.Update(course);

            await _unitOfWork.CourseUserRepository.Add(mapper.Map<CourseUserModel, CourseUser>(courseUser));
            await _unitOfWork.Save();
        }

        public async Task<CourseUserModel?> Get(CourseUserModel course)
        {
            IMapper mapper = CreateCourseUserToCourseUserModelMapper();

            var courseUser = await _unitOfWork.CourseUserRepository.Find(x => x.UserId == course.UserId && x.CourseId == course.CourseId);

            return mapper.Map<CourseUser, CourseUserModel>(courseUser.SingleOrDefault());
        }

        public async Task<IEnumerable<CourseUserModel>> GetAllFromUser(int userId)
        {
            IMapper mapper = CreateCourseUserToCourseUserModelMapper();

            var courseUsers = await _unitOfWork.CourseUserRepository.Find(x => x.UserId == userId);
            List<CourseUserModel> result = new List<CourseUserModel>();

            foreach (var courseUser in courseUsers)
            {
                result.Add(mapper.Map<CourseUser, CourseUserModel>(courseUser));
            }

            return result;
        }

        private static IMapper CreateCourseUserToCourseUserModelMapper()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<CourseUser, CourseUserModel>());
            var mapper = config.CreateMapper();
            return mapper;
        }
    }
}
