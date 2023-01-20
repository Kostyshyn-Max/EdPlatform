using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdPlatform.Data.Entities
{
    public class Comment
    {
#pragma warning disable CS8618
        public int CommentId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public string CommentText { get; set; }
        public int RateStarsCount { get; set; }
    }
}
