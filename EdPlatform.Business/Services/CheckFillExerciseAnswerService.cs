using EdPlatform.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EdPlatform.Business.Services
{
    public class CheckFillExerciseAnswerService : ICheckFillExerciseAnswerService
    {
        private readonly IAttemptService _attemptService;
        public CheckFillExerciseAnswerService(IAttemptService attemptService)
        {
            _attemptService = attemptService;
        }

        public async Task ReviewUserAnswer(FillExerciseCheckModel fillExerciseCheckModel)
        {
            Regex pattern = new Regex("[\a\b\t\r\v\f\n]");
            fillExerciseCheckModel.Answer = pattern.Replace(fillExerciseCheckModel.Answer, "");
            fillExerciseCheckModel.UserAnswer = pattern.Replace(fillExerciseCheckModel.UserAnswer, "");

            await _attemptService.Create(new AttemptModel
            {
                ExerciseId = fillExerciseCheckModel.ExerciseId,
                UserId = fillExerciseCheckModel.UserId,
                UserAnswer = fillExerciseCheckModel.UserAnswer,
            }, new List<bool>()
            {
                fillExerciseCheckModel.Answer.Equals(fillExerciseCheckModel.UserAnswer)
            });
        }
    }
}