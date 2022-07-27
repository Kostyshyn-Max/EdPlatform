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
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Add(User entity)
        {
            await _context.Users.AddAsync(entity);
        }

        public async Task<User?> Get(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<IEnumerable<User>> Find(Expression<Func<User, bool>> expression)
        {
            return await _context.Users.Where(expression).ToListAsync();
        }

        public void Update(User entity)
        {
            _context.Users.Update(entity);  
        }

        public void Remove(int id)
        {
            var entity = _context.Users.Find(id);
            if (entity != null)
                _context.Users.Remove(entity);
        }
    }
}
