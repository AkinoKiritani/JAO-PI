using System;
using System.Collections.Generic;
using System.IO;

namespace JAO_PI.Data
{
    public class Parser
    {
        public static bool Analysis(string file)
        {
            if (!File.Exists(file)) return false;

            var analytic = new StreamReader(file);
            string line;
            string[] checkStuff = { "native", "#define", "forward" };
            var ignoreTab = false;
            var paramLine = false;
            var dic = new Dictionary<string, string>();
            var param = "";
            var name = "";
            while ((line = analytic.ReadLine()) != null)
            {
                if (paramLine)
                {
                    var braceIndex = line.IndexOf(')');
                    if (braceIndex == -1)
                    {
                        param += line;
                        continue;
                    }
                    param += line.Substring(0, braceIndex - 1);
                    if (param.StartsWith(" "))
                    {
                        param = param.Remove(0, 1);
                    }
                    else if (param.StartsWith("\t"))
                    {
                        param = param.Remove(0, "\t".Length-1);
                    }

                    if (param.EndsWith(" "))
                    {
                        param = param.Remove(line.Length - 1);
                    }
                    else if (param.EndsWith("\t"))
                    {
                        param = param.Remove(line.Length - "\t".Length);
                    }

                    if (name.StartsWith(" "))
                    {
                        name = name.Remove(0, 1);
                    }
                    else if (name.StartsWith("\t"))
                    {
                        name = name.Remove(0, "\t".Length);
                    }
                    dic.Add(name, param.Length > 0 ? param : " ");
                    paramLine = false;
                    continue;
                }
                for (var i = 0; i != checkStuff.Length; i++)
                {
                    if (!line.StartsWith(checkStuff[i])) continue;
                    if (line.Contains("("))
                    {
                        name = line.Substring(checkStuff[i].Length, line.IndexOf('(') - checkStuff[i].Length);
                        if (line.Contains("\t"))
                        {
                            if (line.Contains(";"))
                            {
                                var tabIndex = line.IndexOf("\t", StringComparison.Ordinal);
                                if (tabIndex > line.IndexOf(";", StringComparison.Ordinal))
                                {
                                    line = line.Substring(0, tabIndex);
                                    ignoreTab = true;
                                }
                            }
                        }

                        line = line.Substring(name.Length + checkStuff[i].Length);
                        
                        if (ignoreTab == false && line.Contains("\t"))
                        {
                            line = line.Substring(line.LastIndexOf('\t') + "\t".Length);
                        }

                        
                        var braceIndex = line.IndexOf(')');
                        if (braceIndex == -1)
                        {
                            param = line.Substring(line.IndexOf('(') + 1);
                            paramLine = true;
                            break;
                        }
                        param = line.Substring(line.IndexOf('(') + 1, braceIndex - 1);

                        if (param.StartsWith(" "))
                        {
                            param = param.Remove(0,1);
                        }
                        else if (param.StartsWith("\t"))
                        {
                            param = param.Remove(0, "\t".Length);
                        }

                        if (param.EndsWith(" "))
                        {
                            param = param.Remove(line.Length - 1);
                        }
                        else if (param.EndsWith("\t"))
                        {
                            param = param.Remove(line.Length - "\t".Length);
                        }
                        if (name.StartsWith(" "))
                        {
                            name = name.Remove(0, 1);
                        }
                        else if (name.StartsWith("\t"))
                        {
                            name = name.Remove(0, "\t".Length);
                        }
                        dic.Add(name, param.Length > 0 ? param : " ");
                    }
                    else
                    {
                        line = line.Substring(checkStuff[i].Length + 1);
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

                        if (name.StartsWith(" "))
                        {
                            name = name.Remove(0, 1);
                        }
                        else if (name.StartsWith("\t"))
                        {
                            name = name.Remove(0, "\t".Length);
                        }
                        dic.Add(name, param.Length > 0 ? param : " ");
                    }
                    ignoreTab = false;
                }
            }
            analytic.Close();
            return true;
        }
    }
}
