using EdPlatform.Business.Models;
using EdPlatform.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdPlatform.Business.Services
{
    public class CheckQuizAnswerService : ICheckQuizAnswerService
    {
        private readonly UnitOfWork _unitOfWork; 
        private readonly IAttemptService _attemptService;
        public CheckQuizAnswerService(IAttemptService attemptService)
        {
            _unitOfWork = new UnitOfWork();
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
