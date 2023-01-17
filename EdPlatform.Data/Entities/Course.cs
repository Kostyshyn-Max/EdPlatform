using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdPlatform.Data.Entities
{
    public class Course
    {
#pragma warning disable CS8618
        public int CourseId { get; set; }
        [Required]
        public string CourseName { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public IEnumerable<Module> Modules { get; set; }
        public int AuthorId { get; set; }
        public int UsersJoined { get; set; }
        public string ImageName { get; set; }
    }
}
