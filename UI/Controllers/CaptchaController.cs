using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SRC;
using System.Web;
using Microsoft.AspNetCore.Http;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Drawing.Imaging;

namespace UI.Controllers
{
    public class CaptchaController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Get()
        {
            byte[] captchaValue = MakeCaptcha(out string value);
            HttpContext.Session.SetString(Const.CAPTCHA, value);
            return File(captchaValue,"image/jpeg");

        }
        public byte[] MakeCaptcha(out string value)
        {
            Bitmap bitmap = new Bitmap(60, 24);
            Graphics graphics = Graphics.FromImage(bitmap);
            Font font = new Font("TimesNewRoman", 15, (FontStyle.Bold | FontStyle.Italic));
            LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, bitmap.Width, bitmap.Height)
                , Color.AliceBlue, Color.Brown, 1.5f, true);

            value = new Random().Next(999, 10000).ToString();

            graphics.DrawString(value, font, brush, 3, 2);

            MemoryStream stream = new MemoryStream();
            bitmap.Save(stream, ImageFormat.Jpeg);

            return stream.ToArray();

        }
    }
}
