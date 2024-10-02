using System.ComponentModel.DataAnnotations;
using WAMS.Components.Model;

namespace WAMS.Backend.Model
{
    public class UserPolicy
    {
        [Key]
        public int UserPolicyId { get; set; }

        public User User { get; set; }
        public int UserId { get; set; }

        public string? Policy { get; set; }

        public bool IsEnabled { get; set; }
    }
}