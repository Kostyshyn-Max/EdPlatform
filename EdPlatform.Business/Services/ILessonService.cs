using EdPlatform.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdPlatform.Business.Services
{
    public interface ILessonService
    {
        Task CreateLesson(LessonModel lesson);
        Task<LessonModel> Get(int lessonId);
        Task EditLesson (LessonModel lesson);
    }
}
