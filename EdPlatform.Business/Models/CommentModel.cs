using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdPlatform.Business.Models
{
    public class CommentModel
    {
        public int CommentId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int CourseId { get; set; }
        public string CommentText { get; set; }
        public int RateStarsCount { get; set; }
    }
}
