using Microsoft.AspNetCore.Identity;

namespace Identity.Net.Simple.Models.DB
{
    public sealed class User : IdentityUser
    {
        // Add properties required in your project domain user

        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
