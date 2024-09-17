using System.ComponentModel.DataAnnotations;

namespace WAMS.Components.Model
{
    public class Course
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public Teacher? Teacher { get; set; }
        public Room? Room { get; set; }
        public Class? Class { get; set; }
    }
}
