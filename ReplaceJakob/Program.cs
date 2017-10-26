using ImageMagick;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReplaceJakob
{
    class Program
    {
        static void Main(string[] args)
        {
            string read = Console.ReadLine();

            Directory.CreateDirectory(Directories.Images);
            Directory.CreateDirectory(Directories.BWPrints);
            Directory.CreateDirectory(Directories.ColorPrints);
            Directory.CreateDirectory(Directories.NoPrints);


            Directory.SetCurrentDirectory(read);
            MagickReadSettings settings = new MagickReadSettings();
            // Settings the density to 300 dpi will create an image with a better quality
            settings.Density = new Density(300, 300);

            foreach (string fileName in Directories.ListPdfs(read))
            {
                using (MagickImageCollection images = new MagickImageCollection())
                {
                    // Add all the pages of the pdf file to the collection
                    images.Read(fileName, settings);
                    IMagickImage image = images[0];

                        // Writing to a specific format works the same as for a single image
                        image.Format = MagickFormat.Png;
                        image.Write(Directories.Images + Path.GetFileNameWithoutExtension(Path.GetFileName(fileName)) + ".png");
                }
            }

            Console.ReadKey();
        }
    }
}
