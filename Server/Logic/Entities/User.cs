using System.Collections.Generic;

namespace Logic.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public List<Product> Products { get; set; }
    }
}
