using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdPlatform.Business.Models
{
    public class QuizAnswerCheckModel
    {
        public int SelectedCaseId { get; set; }
        public bool Result { get; set; }
        public int UserId { get; set; }
        public int ExerciseId { get; set; }
    }
}
