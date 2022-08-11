using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdPlatform.Business.Models
{
    public class CodeModel
    {
        public List<string>? InputDatas { get; set; }
        public List<string> OutputDatas { get; set; } 
        public string Code { get; set; }
        public string Language { get; set; }
        public string VersionIndex { get; set; }
    }
}
