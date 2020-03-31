using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace JAO_PI.Data
{
    public class Parser
    {
        public static bool Analysis(string file, Dictionary<string, string> dicToSave)
        {
            if (!File.Exists(file)) return false;

            var analytic = new StreamReader(file);
            string line;
            string[] checkStuff = { "native", /*"#define",*/ "forward" };
            char[] trimChars = { ' ', '\t' };

            var paramLine = false;

            var param = "";
            var name = "";
            while ((line = analytic.ReadLine()) != null)
            {
                if (paramLine) // multible paramlines
                {
                    var braceIndex = line.LastIndexOf(')');
                    if (braceIndex == -1)
                    {
                        param += line;
                        continue;
                    }
                    param += line.Substring(0, braceIndex - 1);

                    Utility.SaveToDictonary(dicToSave, name, param);
                    name = string.Empty;
                    param = string.Empty;
                    paramLine = false;
                    continue;
                }
                for (var i = 0; i != checkStuff.Length; i++)
                {
                    if (!line.StartsWith(checkStuff[i])) continue;
                    /*if (i == 1) // there is something special with #define
                    {
                        name = line.Substring(checkStuff[i].Length).Trim(trimChars);

                        var len = name.IndexOfAny("( \t".ToCharArray());                        
                        if(len != -1)  name = name.Substring(0, len).Trim(trimChars);

                        if (!char.IsLetterOrDigit(name.FirstOrDefault())) continue;
                        Utility.SaveToDictonary(dicToSave, name, param);

                        name = string.Empty;
                        param = string.Empty;
                        continue;
                    }*/
                    if (line.Contains("("))
                    {
                        name = line.Substring(checkStuff[i].Length, line.IndexOf('(') - checkStuff[i].Length);
                        line = line.Substring(name.Length + checkStuff[i].Length).Trim(trimChars);
                        
                        var braceIndex = line.LastIndexOf(')');
                        if (braceIndex == -1) // check if the function has multible paramlines
                        {
                            param = line.Substring(line.IndexOf('(') + 1);
                            paramLine = true;
                            break;
                        }
                        param = line.Substring(line.IndexOf('(') + 1, braceIndex - 1);

                        // check to prevent adding a key twice
                        Utility.SaveToDictonary(dicToSave, name, param);

                        name = string.Empty;
                        param = string.Empty;
                    }
                    else // for e.g. defines 
                    {
                        line = line.Substring(checkStuff[i].Length + 1).Trim(trimChars);
                        {
                            if (line.Contains("\t"))
                            {
                                name = line.Substring(0, line.IndexOf("\t", StringComparison.Ordinal));
                                param = line.Substring(line.LastIndexOf('\t') + "\t".Length);
                            }
                            else if (line.Contains(" "))
                            {
                                name = line.Substring(0, line.IndexOf(" ", StringComparison.Ordinal));
                                param = line.Substring(line.LastIndexOf(' ') + 1);
                            }
                            else continue;

                            param = char.IsDigit(param.FirstOrDefault()) ? string.Empty : param;
                            if (!char.IsLetterOrDigit(name.FirstOrDefault())) continue;
                            Utility.SaveToDictonary(dicToSave, name, param);

                            name = string.Empty;
                            param = string.Empty;
                        }
                    }
                }
            }
            analytic.Close();
            return true;
        }
    }
}
