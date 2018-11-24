using System;
using System.IO;
using System.Security.Cryptography;

namespace JAO_PI.Data
{
    public class Utility
    {
        public static string GetFileChecksum(string file)
        {
            if (File.Exists(file))
            {
                FileStream stream = File.OpenRead(file);
                var sha = new SHA256Managed();
                byte[] checksum = sha.ComputeHash(stream);
                return BitConverter.ToString(checksum).Replace("-", String.Empty);
            }
            return "";
        }
    }
}
