using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;

namespace ReplaceJakob
{
    class Directories
    {
      
        /// <summary>
        /// Creates directories and cleans them if the exist
        /// </summary>
        public static void CreateClean()
        {
            foreach (string dirPath in UsedDirs)
            {
                if (Directory.Exists(dirPath))
                {
                    DirectoryInfo dir = new DirectoryInfo(dirPath);

                    foreach (FileInfo fileInfo in dir.GetFiles())
                    {
                        fileInfo.Delete();
                    }
                    foreach (DirectoryInfo dirInfo in dir.GetDirectories())
                    {
                        dirInfo.Delete(true);
                    }
                }
                else
                {
                    Directory.CreateDirectory(dirPath);
                }
            }
        }

        /// <summary>
        /// Takes pdfs from directory and sorts them by size
        /// </summary>
        /// <param name="directory">path to directory</param>
        /// <param name="maxPages">maximum pages a pdf can have</param>
        public static void SortPdfSize(string directory, int maxPages)
        {
            List<string> files = new List<string>();

            foreach (string file in Directory.GetFiles(directory))
            {
                if (file.Contains(".pdf"))
                {
                    files.Add(file);
                }
            }

            PdfReader pdfReader;
            foreach (string file in files)
            {
                pdfReader = new PdfReader(file);
                if (pdfReader.NumberOfPages > maxPages)
                {
                    File.Copy(file, NoPrints + Path.GetFileName(file));
                }
                else
                {
                    File.Copy(file, Pdfs + Path.GetFileName(file));
                }

            }
        }
        
        public static List<string> ListPdfs()
        {
            List<string> files = new List<string>();

            foreach (string file in Directory.GetFiles(Pdfs))
            {
                if (file.Contains(".pdf"))
                {
                    files.Add(file);
                }
            }
            return files;
        }

        public static string Temps = Environment.CurrentDirectory + @"\temps\";
        public static string Pdfs = Environment.CurrentDirectory + @"\temps\pdfs\";
        public static string NoPrints = Environment.CurrentDirectory + @"\Dont Print\";
        public static string BWPrints = Environment.CurrentDirectory + @"\Print without color\";
        public static string ColorPrints = Environment.CurrentDirectory + @"\Print with color\";
        public static string[] UsedDirs = new string[] { Temps, Pdfs, NoPrints, BWPrints, ColorPrints };

    }
}
