using Contract.Responses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI.HttpClient;
using UI.Services;

namespace UI.Forms.People
{
    
        public partial class frmShowPeople : Form
        {
            private List<PersonForListDto> _allPeople = new List<PersonForListDto>();
 
        private bool firstCall = true;
        private bool _filtersInitialised;
        private bool _suppressFilterEvents;
            public frmShowPeople()
            {
                InitializeComponent();
                SetupUI();
            }

            private async void frmShowPeople_Load(object sender, EventArgs e)
            {
                   dgvPeople.SubscribeToLoadData(LoadPeople);
            await dgvPeople.LoadDataGridViewData();
            firstCall = false;
            }

            private void SetupUI()
            {
                this.FormBorderStyle = FormBorderStyle.None;
                this.TopLevel = false;
                this.Dock = DockStyle.Fill;
                this.BackColor = Color.FromArgb(243, 246, 249);
            this.cmbOrderBy.Size = new System.Drawing.Size(270, 55);

            cmbOrderBy.Title = "Order By";
            cmbCity.Title = "City";
            cmbCountry.Title = "Country";
            cmbGender.Title = "Gender";


            StyleActionButton(btnAdd, Color.FromArgb(74, 112, 139), Color.White);
                StyleActionButton(btnEdit, Color.FromArgb(243, 246, 249), Color.FromArgb(24, 33, 45));
                StyleActionButton(btnView, Color.FromArgb(243, 246, 249), Color.FromArgb(24, 33, 45));
                StyleActionButton(btnImage, Color.FromArgb(243, 246, 249), Color.FromArgb(24, 33, 45));
                StyleActionButton(btnDocument, Color.FromArgb(243, 246, 249), Color.FromArgb(24, 33, 45)); 

                 StyleActionButton(btnRefresh, Color.FromArgb(243, 246, 249), Color.FromArgb(24, 33, 45));
            StyleActionButton(btnDelete, Color.FromArgb(220, 53, 69), Color.White);

            StyleTextBox(txtSearch);
 
                
             }
            private void StyleActionButton(Button button, Color backColor, Color foreColor)
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

 
            private async Task<ApiResult<PaginatedList>> LoadPeople(int pageNo , int pageSize)
            {

                var result = await PeopleServices.GetAll(firstCall,pageNo , pageSize);

                if (!result.IsSuccess)
                {
                    MessageBox.Show(result.Title_Full, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return "Failed to load data";
                }

            if (result.DataNotModified)
                return ApiResult<PaginatedList>.NotModified();

            var data = result.Data;

            if (data == null)
                return "Failed to load data";

            _allPeople = data.Items ?? new List<PersonForListDto>();
            this.dgvPeople.SetData(_allPeople);
          
            LoadFilterSources();
            ApplyCurrentView();

            return data;
            }

    
        private void LoadFilterSources()
        {
            _suppressFilterEvents = true;

            try
            {
                cmbCity.LoadData<PersonForListDto>(_allPeople, p => p.City);
                cmbGender.LoadData<PersonForListDto>(_allPeople, p => p.Gender);
                cmbCountry.LoadData<PersonForListDto>(_allPeople, p => p.Country);

                cmbOrderBy.LoadData(dgvPeople.DgvCustom.GetColumnNamesExcept(new HashSet<string>
                {
                    "Id", "DocumentId"
                }));

                if (_filtersInitialised)
                    return;

                cmbCity.IndexChanged += ApplyCurrentView;
                cmbGender.IndexChanged += ApplyCurrentView;
                cmbCountry.IndexChanged += ApplyCurrentView;
                cmbOrderBy.IndexChanged += ApplyCurrentView;

                _filtersInitialised = true;
            }
            finally
            {
                _suppressFilterEvents = false;
            }
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
                        (p.City ?? "").ToLower().Contains(search)
                    );
                }


 
                
                switch (cmbOrderBy.GetSelectedItemName())
                { case "Email":
                    query = cmbOrderBy.SortData(query, c => c.Email); 
                        break;
              case "PhoneNumber":
                    query = cmbOrderBy.SortData(query, c => c.PhoneNumber); 
                        break;
                    case "FullName":
                    query = cmbOrderBy.SortData(query, c => c.FullName); 
                        break;

                    case "NationalNo":
                    query = cmbOrderBy.SortData(query, c => c.NationalNo); 
                        break;

                    case "Gender":
                    query = cmbOrderBy.SortData(query, c => c.Gender);

                    break;

                    case "DateOfBirth":
                    query = cmbOrderBy.SortData(query, c => c.DateOfBirth);
                    break;

                    case "Country":
                    query = cmbOrderBy.SortData(query, c => c.Country);
                    break;

                    case "City":
                    query = cmbOrderBy.SortData(query, c => c.City);
                    break;

                    default:
                    query = cmbOrderBy.SortData(query, c => c.NationalNo);
                    break;
                }

             query = cmbGender.FilterData<PersonForListDto>(query,
             p => p.Gender == cmbGender.GetSelectedItemName());
            query = cmbCity.FilterData<PersonForListDto>(query,
                 p => p.City == cmbCity.GetSelectedItemName());
            query = cmbCountry.FilterData<PersonForListDto>(query,
                 p => p.Country == cmbCountry.GetSelectedItemName());

            return query.ToList();
          }

            private void ApplyCurrentView()
            {
            if (_suppressFilterEvents)
                return;

                var people = ApplyLocalFilters();

                dgvPeople.SetData(people);

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
            dgvPeople.DgvCustom.FormatColumnAsDate("DateOfBirth");

            }

            private PersonForListDto GetSelectedPerson()
            {
                return dgvPeople.DgvCustom.GetSelectedItem<PersonForListDto>();
            }

            private void txtSearch_TextChanged(object sender, EventArgs e)
            {
                ApplyCurrentView();
            }

            private void cmbGender_SelectedIndexChanged(object sender, EventArgs e)
            {
                 ApplyCurrentView();
            }

            private void cmbCountry_SelectedIndexChanged(object sender, EventArgs e)
            {
                ApplyCurrentView();
            }

            private void cmbCity_SelectedIndexChanged(object sender, EventArgs e)
            {
                ApplyCurrentView();
            }

            private void cmbOrderBy_SelectedIndexChanged(object sender, EventArgs e)
            {
                ApplyCurrentView();
            }

            private void cmbSortDirection_SelectedIndexChanged(object sender, EventArgs e)
            {
                ApplyCurrentView();
            }

            private async void btnAdd_Click(object sender, EventArgs e)
            {
                
             using (var frm = new frmPersonEditor())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                    await dgvPeople.LoadDataGridViewData();
            }
        }

            private async void btnEdit_Click(object sender, EventArgs e)
            {
                var selected = GetSelectedPerson();

                if (selected == null)
                {
                    MessageBox.Show("Please select a person first.");
                    return;
                }
 
            using (var frm = new frmPersonEditor(selected.Id))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                   await  dgvPeople.LoadDataGridViewData();
            }
        }

            private void btnView_Click(object sender, EventArgs e)
            {
                var selected = GetSelectedPerson();

                if (selected == null)
                {
                    MessageBox.Show("Please select a person first.");
                    return;
                }

 
             using (var frm = new frmPersonDetails(selected.Id))
            {
                frm.ShowDialog();
            }
        }

        private void btnImage_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedPerson();

            if (selected == null)
            {
                MessageBox.Show("Please select a person first.");
                return;
            }

            using (var frm = new frmPersonImageManager(selected.Id))
            {
                frm.ShowDialog();
            }
        }
            private async void btnDocument_Click(object sender, EventArgs e)
            {
                var selected = GetSelectedPerson();

                if (selected == null)
                {
                    MessageBox.Show("Please select a person first.");
                    return;
                }

            using (var frm = new frmMakePersonDocument(selected.Id ,selected.DocumentId))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {   
                    await dgvPeople.LoadDataGridViewData();
                }
            }


        }

            private async void btnDelete_Click(object sender, EventArgs e)
            {
                var selected = GetSelectedPerson();

                if (selected == null)
                {
                    MessageBox.Show("Please select a person first.");
                    return;
                }

                var confirm = MessageBox.Show(
                    $"Are you sure you want to delete {selected.FullName}?",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirm != DialogResult.Yes)
                    return;

                btnDelete.Enabled = false;
 
                var result = await PeopleServices.Delete(selected.Id);

                btnDelete.Enabled = true;

                if (!result.IsSuccess)
                {
                     MessageBox.Show(result.Title_Full, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            await dgvPeople.LoadDataGridViewData();

        }

        private async void btnRefresh_Click(object sender, EventArgs e)
            {
            await dgvPeople.LoadDataGridViewData();
            }

        private void ctrlSortByCmb1_Load(object sender, EventArgs e)
        {

        }
    }
    }

