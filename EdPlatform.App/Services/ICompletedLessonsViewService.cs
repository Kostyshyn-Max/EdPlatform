using EdPlatform.App.Models;
using EdPlatform.Business.Models;

namespace EdPlatform.App.Services
{
    public interface ICompletedLessonsViewService
    {
        Task<List<LessonCompletedViewModel>> CreateListOfCompletedLessons(List<LessonModel> lessons, int userId);
    }
}
