using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdPlatform.Data.Entities
{
    public class Exercise
    {
        [Key]
        public int ExerciseId { get; set; }
        public string ExerciseName { get; set; }
        public int Order { get; set; }
        public int LessonId { get; set; }
        public Lesson Lesson { get; set; }
        public string Discriminator { get; set; }
    }
}
