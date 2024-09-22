namespace UniversityManagementSystem.DLL.Model
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
