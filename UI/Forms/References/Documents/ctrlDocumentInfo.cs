using Contract.Responses;
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
using System.Xml.Linq;
using UI.Helpers.UI_Helpers;
using UI.Services;
using UI.Shared;
using UI.Shared.Helpers.IO_Helper;

namespace UI.Forms.References.Documents
{
        public partial class ctrlDocumentInfo : UserControl
        {
            private byte[] _selectedImageBytes;
            private string _selectedImagePath = string.Empty;
            private Guid? _loadedDocumentId;

            public ctrlDocumentInfo()
            {
                InitializeComponent();
                SetupUI();
            }

            private void SetupUI()
            {
                this.BackColor = Color.White;
                this.AutoScaleMode = AutoScaleMode.None;

                cmbDocumentType.DropDownStyle = ComboBoxStyle.DropDownList;
                cmbDocumentType.DataSource = Enum.GetValues(typeof(DocumentType));

                StyleTextBox(txtDocumentPath);
                StyleButton(btnBrowse, Color.FromArgb(248, 250, 252), Color.FromArgb(74, 112, 139));
                StyleButton(btnClearFile, Color.FromArgb(243, 246, 249), Color.FromArgb(24, 33, 45));

                txtDocumentPath.ReadOnly = true;
                picPreview.SizeMode = PictureBoxSizeMode.Zoom;
                picPreview.BackColor = Color.FromArgb(248, 250, 252);

                lblStatus.Text = "Document information";
            ImageHelper.ShowEmptyImageWithText(picPreview, "DOC");
        }
            private void StyleTextBox(TextBox textBox)
            {
                textBox.BackColor = Color.FromArgb(248, 250, 252);
                textBox.BorderStyle = BorderStyle.FixedSingle;
                textBox.Font = new Font("Segoe UI", 10F);
                textBox.ForeColor = Color.FromArgb(24, 33, 45);
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
        public bool ImageChanged { get; private set; }

        public Guid? LoadedDocumentId => _loadedDocumentId;
            public bool HasSelectedImage =>
                _selectedImageBytes != null && _selectedImageBytes.Length > 0;
            public string SelectedImagePath => _selectedImagePath;
          
            public DocumentType GetDocumentType()
            {
                if (cmbDocumentType.SelectedItem == null)
                    return default(DocumentType);

                return (DocumentType)cmbDocumentType.SelectedItem;
            }
            public byte[] GetImageBytes()
            {
                return _selectedImageBytes;
            }
        
        public async Task LoadDocument(DocumentDto document)
            {
                if (document == null)
                {
                    Clear();
                    return;
                }

                
                _selectedImageBytes = null;
                _selectedImagePath = string.Empty;
               _loadedDocumentId = document.Id;


            if (Enum.IsDefined(typeof(DocumentType), document.DocumentType))
                cmbDocumentType.SelectedItem = document.DocumentType;
            else {
                MessageBox.Show("Invalid document type" , "Error" , MessageBoxButtons.OK , MessageBoxIcon.Error);
                lblStatus.Text = "Invalid document type"; 
                this.Enabled = false;
            }

            lblStatus.Text = "Document loaded";
            await LoadImage(_loadedDocumentId.Value);
                errorProvider.Clear();
        }
        public void SetImage(byte[] imageBytes)
        {
            if (imageBytes == null || imageBytes.Length == 0)
            {
                ImageHelper.ShowEmptyImageWithText(picPreview, "DOC");
                return;
            }
            var old = picPreview.Image;
            picPreview.Image = FileHelper.BytesToImage(imageBytes);
            old?.Dispose();
        }

        private async Task LoadImage(Guid docId)
        {

            var imageBytes = await DocumentsServices.GetDocumentImage(docId);


            if (imageBytes == null || imageBytes.Length == 0)
            {
                ImageHelper.LoadDefaultImage(picPreview);
                return;
            }

            SetImage(imageBytes);

        }
        public bool ValidateControl(bool imageRequired = true)
            {
                errorProvider.Clear();

                bool isValid = true;

                if (cmbDocumentType.SelectedItem == null)
                {
                    errorProvider.SetError(cmbDocumentType, "Document type is required.");
                    isValid = false;
                }
              

                if (imageRequired && !HasSelectedImage)
                {
                    errorProvider.SetError(txtDocumentPath, "Document image is required.");
                    isValid = false;
                }

                lblStatus.Text = isValid ? "Document information is valid" : "Please fix document errors";

                return isValid;
            }

            public void Clear()
            {
                _loadedDocumentId = null;
                _selectedImageBytes = null;
                _selectedImagePath = string.Empty;

                if (cmbDocumentType.Items.Count > 0)
                    cmbDocumentType.SelectedIndex = 0;

                txtDocumentPath.Clear();
                errorProvider.Clear();

                lblStatus.Text = "Document information";
                ImageHelper.ShowEmptyImageWithText(picPreview,"DOC");
            }


            private void btnBrowse_Click(object sender, EventArgs e)
            {
                using (OpenFileDialog dialog = new OpenFileDialog())
                {
                    dialog.Title = "Choose Document Image";
                    dialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp|All Files|*.*";

                    if (dialog.ShowDialog() != DialogResult.OK)
                        return;

                    try
                    {
                        byte[] bytes = File.ReadAllBytes(dialog.FileName);

                        _selectedImageBytes = bytes;
                        _selectedImagePath = dialog.FileName;
                    ImageChanged = true;

                    txtDocumentPath.Text = Path.GetFileName(dialog.FileName);
                
                        ShowPreview(bytes);

                        lblStatus.Text = "Document image selected";
                        errorProvider.SetError(txtDocumentPath, "");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(
                            "Failed to read selected document image.\n" + ex.Message,
                            "File Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
            }
        private void ShowPreview(byte[] iamge) {

            var old = picPreview.Image;
            picPreview.Image = FileHelper.BytesToImage(iamge);
            old?.Dispose();
        }

            private void btnClearFile_Click(object sender, EventArgs e)
            {
            
            ImageChanged = true;
                _selectedImageBytes = null;
                _selectedImagePath = string.Empty;
                txtDocumentPath.Clear();
            picPreview.Image = null;

                lblStatus.Text = "Document image cleared";
            ImageHelper.ShowEmptyImageWithText(picPreview,"DOC");
            }

        private void picPreview_Click(object sender, EventArgs e)
        {
            if (picPreview.Image == null) return;


            frmImagePreviewer frm = new frmImagePreviewer(picPreview.Image);

            frm.ShowDialog();
        }
    }
    }

