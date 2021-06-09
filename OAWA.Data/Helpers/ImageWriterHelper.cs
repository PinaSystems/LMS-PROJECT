using System.Linq;
using System.Text;

namespace OAWA.Data.Helpers
{
    public class ImageWriterHelper
    {
         public enum ImageFormat
        {
            bmp,
            jpeg,
            gif,
            tiff,
            png,
            unknown
        }

        public static ImageFormat GetImageFormat(byte[] bytes)
        {
            var bmp = Encoding.ASCII.GetBytes("BM");     // BMP
            var gif = Encoding.ASCII.GetBytes("GIF");    // GIF
            var png = new byte[] { 137, 80, 78, 71 };              // PNG
            var tiff = new byte[] { 73, 73, 42 };                  // TIFF
            var tiff2 = new byte[] { 77, 77, 42 };                 // TIFF
            var jpeg = new byte[] { 255, 216, 255, 224 };          // jpeg
            var jpeg2 = new byte[] { 255, 216, 255, 225 };         // jpeg canon
            var jpg = new byte[] { 255, 216, 255, 254 };            //jpg

            if (bmp.SequenceEqual(bytes.Take(bmp.Length)))
                return ImageFormat.bmp;

            if (gif.SequenceEqual(bytes.Take(gif.Length)))
                return ImageFormat.gif;

            if (png.SequenceEqual(bytes.Take(png.Length)))
                return ImageFormat.png;

            if (tiff.SequenceEqual(bytes.Take(tiff.Length)))
                return ImageFormat.tiff;

            if (tiff2.SequenceEqual(bytes.Take(tiff2.Length)))
                return ImageFormat.tiff;

            if (jpeg.SequenceEqual(bytes.Take(jpeg.Length)))
                return ImageFormat.jpeg;

            if (jpeg2.SequenceEqual(bytes.Take(jpeg2.Length)))
                return ImageFormat.jpeg;

            if (jpg.SequenceEqual(bytes.Take(jpg.Length)))
                return ImageFormat.jpeg;

            return ImageFormat.unknown;
        }

        public static DocFormat GetDocFormat(byte[] bytes)
        {
            var doc= new byte[] { 208, 207, 17, 224, 161, 177, 26, 225 };
            var pdf= new byte[] { 37, 80, 68, 70, 45, 49, 46 };
            var docx= new byte[] { 80, 75, 3, 4 };

            if(doc.SequenceEqual(bytes.Take(doc.Length)))
                return DocFormat.doc;
            else if(docx.SequenceEqual(bytes.Take(docx.Length)))
                return DocFormat.docx;
            else if(pdf.SequenceEqual(bytes.Take(pdf.Length)))
                return DocFormat.pdf;
            else
                return DocFormat.unknown;
        }

        public enum DocFormat
        {
            doc,
            docx,
            pdf,
            unknown,
        }
    }
}