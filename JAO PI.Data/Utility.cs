using System.Collections.Generic;
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
                return BitConverter.ToString(checksum).Replace("-", string.Empty);
            }
            return "";
        }
        
        public static bool IsInvalidSymbolInString(string str)
        {
            if (str.IndexOfAny("+-*/:%#|".ToCharArray()) != -1) return true;
            return false;
        }

        public static void SaveToDictonary(Dictionary<string, string> dic, string key, string value)
        {
            char[] trimChars = { ' ', '\t' };
            if (IsInvalidSymbolInString(key)) return;

            key = key.Trim(trimChars);
            if (!dic.ContainsKey(key)) // check to prevent adding a key twice
            {
                dic.Add(key, value.Trim(trimChars));
            }
        }
    }
}
