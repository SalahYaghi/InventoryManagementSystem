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
         //   this.Icon = UI.Properties.Resources.inventory_x_icon;
            this.Load += (s, e) => CenterLoginCard();
                this.Resize += (s, e) => CenterLoginCard();
           // this.txtPassword.Text = "Salahnour1*";
            //this.txtEmail.Text = "nour@gmail.com";


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

                if (!string.IsNullOrWhiteSpace(errorProvider1.GetError(txtEmail))) return;
                if (!string.IsNullOrWhiteSpace(errorProvider1.GetError(txtPassword))) return;

                await LoginByCredintaials();
            }

            private async Task LoginByCredintaials()
            {
                SetLoading(true);

                try
                {
                    var result = await IdentityService.GenerateJwt(new JwtGeneratCommand
                    {
                        email = Email,
                        password = Password
                    });


                    if (!result.IsSuccess)
                    {
                        MessageBox.Show(result.Title_Full, "Failed",
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
                var email = RegisteryStorage.GetEmail();

                if (string.IsNullOrEmpty(refreshToken)) return;
                
                if (string.IsNullOrEmpty(email)) return;

                txtEmail.Text = email;

                var result = await IdentityService.GenerateJwtByRefreshToken(new JwtGenerateByRefreshTokenCommand()
                {

                    refresh = refreshToken,
                    loginSource = true

                });

                if (!result.IsSuccess)
                {
                    MessageBox.Show(result.Title_Full, "Failed",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                await Login(result.Data , true);

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
                RegisteryStorage.SaveEmail(Email);
            

            var user = await UserServices.GetByEmail(Email);

            if (!user.IsSuccess)
            {
                MessageBox.Show(user.Title_Full, "Failed",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            private void chkRememberMe_CheckedChanged(object sender, EventArgs e)
        {

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

