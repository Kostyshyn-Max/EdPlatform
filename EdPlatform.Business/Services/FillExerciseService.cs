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
    public class FillExerciseService : IFillExerciseService
    {
        private readonly IUnitOfWork _unitOfWork;
        public FillExerciseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Create(FillExerciseModel fillExercise)
        {
            IMapper mapper = CreateFillExerciseModelToFillExerciseMapper();

            await _unitOfWork.FillExerciseRepository.Add(mapper.Map<FillExerciseModel, FillExercise>(fillExercise));
            await _unitOfWork.Save();
        }

        public async Task Edit(FillExerciseModel fillExercise)
        {
            IMapper mapper = CreateFillExerciseModelToFillExerciseMapper();

            _unitOfWork.FillExerciseRepository.Update(mapper.Map<FillExerciseModel, FillExercise>(fillExercise));
            await _unitOfWork.Save();
        }

        public async Task<FillExerciseModel> Get(int id)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<FillExercise, FillExerciseModel>();
                cfg.CreateMap<Exercise, ExerciseModel>();
                cfg.CreateMap<Lesson, LessonModel>();
                cfg.CreateMap<Module, ModuleModel>();
                cfg.CreateMap<Course, CourseModel>();
                cfg.CreateMap<Category, CategoryModel>();
            });
            var mapper = config.CreateMapper();

            var fillExercise = await _unitOfWork.FillExerciseRepository.Get(id);

            return mapper.Map<FillExercise, FillExerciseModel>(fillExercise);
        }

        private static IMapper CreateFillExerciseModelToFillExerciseMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<FillExerciseModel, FillExercise>();
                cfg.CreateMap<ExerciseModel, Exercise>();
            });
            var mapper = config.CreateMapper();
            return mapper;
        }
    }
}
