﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdPlatform.Data.Entities
{
    public class CourseUser
    {
        public int CourseUserId { get; set; }
        public int UserId { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}
