using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace FileHasherTestTask.Models
{
    internal class FileHasher
    {
        private static byte[] _hashBytes;
        private static string _hashString;

        private static void GetFileHash(string path)
        {
            using (SHA1 sha1 = SHA1.Create())
            {
                try
                {
                    _hashBytes = sha1.ComputeHash(File.OpenRead(path));
                }
                catch (IOException)
                {
                    _hashBytes = null;
                }
                catch (UnauthorizedAccessException)
                {
                    _hashBytes = null;
                }
            }
        }

        private static void ByteArrayToString()
        {
            StringBuilder hashString = new(_hashBytes.Length);

            for (int i = 0; i < _hashBytes.Length; i++)
                hashString.Append(_hashBytes[i].ToString("X2"));

            _hashString = hashString.ToString();
        }

        internal static string Hash(string path)
        {
            GetFileHash(path);

            if (_hashBytes is null) return _hashString = null;

            ByteArrayToString();
            return _hashString;
        }
    }
}