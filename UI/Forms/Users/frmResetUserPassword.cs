using ContracOldCompatibile.Requests.Users;
using Contract.Requests.Users;
using System;
using System.Drawing;
using System.Windows.Forms;
using UI.Shared.Services;

namespace UI.Forms.Users
{
    public partial class frmResetUserPassword : Form
    {
        private readonly Guid _userId;
        private readonly string _username;
        public frmResetUserPassword(Guid userId, string username
            )
        {
            _userId = userId;
            _username = username;
            InitializeComponent();

            SetupUI();
        }

   
        private void SetupUI()
        {
            lblSubtitle.Text = "Update password for user: ";

            BackColor = Color.FromArgb(243, 246, 249);

            StyleTextBox(txtOldPassword);
            StyleTextBox(txtNewPassword);
            StyleTextBox(txtConfirmPassword);

            StyleButton(btnSave, Color.FromArgb(74, 112, 139), Color.White);
            StyleButton(btnCancel, Color.FromArgb(243, 246, 249), Color.FromArgb(24, 33, 45));
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

        private void StyleTextBox(TextBox textBox)
        {
            textBox.BackColor = Color.FromArgb(248, 250, 252);
            textBox.BorderStyle = BorderStyle.FixedSingle;
            textBox.Font = new Font("Segoe UI", 10F);
            textBox.ForeColor = Color.FromArgb(24, 33, 45);
        }

        private bool ValidateForm()
        {
            errorProvider.Clear();

            bool valid = true;

            if (string.IsNullOrWhiteSpace(txtOldPassword.Text))
            {
                errorProvider.SetError(txtOldPassword, "Old password is required.");
                valid = false;
            }

            if (string.IsNullOrWhiteSpace(txtNewPassword.Text))
            {
                errorProvider.SetError(txtNewPassword, "New password is required.");
                valid = false;
            }

            if (txtNewPassword.Text != txtConfirmPassword.Text)
            {
                errorProvider.SetError(txtConfirmPassword, "Passwords do not match.");
                valid = false;
            }

            return valid;
        }

        private UpdateUserPasswordRequest BuildRequest()
        {
            return new UpdateUserPasswordRequest
            {
                oldpassword = txtOldPassword.Text,
                newpassword = txtNewPassword.Text
            };
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateForm())
                return;

            btnSave.Enabled = false;
            lblStatus.Text = "Updating password...";

            var result = await UserServices.UpdateUserPassword(_userId, BuildRequest());

            btnSave.Enabled = true;

            if (!result.IsSuccess)
            {
                lblStatus.Text = "Failed to update password";
                MessageBox.Show(result.Title_Full, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lblStatus.Text = "Password updated";
            DialogResult = DialogResult.OK;
            Close();
        }

        private void chkShowPasswords_CheckedChanged(object sender, EventArgs e)
        {
            bool hide = !chkShowPasswords.Checked;

            txtOldPassword.UseSystemPasswordChar = hide;
            txtNewPassword.UseSystemPasswordChar = hide;
            txtConfirmPassword.UseSystemPasswordChar = hide;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

       
    }
}

