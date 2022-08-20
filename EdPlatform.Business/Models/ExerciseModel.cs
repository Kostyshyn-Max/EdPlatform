using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdPlatform.Business.Models
{
    public class ExerciseModel
    {
        public int ExerciseId { get; set; }
        public string ExerciseName { get; set; }
        public int Order { get; set; }
        public int LessonId { get; set; }
        public LessonModel Lesson { get; set; }
        public string Discriminator { get; set; }
    }
}
