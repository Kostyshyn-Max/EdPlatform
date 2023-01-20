using AutoMapper;
using EdPlatform.Business.Models;
using EdPlatform.Data.Entities;
using EdPlatform.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdPlatform.Business.Services
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CommentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Create(CommentModel comment)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CommentModel, Comment>();
                cfg.CreateMap<CourseModel, Course>();
            });
            IMapper mapper = config.CreateMapper();

            await _unitOfWork.CommentRepository.Add(mapper.Map<CommentModel, Comment>(comment));
            await _unitOfWork.Save();
        }

        public async Task Delete(int id)
        {
            _unitOfWork.CommentRepository.Remove(id);
            await _unitOfWork.Save();
        }

        public async Task Edit(CommentModel comment)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CommentModel, Comment>();
                cfg.CreateMap<CourseModel, Course>();
            });
            IMapper mapper = config.CreateMapper();

            _unitOfWork.CommentRepository.Update(mapper.Map<CommentModel, Comment>(comment));
            await _unitOfWork.Save();
        }

        public async Task<List<CommentModel>> GetAllByCourseId(int courseId)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Comment, CommentModel>();
                cfg.CreateMap<Course, CourseModel>();
            });
            IMapper mapper = config.CreateMapper();

            List<CommentModel> comments = (await _unitOfWork.CommentRepository.Find(x => x.CourseId == courseId)).ToList().Select(x => mapper.Map<Comment, CommentModel>(x)).ToList();
            return comments;
        }

        public async Task<List<CommentModel>> GetAllByUserId(int userId)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Comment, CommentModel>();
                cfg.CreateMap<Course, CourseModel>();
            });
            IMapper mapper = config.CreateMapper();

            List<CommentModel> comments = (await _unitOfWork.CommentRepository.Find(x => x.UserId == userId)).ToList().Select(x => mapper.Map<Comment, CommentModel>(x)).ToList();
            return comments;
        }

        public async Task<CommentModel> GetById(int id)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Comment, CommentModel>();
                cfg.CreateMap<Course, CourseModel>();
            });
            IMapper mapper = config.CreateMapper();

            return mapper.Map<Comment, CommentModel>(await _unitOfWork.CommentRepository.Get(id));
        }
    }
}
