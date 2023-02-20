using System.Security.Cryptography;
using System.Text;

namespace WebApps
{
    public class Helper
    {
        public static byte[] Hash(string plaintext)
        {
            HashAlgorithm hash = SHA512.Create();
            return hash.ComputeHash(Encoding.ASCII.GetBytes(plaintext));
        }
    }
}
