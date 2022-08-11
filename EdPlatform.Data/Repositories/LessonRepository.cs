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
            return await _context.Lessons.Where(x => x.LessonId == id)
                .Include(x => x.Module).ThenInclude(x => x.Course).ThenInclude(x => x.Modules).ThenInclude(x => x.Lessons)
                .Include(x => x.Module).ThenInclude(x => x.Lessons)
                .Include(x => x.Exercises).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Lesson>> GetAll()
        {
            return await _context.Lessons
                .Include(x => x.Module).ThenInclude(x => x.Course).ThenInclude(x => x.Modules).ThenInclude(x => x.Lessons)
                .Include(x => x.Module).ThenInclude(x => x.Lessons)
                .Include(x => x.Exercises).ToListAsync();
        }

        public async Task<IEnumerable<Lesson>> Find(Expression<Func<Lesson, bool>> expression)
        {
            return await _context.Lessons.Where(expression)
                .Include(x => x.Module).ThenInclude(x => x.Course).ThenInclude(x => x.Modules).ThenInclude(x => x.Lessons)
                .Include(x => x.Module).ThenInclude(x => x.Lessons)
                .Include(x => x.Exercises).ToListAsync();
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
