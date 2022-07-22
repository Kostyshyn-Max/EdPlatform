using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdPlatform.Data.Entities
{
    public class Exercise
    {
#pragma warning disable CS8618
        public int Id { get; set; }

        [Required(ErrorMessage = "String name must not be empty")]
        [StringLength(100, ErrorMessage = "Exercise name is too long")]
        public string ExerciseName { get; set; }
    }
}
