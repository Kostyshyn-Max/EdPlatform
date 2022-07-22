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
    public class UserRepository : IRepository<User>
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Create(User entity)
        {
            _context.Users.Add(entity);
        }

        public User Get(int id)
        {
            return _context.Users.Find(id);
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users;
        }


        public void Update(User entity)
        {
            _context.Users.Update(entity);  
        }

        public void Delete(int id)
        {
            var entity = _context.Users.Find(id);
            if (entity != null)
                _context.Users.Remove(entity);
        }
    }
}
