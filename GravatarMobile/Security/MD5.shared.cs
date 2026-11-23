using System;
using System.Text;

namespace GravatarMobile
{
    /// <summary>
    /// MD5 hashing helper used to build Gravatar hashes. A thin wrapper over <see cref="System.Security.Cryptography.MD5"/>.
    /// </summary>
    /// <remarks>MD5 is used because Gravatar identifies accounts by the MD5 hash of the email address; it is not used for security.</remarks>
    public sealed class MD5
    {
        private MD5()
        {
        }

        /// <summary>Gets the MD5 hash of the string, encoded as UTF-8.</summary>
        public static byte[] GetHash(string input)
        {
            return GetHash(input, new UTF8Encoding());
        }

        /// <summary>Gets the MD5 hash of the string using the specified encoding.</summary>
        public static byte[] GetHash(string input, Encoding encoding)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input), "Unable to calculate hash over null input data");

            if (encoding == null)
                throw new ArgumentNullException(nameof(encoding), "Unable to calculate hash over a string without a default encoding. Consider using the GetHash(string) overload to use UTF8 Encoding");

            return GetHash(encoding.GetBytes(input));
        }

        /// <summary>Gets the MD5 hash of the data.</summary>
        public static byte[] GetHash(byte[] input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input), "Unable to calculate hash over null input data");

            using (var md5 = System.Security.Cryptography.MD5.Create())
            {
                return md5.ComputeHash(input);
            }
        }

        /// <summary>Gets the MD5 hash of the data as an upper case hexadecimal string.</summary>
        public static string GetHashString(byte[] input)
        {
            return BitConverter.ToString(GetHash(input)).Replace("-", string.Empty);
        }

        /// <summary>Gets the MD5 hash of the string, using the specified encoding, as an upper case hexadecimal string.</summary>
        public static string GetHashString(string input, Encoding encoding)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input), "Unable to calculate hash over null input data");

            if (encoding == null)
                throw new ArgumentNullException(nameof(encoding), "Unable to calculate hash over a string without a default encoding. Consider using the GetHashString(string) overload to use UTF8 Encoding");

            return GetHashString(encoding.GetBytes(input));
        }

        /// <summary>Gets the MD5 hash of the string, encoded as UTF-8, as an upper case hexadecimal string.</summary>
        public static string GetHashString(string input)
        {
            return GetHashString(input, new UTF8Encoding());
        }
    }
}
