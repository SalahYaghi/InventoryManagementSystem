using Contract.Requests.People;
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

namespace UI.Forms.People.Controlls
{
     
        public partial class ctrlPersonEditor : UserControl
        {
            public ctrlPersonEditor()
            {
                InitializeComponent();
                SetupUI();
            }

            private void SetupUI()
            {
                this.BackColor = Color.White;
                this.AutoScaleMode = AutoScaleMode.None;

                StyleTextBox(txtNationalNo);
                StyleTextBox(txtFirstName);
                StyleTextBox(txtSecondName);
                StyleTextBox(txtThirdName);
                StyleTextBox(txtLastName);

                cmbGender.DropDownStyle = ComboBoxStyle.DropDownList;
                cmbGender.Items.Clear();
                cmbGender.Items.Add("Male");
                cmbGender.Items.Add("Female");
                cmbGender.SelectedIndex = 0;

                dtpDateOfBirth.Format = DateTimePickerFormat.Short;
                dtpDateOfBirth.MaxDate = DateTime.Today;

                lblStatus.Text = "Person information";
            }

            private void StyleTextBox(TextBox textBox)
            {
                textBox.BackColor = Color.FromArgb(248, 250, 252);
                textBox.BorderStyle = BorderStyle.FixedSingle;
                textBox.Font = new Font("Segoe UI", 10F);
                textBox.ForeColor = Color.FromArgb(24, 33, 45);
            }

            public async Task LoadData()
            {
                await ctrlAddressInfo1.LoadData();
            }

            public async Task LoadPerson(PersonDto person)
            {
                if (person == null)
                {
                    Clear();
                    return;
                }

                txtNationalNo.Text = person.NationalNo;
                txtFirstName.Text = person.FirstName;
                txtSecondName.Text = person.SecondName;
                txtThirdName.Text = person.ThirdName ?? "";
                txtLastName.Text = person.LastName;

                cmbGender.SelectedItem = person.Gender ? "Male" : "Female";

            DateTimeOffset birthDate = person.DateOfBirth;
                if (birthDate < dtpDateOfBirth.MinDate)
                    birthDate = dtpDateOfBirth.MinDate;

                if (birthDate > dtpDateOfBirth.MaxDate)
                    birthDate = dtpDateOfBirth.MaxDate;

                dtpDateOfBirth.Value = birthDate.UtcDateTime;

                ctrlContactInfo1.LoadContact(person.Contact);

                if (person.Address != null)
                    await ctrlAddressInfo1.LoadAddress(person.Address);

           
                lblStatus.Text = "Person loaded";
            }

            public bool ValidateControl()
            {
                errorProvider.Clear();

                bool isValid = true;

                if (string.IsNullOrWhiteSpace(txtNationalNo.Text))
                {
                    errorProvider.SetError(txtNationalNo, "National number is required.");
                    isValid = false;
                }
                else if (txtNationalNo.Text.Trim().Length > 20)
                {
                    errorProvider.SetError(txtNationalNo, "National number must not exceed 20 characters.");
                    isValid = false;
                }

                if (string.IsNullOrWhiteSpace(txtFirstName.Text))
                {
                    errorProvider.SetError(txtFirstName, "First name is required.");
                    isValid = false;
                }
                else if (txtFirstName.Text.Trim().Length > 10)
                {
                    errorProvider.SetError(txtFirstName, "First name must not exceed 10 characters.");
                    isValid = false;
                }

                if (string.IsNullOrWhiteSpace(txtSecondName.Text))
                {
                    errorProvider.SetError(txtSecondName, "Second name is required.");
                    isValid = false;
                }
                else if (txtSecondName.Text.Trim().Length > 10)
                {
                    errorProvider.SetError(txtSecondName, "Second name must not exceed 10 characters.");
                    isValid = false;
                }

                if (!string.IsNullOrWhiteSpace(txtThirdName.Text) &&
                    txtThirdName.Text.Trim().Length > 10)
                {
                    errorProvider.SetError(txtThirdName, "Third name must not exceed 10 characters.");
                    isValid = false;
                }

                if (string.IsNullOrWhiteSpace(txtLastName.Text))
                {
                    errorProvider.SetError(txtLastName, "Last name is required.");
                    isValid = false;
                }
                else if (txtLastName.Text.Trim().Length > 10)
                {
                    errorProvider.SetError(txtLastName, "Last name must not exceed 10 characters.");
                    isValid = false;
                }

                if (cmbGender.SelectedItem == null)
                {
                    errorProvider.SetError(cmbGender, "Gender is required.");
                    isValid = false;
                }

                if (!ctrlContactInfo1.ValidateControl())
                    isValid = false;

                if (!ctrlAddressInfo1.ValidateControl())
                    isValid = false;

             
                lblStatus.Text = isValid ? "Person information is valid" : "Please fix person errors";

                return isValid;
            }

            public CreatePersonRequest GetCreateRequest()
            {
                return new CreatePersonRequest
                {
                    NationalNo = txtNationalNo.Text.Trim(),
                    FirstName = txtFirstName.Text.Trim(),
                    SecondName = txtSecondName.Text.Trim(),
                    ThirdName = EmptyToNull(txtThirdName.Text),
                    LastName = txtLastName.Text.Trim(),
                    Gender = cmbGender.SelectedItem.ToString() == "Male",
                    DateOfBirth = (dtpDateOfBirth.Value.Date),
                    Contact = ctrlContactInfo1.GetCreateRequest(),
                    Address = ctrlAddressInfo1.GetCreateRequest()
                };
            }

            public UpdatePersonRequest GetUpdateRequest()
            {
                return new UpdatePersonRequest
                {
                    NationalNo = txtNationalNo.Text.Trim(),
                    FirstName = txtFirstName.Text.Trim(),
                    SecondName = txtSecondName.Text.Trim(),
                    ThirdName = EmptyToNull(txtThirdName.Text),
                    LastName = txtLastName.Text.Trim(),
                    Gender = cmbGender.SelectedItem.ToString() == "Male",
                    DateOfBirth = (dtpDateOfBirth.Value.Date),
                    Contact = ctrlContactInfo1.GetUpdateRequest(),
                    Address = ctrlAddressInfo1.GetUpdateRequest()
                };
            }

        
            public void Clear()
            {
                txtNationalNo.Clear();
                txtFirstName.Clear();
                txtSecondName.Clear();
                txtThirdName.Clear();
                txtLastName.Clear();

                cmbGender.SelectedIndex = 0;
                dtpDateOfBirth.Value = DateTime.Today;

                ctrlContactInfo1.Clear();
                ctrlAddressInfo1.Clear();
                
                errorProvider.Clear();
                lblStatus.Text = "Person information";
            }

            private string EmptyToNull(string value)
            {
                return string.IsNullOrWhiteSpace(value) ? string.Empty : value.Trim();
            }
        }
    }
