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
    public class LessonRepository : IRepository<Lesson>
    {
        private readonly ApplicationDbContext _context;
        public LessonRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Create(Lesson entity)
        {
            _context.Lessons.Add(entity);
        }

        public Lesson Get(int id)
        {
            return _context.Lessons.Find(id);
        }

        public IEnumerable<Lesson> GetAll()
        {
            return _context.Lessons;
        }

        public void Update(Lesson entity)
        {
            _context.Lessons.Update(entity);
        }

        public void Delete(int id)
        {
            var entity = _context.Lessons.Find(id);
            if (entity != null)
                _context.Lessons.Remove(entity);
        }
    }
}
