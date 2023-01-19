using EdPlatform.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdPlatform.Business.Services
{
    public interface ICommentService
    {
        Task Create(CommentModel comment);
        Task<List<CommentModel>> GetAllByCourseId(int courseId);
        Task<List<CommentModel>> GetAllByUserId(int userId);
        Task<CommentModel> GetById(int id);
        Task Edit(CommentModel comment);
        Task Delete(int id);
    }
}
