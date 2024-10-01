using System.ComponentModel.DataAnnotations;

namespace WAMS.Backend.Model
{
	public class UserAccountPolicy
	{
		[Key]
		public int Id { get; set; }

		public int UserId { get; set; }

		public string? Policy { get; set; }

		public bool IsEnabled { get; set; }
	}
}