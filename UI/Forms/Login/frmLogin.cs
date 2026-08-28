using Contract.Requests.Identity;
using Contract.Requests.Suppliers;
using Infrastructure.Identity;
using InventorySystemUI.Main;
using OldContract.Features.User.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI;
using Domain.Common.Helpers;
using UI.Shared.CurrentUser;
using UI.Shared.Services;
using UI.Shared.Storage;
using static HotelSystemUI.HttpClients.HttpClientHelper;


namespace HotelSystemUI.Login
{
  
   
        public partial class frmLogin : BaseForm
        {
            public frmLogin()
            {
                InitializeComponent();
            this.Load += (s, e) => CenterLoginCard();
            this.Resize += (s, e) => CenterLoginCard();

        }

            public string Email => txtEmail.Text.Trim();
            public string Password => txtPassword.Text;
            public bool RememberMe => chkRememberMe.Checked;

            private void CenterLoginCard()
            {
                pnlLoginCard.Left = (ClientSize.Width - pnlLoginCard.Width) / 2;
                pnlLoginCard.Top = (ClientSize.Height - pnlLoginCard.Height) / 2 + 30;
            }

            private void RequiredField_Validating(object sender, CancelEventArgs e)
            {
                if (sender is TextBox txt)
                {
                    if (string.IsNullOrWhiteSpace(txt.Text))
                        errorProvider1.SetError(txt, "This field is required.");
                    else
                        errorProvider1.SetError(txt, "");
                }
            }

            private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
            {
                txtPassword.UseSystemPasswordChar = !chkShowPassword.Checked;
            }

            private async void btnLogin_Click(object sender, EventArgs e)
            {
                ValidateChildren();

                if (!string.IsNullOrWhiteSpace(errorProvider1.GetError(txtEmail)))
                    return;

                if (!string.IsNullOrWhiteSpace(errorProvider1.GetError(txtPassword)))
                    return;

                if (!ValidationHelper.ValidateEmail(Email))
                {
                    errorProvider1.SetError(txtEmail, "Enter a valid email address.");
                    return;
                }

                errorProvider1.SetError(txtEmail, string.Empty);

                await LoginByCredentials();
            }

            private async Task LoginByCredentials()
            {
                SetLoading(true);

                try
                {
                    var result = await IdentityService.GenerateJwt(new JwtGeneratCommand
                    {
                        email = Email,
                        password = Password
                    });


                    if (!result.IsSuccess || result.Data == null)
                    {
                        MessageBox.Show(result.Title_Full, "Sign In Failed",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    await Login(result.Data, chkRememberMe.Checked);

                }
                finally
                {
                    SetLoading(false);
                }
            }

            private async Task LoginByRefreshToken() {

            SetLoading(true);

            try
            {

                var refreshToken = SecurityStorage.ReadRefreshToken();
                var email = RegistryStorage.GetEmail();

                if (string.IsNullOrEmpty(refreshToken)) return;
                
                if (string.IsNullOrEmpty(email)) return;

                txtEmail.Text = email;

                var result = await IdentityService.GenerateJwtByRefreshToken(new JwtGenerateByRefreshTokenCommand()
                {

                    refresh = refreshToken,
                    loginSource = true

                });

                if (!result.IsSuccess || result.Data == null)
                    return;

                await Login(result.Data, true);

            }
            finally
            {
                SetLoading(false);
            }

        }

            private async Task Login(JwtDto jwt , bool rememberMe) {
          
            CurrentUser.Jwt = jwt.AccessToken;
            _inventoryClient.DefaultRequestHeaders.Authorization =
                   new AuthenticationHeaderValue("Bearer", CurrentUser.Jwt);
 
            if (!rememberMe)
            {
                ClearStoredData();
            }
             
                SecurityStorage.StoreRefreshToken(jwt.RefreshToken);
                RegistryStorage.SaveEmail(Email);
            

            var user = await UserServices.GetByEmail(Email);

            if (!user.IsSuccess || user.Data == null)
            {
                MessageBox.Show(user.Title_Full, "Sign In Failed",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!user.Data.IsActive)
            {
                MessageBox.Show(
                    "This account has been deactivated. Please contact your administrator.",
                    "Account Inactive",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            CurrentUser.User = user.Data;

            Hide();

            DialogResult frmDialogResult;
            using (MainForm frm = new MainForm(this)) {
                frm.ShowDialog();
                frmDialogResult = frm.DialogResult;
            }
            DialogResult = DialogResult.OK;


            if (frmDialogResult == DialogResult.OK)
                Close();
            else
                await StartForm();

        }

            private void SetLoading(bool isLoading)
            {
                btnLogin.Enabled = !isLoading;
                btnExit.Enabled = !isLoading;
                txtEmail.Enabled = !isLoading;
                txtPassword.Enabled = !isLoading;
                chkRememberMe.Enabled = !isLoading;
                chkShowPassword.Enabled = !isLoading;

                btnLogin.Text = isLoading ? "Signing in..." : "Login";
                lblStatus.Text = isLoading ? "Please wait while we verify your account..." : "";
            }

            private void btnExit_Click(object sender, EventArgs e)
            {
                DialogResult = DialogResult.Cancel;
                Close();
            }

            private async void frmLogin_Load(object sender, EventArgs e)
        {
            await StartForm();
        }
            private void ClearStoredData() {
            SecurityStorage.Clear();
            ConfigurationManagement.ResetEmail();
        }
            public async Task StartForm() {

            this.Visible = true;
            this.txtEmail.Clear();
            this.txtPassword.Clear();
            this.chkRememberMe.Checked = false;
            this.chkShowPassword.Checked = false;
            SetLoading(false);

            await LoginByRefreshToken();
        }

    }
 
} 

