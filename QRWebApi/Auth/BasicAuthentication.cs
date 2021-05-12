using System;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using QRWebApi.Models;

namespace QRWebApi.Auth
{
    public class BasicAuthentication: AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly QRappContext _context;

        public BasicAuthentication(
            IOptionsMonitor<AuthenticationSchemeOptions> options, 
            ILoggerFactory logger, 
            UrlEncoder encoder, 
            ISystemClock clock,
            QRappContext context) 
            : base(options, logger, encoder, clock)
        {
            _context = context;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (Equals(!Request.Headers.ContainsKey("Authorization")))
                return AuthenticateResult.Fail("Authorization header not found");

            try
            {
                var _authenticationHeaderValue = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);

                var bytes = Convert.FromBase64String(_authenticationHeaderValue.Parameter);

                string[] credensial = Encoding.UTF8.GetString(bytes).Split(":");

                string login = credensial[0];
                string passw = credensial[1];

                User user = _context.Users.Where(user => user.Login == login && user.Password == passw).FirstOrDefault();

                if(user == null)
                    return AuthenticateResult.Fail("");
                else
                {
                    var claims = new[] {new Claim(ClaimTypes.Name, user.Login)};
                    var identity = new ClaimsIdentity(claims, Scheme.Name);
                    var principal = new ClaimsPrincipal(identity);
                    var ticket = new AuthenticationTicket(principal, Scheme.Name);

                    return AuthenticateResult.Success(ticket);
                }

            }
            catch (Exception e)
            {
                return AuthenticateResult.Fail("");

            }

            return AuthenticateResult.Fail("");
        }
    }
}
