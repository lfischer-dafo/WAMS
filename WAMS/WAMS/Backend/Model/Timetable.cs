using System.ComponentModel.DataAnnotations;
using WAMS.Backend.Model;

namespace WAMS.Components.Model
{
    public class Timetable
    {
        [Key]
        public int Id { get; set; }
        public ICollection<Day>? Days { get; set; }
    }
}
