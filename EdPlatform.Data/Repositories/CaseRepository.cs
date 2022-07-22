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
    public class CaseRepository : IRepository<Case>
    {
        private readonly ApplicationDbContext _context;

        public CaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Create(Case entity)
        {
            _context.Cases.Add(entity);
        }

        public Case Get(int id)
        {
            return _context.Cases.Find(id);
        }

        public IEnumerable<Case> GetAll()
        {
            return _context.Cases;
        }

        public void Update(Case entity)
        {
            _context.Cases.Update(entity);
        }

        public void Delete(int id)
        {
            var entity = _context.Cases.Find(id);
            if (entity != null)
                _context.Cases.Remove(entity);
        }
    }
}
