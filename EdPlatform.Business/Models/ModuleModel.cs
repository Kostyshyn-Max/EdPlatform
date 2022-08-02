using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdPlatform.Business.Models
{
    public class ModuleModel
    {
        public int ModuleId { get; set; }
        public string ModuleName { get; set; }
        public int CourseId { get; set; }
        public int Order { get; set; }
    }
}
