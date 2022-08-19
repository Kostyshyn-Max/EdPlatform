using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdPlatform.Business.Models
{
    public class AttemptModel
    {
        public int AttemptId { get; set; }
        public int UserId { get; set; }
        public int ExerciseId { get; set; }
        public ExerciseModel Exercise { get; set; }
        public string UserAnswer { get; set; }
        public bool IsCompleted { get; set; }
    }
}
