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
    public class IOCaseRepository : IRepository<IOCase>
    {
        private readonly ApplicationDbContext _context;
        public IOCaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Create(IOCase entity)
        {
            _context.IOCases.Add(entity);
        }

        public IOCase Get(int id)
        {
            return _context.IOCases.Find(id);
        }

        public IEnumerable<IOCase> GetAll()
        {
            return _context.IOCases;
        }

        public void Update(IOCase entity)
        {
            _context.IOCases.Update(entity);
        }

        public void Delete(int id)
        {
            var entity = _context.IOCases.Find(id);
            if (entity != null)
                _context.IOCases.Remove(entity);
        }
    }
}
