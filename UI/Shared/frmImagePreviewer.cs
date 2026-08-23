using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace UI.Shared
{
    public class frmImagePreviewer : Form
    {
        private PictureBox pictureBox;

        public frmImagePreviewer(Image image) {
            this.StartPosition = FormStartPosition.CenterParent;
            this.Size = new Size(500, 500);
            this.BackColor = Color.White;
            this.Text = "Image Preview";

            this.pictureBox = new PictureBox();
            this.pictureBox.Dock = DockStyle.Fill;
            this.pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            this.MinimizeBox = false;
            this.pictureBox.Image = new Bitmap(image);

            this.Controls.Add(this.pictureBox);

        }

    }
}

