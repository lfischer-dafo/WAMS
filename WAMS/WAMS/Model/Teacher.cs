using System.ComponentModel.DataAnnotations;
using WAMS.Components.Model;

namespace WAMS.Model
{
   public class Teacher
   {

      [Key]
      public int UserId { get; set; }
      public string? Username { get; set; }
      public byte[]? Password { get; set; } //MD5
      public string? FirstName { get; set; }
      public string? LastName { get; set; }
      public DateTime? LastLogin { get; set; }
      public Status Status { get; set; } = Status.Present;
      public Room? Room { get; set; }
      public int RoomId { get; set; }
      public string? MailAdress { get; set; }
      public ICollection<Class>? Classes { get; set; }
   }
}
