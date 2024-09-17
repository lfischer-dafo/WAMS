using Microsoft.AspNetCore.Identity;
using WAMS.Components.Model;

namespace WAMS.Components
{
   public class IdentityUserAccessor(UserManager<User> userManager)
   {
      public async Task<User> GetRequiredUserAsync(HttpContext context)
      {
         var user = await userManager.GetUserAsync(context.User);
         if (user != null)
         {
            return user;
         }
         else
         {
            throw new InvalidOperationException();
         }
      }
   }
}
