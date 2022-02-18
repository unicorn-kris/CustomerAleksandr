using System.Collections.Generic;

namespace Repository.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public List<Product> Products { get; set; } = new List<Product>();
    }
}
