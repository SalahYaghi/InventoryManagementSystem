using Contract.Requests.ContactInfos;
using Contract.Responses;
using Domain.Common.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI.Forms.Refrences.Contacts
{
    
        public partial class ctrlContactInfo : UserControl
        {
            public ctrlContactInfo()
            {
                InitializeComponent();
            this.AutoScaleMode = AutoScaleMode.None;
          
            SetupUI(); 
            
        }

        private void SetupUI()
            {
                this.BackColor = Color.White;

                StyleTextBox(txtEmail);
                StyleTextBox(txtPhoneNumber);
                StyleTextBox(txtAlternativePhoneNumber);
                StyleTextBox(txtFaxNumber);
                StyleTextBox(txtWebsiteUrl);

                lblStatus.Text = "Contact information";
            }

            private void StyleTextBox(TextBox textBox)
            {
                textBox.BackColor = Color.FromArgb(248, 250, 252);
                textBox.BorderStyle = BorderStyle.FixedSingle;
                textBox.Font = new Font("Segoe UI", 10F);
                textBox.ForeColor = Color.FromArgb(24, 33, 45);
            }

            public void LoadContact(ContactInfoDto contact)
            {
                if (contact == null)
                {
                    Clear();
                    return;
                }

                txtEmail.Text = contact.Email ?? "";
                txtPhoneNumber.Text = contact.PhoneNumber ?? "";
                txtAlternativePhoneNumber.Text = contact.AlternitavePhoneNumber ?? "";
                txtFaxNumber.Text = contact.FaxNumber ?? "";
                txtWebsiteUrl.Text = contact.WebsiteUrl ?? "";

                errorProvider.Clear();
                lblStatus.Text = "Contact loaded";
            }

            public CreateContactInfoRequest GetCreateRequest()
            {
                return new CreateContactInfoRequest
                {
                    Email = txtEmail.Text.Trim(),
                    PhoneNumber = txtPhoneNumber.Text.Trim(),
                    AlternitavePhoneNumber = EmptyToNull(txtAlternativePhoneNumber.Text),
                    FaxNumber = EmptyToNull(txtFaxNumber.Text),
                    WebsiteUrl = EmptyToNull(txtWebsiteUrl.Text)
                };
            }

            public UpdateContactInfoRequest GetUpdateRequest()
            {
                return new UpdateContactInfoRequest
                {
                    Email = txtEmail.Text.Trim(),
                    PhoneNumber = txtPhoneNumber.Text.Trim(),
                    AlternitavePhoneNumber = EmptyToNull(txtAlternativePhoneNumber.Text),
                    FaxNumber = EmptyToNull(txtFaxNumber.Text),
                    WebsiteUrl = EmptyToNull(txtWebsiteUrl.Text)
                };
            }

            public bool ValidateControl()
            {
                errorProvider.Clear();

                bool isValid = true;

                if (string.IsNullOrWhiteSpace(txtEmail.Text))
                {
                    errorProvider.SetError(txtEmail, "Email is required.");
                    isValid = false;
                }
                else if (!ValidationHelper.ValidateEmail(txtEmail.Text.Trim()))
                {
                    errorProvider.SetError(txtEmail, "Email format is invalid.");
                    isValid = false;
                }

                if (string.IsNullOrWhiteSpace(txtPhoneNumber.Text))
                {
                    errorProvider.SetError(txtPhoneNumber, "Phone number is required.");
                    isValid = false;
                }

                if (!string.IsNullOrWhiteSpace(txtWebsiteUrl.Text) &&
                    !Uri.IsWellFormedUriString(txtWebsiteUrl.Text.Trim(), UriKind.Absolute))
                {
                    errorProvider.SetError(txtWebsiteUrl, "Website URL is invalid.");
                    isValid = false;
                }

                lblStatus.Text = isValid ? "Contact information is valid" : "Please fix contact errors";
                return isValid;
            }

            public void Clear()
            {
                txtEmail.Clear();
                txtPhoneNumber.Clear();
                txtAlternativePhoneNumber.Clear();
                txtFaxNumber.Clear();
                txtWebsiteUrl.Clear();

                errorProvider.Clear();
                lblStatus.Text = "Contact information";
                txtEmail.Focus();
            }

            public bool HasData()
            {
                return
                    !string.IsNullOrWhiteSpace(txtEmail.Text) ||
                    !string.IsNullOrWhiteSpace(txtPhoneNumber.Text) ||
                    !string.IsNullOrWhiteSpace(txtAlternativePhoneNumber.Text) ||
                    !string.IsNullOrWhiteSpace(txtFaxNumber.Text) ||
                    !string.IsNullOrWhiteSpace(txtWebsiteUrl.Text);
            }

            private string EmptyToNull(string value)
            {
                return string.IsNullOrWhiteSpace(value) ? string.Empty : value.Trim();
            }

        private void groupContact_Enter(object sender, EventArgs e)
        {

        }

        private void txtPhoneNumber_TextChanged(object sender, EventArgs e)
        {

        }
    }
    }

