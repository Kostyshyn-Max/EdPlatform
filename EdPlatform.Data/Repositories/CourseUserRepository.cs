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
    public class CourseUserRepository : IRepository<CourseUser>
    {
        private readonly ApplicationDbContext _context;

        public CourseUserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Create(CourseUser entity)
        {
            _context.CourseUsers.Add(entity);
        }

        public CourseUser Get(int id)
        {
            return _context.CourseUsers.Find(id);
        }

        public IEnumerable<CourseUser> GetAll()
        {
            return _context.CourseUsers;
        }

        public void Update(CourseUser entity)
        {
            _context.CourseUsers.Update(entity);
        }

        public void Delete(int id)
        {
            var entity = _context.CourseUsers.Find(id);
            if (entity != null)
                _context.CourseUsers.Remove(entity);
        }
    }
}
