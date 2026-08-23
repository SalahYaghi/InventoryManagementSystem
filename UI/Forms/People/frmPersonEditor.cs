using Contract.Responses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI.Services;

namespace UI.Forms.People
{
     
        public partial class frmPersonEditor : Form
        {
            private readonly bool _isUpdateMode;
            private readonly Guid _personId;
            private PersonDto _person;

            public frmPersonEditor()
            {
                InitializeComponent();
                _isUpdateMode = false;
                SetupUI();
            }

            public frmPersonEditor(Guid personId)
            {
                InitializeComponent();
                _personId = personId;
                _isUpdateMode = true;
                SetupUI();
            }

            private async void frmPersonEditor_Load(object sender, EventArgs e)
            {
                await ctrlPersonEditor1.LoadData();

                if (_isUpdateMode)
                    await LoadPerson();
            }

            private void SetupUI()
            {
                this.BackColor = Color.FromArgb(243, 246, 249);
                this.FormBorderStyle = FormBorderStyle.FixedDialog;
                this.StartPosition = FormStartPosition.CenterParent;
                this.MaximizeBox = false;
                this.MinimizeBox = false;

                lblTitle.Text = _isUpdateMode ? "Edit Person" : "Add Person";
                lblSubtitle.Text = _isUpdateMode
                    ? "Update identity, contact, address and document information."
                    : "Create a new person profile with contact, address and optional document.";

            StyleButton(btnChangeImage, Color.FromArgb(74, 112, 139), Color.White);

            StyleButton(btnSave, Color.FromArgb(74, 112, 139), Color.White);
                StyleButton(btnCancel, Color.FromArgb(243, 246, 249), Color.FromArgb(24, 33, 45));

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

            private async Task LoadPerson()
            {
                lblStatus.Text = "Loading person...";

                var result = await PeopleServices.Get(_personId);

                if (!result.IsSuccess)
                {
                    lblStatus.Text = "Failed to load person";
                    MessageBox.Show(result.Title_Full, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                _person = result.Data;
                await ctrlPersonEditor1.LoadPerson(_person);

                lblStatus.Text = "Ready";
            }

            private async void btnSave_Click(object sender, EventArgs e)
            {
                if (!ctrlPersonEditor1.ValidateControl())
                    return;

                btnSave.Enabled = false;
                lblStatus.Text = "Saving person...";

                if (_isUpdateMode)
                {
                    var result = await PeopleServices.Update(_personId, ctrlPersonEditor1.GetUpdateRequest());

                    if (!result.IsSuccess)
                    {
                        btnSave.Enabled = true;
                        lblStatus.Text = "Failed to save person";
                        MessageBox.Show(result.Title_Full, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Document saving is intentionally separate.
                    // Later, after you add a PeopleServices.CreatePersonDocument(...) method,
                    // you can call it here if ctrlPersonEditor1.HasDocumentImage().
                }
                else
                {
                    var result = await PeopleServices.Create(ctrlPersonEditor1.GetCreateRequest());

                    if (!result.IsSuccess)
                    {
                        btnSave.Enabled = true;
                        lblStatus.Text = "Failed to save person";
                        MessageBox.Show(result.Title_Full, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Optional future hook:
                    // if (ctrlPersonEditor1.HasDocumentImage())
                    // {
                    //     await PeopleServices.CreatePersonDocument(
                    //         result.Data.Id,
                    //         ctrlPersonEditor1.GetDocumentType(),
                    //         ctrlPersonEditor1.GetDocumentImageBytes());
                    // }
                }

                lblStatus.Text = "Saved successfully";
                DialogResult = DialogResult.OK;
                Close();
            }

            private void btnCancel_Click(object sender, EventArgs e)
            {
                DialogResult = DialogResult.Cancel;
                Close();
            }

        private async void btnChangeImage_Click(object sender, EventArgs e)
        { 
            using (var frm = new frmPersonImageManager(_personId))
            {
                frm.ShowDialog();

                if(frm.DialogResult == DialogResult.OK) 
                    this.DialogResult = DialogResult.OK;
               
            }
        }
    }
    
}

