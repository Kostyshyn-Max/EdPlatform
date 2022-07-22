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
    public class AtemptRepository : IRepository<Atempt>
    {
        private readonly ApplicationDbContext _context;

        public AtemptRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Create(Atempt entity)
        {
            _context.Atempts.Add(entity);
        }
        
        public Atempt Get(int id)
        {
            return _context.Atempts.Find(id);
        }

        public IEnumerable<Atempt> GetAll()
        {
            return _context.Atempts;
        }

        public void Update(Atempt entity)
        {
            _context.Atempts.Update(entity);
        }

        public void Delete(int id)
        {
            var entity = _context.Atempts.Find(id);
            if (entity != null)
                _context.Atempts.Remove(entity);
        }
    }
}
