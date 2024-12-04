using Microsoft.AspNetCore.Identity;
using MySQLIdentityWebAPI.Data;
using MySQLIdentityWebAPI.Models.Identity;

namespace MySQLIdentityWebAPI.Services
{
    public class ApiKeyService
    {
        private readonly ApplicationDBContext _context;

        public ApiKeyService(ApplicationDBContext context)
        {
            _context = context;
        }

        //TOD: Encrypt -> https://jonathancrozier.com/blog/how-to-generate-a-cryptographically-secure-random-string-in-dot-net-with-c-sharp
        public UserApiKey CreateApiKey(IdentityUser user)
        {
            var newApiKey = new UserApiKey
            {
                User = user,
                Value = GenerateApiKeyValue()
            };

            _context.UserApiKeys.Add(newApiKey);

            _context.SaveChanges();

            return newApiKey;
        }

        private string GenerateApiKeyValue() =>
            $"{Guid.NewGuid().ToString()}-{Guid.NewGuid().ToString()}";
    }
}
