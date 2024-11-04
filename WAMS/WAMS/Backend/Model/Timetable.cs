using WAMS.Components.Model;

namespace WAMS.Backend.Model
{
    public class Timetable
    {
        public int TimetableId { get; set; }

        public ICollection<Week> Weeks { get; set; } = new List<Week>();

        public Class? Class { get; set; }

        public int ClassId { get; set; }
    }
}
