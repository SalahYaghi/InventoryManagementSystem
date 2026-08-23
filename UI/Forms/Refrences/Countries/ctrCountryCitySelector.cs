using Contract.Responses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI.Services;

namespace UI.Forms.Refrences.Countries
{
    public partial class ctrCountryCitySelector : UserControl
    {
        private Guid? _selectedCityId; 
        public ctrCountryCitySelector()
        {
            InitializeComponent();
            SetupUI();
        }
        public Guid? SelectedCountryId => GetSelectedCountry()?.Id;
        public Guid? SelectedCityId { get => GetSelectedCity()?.Id;
      
        }

        public void SelectCountry(Guid countryId)
        {
            cmbCountry.SelectedValue = countryId;
        }

        public void SelectCity(Guid cityId)
        {
             cmbCity.SelectedValue = cityId;
            this._selectedCityId = cityId;
         


        }
        private void SetupUI() {
        
            cmbCity.DropDownStyle = ComboBoxStyle.DropDownList;

            cmbCountry.DropDownStyle = ComboBoxStyle.DropDownList;
        }


        public async Task LoadData() {

            var countriesResult = await CountriesServices.GetAll();

            if (!countriesResult.IsSuccess) {
                MessageBox.Show("Can't load countries" , "Error" , MessageBoxButtons.OK , MessageBoxIcon.Error); 
                return;
            }
            cmbCountry.DataSource = countriesResult.Data;
            cmbCountry.ValueMember = "Id";
            cmbCountry.DisplayMember = "Name";

            if (cmbCountry.Items.Count > 0)
            cmbCountry.SelectedIndex = 0;
        }
        public CountryDto GetSelectedCountry() {

            return cmbCountry.SelectedItem as CountryDto;   
        }
        public async Task LoadCitites() {

            var country = GetSelectedCountry();

            if (country == null) return;
         
            cmbCity.DataSource = null;
            cmbCity.Items.Clear();

            var citites = await CountriesServices.GetCities(country.Id);
           
            if (!citites.IsSuccess)
            {
                MessageBox.Show("Can't load cities", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            cmbCity.DataSource = citites.Data;
            cmbCity.ValueMember = "Id";
            cmbCity.DisplayMember = "Name";

            if(cmbCity.Items.Count > 0 )
            cmbCity.SelectedIndex = 0;

            if(_selectedCityId != null) 
                cmbCity.SelectedValue = _selectedCityId;
        }

        public CityDto GetSelectedCity()
        {

            return cmbCity.SelectedItem as CityDto;
        }



        private async void cmbCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
             await LoadCitites();


        }

        private   void cmbCity_SelectedIndexChanged(object sender, EventArgs e)
        {
         }
    }
}

