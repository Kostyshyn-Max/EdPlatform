﻿using EdPlatform.Data.EF;
using EdPlatform.Data.Entities;
using EdPlatform.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdPlatform.Data.Repositories
{
    public class CourseRepository : IRepository<Course>
    {
        private readonly ApplicationDbContext _context;

        public CourseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Create(Course entity)
        {
            await _context.Courses.AddAsync(entity);
        }

        public async Task<Course> Get(int id)
        {
            return await _context.Courses.FindAsync(id);
        }

        public async Task<IEnumerable<Course>> GetAll()
        {
            return _context.Courses;
        }

        public void Update(Course entity)
        {
            _context.Courses.Update(entity);
        }

        public void Delete(int id)
        {
            var entity = _context.Courses.Find(id);
            if (entity != null)
                _context.Courses.Remove(entity);
        }
    }
}