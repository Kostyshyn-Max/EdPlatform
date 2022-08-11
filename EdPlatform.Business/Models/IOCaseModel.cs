using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdPlatform.Business.Models
{
    public class IOCaseModel
    {
        public int IOCaseId { get; set; }
        public string? InputData { get; set; }
        public string OutputData { get; set; }
        public int CodeExerciseExerciseId { get; set; }
    }
}
