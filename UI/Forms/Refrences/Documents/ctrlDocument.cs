using Contract.Responses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI.Forms.People;
using UI.Helpers.UI_Helpers;
using UI.Services;
using UI.Shared;
using UI.Shared.Helpers.IO_Helper;

namespace UI.Forms.Refrences.Documents
{
        public partial class ctrlDocumentDetails : UserControl
        {
            private DocumentDto _document;
        private Guid _relatedPersonId;

        public void AssignPerson(Guid personId) {

            if (personId == Guid.Empty) return;
            this._relatedPersonId = personId;
        }
            public ctrlDocumentDetails()
            {
                InitializeComponent();
                SetupUI();
            }
            private void SetupUI()
            {
                this.BackColor = Color.White;
                this.AutoScaleMode = AutoScaleMode.None;

                StyleValue(lblDocumentTypeValue);
             
                picPreview.SizeMode = PictureBoxSizeMode.Zoom;
                picPreview.BackColor = Color.FromArgb(248, 250, 252);

                Clear();
            }
            private void StyleValue(Label label)
            {
                label.BackColor = Color.FromArgb(248, 250, 252);
                label.ForeColor = Color.FromArgb(24, 33, 45);
                label.Font = new Font("Segoe UI", 10F);
                label.Padding = new Padding(8, 0, 0, 0);
            }
            public DocumentDto Document => _document;

        public async Task LoadDocument(Guid id)
        {
            var result = await DocumentsServices.Get(_document.Id);

            if (!result.IsSuccess)
            {
                lblUpdateDocument.Enabled = false;
                MessageBox.Show(result.Title_Full, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            await LoadImage(_document.Id);

            lblUpdateDocument.Enabled = true;


            if (result.Data == null)
            {
                Clear();
                return;
            }

            _document = result.Data;

            lblDocumentTypeValue.Text = TransformIfNull(_document.DocumentType.ToString());


        }

        public async Task LoadDocument(DocumentDto document)
            {
                _document = document;

                if (document == null)
                {
                    Clear();
                    return;
                }

                lblDocumentTypeValue.Text = TransformIfNull(document.DocumentType.ToString());


            await LoadImage(_document.Id);
            }

            private async Task LoadImage(Guid docId) {

            var imageBytes = await DocumentsServices.GetDocumentImage(docId);


            if (imageBytes == null || imageBytes.Length == 0)
            {
                ImageHelper.LoadDefaultImage(picPreview);
                return;
            }
        
            SetImage(imageBytes);

        }
        
            public void SetImage(byte[] imageBytes)
            {
                if (imageBytes == null || imageBytes.Length == 0)
                {
                    ImageHelper.ShowEmptyImageWithText( picPreview ,"DOC");
                    return;
                }
                 var old = picPreview.Image;
                picPreview.Image =  FileHelper.BytesToImage(imageBytes);
                old?.Dispose();
            }
            public void Clear()
        {
            _document = null;

            lblDocumentTypeValue.Text = "-";

            ImageHelper.ShowEmptyImageWithText(picPreview, "DOC");
        }
            private string TransformIfNull(string value)
            {
                return string.IsNullOrWhiteSpace(value) ? "-" : value.Trim();
            }

            private void picPreview_Click(object sender, EventArgs e)
        {
            if (picPreview.Image == null) return;


            frmImagePreviewer frm = new frmImagePreviewer(picPreview.Image);

            frm.ShowDialog();
        }

        private void groupDocument_Enter(object sender, EventArgs e)
        {

        }


       

        private async void lblUpdateDocument_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_document == null) {

                if (_relatedPersonId == null) return;

                using (frmMakePersonDocument frmMakeImage = new frmMakePersonDocument(_relatedPersonId)) { 
  
                    frmMakeImage.ShowDialog();
                    if (frmMakeImage.DialogResult == DialogResult.OK) {
                        if (
                        frmMakeImage.Document != null)
                            await LoadDocument(frmMakeImage.Document);
                        else {

                            Clear();
                        }
                    }
                   
                }
                return;
            }


            using (
            frmUpdateDocument frm = new frmUpdateDocument(_document.Id))
            {
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK) {
                    if (!frm.DocumentDeleted)
                        await LoadDocument(_document.Id);
                    else
                        Clear();
                }
                   
            }

        }
    }
    }

