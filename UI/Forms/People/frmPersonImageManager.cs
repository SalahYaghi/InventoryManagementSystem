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
using UI.Helpers.UI_Helpers;
using UI.Services;
using UI.Shared;
using UI.Shared.Helpers.IO_Helper;

namespace UI.Forms.People
{
     public partial class frmPersonImageManager : Form
        {
            private readonly Guid _personId;
            private byte[] _selectedImageBytes;
            private string _selectedImagePath = string.Empty;

            public frmPersonImageManager(Guid personId)
            {
                InitializeComponent();
                _personId = personId;
                SetupUI();
            }

            private async void frmPersonImageManager_Load(object sender, EventArgs e)
            {
                await LoadPersonImage();
            }

            private void SetupUI()
            {
                BackColor = Color.FromArgb(243, 246, 249);
                FormBorderStyle = FormBorderStyle.FixedDialog;
                StartPosition = FormStartPosition.CenterParent;
                MaximizeBox = false;
                MinimizeBox = false;

                StyleButton(btnChooseImage, Color.FromArgb(74, 112, 139), Color.White);
                StyleButton(btnSave, Color.FromArgb(74, 112, 139), Color.White);
                StyleButton(btnClearSelection, Color.FromArgb(243, 246, 249), Color.FromArgb(24, 33, 45));
                 StyleButton(btnClose, Color.FromArgb(243, 246, 249), Color.FromArgb(24, 33, 45));

                picPersonImage.SizeMode = PictureBoxSizeMode.Zoom;
                picPersonImage.BackColor = Color.FromArgb(248, 250, 252);

                lblSelectedFile.Text = "No image selected";
                lblStatus.Text = "Ready";

            ImageHelper.LoadDefaultImage(picPersonImage);
        }

        private void StyleButton(Button button, Color backColor, Color foreColor)
            {
                button.BackColor = backColor;
                button.ForeColor = foreColor;
                button.FlatStyle = FlatStyle.Flat;
                button.FlatAppearance.BorderSize = 0;
                button.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
                button.Cursor = Cursors.Hand;
            }

            private async Task LoadPersonImage()
            {
                lblStatus.Text = "Loading image...";

                try
                {
                    var bytes = await PeopleServices.GetPersonImage(_personId);

                    if (bytes == null || bytes.Length == 0)
                    {
                    ImageHelper.LoadDefaultImage(picPersonImage);
                    lblStatus.Text = "No image found";
                        return;
                    }

                    ShowImage(bytes);
                    lblStatus.Text = "Image loaded";
                }
                catch
                {
                ImageHelper.LoadDefaultImage(picPersonImage);
                lblStatus.Text = "Could not load image";
                }
            }

            private void btnChooseImage_Click(object sender, EventArgs e)
            {
                using (OpenFileDialog dialog = new OpenFileDialog())
                {
                    dialog.Title = "Choose Person Image";
                    dialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp|All Files|*.*";

                    if (dialog.ShowDialog() != DialogResult.OK)
                        return;

                    try
                    {
                        _selectedImageBytes = File.ReadAllBytes(dialog.FileName);
                        _selectedImagePath = dialog.FileName;

                        lblSelectedFile.Text = Path.GetFileName(dialog.FileName);
                        ShowImage(_selectedImageBytes);

                        lblStatus.Text = "New image selected";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(
                            "Failed to read selected image.\n" + ex.Message,
                            "File Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
            }

            private async void btnSave_Click(object sender, EventArgs e)
            {

                btnSave.Enabled = false;
                lblStatus.Text = "Updating image...";

                var result = await PeopleServices.UpdatePersonImage(_personId, _selectedImageBytes);

                btnSave.Enabled = true;

                if (!result.IsSuccess)
                {
                    lblStatus.Text = "Failed to update image";
                    MessageBox.Show(result.Title_Full, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                _selectedImageBytes = null;
                _selectedImagePath = string.Empty;
                lblSelectedFile.Text = "No image selected";

                lblStatus.Text = "Image updated successfully";
                DialogResult = DialogResult.OK;
            }

            private void btnClearSelection_Click(object sender, EventArgs e)
            {
                _selectedImageBytes = null;
                _selectedImagePath = string.Empty;
                lblSelectedFile.Text = "No image selected";

            picPersonImage.Image = null;
            ImageHelper.LoadDefaultImage(picPersonImage);
            }

     

            private void btnClose_Click(object sender, EventArgs e)
            {
                Close();
            }

            private void ShowImage(byte[] bytes)
            {
                try
                {

                Image old = picPersonImage.Image;
                
                        picPersonImage.Image = FileHelper.BytesToImage(bytes);
                old?.Dispose();
                    
                }
                catch
                {
                ImageHelper.LoadDefaultImage(picPersonImage);
                    lblStatus.Text = "Selected file cannot be previewed";
                }
            }

        private void picPersonImage_Click(object sender, EventArgs e)
        {
            if (picPersonImage.Image == null) return;
            frmImagePreviewer frm = new frmImagePreviewer(picPersonImage.Image);
            frm.ShowDialog();
        }
    }
    }

