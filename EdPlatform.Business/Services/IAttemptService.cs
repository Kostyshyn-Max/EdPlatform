﻿using EdPlatform.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdPlatform.Business.Services
{
    public interface IAttemptService
    {
        Task Create(AttemptModel attempt, List<bool> codeExecutionResults);
        Task<AttemptModel?> GetFromUserExercise(int userId, int exerciseId);
        Task<int> GetNotSolvedExerciseId(IEnumerable<ExerciseModel> exercises, int userId);
        Task<List<AttemptModel?>> GetAllAttemptsFromExercises(IEnumerable<ExerciseModel> exercises, int userId);
        Task EditAttempt(AttemptModel attempt);
    }
}
