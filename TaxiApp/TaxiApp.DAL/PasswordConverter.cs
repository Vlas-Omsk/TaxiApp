using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;

namespace TaxiApp.DAL
{
    internal sealed class PasswordConverter : ValueConverter<string, byte[]>
    {
        private static readonly Expression<Func<string, byte[]>> _encode =
            x => ComputeHash(x);
        private static readonly Expression<Func<byte[], string>> _getHash =
            x => null;

        public PasswordConverter()
            : base(_encode, _getHash, null)
        {
        }

        private static byte[] ComputeHash(string value)
        {
            using var sha256 = SHA256.Create();

            return sha256.ComputeHash(Encoding.UTF8.GetBytes(value));
        }
    }
}
