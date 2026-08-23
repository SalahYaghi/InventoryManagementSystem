using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI.Services;
using UI.Shared;
using UI.Shared.Helpers.IO_Helper;

namespace UI.Forms.Products
{
    
        public partial class frmProductImagesGallery : Form
        {
            private readonly Guid _productId;
            private FileResponse _selectedImage;

            public frmProductImagesGallery(Guid productId)
            {
                InitializeComponent();
                _productId = productId;
                SetupUI();
            }

            private async void frmProductImagesGallery_Load(object sender, EventArgs e)
            {
                await LoadImages();
            }
            
             private void SetupUI()
            {
                this.StartPosition = FormStartPosition.CenterParent;
                this.FormBorderStyle = FormBorderStyle.FixedDialog;
                this.MaximizeBox = false;
                this.MinimizeBox = false;
                this.BackColor = Color.FromArgb(243, 246, 249);

                flowImages.AutoScroll = true;
                picPreview.SizeMode = PictureBoxSizeMode.Zoom;

                StyleButton(btnUpload, Color.FromArgb(74, 112, 139), Color.White);
                StyleButton(btnDelete, Color.FromArgb(220, 53, 69), Color.White);
                StyleButton(btnRefresh, Color.FromArgb(243, 246, 249), Color.FromArgb(24, 33, 45));
                StyleButton(btnClose, Color.FromArgb(243, 246, 249), Color.FromArgb(24, 33, 45));

                lblStatus.Text = "Ready";
            }
             private void StyleButton(Button button, Color backColor, Color foreColor)
            {
                button.BackColor = backColor;
                button.ForeColor = foreColor;
                button.FlatStyle = FlatStyle.Flat;
                button.FlatAppearance.BorderSize = 0;
                button.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
                button.Cursor = Cursors.Hand;
            }             private async Task LoadImages()
            {
                lblStatus.Text = "Loading images...";
                flowImages.Controls.Clear();
                picPreview.Image = null;
                _selectedImage = null;

                var result = await ProductsServices.GetProductImages(_productId);

                if (!result.IsSuccess)
                {
                    lblStatus.Text = "Failed to load images";
                    MessageBox.Show(result.Title_Full, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                List<FileResponse> images = result.Data ?? new List<FileResponse>();

                foreach (var image in images)
                    AddImageCard(image);

                lblStatus.Text = images.Count + " image(s) loaded";
            }
            private void AddImageCard(FileResponse file)
            {
                Panel card = new Panel();
                card.Width = 150;
                card.Height = 150;
                card.BackColor = Color.White;
                card.Margin = new Padding(10);
                card.Cursor = Cursors.Hand;
                card.Tag = file;

                PictureBox picture = new PictureBox();
                picture.Width = 130;
                picture.Height = 105;
                picture.Location = new Point(10, 10);
                picture.SizeMode = PictureBoxSizeMode.Zoom;
                picture.BackColor = Color.FromArgb(248, 250, 252);
                picture.Image = FileHelper.BytesToImage(file.FileBytes);
                picture.Tag = file;
                picture.Cursor = Cursors.Hand;

                

                card.Click += ImageCard_Click;
                picture.Click += ImageCard_Click;
 
                card.Controls.Add(picture);
                 flowImages.Controls.Add(card);
            }
 
            private void ImageCard_Click(object sender, EventArgs e)
            {
                Control control = sender as Control;
                FileResponse file = control.Tag as FileResponse;

                if (file == null && control.Parent != null)
                    file = control.Parent.Tag as FileResponse;

                if (file == null)
                    return;

                _selectedImage = file;

                var old = picPreview.Image;
                picPreview.Image = FileHelper.BytesToImage(file.FileBytes);
                old?.Dispose();

            }
   

            private async void btnUpload_Click(object sender, EventArgs e)
            {
                using (OpenFileDialog dialog = new OpenFileDialog())
                {
                    dialog.Title = "Choose Product Image";
                    dialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.webp";

                    if (dialog.ShowDialog() != DialogResult.OK)
                        return;

                    byte[] bytes = File.ReadAllBytes(dialog.FileName);

                    lblStatus.Text = "Uploading image...";
                    btnUpload.Enabled = false;

                    var result = await ProductsServices.CreateProductImage(_productId, bytes);

                    btnUpload.Enabled = true;

                    if (!result.IsSuccess)
                    {
                        lblStatus.Text = "Failed to upload image";
                        MessageBox.Show(result.Title_Full, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    await LoadImages();
                }
            }

            private async void btnDelete_Click(object sender, EventArgs e)
            {
                if (_selectedImage == null)
                {
                    MessageBox.Show("Please select an image first.");
                    return;
                }

                Guid imageId;

                if (!Guid.TryParse(_selectedImage.FileName, out imageId))
                {
                    MessageBox.Show("Image id is invalid.");
                    return;
                }

                var confirm = MessageBox.Show(
                    "Are you sure you want to delete this image?",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirm != DialogResult.Yes)
                    return;

                lblStatus.Text = "Deleting image...";
                btnDelete.Enabled = false;

                var result = await ProductsServices.DeleteProductImage(imageId);

                btnDelete.Enabled = true;

                if (!result.IsSuccess)
                {
                    lblStatus.Text = "Failed to delete image";
                    MessageBox.Show(result.Title_Full, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                await LoadImages();
            }

            private async void btnRefresh_Click(object sender, EventArgs e)
            {
                await LoadImages();
            }

            private void btnClose_Click(object sender, EventArgs e)
            {
                Close();
            }

        private void picPreview_Click(object sender, EventArgs e)
        {
            if (this.picPreview.Image == null) return;
            frmImagePreviewer frm = new frmImagePreviewer(this.picPreview.Image);
            frm.ShowDialog();
        }
    }
    }
