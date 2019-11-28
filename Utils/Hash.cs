using System.Security.Cryptography;
using System.Text;

namespace HerbShop.Services
{
    public class Hash
    {
        public static string SHA_512(string plain)
        {
            var builder = new StringBuilder();
            using (SHA512 sha = SHA512.Create())
            {
                var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(plain));
                foreach (var b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
            }
            return builder.ToString();
        }
    }
}
