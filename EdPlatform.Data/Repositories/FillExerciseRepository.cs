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
    public class FillExerciseRepository : IFillExerciseRepository
    {
        private readonly ApplicationDbContext _context;
        public FillExerciseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Add(FillExercise entity)
        {
            await _context.FillExercises.AddAsync(entity);
        }

        public async Task<FillExercise?> Get(int id)
        {
            return await _context.FillExercises.FindAsync(id);
        }

        public async Task<IEnumerable<FillExercise>> GetAll()
        {
            return await _context.FillExercises.ToListAsync();
        }

        public async Task<IEnumerable<FillExercise>> Find(Expression<Func<FillExercise, bool>> expression)
        {
            return await _context.FillExercises.Where(expression).ToListAsync();
        }

        public void Update(FillExercise entity)
        {
            _context.FillExercises.Update(entity);
        }

        public void Remove(int id)
        {
            var entity = _context.FillExercises.Find(id);
            if (entity != null)
                _context.FillExercises.Remove(entity);
        }
    }
}
