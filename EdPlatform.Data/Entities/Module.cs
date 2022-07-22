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
#pragma warning disable CS8618
        public int ModuleId { get; set; }

        [Required(ErrorMessage = "Module name must not be empty")]
        [StringLength(100)]
        public string ModuleName { get; set; }

        public IEnumerable<Lesson> Lessons { get; set; }
        public int Order { get; set; }
    }
}
