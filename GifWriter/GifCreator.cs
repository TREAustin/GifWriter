using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Interop;

namespace GifWriter
{
    public class GifCreator
    {
        //List to hold the images for the GIF
        readonly List<Bitmap> images = new List<Bitmap>();
        //Encoder for the GIF.
        readonly GifBitmapEncoder gEnc = new GifBitmapEncoder();
        readonly string gifPath = string.Empty;
        //Number of repeats.
        readonly int repeat = 0;
        //Constructor for the GifCreator
        public GifCreator(List<byte[]> byteArray, string _gifPath, int _repeat = 0)
        {
            gifPath = _gifPath;
            repeat = _repeat;
            //Method for converting byte arrays and adding bitmaps to the bitmap list.
            ConvertToBitmap(byteArray);
        }

        public void CreateGif()
        {
            //For loop to repeat the number of times the user wants.
            for (int i = 0; i < repeat; i++)
            {
                //Loop to add each image into the BitmapSource object.
                foreach (Bitmap bmpImage in images)
                {
                    var bmp = bmpImage.GetHbitmap();
                    //Setup BitmapSource object to be stored.
                    var src = Imaging.CreateBitmapSourceFromHBitmap(
                        bmp,
                        IntPtr.Zero,
                        Int32Rect.Empty,
                        BitmapSizeOptions.FromEmptyOptions());
                    gEnc.Frames.Add(BitmapFrame.Create(src));
                }
            }
            Console.WriteLine("Times repeated: " + repeat);
            //Save the BitmapSource object.
            using (FileStream fs = new FileStream(gifPath, FileMode.Create))
            {
                gEnc.Save(fs);
            }
        }
        private void ConvertToBitmap(List<byte[]> byteArray)
        {
            for(int i = 0; i < byteArray.Count; i++)
            {
                //Converts byte array to Image object.
                ImageConverter converter = new ImageConverter();
                //Converts Image object to Bitmap object
                Bitmap temp = new Bitmap((Image)converter.ConvertFrom(byteArray[i]));
                //Add Bitmap object to list of Bitmaps.
                images.Add(temp);
            }
        }
    }
}
