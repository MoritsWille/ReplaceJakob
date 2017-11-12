using ImageMagick;
using iTextSharp.text.pdf;
using System;
using System.IO;
using System.Windows.Forms;

namespace ReplaceJakob
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Directories.CreateClean();

            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                Directories.SortPdfSize(fbd.SelectedPath, 60);
            }

            MagickReadSettings settings = new MagickReadSettings();
            // Settings the density to 300 dpi will create an image with a better quality
            settings.Density = new Density(300, 300);

            foreach (string fileName in Directories.ListPdfs())
            {
                using (MagickImageCollection images = new MagickImageCollection())
                {
                    // Add all the pages of the pdf files to the collection
                    images.Read(fileName);
                    PdfReader pdfReader = new PdfReader(fileName);
                    int pdfPages = 3;

                    if (pdfReader.NumberOfPages < 3)
                    {
                        pdfPages = pdfReader.NumberOfPages;
                    }

                    for (int i = 0; i < pdfPages; i++)
                    {
                        IMagickImage image = images[i];
                        image.Format = MagickFormat.Png;

                        if (Recognition.ImageHasTag(image.ToByteArray(), "map"))
                        {
                            File.Copy(fileName, Directories.ColorPrints + Path.GetFileName(fileName));
                            i = pdfPages;
                        }
                        else if(i == pdfPages - 1)
                        {
                            File.Copy(fileName, Directories.BWPrints + Path.GetFileName(fileName));
                        }


                        /*
                        Directory.CreateDirectory(Directories.Images + Path.GetFileNameWithoutExtension(Path.GetFileName(fileName)));
                        image.Write(Directories.Images + Path.GetFileNameWithoutExtension(Path.GetFileName(fileName)) + @"\" + i.ToString() + ".png");
                        */
                    }
                }
            }

            Console.ReadKey();
        }
    }
}
