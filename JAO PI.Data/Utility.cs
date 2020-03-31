using System.Collections.Generic;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

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
        public static Dictionary<string, string> includeDictonary = new Dictionary<string, string>();

        public static bool IsInvalidSymbolInString(string str)
        {
            return !new Regex(@"[a-zA-Z0-9_@:]+").IsMatch(str);
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
