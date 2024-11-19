using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace SecretSanta_Core.BusinessLogic
{
    public class BusinessMethods
    {
        public static DateTime GetLocalDateTime(DateTime utcDateTime)
        {
            DateTime utc = utcDateTime;
            TimeZoneInfo zone = TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time");
            return TimeZoneInfo.ConvertTimeFromUtc(utc, zone);
        }

        public static string GetStatusTypeText(int statusId)
        {
            foreach (var value in Enum.GetValues(typeof(Enumerations.StatusTypes)))
            {
                if ((int)value == statusId)
                {
                    return value.ToString();
                }
            }

            return String.Empty;
        }

        public static string RTFToHtml(string body)
        {
            var temp = body.Replace("\r\n", "<br/>").Replace("\r", "<br/>").Replace("\n", "<br/>");

            return temp;
        }

        public static string GetGoogleUrl(string street, string city, string state, string zip)
        {
            var url = String.Empty;
            var hasAddress = false;
            if ((!String.IsNullOrEmpty(street) && !String.IsNullOrEmpty(city) && !String.IsNullOrEmpty(state)) ||
                !string.IsNullOrEmpty(zip))
            {
                url += "https://www.google.com/maps/dir//";

                if (!String.IsNullOrEmpty(street) && !String.IsNullOrEmpty(city) && !String.IsNullOrEmpty(state))
                {
                    hasAddress = true;
                    if (street.Contains("#"))
                    {
                        street = street.Substring(0, street.IndexOf("#"));
                    }

                    if (street.Contains("Ste"))
                    {
                        street = street.Substring(0, street.IndexOf("Ste"));
                    }

                    url += street.Replace(" ", "+");
                    url += ",+";
                    url += city.Replace(" ", "+");
                    url += ",+";
                    url += state.Replace(" ", "+");
                }

                if (!String.IsNullOrEmpty(zip))
                {
                    if (hasAddress)
                    {
                        url += "+";
                    }

                    url += zip;
                }
            }

            return url;
        }

        public static string GetFileExtension(string fileName)
        {
            return fileName.Substring(fileName.LastIndexOf('.'));
        }



        public static Bitmap ResizeBitmap(Bitmap b, Size nzMax, Size nzReal)
        {
            var size = adaptProportionalSize(nzMax, nzReal);
            var result = new Bitmap(size.Width, size.Height);
            using (Graphics g = Graphics.FromImage(result))
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(
                    b,
                    new Rectangle(Point.Empty, result.Size),
                    new Rectangle(Point.Empty, b.Size),
                    GraphicsUnit.Pixel);
            }

            return result;
        }

        private static Size adaptProportionalSize(Size szMax, Size szReal)
        {
            int nWidth;
            int nHeight;
            double sMaxRatio;
            double sRealRatio;

            if (szMax.Width < 1 || szMax.Height < 1 || szReal.Width < 1 || szReal.Height < 1)
                return Size.Empty;

            sMaxRatio = (double)szMax.Width / (double)szMax.Height;
            sRealRatio = (double)szReal.Width / (double)szReal.Height;

            if (sMaxRatio < sRealRatio)
            {
                nWidth = Math.Min(szMax.Width, szReal.Width);
                nHeight = (int)Math.Round(nWidth / sRealRatio);
            }
            else
            {
                nHeight = Math.Min(szMax.Height, szReal.Height);
                nWidth = (int)Math.Round(nHeight * sRealRatio);
            }

            return new Size(nWidth, nHeight);
        }

        public static Image ReorientBitmap(Image img)
        {
            var rft = RotateFlipType.RotateNoneFlipNone;
            var properties = img.PropertyItems;

            foreach (var prop in properties.Where(i => i.Id == 274))
            {
                var orientation = BitConverter.ToInt16(prop.Value, 0);
                rft = orientation == 1 ? RotateFlipType.RotateNoneFlipNone :
                    orientation == 3 ? RotateFlipType.Rotate180FlipNone :
                    orientation == 6 ? RotateFlipType.Rotate90FlipNone :
                    orientation == 8 ? RotateFlipType.Rotate270FlipNone :
                    RotateFlipType.RotateNoneFlipNone;
            }

            if (rft != RotateFlipType.RotateNoneFlipNone)
            {
                img.RotateFlip(rft);
                return img;
            }

            return img;
        }

        public static ImageFormat GetMimeType(string name)
        {
            var dictionary = new Dictionary<string, ImageFormat> { { ".JPEG", ImageFormat.Jpeg }, { ".jpeg", ImageFormat.Jpeg }, { ".JPG", ImageFormat.Jpeg }, { ".jpg", ImageFormat.Jpeg}, { ".PNG", ImageFormat.Png }, { ".png", ImageFormat.Png}, { ".GIF", ImageFormat.Gif }, { ".gif", ImageFormat.Gif} };
            var extension = BusinessLogic.BusinessMethods.GetFileExtension(name);
            if (dictionary.ContainsKey(name))
            {
                return dictionary[name];
            }
            return ImageFormat.Bmp;
        }
    }
}
