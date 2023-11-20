using Microsoft.AspNetCore.Authorization;

namespace Lab_2.Authentication
{
    public class BasicAuthorizationAttribute : AuthorizeAttribute
    {
        public BasicAuthorizationAttribute() 
        {
            AuthenticationSchemes = "Basic";
        }
    }
}
