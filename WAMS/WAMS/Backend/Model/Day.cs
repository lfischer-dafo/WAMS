using System.ComponentModel.DataAnnotations;
using WAMS.Components.Model;

namespace WAMS.Backend.Model
{
	public class Day
	{
		[Key]
		public int Id { get; set; }
		public Timetable? Timetable { get; set; }
		public ICollection<Course>? Courses { get; set; }
		public Weekday Weekday { get; set; }

	}

	public enum Weekday
	{
		Monday,
		Tuesday,
		Wednesday,
		Thursday,
		Friday,
		Saturday,
		Sunday
	}
}