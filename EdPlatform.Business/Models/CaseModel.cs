using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdPlatform.Business.Models
{
    public class CaseModel
    {
        public int CaseId { get; set; }
        public string CaseName { get; set; }
        public bool IsCorrect { get; set; }
        public int QuizExerciseId { get; set; }
    }
}
