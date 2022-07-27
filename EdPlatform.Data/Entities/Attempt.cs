using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdPlatform.Data.Entities
{
    public class Attempt
    {
        [Key]
        public int UserId { get; set; }
        public int ExerciseId { get; set; }
        public string UserAnswer { get; set; }
        public bool IsCompleted { get; set; }
    }
}
