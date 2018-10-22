using System;
using System.Collections.Generic;

namespace CoreService.Models
{
    public partial class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? CategoryId { get; set; }
        public DateTime? CreatedDate { get; set; }

        public Category Category { get; set; }
    }
}
