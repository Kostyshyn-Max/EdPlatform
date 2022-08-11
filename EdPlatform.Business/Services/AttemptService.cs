using AutoMapper;
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
    public class AttemptService : IAttemptService
    {
        public AttemptService()
        {
        }

        public async Task Create(AttemptModel attempt, List<bool> codeExecutionResults)
        {
            IMapper mapper = CreateAttemptModelToAttemptMapper();

            attempt.IsCompleted = codeExecutionResults.Where(x => x.Equals(true)).Count().Equals(codeExecutionResults.Count());

            Attempt existingAttempt;
            await using (UnitOfWork u = new())
            {
                existingAttempt = (await u.AttemptRepository.Find(x => x.UserId == attempt.UserId && x.ExerciseId == attempt.ExerciseId)).SingleOrDefault();
            }
            await using (UnitOfWork u = new())
            {
                if (existingAttempt != null)
                {
                    attempt.AttemptId = existingAttempt.AttemptId;
                    u.AttemptRepository.Update(mapper.Map<AttemptModel, Attempt>(attempt));
                    await u.Save();
                }
                else
                {
                    await u.AttemptRepository.Add(mapper.Map<AttemptModel, Attempt>(attempt));
                    await u.Save();
                }
            }

        }

        public async Task EditAttempt(AttemptModel attempt)
        {
            IMapper mapper = CreateAttemptModelToAttemptMapper();
            await using (UnitOfWork u = new())
            {
                u.AttemptRepository.Update(mapper.Map<AttemptModel, Attempt>(attempt));
                await u.Save();
            }
        }

        public async Task<AttemptModel> GetFromUserExercise(int userId, int exerciseId)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Attempt, AttemptModel>();
            });
            IMapper mapper = config.CreateMapper();
            Attempt? attempt;
            await using (UnitOfWork u = new())
            {
                attempt = (await u.AttemptRepository.Find(x => x.UserId == userId && x.ExerciseId == exerciseId)).SingleOrDefault();
            }

            return mapper.Map<Attempt, AttemptModel>(attempt);
        }

        private IMapper CreateAttemptModelToAttemptMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AttemptModel, Attempt>();
            });
            var mapper = config.CreateMapper();
            return mapper;
        }
    }
}
