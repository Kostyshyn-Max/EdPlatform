using EdPlatform.Data.EF;
using EdPlatform.Data.Entities;
using EdPlatform.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdPlatform.Data.Repositories
{
    public class QuizRepository : IRepository<Quiz>
    {
        private readonly ApplicationDbContext _context;
        public QuizRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Create(Quiz entity)
        {
            _context.Quizzes.Add(entity);
        }

        public Quiz Get(int id)
        {
            return _context.Quizzes.Find(id);
        }

        public IEnumerable<Quiz> GetAll()
        {
            return _context.Quizzes;
        }

        public void Update(Quiz entity)
        {
            _context.Quizzes.Update(entity);
        }

        public void Delete(int id)
        {
            var entity = _context.Quizzes.Find(id);
            if (entity != null)
                _context.Quizzes.Remove(entity);
        }
    }
}
