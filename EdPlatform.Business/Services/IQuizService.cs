using EdPlatform.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdPlatform.Business.Services
{
    public interface IQuizService
    {
        Task Create(QuizModel quizExercise);
        Task<QuizModel> Get(int quizExerciseId);
        Task Edit(QuizModel quiz);
    }
}
