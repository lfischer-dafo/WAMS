using System.ComponentModel.DataAnnotations;
using WAMS.Backend.Model;

namespace WAMS.Components.Model
{
    public class Class
    {
        [Key]
        public int ClassId { get; set; }
        public string? Name { get; set; }
        public string? Year { get; set; }
        public string? Description { get; set; }
        public ICollection<User>? Students { get; set; }
        public Timetable? Timetable { get; set; }

    }
}
