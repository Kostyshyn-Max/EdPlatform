using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdPlatform.Data.Entities
{
    public class Case
    {
#pragma warning disable CS8618
        public int CaseId { get; set; }
        [Required(ErrorMessage = "Case name must be not empty")]
        public string CaseName { get; set; }
        public bool IsCorrect { get; set; }
    }
}
