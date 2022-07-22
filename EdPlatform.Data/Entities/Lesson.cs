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
#pragma warning disable CS8618
        public int LessonId { get; set; }

        [Required(ErrorMessage = "Lesson name must not be empty")]
        [StringLength(100)]
        public string LessonName { get; set; }

        [Required(ErrorMessage = "Lesson content must not be empty")]
        public string LessonContent { get; set; }

        public string? VideoUrl { get; set; }
        public IEnumerable<Exercise> Exercises { get; set; }
    }
}
