using System.ComponentModel.DataAnnotations;

namespace WAMS.Backend.Model
{
    public class Class
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Year { get; set; }
        public string? Description { get; set; }
        public List<Student>? Students { get; set; }
        public Timetable? Timetable { get; set; }

    }
}
