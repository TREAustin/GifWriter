using System;
using System.Collections.Generic;
using System.IO;

namespace GifWriter
{
    class Program
    {
        static void Main(string[] args)
        {
            //Checks to make sure there are 4 arguments for the image, rows, cols and number of repeats.
            if(args.Length != 5)
            {
                Console.WriteLine("You didn't enter enough information.");
            }
            else if (args.Length == 5) 
            {
                string imageName = args[0];
                string gifName = args[1];

                string imagePath = Path.GetFullPath(imageName);
                string gifPath = Path.GetFullPath(gifName);

                //Formats Sprite Sheeet into list of byte arrays.
                Sprite _sprite = new Sprite(imagePath, Int32.Parse(args[2]), Int32.Parse(args[3]));
                Console.WriteLine("Loading image: " + args[0] + ", Rows: " + args[2] + ", Columns: " + args[3] + ", Number of Repeats: " + args[4] + ", Saved as " + args[1]);
                //Initializes GifWriter object.

                GifCreator gifWriter = new GifCreator(_sprite.ByteArray, gifPath, Int32.Parse(args[4]));
                //Creates a Gif from the list of byte arrays.
                gifWriter.CreateGif();
            }
            Console.ReadKey();
        }
    }
}
