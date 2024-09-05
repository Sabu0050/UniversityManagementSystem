namespace UniversityManagementSystem.DLL.Model
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; } = false;
        public ICollection<CategoryProduct> CategoryProducts { get; set; } = new List<CategoryProduct>();
    }
}
