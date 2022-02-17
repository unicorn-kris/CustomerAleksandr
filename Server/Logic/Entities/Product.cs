using System.Collections.Generic;

namespace Logic.Entities
{
    public class Product
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int Price { get; set; }

        public List<User> Users { get; set; }

        public int Count { get; set; }
    }
}
