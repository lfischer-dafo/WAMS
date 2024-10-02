using System.ComponentModel.DataAnnotations;
using System.Globalization;
using WAMS.Components.Model;

namespace WAMS.Backend.Model
{
	public class Day
	{
		[Key]
		public int DayId { get; set; }
		public Week? Week { get; set; }
		public int WeekId { get; set; }
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