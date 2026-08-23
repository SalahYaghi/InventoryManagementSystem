using Contract.Responses;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI.HttpClient;
using UI.Services;

namespace UI.Forms.People
{
    public partial class frmPersonSelector : Form
    {
        private List<PersonForListDto> _allPeople = new List<PersonForListDto>();
        private bool _isLoadingFilters = false;
        private bool firstCall = true;

        public PersonForListDto SelectedPerson { get; private set; }

        public frmPersonSelector()
        {
            InitializeComponent();
            SetupUI();
        }

        private async void frmPersonSelector_Load(object sender, EventArgs e)
        {

            dgvPeople.SubscribeToLoadData(LoadPeople);
            await dgvPeople.LoadDataGridViewData();
            firstCall = false;
        }

        private void SetupUI()
        {
            BackColor = Color.FromArgb(243, 246, 249);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            MaximizeBox = false;
            MinimizeBox = false;

            StyleButton(btnSelect, Color.FromArgb(74, 112, 139), Color.White);
            StyleButton(btnRefresh, Color.FromArgb(243, 246, 249), Color.FromArgb(24, 33, 45));
            StyleButton(btnClose, Color.FromArgb(243, 246, 249), Color.FromArgb(24, 33, 45));
            StyleButton(btnAdd, Color.FromArgb(74, 112, 139), (Color.White));

            StyleTextBox(txtSearch);
            StyleComboBox(cmbGender);
            StyleComboBox(cmbCountry);
            StyleComboBox(cmbCity);

            cmbGender.Items.AddRange(new object[] { "All", "Male", "Female" });
            cmbGender.SelectedIndex = 0;

            cmbCountry.Items.Add("All");
            cmbCountry.SelectedIndex = 0;

            cmbCity.Items.Add("All");
            cmbCity.SelectedIndex = 0;

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

        private void StyleComboBox(ComboBox comboBox)
        {
            comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox.BackColor = Color.FromArgb(248, 250, 252);
            comboBox.Font = new Font("Segoe UI", 10F);
            comboBox.ForeColor = Color.FromArgb(24, 33, 45);
        }
        private async Task<ApiResult<PaginatedList>> LoadPeople(int pageNo , int pageSize)
        {

            var result = await PeopleServices.GetAll(firstCall,pageNo, pageSize);

            if (!result.IsSuccess)
            {
                MessageBox.Show(result.Title_Full, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "Failed to load";
            }
            if (result.DataNotModified)
                return ApiResult<PaginatedList>.NotModified();

            var data = result.Data;
            _allPeople = data.Items ?? new List<PersonForListDto>();


            LoadFilterSources();
            ApplyCurrentView();

            return data; 
        }

        private void LoadFilterSources()
        {
            _isLoadingFilters = true;

            string selectedCountry = cmbCountry.SelectedItem == null ? "All" : cmbCountry.SelectedItem.ToString();
            string selectedCity = cmbCity.SelectedItem == null ? "All" : cmbCity.SelectedItem.ToString();

            var countries = _allPeople
                .Select(p => p.Country)
                .Where(c => !string.IsNullOrWhiteSpace(c))
                .Distinct()
                .OrderBy(c => c)
                .ToList();

            cmbCountry.Items.Clear();
            cmbCountry.Items.Add("All");

            foreach (var country in countries)
                cmbCountry.Items.Add(country);

            cmbCountry.SelectedItem = cmbCountry.Items.Contains(selectedCountry) ? selectedCountry : "All";

            var cities = _allPeople
                .Select(p => p.City)
                .Where(c => !string.IsNullOrWhiteSpace(c))
                .Distinct()
                .OrderBy(c => c)
                .ToList();

            cmbCity.Items.Clear();
            cmbCity.Items.Add("All");

            foreach (var city in cities)
                cmbCity.Items.Add(city);

            cmbCity.SelectedItem = cmbCity.Items.Contains(selectedCity) ? selectedCity : "All";

            _isLoadingFilters = false;
        }

        private List<PersonForListDto> ApplyLocalFilters()
        {
            IEnumerable<PersonForListDto> query = _allPeople;

            string search = txtSearch.Text.Trim().ToLower();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(p =>
                    (p.NationalNo ?? "").ToLower().Contains(search) ||
                    (p.FullName ?? "").ToLower().Contains(search) ||
                    (p.Gender ?? "").ToLower().Contains(search) ||
                    (p.PhoneNumber ?? "").ToLower().Contains(search) ||
                    (p.Email ?? "").ToLower().Contains(search) ||
                    (p.Country ?? "").ToLower().Contains(search) ||
                    (p.City ?? "").ToLower().Contains(search));
            }

            if (cmbGender.SelectedItem != null && cmbGender.SelectedItem.ToString() != "All")
            {
                string gender = cmbGender.SelectedItem.ToString();
                query = query.Where(p => p.Gender == gender);
            }

            if (cmbCountry.SelectedItem != null && cmbCountry.SelectedItem.ToString() != "All")
            {
                string country = cmbCountry.SelectedItem.ToString();
                query = query.Where(p => p.Country == country);
            }

            if (cmbCity.SelectedItem != null && cmbCity.SelectedItem.ToString() != "All")
            {
                string city = cmbCity.SelectedItem.ToString();
                query = query.Where(p => p.City == city);
            }

            return query.OrderBy(p => p.FullName).ToList();
        }

        private void ApplyCurrentView()
        {
            var people = ApplyLocalFilters();

            dgvPeople.DgvCustom.SetData(people);

             dgvPeople.DgvCustom.HideColumn("Id");
            dgvPeople.DgvCustom.HideColumn("DocumentId");

            dgvPeople.DgvCustom.SetColumnHeader("NationalNo", "National No");
            dgvPeople.DgvCustom.SetColumnHeader("FullName", "Full Name");
            dgvPeople.DgvCustom.SetColumnHeader("Gender", "Gender");
            dgvPeople.DgvCustom.SetColumnHeader("DateOfBirth", "Birth Date");
            dgvPeople.DgvCustom.SetColumnHeader("PhoneNumber", "Phone");
            dgvPeople.DgvCustom.SetColumnHeader("Email", "Email");
            dgvPeople.DgvCustom.SetColumnHeader("Country", "Country");
            dgvPeople.DgvCustom.SetColumnHeader("City", "City");

        }


        private PersonForListDto GetSelectedPerson()
        {
            return dgvPeople.DgvCustom.GetSelectedItem<PersonForListDto>();
        }

        private void SelectCurrentPerson()
        {
            var selected = GetSelectedPerson();

            if (selected == null)
            {
                MessageBox.Show("Please select a person first.");
                return;
            }

            SelectedPerson = selected;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            ApplyCurrentView();
        }

        private void cmbGender_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isLoadingFilters) return;
            ApplyCurrentView();
        }

        private void cmbCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isLoadingFilters) return;
            ApplyCurrentView();
        }

        private void cmbCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isLoadingFilters) return;
            ApplyCurrentView();
        }

        private void DgvCustom_DoubleClick(object sender, EventArgs e)
        {
            SelectCurrentPerson();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            SelectCurrentPerson();
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            await dgvPeople.LoadDataGridViewData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            if (_isLoadingFilters) return;

            using (frmPersonEditor frmPersonEditor = new frmPersonEditor()) {
                frmPersonEditor.ShowDialog();
                if (frmPersonEditor.DialogResult == DialogResult.OK)
                    await dgvPeople.LoadDataGridViewData();
                ;
            }
        }
    }
}

