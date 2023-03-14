using Microsoft.AspNetCore.Identity;

namespace FrontToBack2.Helpers
{
    public class CustomIdentityErrorDescriber : IdentityErrorDescriber
    {
        public override IdentityError DuplicateUserName(string userName)
        {
            return new IdentityError
            {
                Code = nameof(DuplicateUserName),
                Description = $"Login '{userName}'artiq movcuddur..."
            };
        }
    }
}
