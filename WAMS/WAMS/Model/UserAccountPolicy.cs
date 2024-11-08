using System.ComponentModel.DataAnnotations;
using WAMS.Components.Model;

namespace WAMS.Backend.Model
{
    public class UserAccountPolicy
    {
        [Key]
        public int UserPolicyId { get; set; }

        public Student User { get; set; }
        public int UserId { get; set; }

        public string? Policy { get; set; }

        public bool IsEnabled { get; set; }
    }
}