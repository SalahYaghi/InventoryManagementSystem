using Contract.Requests.Addresses;
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
using UI.Forms.Refrences.Countries;

namespace UI.Forms.Refrences.Contacts
{

        public partial class ctrlAddressInfo : UserControl
        {
            public ctrlAddressInfo()
            {
                InitializeComponent(); this.Font = new Font("Segoe UI", 9F);
            this.AutoScaleMode = AutoScaleMode.None;
 
            SetupUI(); this.Size = new System.Drawing.Size(691, 82);

        }

        private void SetupUI()
            {
                this.BackColor = Color.White;

                StyleTextBox(txtStreet);
                StyleTextBox(txtBuildingNumber);
                StyleTextBox(txtPostalCode);
                StyleTextBox(txtDescription);

                lblStatus.Text = "Address information";
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
                lblStatus.Text = "Loading countries and cities...";
                 await ctrCountryCitySelector1.LoadData();
        
                lblStatus.Text = "Address information";
            }

            public async Task LoadAddress(AddressDto address)
            {
                if (address == null)
                {
                    Clear();
                    return;
                }

                await LoadData();

                 ctrCountryCitySelector1.SelectCountry(address.CountryId);

                             
                  ctrCountryCitySelector1.SelectCity(address.CityId);

                txtStreet.Text = address.Street ?? "";
                txtBuildingNumber.Text = address.BuildingNumber ?? "";
                txtPostalCode.Text = address.PostalCode ?? "";
                txtDescription.Text = address.Description ?? "";

                errorProvider.Clear();
                lblStatus.Text = "Address loaded";
            }

            public CreateAddressRequest GetCreateRequest()
            {
                return new CreateAddressRequest
                {
                    CountryId = ctrCountryCitySelector1.SelectedCountryId.Value,
                     CityId = ctrCountryCitySelector1.SelectedCityId.Value,
                    Street = EmptyToNull(txtStreet.Text),
                    BuildingNumber = EmptyToNull(txtBuildingNumber.Text),
                    PostalCode = EmptyToNull(txtPostalCode.Text),
                    Description = EmptyToNull(txtDescription.Text)
                };
            }

            public UpdateAddressRequest GetUpdateRequest()
            {
             
            return new UpdateAddressRequest
                {
                     CountryId = ctrCountryCitySelector1.SelectedCountryId.Value,
                     CityId = ctrCountryCitySelector1.SelectedCityId.Value,
                    Street = EmptyToNull(txtStreet.Text),
                    BuildingNumber = EmptyToNull(txtBuildingNumber.Text),
                    PostalCode = EmptyToNull(txtPostalCode.Text),
                    Description = EmptyToNull(txtDescription.Text)
                };
            }

            public bool ValidateControl()
            {
                errorProvider.Clear();

                bool isValid = true;

            if (ctrCountryCitySelector1.SelectedCountryId == null)
            {
                errorProvider.SetError(ctrCountryCitySelector1, "Country is required.");
                isValid = false;
            }

            if (ctrCountryCitySelector1.SelectedCityId == null)
            {
                errorProvider.SetError(ctrCountryCitySelector1, "City is required.");
                isValid = false;
            }

            if (!string.IsNullOrWhiteSpace(txtStreet.Text) &&
                    txtStreet.Text.Trim().Length > 20)
                {
                    errorProvider.SetError(txtStreet, "Postal code must not exceed 20 characters.");
                    isValid = false;
                }

                if (!string.IsNullOrWhiteSpace(txtBuildingNumber.Text) &&
                    txtBuildingNumber.Text.Trim().Length > 20)
                {
                    errorProvider.SetError(txtBuildingNumber, "Building number must not exceed 20 characters.");
                    isValid = false;
                }

                if (!string.IsNullOrWhiteSpace(txtPostalCode.Text) &&
                    txtPostalCode.Text.Trim().Length > 100)
                {
                    errorProvider.SetError(txtPostalCode, "Street must not exceed 100 characters.");
                    isValid = false;
                }

                if (!string.IsNullOrWhiteSpace(txtDescription.Text) &&
                    txtDescription.Text.Trim().Length > 500)
                {
                    errorProvider.SetError(txtDescription, "Description must not exceed 500 characters.");
                    isValid = false;
                }

                lblStatus.Text = isValid ? "Address information is valid" : "Please fix address errors";
                return isValid;
            }

            public void Clear()
            {
                txtStreet.Clear();
                txtBuildingNumber.Clear();
                txtPostalCode.Clear();
                txtDescription.Clear();

                errorProvider.Clear();
                lblStatus.Text = "Address information";
            }

            private string EmptyToNull(string value)
            {
                return string.IsNullOrWhiteSpace(value) ? string.Empty : value.Trim();
            }

        private void txtDescription_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupAddress_Enter(object sender, EventArgs e)
        {
 
            txtStreet.Focus();
        }

        private void txtBuildingNumber_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtStreet_TextChanged(object sender, EventArgs e)
        {

        }

        private void ctrCountryCitySelector1_Load(object sender, EventArgs e)
        {

        }
    }
    } 
