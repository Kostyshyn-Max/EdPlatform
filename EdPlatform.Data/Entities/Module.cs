using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdPlatform.Data.Entities
{
    public class Module
    {
        public int ModuleId { get; set; }
        public string ModuleName { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public int Order { get; set; }
        public IEnumerable<Lesson> Lessons { get; set; }
    }
}
