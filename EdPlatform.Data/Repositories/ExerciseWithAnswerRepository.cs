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
    public class ExerciseWithAnswerRepository : IRepository<ExerciseWithAnswer>
    {
        private readonly ApplicationDbContext _context;
        public ExerciseWithAnswerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Create(ExerciseWithAnswer entity)
        {
            _context.ExercisesWithAnswer.Add(entity);
        }

        public ExerciseWithAnswer Get(int id)
        {
            return _context.ExercisesWithAnswer.Find(id);
        }

        public IEnumerable<ExerciseWithAnswer> GetAll()
        {
            return _context.ExercisesWithAnswer;
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
