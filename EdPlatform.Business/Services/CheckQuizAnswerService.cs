using EdPlatform.Business.Models;
using EdPlatform.Data;
using EdPlatform.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdPlatform.Business.Services
{
    public class CheckQuizAnswerService : ICheckQuizAnswerService
    { 
        private readonly IAttemptService _attemptService;

        private readonly IUnitOfWork _unitOfWork;
        public CheckQuizAnswerService(IUnitOfWork unitOfWork, IAttemptService attemptService)
        {
            _unitOfWork = unitOfWork;
            _attemptService = attemptService;
        }
        public async Task CheckAnswer(QuizAnswerCheckModel checkModel)
        {
            var quiz = await _unitOfWork.QuizRepository.Get(checkModel.ExerciseId);
            var rightCase = quiz.Cases.Where(x => x.IsCorrect == true).First();

            await _attemptService.Create(new AttemptModel()
            {
                ExerciseId = checkModel.ExerciseId,
                UserAnswer = checkModel.SelectedCaseId.ToString(),
                UserId = checkModel.UserId,
            }, new List<bool>()
            {
                checkModel.Result
            });
        }
    }
}
