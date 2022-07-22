using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdPlatform.Data.Entities
{
    public class ExerciseWithAnswer : Exercise
    {
#pragma warning disable CS8618
        [Required]
        public string Condition { get; set; }
        [Required]
        public string Answer { get; set; }
    }
}
