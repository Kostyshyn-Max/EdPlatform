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
    public class CourseUserRepository : ICourseUserRepository
    {
        private readonly ApplicationDbContext _context;

        public CourseUserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Add(CourseUser entity)
        {
            await _context.CourseUsers.AddAsync(entity);
        }

        public async Task<CourseUser?> Get(int id)
        {
            return await _context.CourseUsers.FindAsync(id);
        }

        public async Task<IEnumerable<CourseUser>> GetAll()
        {
            return await _context.CourseUsers.ToListAsync();
        }

        public async Task<IEnumerable<CourseUser>> Find(Expression<Func<CourseUser, bool>> expression)
        {
            return await _context.CourseUsers.Where(expression).ToListAsync();
        }

        public void Update(CourseUser entity)
        {
            _context.CourseUsers.Update(entity);
        }

        public void Remove(int id)
        {
            var entity = _context.CourseUsers.Find(id);
            if (entity != null)
                _context.CourseUsers.Remove(entity);
        }
    }
}
