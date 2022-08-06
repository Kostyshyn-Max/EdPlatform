﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdPlatform.Business.Models
{
    public class CourseViewModel
    {
        public int CourseId { get; set; } 
        public string CourseName { get; set; }
        public string Description { get; set; }
        public CategoryModel Category { get; set; }
        public int AuthorId { get; set; }
        public IEnumerable<ModuleModel> Modules { get; set; }
        public int UsersJoined { get; set; }
        public IFormFile Image { get; set; }
        public string ImageName { get; set; }
    }
}
