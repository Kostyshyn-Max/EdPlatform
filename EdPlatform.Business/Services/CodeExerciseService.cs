using AutoMapper;
using EdPlatform.Business.Models;
using EdPlatform.Data;
using EdPlatform.Data.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdPlatform.Business.Services
{
    public class CodeExerciseService : ICodeExerciseService
    {
        private readonly UnitOfWork _unitOfWork;
        public CodeExerciseService()
        {
            _unitOfWork = new UnitOfWork();
        }

        public async Task Create(CodeExerciseModel codeExercise)
        {
            IMapper mapper = CreateCodeExerciseModelToCodeExerciseMapper();

            await _unitOfWork.CodeExerciseRepository.Add(mapper.Map<CodeExerciseModel, CodeExercise>(codeExercise));
            await _unitOfWork.Save();
        }

        public async Task Edit(CodeExerciseModel codeExercise)
        {
            IMapper mapper = CreateCodeExerciseModelToCodeExerciseMapper();

            _unitOfWork.CodeExerciseRepository.Update(mapper.Map<CodeExerciseModel, CodeExercise>(codeExercise));
            await _unitOfWork.Save();
        }

        public async Task<CodeExerciseModel?> GetById(int id)
        {
            var codeExercise = await _unitOfWork.CodeExerciseRepository.Get(id);
            
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CodeExercise, CodeExerciseModel>();
                cfg.CreateMap<Exercise, ExerciseModel>();
                cfg.CreateMap<Lesson, LessonModel>();
                cfg.CreateMap<Module, ModuleModel>();
                cfg.CreateMap<Course, CourseModel>();
                cfg.CreateMap<Category, CategoryModel>();
                cfg.CreateMap<IOCase, IOCaseModel>();
            });
            var mapper = config.CreateMapper();

            return mapper.Map<CodeExercise, CodeExerciseModel>(codeExercise);
        }

        private static IMapper CreateCodeExerciseModelToCodeExerciseMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CodeExerciseModel, CodeExercise>();
                cfg.CreateMap<ExerciseModel, Exercise>();
            });
            var mapper = config.CreateMapper();
            return mapper;
        }
    }
}
