using Microsoft.IdentityModel.Tokens;
using System;
using System.Threading.Tasks;

namespace BusinessLogicShared.Security
{
    public class JwtSettings
    {
        public string Secret { get; set; }
        public string Refresh { get; set; }
        public int TokenLifeTime { get; set; }

        public string Issuer { get; set; }

        public string Subject { get; set; }

        public string Audience { get; set; }

        public DateTime Expiration => IssuedAt.Add(ValidFor);

        public DateTime NotBefore => DateTime.UtcNow;

        public DateTime IssuedAt => DateTime.UtcNow;

        public TimeSpan ValidFor { get; set; } = TimeSpan.FromMinutes(120);

        /// <summary>
        /// "jti" (JWT ID) Claim (default ID is a GUID)
        /// </summary>
        public Func<Task<string>> JtiGenerator { get =>
           () => Task.FromResult(Guid.NewGuid().ToString()); }

        /// <summary>
        /// The signing key to use when generating tokens.
        /// </summary>
        public SigningCredentials SigningCredentials { get; set; }
    }

    public class SeriglogSettings
    {
        public string TableName { get; set; }
        public int BatchPostingLimit { get; set; }
        public string MinimumLevel { get; set; }
    }
}
