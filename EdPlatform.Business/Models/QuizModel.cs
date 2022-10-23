using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdPlatform.Business.Models
{
    public class QuizModel : ExerciseModel
    {
        public IEnumerable<CaseModel> Cases { get; set; }
    }
}
