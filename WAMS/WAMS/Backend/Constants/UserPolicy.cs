namespace WAMS.Backend.Constants
{
   public class UserPolicy
   {
		public const string USER = "USER";
		public const string TEACHER = "TEACHER";
		public const string ADMIN = "ADMIN";
		public static List<string> GetPolicies()
		{
			return new List<string>
			{
				USER, TEACHER, ADMIN
			};
		}
	}
}
