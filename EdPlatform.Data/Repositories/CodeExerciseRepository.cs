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
    public class CodeExerciseRepository : IRepository<CodeExercise>
    {
        private readonly ApplicationDbContext _context;

        public CodeExerciseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Create(CodeExercise entity)
        {
            await _context.CodeExercises.AddAsync(entity);
        }

        public async Task<CodeExercise> Get(int id)
        {
            return await _context.CodeExercises.FindAsync(id);
        }

        public async Task<IEnumerable<CodeExercise>> GetAll()
        {
            return await _context.CodeExercises.ToListAsync();
        }

        public void Update(CodeExercise entity)
        {
            _context.CodeExercises.Update(entity);
        }

        public void Delete(int id)
        {
            var entity = _context.CodeExercises.Find(id);
            if (entity != null)
                _context.CodeExercises.Remove(entity);
        }
    }
}
