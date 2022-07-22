using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdPlatform.Data.Entities
{
    public class Course
    {
#pragma warning disable CS8618
        public int CourseId { get; set; }
        [Required(ErrorMessage = "Course name must be not empty!")]
        [StringLength(50, ErrorMessage = "Course name is too long")]

        public string CourseName { get; set; }
        [Required(ErrorMessage = "Course description must be not empty!")]
        [StringLength(1000, ErrorMessage = "Course description is too long")]

        public string Description { get; set; }
        public Categories Category { get; set; }
        public IEnumerable<Module> Modules { get; set; }
        public int AuthorId { get; set; }
    }
}
