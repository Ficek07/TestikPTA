using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Class
{
    internal class Changer
    {
        string path;
        public Changer() 
        {
            if (File.Exists(path))
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    string text = sr.ReadToEnd().ToLower();
                    string[] words = text.Split(new[] { ' ', '\n', '\r', '\t', ';' }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < words.Length; i++)
                    {
                        if (words[i].Length > 0)
                        {
                            words[i] = char.ToUpper(words[i][0]) + words[i].Substring(1);
                        }
                    }
                    string updatedText = string.Join("\n", words);
                    updatedText = updatedText.Replace(";", ";\n");
                }
            }
        }
    }
}
