using Shop.Services.Abstract;
using static BCrypt.Net.BCrypt;

namespace Shop.Services
{
    public class BcryptHasher : ICryptoService
    {
        public string EncryptPassword(string password)
        {
            return HashPassword(password, BCrypt.Net.SaltRevision.Revision2);
        }

        public bool VerifyPassword(string password, string passwordCandidate)
        {
            return Verify(passwordCandidate, password);
        }
    }
}
