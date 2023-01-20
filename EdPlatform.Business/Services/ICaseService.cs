using EdPlatform.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdPlatform.Business.Services
{
    public interface ICaseService
    {
        Task Create(CaseModel quizCase);
        Task<CaseModel> Get(int id);
        Task Edit(CaseModel quizCase);
        Task Delete(int id);
    }
}
