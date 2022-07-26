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
    public class UserRepository : IRepository<User>
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Create(User entity)
        {
            await _context.Users.AddAsync(entity);
        }

        public async Task<User> Get(int id)
        {
            var user = await _context.Users.FindAsync(id);
            return user;
        }

        public async Task<User> Get(string login)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Login == login);
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _context.Users.ToListAsync();
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
