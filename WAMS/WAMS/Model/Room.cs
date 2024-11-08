using System.ComponentModel.DataAnnotations;

namespace WAMS.Components.Model
{
    public class Room
    {
        [Key]
        public int RoomId { get; set; }
        public string? Name { get; set; }
        public int Floor { get; set; }
        public ICollection<Course>? Courses { get; set; }
        public ICollection<Student>? Users { get; set; }
    }
}
