using System.ComponentModel.DataAnnotations;
using WAMS.Backend.Model;

namespace WAMS.Components.Model
{
    public class Week
    {
        [Key]
        public int WeekId { get; set; }
        public int CalendarWeek {  get; set; }
        public ICollection<Day>? Days { get; set; }
        public Timetable? Timetable { get; set; }
        public int TimetableId { get; set; }
    }
}
