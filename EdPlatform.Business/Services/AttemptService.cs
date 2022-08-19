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

        public async Task<AttemptModel?> GetFromUserExercise(int userId, int exerciseId)
        {
            IMapper mapper = CreateAttemptToAttemptModelMapper();
            Attempt? attempt;
            await using (UnitOfWork u = new())
            {
                attempt = (await u.AttemptRepository.Find(x => x.UserId == userId && x.ExerciseId == exerciseId)).SingleOrDefault();
            }

            return mapper.Map<Attempt, AttemptModel>(attempt);
        }

        public async Task<int> GetNotSolvedExercise(IEnumerable<ExerciseModel> exercises, int userId)
        {
            IMapper mapper = CreateAttemptToAttemptModelMapper();

            List<AttemptModel?> attempts = new List<AttemptModel>();

            await using (UnitOfWork u = new())
            {
                foreach(var exercise in exercises)
                {
                    var attempt = (await u.AttemptRepository.Find(x => x.ExerciseId == exercise.ExerciseId && x.UserId == userId)).SingleOrDefault();

                    if (attempt != null)
                        attempts.Add(mapper.Map<Attempt, AttemptModel>(attempt));
                    else
                        attempts.Add(null);
                }
            }

            for (int i = 0; i < exercises.Count(); i++)
            {
                if (attempts[i] == null)
                {
                    return exercises.ElementAt(i).ExerciseId;
                }
            }

            return -1;
        }

        private static IMapper CreateAttemptToAttemptModelMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Attempt, AttemptModel>();
                cfg.CreateMap<Exercise, ExerciseModel>();
            });
            IMapper mapper = config.CreateMapper();
            return mapper;
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
