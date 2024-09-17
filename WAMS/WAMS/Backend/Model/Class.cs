using System.ComponentModel.DataAnnotations;

namespace WAMS.Components.Model
{
    public class Class
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Year { get; set; }
        public string? Description { get; set; }
        public ICollection<User>? Students { get; set; }
        public Timetable? Timetable { get; set; }

    }
}
