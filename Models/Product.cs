namespace amazen.Models
{
    public class Product
    {

        public int Id { get; set; }
        public string Name { get; set; }

        public int? Price { get; set; }

        public string Description { get; set; }

        public string CreatorId { get; set; }

        public Profile Creator { get; set; }

    }

}