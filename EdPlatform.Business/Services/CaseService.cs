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
    public class CaseService : ICaseService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CaseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Create(CaseModel quizCase)
        {
            IMapper mapper = CreateCaseModelToCaseMapper();

            await _unitOfWork.CaseRepository.Add(mapper.Map<CaseModel, Case>(quizCase));
            await _unitOfWork.Save();
        }

        public async Task Edit(CaseModel quizCase)
        {
            IMapper mapper = CreateCaseModelToCaseMapper();

            _unitOfWork.CaseRepository.Update(mapper.Map<CaseModel, Case>(quizCase));
            await _unitOfWork.Save();
        }

        public async Task<CaseModel> Get(int id)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Case, CaseModel>();
            });
            IMapper mapper = config.CreateMapper();

            return mapper.Map<Case, CaseModel>(await _unitOfWork.CaseRepository.Get(id));
        }

        public async Task Delete(int id)
        {
            _unitOfWork.CaseRepository.Remove(id);
            await _unitOfWork.Save();
        }

        private static IMapper CreateCaseModelToCaseMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CaseModel, Case>();
            });
            
            return config.CreateMapper(); 
        }
    }
}
