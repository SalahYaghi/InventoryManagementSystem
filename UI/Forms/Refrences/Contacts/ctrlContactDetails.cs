using Contract.Responses;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace UI.Forms.Refrences.Contacts
{
    public partial class ctrlContactDetails : UserControl
    {
        public ctrlContactDetails()
        {
            InitializeComponent();
            SetupUI();
        }

        private void SetupUI()
        {
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.White;

            StyleValue(lblEmailValue);
            StyleValue(lblPhoneValue);
            StyleValue(lblAlternativePhoneValue);
            StyleValue(lblFaxValue);
            StyleValue(lblWebsiteValue);
        }

        private void StyleValue(Label label)
        {
            label.BackColor = Color.FromArgb(248, 250, 252);
            label.ForeColor = Color.FromArgb(24, 33, 45);
            label.Font = new Font("Segoe UI", 10F);
            label.Padding = new Padding(8, 0, 0, 0);
        }

        public void LoadContact(ContactInfoDto contact)
        {
            if (contact == null)
            {
                Clear();
                return;
            }

            lblEmailValue.Text = Safe(contact.Email);
            lblPhoneValue.Text = Safe(contact.PhoneNumber);
            lblAlternativePhoneValue.Text = Safe(contact.AlternitavePhoneNumber);
            lblFaxValue.Text = Safe(contact.FaxNumber);
            lblWebsiteValue.Text = Safe(contact.WebsiteUrl);
        }

        public void Clear()
        {
            lblEmailValue.Text = "-";
            lblPhoneValue.Text = "-";
            lblAlternativePhoneValue.Text = "-";
            lblFaxValue.Text = "-";
            lblWebsiteValue.Text = "-";
        }

        private string Safe(string value)
        {
            return string.IsNullOrWhiteSpace(value) ? "-" : value.Trim();
        }
    }
}

