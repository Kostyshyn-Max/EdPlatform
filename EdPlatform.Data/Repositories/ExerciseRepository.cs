using EdPlatform.Data.EF;
using EdPlatform.Data.Entities;
using EdPlatform.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EdPlatform.Data.Repositories
{
    public class ExerciseRepository : IExerciseRepository
    {
        private readonly ApplicationDbContext _context;
        public ExerciseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task Add(Exercise entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Exercise>> Find(Expression<Func<Exercise, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public async Task<Exercise?> Get(int id)
        {
            return await _context.Exercise.FindAsync(id);
        }

        public Task<IEnumerable<Exercise>> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            var entity = _context.Exercise.Find(id);
            if (entity != null)
                _context.Exercise.Remove(entity);
        }

        public void Update(Exercise entity)
        {
            throw new NotImplementedException();
        }
    }
}
