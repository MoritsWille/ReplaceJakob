using System;
using System.Collections.Generic;
using System.IO;

namespace ReplaceJakob
{
    class Directories
    {
        public static List<string> ListPdfs(string directory)
        {
            List<string> names = new List<string>();
            
            foreach (string file in Directory.GetFiles(directory))
            {
                if (file.Contains(".pdf"))
                {
                    names.Add(file);
                }
            }
            return names;
        }

        public static List<string> ListImgs (string directory)
        {
            List<string> names = new List<string>();
            foreach (string pdf in ListPdfs(directory))
            {
                names.Add(Path.GetFileNameWithoutExtension(pdf) + ".png");
            }

            return names;
        }

        public static string Images = Environment.CurrentDirectory + @"\temps\";
        public static string NoPrints = Environment.CurrentDirectory + @"\Dont Print\";
        public static string BWPrints = Environment.CurrentDirectory + @"\Print without color\";
        public static string ColorPrints = Environment.CurrentDirectory + @"\Print with color\";

    }
}
