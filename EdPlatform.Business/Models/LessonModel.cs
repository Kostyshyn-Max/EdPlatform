using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdPlatform.Business.Models
{
    public class LessonModel
    {
        public int LessonId { get; set; }
        public string LessonName { get; set; }
        public string LessonContent { get; set; }
        public int ModuleId { get; set; }
        public ModuleModel Module { get; set; }
        public string? VideoUrl { get; set; }
        public int Order { get; set; }
        public IEnumerable<ExerciseModel> Exercises { get; set; }
    }
}
