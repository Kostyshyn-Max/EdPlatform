using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdPlatform.Business.Models
{
    public class CodeExerciseModel : ExerciseModel
    {
        public string Condition { get; set; }
        public IEnumerable<IOCaseModel> IOCases { get; set; }
    }
}
