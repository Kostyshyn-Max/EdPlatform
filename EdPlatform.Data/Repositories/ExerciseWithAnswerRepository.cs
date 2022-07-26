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
    public class ExerciseWithAnswerRepository : IRepository<ExerciseWithAnswer>
    {
        private readonly ApplicationDbContext _context;
        public ExerciseWithAnswerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Create(ExerciseWithAnswer entity)
        {
            await _context.ExercisesWithAnswer.AddAsync(entity);
        }

        public async Task<ExerciseWithAnswer> Get(int id)
        {
            return await _context.ExercisesWithAnswer.FindAsync(id);
        }

        public async Task<IEnumerable<ExerciseWithAnswer>> GetAll()
        {
            return await _context.ExercisesWithAnswer.ToListAsync();
        }

        public void Update(ExerciseWithAnswer entity)
        {
            _context.ExercisesWithAnswer.Update(entity);
        }

        public void Delete(int id)
        {
            var entity = _context.ExercisesWithAnswer.Find(id);
            if (entity != null)
                _context.ExercisesWithAnswer.Remove(entity);
        }
    }
}
