using System.ComponentModel.DataAnnotations;

namespace WAMS.Components.Model
{
    public class User
    {
        [Key]
        public int ID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? LastLogin { get; set; }
        public Status Status { get; set; } = Status.Present;
        public Room? BookedRoom { get; set; }
        public Room? CurrentRoom { get; set; }
        public string? MailAdress { get; set; }

    }
    public enum Status
    {
        Missing = 0,
        Sick = 1,
        Present = 2,
    }
}
