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
    public class CodeExerciseRepository : ICodeExerciseRepository
    {
        private readonly ApplicationDbContext _context;

        public CodeExerciseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Add(CodeExercise entity)
        {
            await _context.CodeExercises.AddAsync(entity);
        }

        public async Task<CodeExercise?> Get(int id)
        {
            return await _context.CodeExercises.Where(x => x.ExerciseId == id)
                .Include(x => x.Lesson).ThenInclude(x => x.Module).ThenInclude(x => x.Course)
                .Include(x => x.IOCases)
                .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<CodeExercise>> GetAll()
        {
            return await _context.CodeExercises
                .Include(x => x.Lesson).ThenInclude(x => x.Module).ThenInclude(x => x.Course)
                .Include(x => x.IOCases)
                .ToListAsync();
        }

        public async Task<IEnumerable<CodeExercise>> Find(Expression<Func<CodeExercise, bool>> expression)
        {
            return await _context.CodeExercises.Where(expression)
                .Include(x => x.Lesson).ThenInclude(x => x.Module).ThenInclude(x => x.Course)
                .Include(x => x.IOCases)
                .ToListAsync();
        }

        public void Update(CodeExercise entity)
        {
            _context.CodeExercises.Update(entity);
        }

        public void Remove(int id)
        {
            var entity = _context.CodeExercises.Find(id);
            if (entity != null)
                _context.CodeExercises.Remove(entity);
        }
    }
}
