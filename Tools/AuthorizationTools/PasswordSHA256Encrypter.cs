using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace KeyboardApplicationToolsLibrary.AuthorizationTools
{
    /// <summary>
    /// Provide password SHA-256 encrypt
    /// </summary>
    public static class PasswordSHA256Encrypter
    {
        /// <summary>
        /// Encrypt password SHA-256
        /// </summary>
        /// <param name="password">Password before encrypt</param>
        /// <returns>Password after encrypt</returns>
        public static string EncryptPassword(string password)
        {
            using var _sha256 = SHA256.Create();
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] hashPasswordBytes = _sha256.ComputeHash(passwordBytes);
            _sha256.Dispose();
            return BitConverter.ToString(hashPasswordBytes);
        }

    }
}
