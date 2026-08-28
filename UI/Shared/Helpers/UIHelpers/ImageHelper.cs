using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace UI.Helpers.UI_Helpers
{
    public static class ImageHelper
    {
        public static void MakePictureBoxCircular(PictureBox pictureBox)
        {
            if (pictureBox == null || pictureBox.Width <= 1 || pictureBox.Height <= 1)
                return;

            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddEllipse(0, 0, pictureBox.Width - 1, pictureBox.Height - 1);

                Region old = pictureBox.Region;
                pictureBox.Region = new Region(path);

                if (old != null)
                    old.Dispose();
            }
        }

        public static void SetImage(PictureBox pb, Image image)
        {
            if (pb == null)
                return;

            Image old = pb.Image;
            pb.Image = image;

            if (old != null && !ReferenceEquals(old, image))
                old.Dispose();
        }

        public static void LoadDefaultImage(PictureBox pb)
        {
            if (pb == null)
                return;

            Bitmap bmp = new Bitmap(120, 120);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.Clear(Color.White);

                using (SolidBrush backgroundBrush = new SolidBrush(Color.FromArgb(219, 230, 241)))
                using (SolidBrush avatarBrush = new SolidBrush(Color.FromArgb(79, 109, 136)))
                {
                    g.FillEllipse(backgroundBrush, 0, 0, 120, 120);
                    g.FillEllipse(avatarBrush, 40, 22, 40, 40);
                    g.FillEllipse(avatarBrush, 26, 65, 68, 42);
                }
            }

            SetImage(pb, bmp);
        }

        public static bool PreviewFromPath(PictureBox pb, string path)
        {
            if (pb == null || string.IsNullOrWhiteSpace(path))
                return false;

            try
            {
                if (!File.Exists(path))
                    return false;

                using (Image temp = Image.FromFile(path))
                {
                    SetImage(pb, new Bitmap(temp));
                }

                return true;
            }
            catch (OutOfMemoryException)
            {
                return false;
            }
            catch (IOException)
            {
                return false;
            }
            catch (UnauthorizedAccessException)
            {
                return false;
            }
        }

        public static void ShowEmptyImageWithText(PictureBox pb, string text)
        {
            if (pb == null)
                return;

            if (string.IsNullOrWhiteSpace(text))
                text = "No image";

            Bitmap bmp = new Bitmap(120, 90);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
                g.Clear(Color.FromArgb(248, 250, 252));

                using (Pen pen = new Pen(Color.FromArgb(200, 210, 220), 2))
                using (Brush brush = new SolidBrush(Color.FromArgb(120, 130, 140)))
                using (Font font = new Font("Segoe UI", 8F, FontStyle.Bold))
                using (StringFormat format = new StringFormat())
                {
                    format.Alignment = StringAlignment.Center;
                    format.LineAlignment = StringAlignment.Center;
                    format.Trimming = StringTrimming.EllipsisCharacter;
                    format.FormatFlags = StringFormatFlags.NoWrap;

                    g.DrawRectangle(pen, 12, 15, 96, 60);
                    g.DrawString(text, font, brush, new RectangleF(14, 17, 92, 56), format);
                }
            }

            SetImage(pb, bmp);
        }
    }
}
