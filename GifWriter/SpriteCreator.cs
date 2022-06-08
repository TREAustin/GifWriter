using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;

namespace GifWriter
{
    public class Sprite
    {
        //List of byte arrays for storing converted Bitmaps.
        private List<byte[]> byteArray = new List<byte[]>();
        //Bitmap to store the entire image.
        private readonly Bitmap bitmap;
        //Width of the image to be used in GIF.
        private readonly int width;
        //Height of the image to be used in GIF.
        private readonly int height;
        //Getter / Setter for the byte array.
        public List<byte[]> ByteArray { get => byteArray; set => byteArray = value; }

        //Default constructor not used in this class.
        private Sprite() { }
        //Only constructor being used for this class.
        public Sprite(string image, int rows, int cols)
        {
            //Bitmap of the original image.
            bitmap  = new Bitmap(image);
            //Width of the image, calculated by dividing overall Image width by rows.
            width = bitmap.Width / rows;
            //Height of the image, calculated by dividing overall Image height by cols.
            height = bitmap.Height / cols;
            //Method for creating the list of byte arrays to be used by the GifCreator.
            CreateListOfByteArrays(width, height, rows, cols);
        }
        //Creates a list of Byte Arrays.
        public void CreateListOfByteArrays(float width, float height, int rows = 1, int cols = 1)
        {
            //Left most x value.
            float x0 = 0.0f;
            //Top most y value.
            float y0 = 0.0f;
            //Loop for columns.
            for (int i = 0; i < cols; i++)
            {
                //Loop for rows.
                for (int j = 0; j < rows; j++) {
                    //Create a new border for the image to cropped.
                    RectangleF rectangleF = new RectangleF(new PointF(x0, y0), new SizeF(width, height));
                    //Bitmap clone of the cropped image.
                    Bitmap cloned = new Bitmap(bitmap).Clone(rectangleF, PixelFormat.DontCare);
                    //Method to convert Bitmap to byte array and add it to the list of byte arrays.
                    ByteArray.Add(ConvertToByteArray(cloned));
                    cloned.Dispose();
                    //Move the image position right by the width of the image.
                    x0 += width;
                }
                //Resets the x position back to 0.w
                x0 = 0.0f;
                //Move the image position down by the height of the image.
                y0 += height;
            }
        }
        //Converts image to byte[] so it can be stored in the list of byte[]
        public byte[] ConvertToByteArray(Bitmap _bitmap)
        {
            for(int i = 0; i < _bitmap.Height; i++)
            {
                for(int j = 0; j < _bitmap.Width; j++)
                {
                    //Console.WriteLine(_bitmap.GetPixel(j, i));
                    _bitmap.SetPixel(j, i, SmoothColor(_bitmap.GetPixel(j, i)));
                    //Console.WriteLine(_bitmap.GetPixel(j, i));
                }
            }
            Image currImage = _bitmap;
            //ImageConverter object to help convert Image to byte array.
            ImageConverter converter = new ImageConverter();
            byte[] temp = converter.ConvertTo(currImage, typeof(byte[])) as byte[];
            //Returns byte array of the ImageConverter object.
            return converter.ConvertTo(currImage,typeof(byte[])) as byte[];
        }

        private Color SmoothColor(Color pixel)
        {
            int R = ((int)pixel.R / 15) * 15;
            int G = ((int)pixel.G / 15) * 15;
            int B = ((int)pixel.B / 15) * 15;
            return Color.FromArgb(255, (byte)R, (byte)G, (byte)B);
        }
    }
}
