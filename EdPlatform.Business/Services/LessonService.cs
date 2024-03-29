﻿using AutoMapper;
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
    public class LessonService : ILessonService
    {
        private readonly IUnitOfWork _unitOfWork;
        public LessonService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateLesson(LessonModel lesson)
        {
            IMapper mapper = CreateLessonModelToLessonMapper();

            await _unitOfWork.LessonRepository.Add(mapper.Map<LessonModel, Lesson>(lesson));
            await _unitOfWork.Save();
        }

        public async Task<LessonModel> Get(int lessonId)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Lesson, LessonModel>();
                cfg.CreateMap<Module, ModuleModel>();
                cfg.CreateMap<Course, CourseModel>();
                cfg.CreateMap<Exercise, ExerciseModel>();
            });
            var mapper = config.CreateMapper();

            return mapper.Map<Lesson, LessonModel>(await _unitOfWork.LessonRepository.Get(lessonId));
        }

        public async Task EditLesson(LessonModel lesson)
        {
            IMapper mapper = CreateLessonModelToLessonMapper();

            _unitOfWork.LessonRepository.Update(mapper.Map<LessonModel, Lesson>(lesson));
            await _unitOfWork.Save();
        }

        private static IMapper CreateLessonModelToLessonMapper()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<LessonModel, Lesson>();
            });
            var mapper = config.CreateMapper();
            return mapper;
        }

        public async Task Delete(int id)
        {
            _unitOfWork.LessonRepository.Remove(id);
            await _unitOfWork.Save();
        }
    }
}
