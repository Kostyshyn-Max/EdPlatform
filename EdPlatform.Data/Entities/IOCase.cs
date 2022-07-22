using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdPlatform.Data.Entities
{
    public class IOCase
    {
#pragma warning disable CS8618
        public int IOCaseId { get; set; }
        public string InputData { get; set; }
        [Required(ErrorMessage = "Output data must be not empty")]
        public string OutputData { get; set; }
    }
}
