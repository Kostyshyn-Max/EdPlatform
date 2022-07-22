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
    public class CodeExerciseRepository : IRepository<CodeExercise>
    {
        private readonly ApplicationDbContext _context;

        public CodeExerciseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Create(CodeExercise entity)
        {
            _context.CodeExercises.Add(entity);
        }

        public CodeExercise Get(int id)
        {
            return _context.CodeExercises.Find(id);
        }

        public IEnumerable<CodeExercise> GetAll()
        {
            return _context.CodeExercises;
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
