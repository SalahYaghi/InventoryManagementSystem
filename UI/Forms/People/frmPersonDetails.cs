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
using UI.Helpers.UI_Helpers;
using UI.Services;
using UI.Shared;
using UI.Shared.Helpers.UI_Helpers;

namespace UI.Forms.People
{
     
        public partial class frmPersonDetails : Form
        {
            private readonly Guid _personId;
            private PersonDto _person;

            public frmPersonDetails(Guid personId)
            {
                InitializeComponent();
                _personId = personId;
                SetupUI();
            }

            private async void frmPersonDetails_Load(object sender, EventArgs e)
            {
                await LoadPerson();
            }

            private void SetupUI()
            {
            this.ctrlDocumentDetails1.AssignPerson(_personId);
                this.BackColor = Color.FromArgb(243, 246, 249);
                this.FormBorderStyle = FormBorderStyle.FixedDialog;
                this.StartPosition = FormStartPosition.CenterParent;
                this.MaximizeBox = false;
                this.MinimizeBox = false;

                StyleButton(btnEdit, Color.FromArgb(74, 112, 139), Color.White);
                StyleButton(btnClose, Color.FromArgb(243, 246, 249), Color.FromArgb(24, 33, 45));

                StyleValueLabel(lblNationalNoValue);
                StyleValueLabel(lblFullNameValue);
                StyleValueLabel(lblDateOfBirthValue);
                StyleValueLabel(lblGenderValue);

                picPersonImage.SizeMode = PictureBoxSizeMode.Zoom;
                picPersonImage.BackColor = Color.FromArgb(248, 250, 252);

                lblStatus.Text = "Loading...";
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

            private void StyleValueLabel(Label label)
            {
                label.BackColor = Color.FromArgb(248, 250, 252);
                label.ForeColor = Color.FromArgb(24, 33, 45);
                label.Font = new Font("Segoe UI", 10F);
                label.Padding = new Padding(8, 0, 0, 0);
                label.TextAlign = ContentAlignment.MiddleLeft;
            }

            private async Task LoadPerson()
            {
                lblStatus.Text = "Loading person details...";

                var result = await PeopleServices.Get(_personId);

                if (!result.IsSuccess)
                {
                    lblStatus.Text = "Failed to load person";
                    MessageBox.Show(result.Title_Full, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                _person = result.Data;

                BindPerson();

                await LoadPersonImage();

                lblStatus.Text = "Ready";
            }

            private async void BindPerson()
            {
                if (_person == null)
                    return;

            string fullName = TextFormattingHelper.JoinString(new string[] {
                    _person.FirstName , _person.SecondName , _person.ThirdName , _person.LastName ,
                });
                lblPersonName.Text = fullName;
                lblPersonSubTitle.Text = "National No: " + TransformIfNull(_person.NationalNo);

                lblNationalNoValue.Text = TransformIfNull(_person.NationalNo);
                lblFullNameValue.Text = fullName;
                lblGenderValue.Text = _person.Gender ? "Male" : "Female";
                lblDateOfBirthValue.Text = _person.DateOfBirth.ToString("dd MMM yyyy");

                ApplyGender();

                ctrlContactDetails1.LoadContact(_person.Contact);
                ctrlAddressDetails1.LoadAddress(_person.Address);
                await ctrlDocumentDetails1.LoadDocument(_person.Document);
            }
            private void ApplyGender()
            {
                if (_person.Gender)
                {
                    lblGenderBadge.Text = "Male";
                    lblGenderBadge.BackColor = Color.FromArgb(219, 230, 241);
                    lblGenderBadge.ForeColor = Color.FromArgb(24, 33, 45);
                }
                else
                {
                    lblGenderBadge.Text = "Female";
                    lblGenderBadge.BackColor = Color.FromArgb(243, 244, 246);
                    lblGenderBadge.ForeColor = Color.FromArgb(107, 114, 128);
                }
            }
            private async Task LoadPersonImage()
            {
                try
                {
                    var bytes = await PeopleServices.GetPersonImage(_personId);

                    if (bytes == null || bytes.Length == 0)
                    {
                        ImageHelper.LoadDefaultImage(picPersonImage);
                        return;
                    }

                    using (System.IO.MemoryStream stream = new MemoryStream(bytes))
                    using (Image temp = Image.FromStream(stream))
                    {
                        Image old = picPersonImage.Image;
                        picPersonImage.Image = new Bitmap(temp);
                        old?.Dispose();
                    }
                }
                catch
                {
                    ImageHelper.LoadDefaultImage(picPersonImage);
                }
            }


            
            private string TransformIfNull(string value)
            {
                return string.IsNullOrWhiteSpace(value) ? "-" : value.Trim();
            }

            private void btnEdit_Click(object sender, EventArgs e)
            {
                using (var frm = new frmPersonEditor(_personId))
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                        _ = LoadPerson();
                }
            }

            private void btnClose_Click(object sender, EventArgs e)
            {
                Close();
            }

        private void picPersonImage_Click(object sender, EventArgs e)
        {
            if (this.picPersonImage.Image == null) return;

            frmImagePreviewer frm = new frmImagePreviewer(this.picPersonImage.Image);

            frm.ShowDialog();
        }

        private void lblUpdateImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            using (var frm = new frmPersonImageManager(_personId))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                    _ = LoadPerson();
            }

        }
    }
     
}

