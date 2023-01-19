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
    public class ModuleService : IModuleService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ModuleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateModule(ModuleModel module)
        {
            IMapper mapper = CreateModuleModelToModuleMapper();

            await _unitOfWork.ModuleRepository.Add(mapper.Map<ModuleModel, Module>(module));
            await _unitOfWork.Save();
        }

        public async Task<IEnumerable<ModuleModel>> GetAllModulesFromCourse(int courseId)
        {
            IMapper mapper = CreateModuleToModuleModelMapper();

            List<ModuleModel> modules = new List<ModuleModel>();
            foreach (var module in await _unitOfWork.ModuleRepository.Find(x => x.CourseId == courseId))
            {
                modules.Add(mapper.Map<Module, ModuleModel>(module));
            }

            modules.OrderByDescending(x => x.Order);
            return modules;
        }

        public async Task EditModule(ModuleModel module)
        {
            IMapper mapper = CreateModuleModelToModuleMapper();

            _unitOfWork.ModuleRepository.Update(mapper.Map<ModuleModel, Module>(module));
            await _unitOfWork.Save();
        }

        public async Task<ModuleModel> GetById(int moduleId)
        {
            IMapper mapper = CreateModuleToModuleModelMapper();
            
            var module = await _unitOfWork.ModuleRepository.Get(moduleId);
            return mapper.Map<Module, ModuleModel>(module);
        }

        private static IMapper CreateModuleModelToModuleMapper()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<ModuleModel, Module>();
            });
            var mapper = config.CreateMapper();
            return mapper;
        }

        private static IMapper CreateModuleToModuleModelMapper()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Module, ModuleModel>();
                cfg.CreateMap<Lesson, LessonModel>();
                cfg.CreateMap<Course, CourseModel>();
                cfg.CreateMap<Exercise, ExerciseModel>();
                cfg.CreateMap<Quiz, QuizModel>();
                cfg.CreateMap<CodeExercise, CodeExerciseModel>();
                cfg.CreateMap<FillExercise, FillExerciseModel>();
            });
            var mapper = config.CreateMapper();
            return mapper;
        }

        public async Task Delete(int id)
        {
            _unitOfWork.ModuleRepository.Remove(id);
            await _unitOfWork.Save();
        }
    }
}
