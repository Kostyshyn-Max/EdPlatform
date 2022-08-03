using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdPlatform.Data.Entities
{
    public class Lesson
    {
        public int LessonId { get; set; }
        public string LessonName { get; set; }
        public string LessonContent { get; set; }
        public int ModuleId { get; set; }
        public string? VideoUrl { get; set; }
        public int Order { get; set; }
        public IEnumerable<Exercise> Exercises { get; set; }
    }
}
