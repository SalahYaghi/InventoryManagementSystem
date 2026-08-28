using Contract.Responses;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace UI.Forms.References.Contacts
{
    public partial class ctrlAddressDetails : UserControl
    {
        public ctrlAddressDetails()
        {
            InitializeComponent();
            SetupUI();
        }

        private void SetupUI()
        {
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.White;

            StyleValue(lblCountryValue);
            StyleValue(lblCityValue);
            StyleValue(lblPostalCodeValue);
            StyleValue(lblBuildingNumberValue);
            StyleValue(lblStreetValue);
            StyleValue(lblDescriptionValue);
        }

        private void StyleValue(Label label)
        {
            label.BackColor = Color.FromArgb(248, 250, 252);
            label.ForeColor = Color.FromArgb(24, 33, 45);
            label.Font = new Font("Segoe UI", 10F);
            label.Padding = new Padding(8, 0, 0, 0);
        }

        public void LoadAddress(AddressDto address)
        {
            if (address == null)
            {
                Clear();
                return;
            }

            lblCountryValue.Text = Safe(address.Country != null ? address.Country.Name : "");
            lblCityValue.Text = Safe(address.City != null ? address.City.Name : "");
            lblPostalCodeValue.Text = Safe(address.PostalCode);
            lblBuildingNumberValue.Text = Safe(address.BuildingNumber);
            lblStreetValue.Text = Safe(address.Street);
            lblDescriptionValue.Text = Safe(address.Description);
        }

        public void Clear()
        {
            lblCountryValue.Text = "-";
            lblCityValue.Text = "-";
            lblPostalCodeValue.Text = "-";
            lblBuildingNumberValue.Text = "-";
            lblStreetValue.Text = "-";
            lblDescriptionValue.Text = "-";
        }

        private string Safe(string value)
        {
            return string.IsNullOrWhiteSpace(value) ? "-" : value.Trim();
        }
    }
}

