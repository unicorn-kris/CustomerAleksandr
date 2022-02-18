using System.Collections.Generic;

namespace Repository.Entities
{
    public class Product
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int Price { get; set; }

        public int Count { get; set; }

        public List<User> Users { get; set; } = new List<User>();
    }
}
