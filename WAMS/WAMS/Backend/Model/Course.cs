using System.ComponentModel.DataAnnotations;

namespace WAMS.Components.Model
{
   public class Course
   {
      [Key]
      public int CourseId { get; set; }
      public string? Name { get; set; }
      public TimeOnly StartTime { get; set; }
      public TimeOnly EndTime { get; set; }
      public User? Teacher { get; set; }
      public int TeacherId { get; set; }
      public Room? Room { get; set; }
      public int RoomId { get; set; }
      public Class? Class { get; set; }
      public int ClassId { get; set; }
   }
}
