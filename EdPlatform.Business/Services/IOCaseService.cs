﻿using AutoMapper;
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
    public class IOCaseService : IIOCaseService
    {
        private readonly UnitOfWork _unitOfWork;
        public IOCaseService()
        {
            _unitOfWork = new UnitOfWork();
        }

        public async Task Create(IOCaseModel iOCase)
        {
            IMapper mapper = CreateIOCaseModelToIOCaseMapper();

            await _unitOfWork.IOCaseRepository.Add(mapper.Map<IOCaseModel, IOCase>(iOCase));
            await _unitOfWork.Save();
        }

        public async Task Edit(IOCaseModel iOCase)
        {
            IMapper mapper = CreateIOCaseModelToIOCaseMapper();

            _unitOfWork.IOCaseRepository.Update(mapper.Map<IOCaseModel, IOCase>(iOCase));
            await _unitOfWork.Save();
        }

        public async Task<IOCaseModel> GetById(int id)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<IOCase, IOCaseModel>();
            });
            var mapper = config.CreateMapper();

            return mapper.Map<IOCase, IOCaseModel>(await _unitOfWork.IOCaseRepository.Get(id));
        }

        public async Task<CourseModel> GetCourseById(int courseId)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Course, CourseModel>();
                cfg.CreateMap<Category, CategoryModel>();
                cfg.CreateMap<Module, ModuleModel>();
                cfg.CreateMap<Lesson, LessonModel>();
            });
            var mapper = config.CreateMapper();

            return mapper.Map<Course, CourseModel>(await _unitOfWork.CourseRepository.Get(courseId));
        }

        private static IMapper CreateIOCaseModelToIOCaseMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<IOCaseModel, IOCase>();
            });
            var mapper = config.CreateMapper();
            return mapper;
        }
    }
}