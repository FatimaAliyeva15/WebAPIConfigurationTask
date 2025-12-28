
using Microsoft.AspNetCore.Identity;

namespace WebApiConfigurations.Entities.UserModel
{
    public class AppUser<Guid>: IdentityUser
    {
        public string FullName { get; set; }
    }
}
