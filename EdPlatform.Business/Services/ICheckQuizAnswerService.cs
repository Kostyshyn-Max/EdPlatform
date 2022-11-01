using EdPlatform.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdPlatform.Business.Services
{
    public interface ICheckQuizAnswerService
    {
        Task CheckAnswer(QuizAnswerCheckModel checkModel);
    }
}
