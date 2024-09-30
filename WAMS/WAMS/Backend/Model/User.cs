using System.ComponentModel.DataAnnotations;

namespace WAMS.Components.Model
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? LastLogin { get; set; }
        public Status Status { get; set; } = Status.Present;
        public Room? BookedRoom { get; set; }
        public Room? CurrentRoom { get; set; }
        public string? MailAdress { get; set; }
        public ICollection<Class>? Classes { get; set; }
        public bool IsAdmin { get; set; }

    }
    public enum Status
    {
        Missing = 0,
        Sick = 1,
        Present = 2,
    }
}
