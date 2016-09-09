using System.Collections.Generic;

namespace WallProject.Models
{
    public class NewsCategory
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public ICollection<News> News { get; set; }
    }
}