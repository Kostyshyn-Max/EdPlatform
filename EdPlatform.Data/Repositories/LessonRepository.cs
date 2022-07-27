using EdPlatform.Data.EF;
using EdPlatform.Data.Entities;
using EdPlatform.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EdPlatform.Data.Repositories
{
    public class LessonRepository : ILessonRepository
    {
        private readonly ApplicationDbContext _context;
        public LessonRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Add(Lesson entity)
        {
            await _context.Lessons.AddAsync(entity);
        }

        public async Task<Lesson?> Get(int id)
        {
            return await _context.Lessons.FindAsync(id);
        }

        public async Task<IEnumerable<Lesson>> GetAll()
        {
            return await _context.Lessons.ToListAsync();
        }

        public async Task<IEnumerable<Lesson>> Find(Expression<Func<Lesson, bool>> expression)
        {
            return await _context.Lessons.Where(expression).ToListAsync();
        }

        public void Update(Lesson entity)
        {
            _context.Lessons.Update(entity);
        }

        public void Remove(int id)
        {
            var entity = _context.Lessons.Find(id);
            if (entity != null)
                _context.Lessons.Remove(entity);
        }
    }
}
