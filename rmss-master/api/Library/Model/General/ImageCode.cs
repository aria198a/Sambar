using Library.Functions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model.General
{
    public class ImageCode
    {
        /// <summary>
        /// 驗證圖形
        /// </summary>
        public string Image { get; set; }
        /// <summary>
        /// 加密編碼
        /// </summary>
        public string Code { get; set; }
    }
    public class GetImageCode
    {
        /// <summary>
        /// 取得驗證碼
        /// </summary>
        public ImageCode GetImage()
        {
            ImageCode imageCode = new ImageCode();
            string code = ImageFunc.GetRandomNumberString(6);
            System.Drawing.Image image = ImageFunc.CreateCheckCodeImage(code);
            imageCode.Image = "data:image/jpeg;base64," + ImageFunc.ImageToBase64(image, System.Drawing.Imaging.ImageFormat.Jpeg);
            imageCode.Code = new AES().Encryption(code);
            image.Dispose();
            //HttpContext.Current.Session["SwcbMapImageCode"] = JsonConvert.SerializeObject(imageCode);
            return imageCode;
        }
    }

    public class ImageFunc
    {
        /// <summary>
        /// 產生驗證碼
        /// </summary>
        /// <param name="int_NumberLength">驗證碼長度</param>
        /// <returns></returns>
        public static string GetRandomNumberString(int int_NumberLength)
        {
            StringBuilder str_Number = new StringBuilder();//字串儲存器
            string[] arr = new string[100];

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                    arr[i * 10 + j] = j.ToString();
            }

            string[] rnds = FixFunc.GetRandom(arr);

            for (int i = 1; i <= int_NumberLength; i++)
                str_Number.Append(rnds[i]);//產生0~9的亂數

            return str_Number.ToString();
        }
        /// <summary>
        /// 產生驗證碼圖片
        /// </summary>
        /// <param name="checkCode">驗證碼</param>
        /// <returns></returns>
        public static Image CreateCheckCodeImage(string checkCode)
        {
            Bitmap image = new Bitmap((checkCode.Length * 42), 80);//產生圖片，寬50*位數，高80像素
            Graphics g = Graphics.FromImage(image);

            //生成隨機生成器
            string[] colorInt = GetIntegerArray(256);
            Random random = new Random(Guid.NewGuid().GetHashCode());
            int int_Red = 0;
            int int_Green = 0;
            int int_Blue = 0;
            int_Red = FixFunc.FixNullNumber(FixFunc.GetRandomSingle(colorInt));//產生0~255
            int_Green = FixFunc.FixNullNumber(FixFunc.GetRandomSingle(colorInt));//產生0~255
            int_Blue = (int_Red + int_Green > 400 ? 0 : 400 - int_Red - int_Green);
            int_Blue = (int_Blue > 255 ? 255 : int_Blue);

            //清空圖片背景色
            g.Clear(Color.FromArgb(int_Red, int_Green, int_Blue));

            string[] imageWidth = GetIntegerArray(image.Width);
            string[] imageHeight = GetIntegerArray(image.Height);

            //畫圖片的背景噪音線
            for (int i = 0; i <= 24; i++)
            {
                int x1 = FixFunc.FixNullNumber(FixFunc.GetRandomSingle(imageWidth));
                int x2 = FixFunc.FixNullNumber(FixFunc.GetRandomSingle(imageWidth));
                int y1 = FixFunc.FixNullNumber(FixFunc.GetRandomSingle(imageHeight));
                int y2 = FixFunc.FixNullNumber(FixFunc.GetRandomSingle(imageHeight));
                Pen pen1 = new Pen(Color.Silver);
                g.DrawLine(pen1, x1, y1, x2, y2);
                Pen pen2 = new Pen(Color.DarkViolet);
                g.DrawEllipse(pen2, new Rectangle(x1, y1, x2, y2));
                pen1.Dispose();
                pen2.Dispose();
            }

            Font font = new Font("Arial", 50, (FontStyle.Italic | FontStyle.Strikeout | FontStyle.Bold));

            System.Drawing.Drawing2D.LinearGradientBrush brush = new System.Drawing.Drawing2D.LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height), Color.Blue, Color.DarkRed, 1.2F, true);
            g.DrawString(checkCode, font, brush, 2, 2);
            font.Dispose();
            brush.Dispose();

            for (int i = 0; i <= 50; i++)
            {
                int tmpInt_Red = FixFunc.FixNullNumber(FixFunc.GetRandomSingle(colorInt));
                int tmpInt_Green = FixFunc.FixNullNumber(FixFunc.GetRandomSingle(colorInt));
                int tmpInt_Blue = FixFunc.FixNullNumber(FixFunc.GetRandomSingle(colorInt));
                //畫圖片的前景噪音點
                int x = FixFunc.FixNullNumber(FixFunc.GetRandomSingle(imageWidth));
                int y = FixFunc.FixNullNumber(FixFunc.GetRandomSingle(imageHeight));
                image.SetPixel(x, y, Color.FromArgb(tmpInt_Red, tmpInt_Green, tmpInt_Blue));
            }

            //畫圖片的邊框線
            Pen pen = new Pen(Color.Silver);
            g.DrawRectangle(pen, 0, 0, image.Width - 1, image.Height - 1);
            pen.Dispose();
            g.Dispose();
            return image;
        }
        /// <summary>
        /// 產生隨機數陣列
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        private static string[] GetIntegerArray(int number)
        {
            string[] arr = new string[number];
            for (int i = 0; i < number; i++)
                arr[i] = i.ToString();
            return arr;
        }
        /// <summary>
        /// 傳換Base64
        /// </summary>
        /// <param name="image">圖片</param>
        /// <param name="format">格式</param>
        /// <returns></returns>
        public static string ImageToBase64(Image image, System.Drawing.Imaging.ImageFormat format)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, format);
                byte[] imageBytes = ms.ToArray();
                string base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }
        }
    }
}
