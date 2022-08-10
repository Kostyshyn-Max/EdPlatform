using EdPlatform.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdPlatform.Business.Services
{
    public interface ICodeExecutingService
    {
        Task<bool[]> ExecuteCode(CodeModel codeModel);
    }
}
