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
            string[] checkStuff = { "native", "#define", "forward" };
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

                    // check to prevent adding a key twice
                    name = name.Trim(trimChars);
                    if (!dicToSave.ContainsKey(name))
                    {
                        dicToSave.Add(name, param.Trim(trimChars));
                    }
                    paramLine = false;
                    continue;
                }
                for (var i = 0; i != checkStuff.Length; i++)
                {
                    if (!line.StartsWith(checkStuff[i])) continue;
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
                        if (!char.IsLetterOrDigit(param.FirstOrDefault())) continue;
                        
                        // check to prevent adding a key twice
                        name = name.Trim(trimChars);
                        if (!dicToSave.ContainsKey(name))
                        {
                            dicToSave.Add(name, param.Trim(trimChars));
                        }
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

                            // check to prevent adding a key twice
                            name = name.Trim(trimChars);                            
                            if (!dicToSave.ContainsKey(name))
                            {
                                dicToSave.Add(name, char.IsDigit(param.FirstOrDefault()) ? string.Empty : param.Trim(trimChars));
                            }
                        }
                    }
                }
            }
            analytic.Close();
            return true;
        }
    }
}
