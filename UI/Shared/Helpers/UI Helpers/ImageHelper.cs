using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI.Helpers.UI_Helpers
{
    public static class ImageHelper
    {
        public static void MakePictureBoxCircular(PictureBox pictureBox)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddEllipse(0, 0, pictureBox.Width - 1, pictureBox.Height - 1);
            pictureBox.Region = new Region(path);
        }
        public static void LoadDefaultImage(PictureBox pb)
        {
            Bitmap bmp = new Bitmap(120, 120);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.Clear(Color.White);

                using (SolidBrush backgroundBrush = new SolidBrush(Color.FromArgb(219, 230, 241)))
                using (SolidBrush avatarBrush = new SolidBrush(Color.FromArgb(79, 109, 136)))
                {
                    g.FillEllipse(backgroundBrush, 0, 0, 120, 120);
                    g.FillEllipse(avatarBrush, 40, 22, 40, 40);
                    g.FillEllipse(avatarBrush, 26, 65, 68, 42);
                }
            }

            Image old = pb.Image;
            pb.Image = bmp;
            old?.Dispose();
        }
        public static void PreviewFromPath(PictureBox pb, string path)
        {

            try
            {
                if (!File.Exists(path))
                {
                    return;
                }

                using (Image temp = Image.FromFile(path))
                {
                    Image old = pb.Image;
                    pb.Image = new Bitmap(temp);
                    old?.Dispose();
                }
            } catch (Exception ex) { };
           
        }
        public static void ShowEmptyImageWithText(PictureBox pb , string text)
        {
            Bitmap bmp = new Bitmap(120, 90);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.FromArgb(248, 250, 252));

                using (Pen pen = new Pen(Color.FromArgb(200, 210, 220), 2))
                using (Brush brush = new SolidBrush(Color.FromArgb(120, 130, 140)))
                using (Font font = new Font("Segoe UI", 9F, FontStyle.Bold))
                {
                    g.DrawRectangle(pen, 22, 15, 76, 60);
                    g.DrawString("Text", font, brush, 42, 37);
                }
            }

            Image old = pb.Image;
            pb.Image = bmp;
            old?.Dispose();
        }


    }
}

