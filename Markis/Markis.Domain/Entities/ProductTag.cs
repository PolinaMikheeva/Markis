namespace Markis.Domain.Entities
{
    public class ProductTag
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Product> Products { get; set; }
    }
}
