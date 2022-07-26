using EdPlatform.Data.EF;
using EdPlatform.Data.Entities;
using EdPlatform.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
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

        public async Task Create(Quiz entity)
        {
            await _context.Quizzes.AddAsync(entity);
        }

        public async Task<Quiz> Get(int id)
        {
            return await _context.Quizzes.FindAsync(id);
        }

        public async Task<IEnumerable<Quiz>> GetAll()
        {
            return await _context.Quizzes.ToListAsync();
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
