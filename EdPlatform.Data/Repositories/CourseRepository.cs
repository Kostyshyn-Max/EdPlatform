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
    public class CourseRepository : ICourseRepository
    {
        private readonly ApplicationDbContext _context;

        public CourseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Add(Course entity)
        {
            await _context.Courses.AddAsync(entity);
        }

        public async Task<Course?> Get(int id)
        {
            return await _context.Courses.Where(x => x.CourseId == id).Include(x => x.Category).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Course>> GetAll()
        {
            return await _context.Courses.Include(x => x.Category).ToListAsync();
        }

        public async Task<IEnumerable<Course>> Find(Expression<Func<Course, bool>> expression)
        {
            return await _context.Courses.Where(expression).Include(x => x.Category).ToListAsync();
        }

        public void Update(Course entity)
        {
            _context.Courses.Update(entity);
        }

        public void Remove(int id)
        {
            var entity = _context.Courses.Find(id);
            if (entity != null)
                _context.Courses.Remove(entity);
        }
    }
}
