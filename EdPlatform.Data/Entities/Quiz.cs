using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdPlatform.Data.Entities
{
    public class Quiz : Exercise
    {
#pragma warning disable CS8618
        [Required(ErrorMessage = "Condition must be not empty")]
        public string Condition { get; set; }
        public IEnumerable<Case> Cases { get; set; }
    }
}
