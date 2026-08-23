using Contract.Requests.Documents;
using Contract.Requests.Warehouses;
using Contract.Responses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI.Forms.People.Controlls;
using UI.Services;
using UI.Shared.CurrentUser;

namespace UI.Forms.People
{
    public partial class frmMakePersonDocument : Form
    {
        private Guid _personId = Guid.Empty;
        private Guid? _documentId = Guid.Empty;
        private bool _isUpdateMode = false;
        private DocumentDto _document;

        public DocumentDto Document => _document;
        public frmMakePersonDocument(Guid personId)
        {
            InitializeComponent();
            this._personId = personId;
            _isUpdateMode = false;
            SetupUI();
        }
        public frmMakePersonDocument(Guid personId , Guid? documentId)
        {
            InitializeComponent();
            this._documentId = documentId;
            this._personId = personId;
            _isUpdateMode = !(documentId == null || documentId == Guid.Empty || personId == Guid.Empty);
            SetupUI();

        }
        public string Title {

            set { 
            
                this.lblTitle.Text = value;
            }
        }
        private void SetupUI()
        {
            this.BackColor = Color.FromArgb(243, 246, 249);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            lblTitle.Text = _isUpdateMode ? "Edit Document" : "Add Document";
            lblSubtitle.Text = _isUpdateMode
                ? "Update  document information."
                : "Create a new person  document.";

            StyleButton(btnSave, Color.FromArgb(74, 112, 139), Color.White);
            StyleButton(btnCancel, Color.FromArgb(243, 246, 249), Color.FromArgb(24, 33, 45));
            StyleButton(btnDelete, Color.FromArgb(243, 246, 249), Color.FromArgb(24, 33, 45));

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
        }


        public async Task LoadData() {

            if (_documentId == null || _documentId == Guid.Empty) return;
            if (_personId == null || _personId == Guid.Empty) return;

            lblStatus.Text = "Loading document...";

            var result = await DocumentsServices.Get(_documentId.Value);

            if (!result.IsSuccess)
            {
                lblStatus.Text = "Failed to load document";
                MessageBox.Show(result.Title_Full, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _isUpdateMode  = false;
                this._documentId = null;

                SetupUI();
                return;
            }

            _document = result.Data;
 
            lblStatus.Text = "Ready";
            BindDoc();
        }

        private async void BindDoc() {

            if (_document == null) return;

            await this.ctrlDocumentInfo1.LoadDocument(_document);
        }

        private UpdateDocumentRequest GenerateUpdateRequest() {

            return new UpdateDocumentRequest() { 
            
                DocumentType = ctrlDocumentInfo1.GetDocumentType() ,
                Id = _document.Id,
                Image = ctrlDocumentInfo1.ImageChanged? ctrlDocumentInfo1.GetImageBytes() : null,
            };

        }

        private CreateDocumentRequest GenerateCreateDocumentRequest() {

            return new CreateDocumentRequest() { 
                DocumentImage = ctrlDocumentInfo1.GetImageBytes()
                ,DocumentType = ctrlDocumentInfo1.GetDocumentType(),
            };
        }
        private CreatePersonDocumentRequest GenerateCreatePersonDocumentRequest()
        {

            return new CreatePersonDocumentRequest()
            {
                Document = GenerateCreateDocumentRequest() ,
                PersonId = _personId
            };

        }

        private bool ValidateForm() {

            return ctrlDocumentInfo1.ValidateControl(!(_isUpdateMode && !ctrlDocumentInfo1.ImageChanged) );

      
        }

        public bool DocumentDeleted { get; private set; } = false;

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateForm()) return;
            btnSave.Enabled = false;
            lblStatus.Text = "Saving product...";

            if (!_isUpdateMode)
            {

                var result = await PeopleServices.CreatePersonDocument(GenerateCreatePersonDocumentRequest());

                if (!result.IsSuccess)
                {
                    btnSave.Enabled = true;
                    lblStatus.Text = "Failed to save document";
                    MessageBox.Show(result.Title_Full, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (result.Data != null)
                {

                    _document = result.Data;

                }
            }
            else {
                var request = GenerateUpdateRequest();

                var result = await DocumentsServices.Update(request);

                if (!result.IsSuccess)
                {
                    btnSave.Enabled = true;
                    lblStatus.Text = "Failed to save document";
                    MessageBox.Show(result.Title_Full, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (result.Data != null) { 
                
                    _document = result.Data;

                }

            }

            lblStatus.Text = "Saved successfully";
            DialogResult = DialogResult.OK;
            Close();



        }
         


        private void panelFooter_Paint(object sender, PaintEventArgs e)
        {


        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private async void frmMakePersonDocument_Load(object sender, EventArgs e)
        {
            if (_isUpdateMode)
                await LoadData();
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (!_isUpdateMode) return;
            if (_documentId == null) return;
            if (_documentId == Guid.Empty) return;


            var confirm = MessageBox.Show(
                $"Are you sure you want to delete?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes)
                return;

            btnDelete.Enabled = false;

            var result = await DocumentsServices.Delete(_documentId.Value);

            btnDelete.Enabled = true;

            if (!result.IsSuccess)
            {
                MessageBox.Show(result.Title_Full, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this._document = null;
            DocumentDeleted = true;
            this.Close();
        }

        private void panelHeader_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

