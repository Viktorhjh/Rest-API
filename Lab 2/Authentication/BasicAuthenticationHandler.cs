using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace Lab_2.Authentication
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return Task.FromResult(
                    AuthenticateResult.Fail("Missing Authorization Key"
                    ));
            }
            
            var authorizationHeader = Request.Headers["Authorization"].ToString();
            if (!authorizationHeader.StartsWith("Basic", StringComparison.OrdinalIgnoreCase))
            {
                return Task.FromResult(
                    AuthenticateResult.Fail("Authorization Key Is Not 'Basic'"
                    ));
            }

            var authBase64Decode = Encoding.UTF8.GetString(
                Convert.FromBase64String(
                    authorizationHeader.Replace("Basic", "", StringComparison.OrdinalIgnoreCase
                    ))
            );

            var authSplit = authBase64Decode.Split(new[] { ':' }, 2);

            if(authSplit.Length != 2 ) 
            {
                return Task.FromResult(
                   AuthenticateResult.Fail("Invalid Authorization Key header format"
                   ));
            }

            var clientId = authSplit[0];
            var clientSecret = authSplit[1];
            
            if(clientId != "admin" ||  clientSecret != "admin34") 
            {
                return Task.FromResult(
                   AuthenticateResult.Fail("The secret is not correct"
                   ));
            }

            var client = new BasicAuthenticationClient
            {
                AuthenticationType = "Basic",
                IsAuthenticated = true,
                Name = clientId
            };

            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(client, new[]
            {
                new Claim(ClaimTypes.NameIdentifier, clientId)
            }));

            return Task.FromResult(
                   AuthenticateResult.Success(
                       new AuthenticationTicket( claimsPrincipal, Scheme.Name)
            ));
            
        }
    }
}
