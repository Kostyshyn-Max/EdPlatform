using EdPlatform.App.Models;
using EdPlatform.Business.Models;
using EdPlatform.Business.Services;

namespace EdPlatform.App.Services
{
    public class CompletedLessonsViewService : ICompletedLessonsViewService
    {
        private readonly IAttemptService _attemptService;
        public CompletedLessonsViewService(IAttemptService attemptService)
        {
            _attemptService = attemptService;
        }

        public async Task<List<LessonCompletedViewModel>> CreateListOfCompletedLessons(List<LessonModel> lessons, int userId)
        {
            List<LessonCompletedViewModel> completedLessons = new List<LessonCompletedViewModel>();

            for (int i = 0; i < lessons.Count(); i++)
            {
                var attempts = await _attemptService.GetAllAttemptsFromExercises(lessons[i].Exercises, userId);

                bool isCompleted = false;

                if (attempts.Count(x => x == null) > 0)
                    isCompleted = false;
                else if (attempts.Count(x => x == null) != attempts.Count())
                    isCompleted = (attempts.Count(x => x.IsCompleted == true) == lessons[i].Exercises.Count()) ? true : false;
                else
                    isCompleted = false;   
                
                completedLessons.Add(new LessonCompletedViewModel()
                {
                    LessonId = lessons[i].LessonId,
                    IsCompleted = isCompleted,
                    ModuleId = lessons[i].ModuleId
                });
            }

            return completedLessons;
        }
    }
}
