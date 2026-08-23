using Contract.Responses;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI.Services;
using UI.Shared.Services;
using static HotelSystemUI.HttpClients.InventorySystemUI.HttpClients.Routes.Entities;

namespace UI.Forms.Employees
{
    public partial class frmShowEmployees : Form
    {
        private List<EmployeeDtoForList> _employees = new List<EmployeeDtoForList>();
        public frmShowEmployees()
        {
            InitializeComponent();
            SetupUI();
        }

        private async void frmShowEmployees_Load(object sender, EventArgs e)
        {
            await LoadEmployees();
        }

        private void SetupUI()
        {
            FormBorderStyle = FormBorderStyle.None;
            TopLevel = false;
            Dock = DockStyle.Fill;
            BackColor = Color.FromArgb(243, 246, 249);

            StyleButton(btnAdd, Color.FromArgb(74, 112, 139), Color.White);
            StyleButton(btnEdit, Color.FromArgb(243, 246, 249), Color.FromArgb(24, 33, 45));
            StyleButton(btnView, Color.FromArgb(243, 246, 249), Color.FromArgb(24, 33, 45));
            StyleButton(btnDelete, Color.FromArgb(220, 53, 69), Color.White);
            StyleButton(btnRefresh, Color.FromArgb(243, 246, 249), Color.FromArgb(24, 33, 45));

            StyleTextBox(txtSearch);
            cmbOrderBy.IndexChanged += ApplyCurrentView;
            cmbWarehouse.IndexChanged += ApplyCurrentView;

            lblStatus.Text = "Ready";
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

        private async Task LoadEmployees()
        {
            lblStatus.Text = "Loading employees...";

            var result = await EmployeeServices.GetAll();

            if (!result.IsSuccess)
            {
                lblStatus.Text = "Failed to load employees";
                MessageBox.Show(result.Title_Full, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _employees = result.Data ?? new List<EmployeeDtoForList>();
            
            dgvEmployees.SetData(_employees);

            cmbWarehouse.LoadData<EmployeeDtoForList>(_employees, e => e.WarehouseName);
            
            cmbOrderBy.LoadData(dgvEmployees.GetColumnNamesExcept(new HashSet<string>() {
                "PersonId" , "WarehouseId","EmployeeId"
            }));
          
            ApplyCurrentView();

            lblStatus.Text = $"{_employees.Count} employee(s) loaded";
        }


        private List<EmployeeDtoForList> ApplyLocalFilters() {


            string txt = txtSearch.Text.ToLower();    


            var query = string.IsNullOrEmpty(txt) ? _employees :  _employees.Where(e =>
            {

                return e.JobTitle.ToLower().Contains(txt) ||
                e.WarehouseName.ToLower().Contains(txt) ||
                e.FullName.ToLower().Contains(txt) ||
                e.City.ToLower().Contains(txt) ||
                e.Country.ToLower().Contains(txt);
                
            });

            string name = cmbOrderBy.GetSelectedItemName();
       
            switch (name)
            {
                case "FullName":
                    query = cmbOrderBy.SortData(query, d => d.FullName);
                    break;
                case "WarehouseName":
                    query = cmbOrderBy.SortData(query, d => d.WarehouseName);
                    break;
                case "Email":
                    query = cmbOrderBy.SortData(query, d => d.Email);
                    break;
                case "JobTitle":
                    query = cmbOrderBy.SortData(query, d => d.JobTitle);
                    break;

                case "PhoneNumber":
                    query = cmbOrderBy.SortData(query, d => d.PhoneNumber);
                    break;

                case "NationalNo":
                    query = cmbOrderBy.SortData(query, d => d.NationalNo);
                    break;

                case "HiringDate":
                    query = cmbOrderBy.SortData(query, d => d.HiringDate);
                    break;

                case "Country":
                    query = cmbOrderBy.SortData(query, d => d.Country);
                    break;
                case "City":
                    query = cmbOrderBy.SortData(query, d => d.City);
                    break;
                default:
                    query = cmbOrderBy.SortData(query, d => d.FullName);
                    break;
            }



            query = cmbWarehouse.FilterData<EmployeeDtoForList>(query , 
                p => p.WarehouseName == cmbWarehouse.GetSelectedItemName());

            return query.ToList();
        }

        private void ApplyCurrentView()
        {

        var employees = ApplyLocalFilters();

            dgvEmployees.SetData(employees);
            dgvEmployees.HideColumn("EmployeeId");
            dgvEmployees.HideColumn("PersonId");
            dgvEmployees.HideColumn("WarehouseId");


            lblStatus.Text = $"Showing {employees.Count} employee(s)";
        }

        private EmployeeDtoForList GetSelectedEmployee()
        {
            return dgvEmployees.GetSelectedItem<EmployeeDtoForList>();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            ApplyCurrentView();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (var frm = new frmEmployeeEditor())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                    _ = LoadEmployees();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedEmployee();

            if (selected == null)
            {
                MessageBox.Show("Please select an employee first.");
                return;
            }

            using (var frm = new frmEmployeeEditor(selected.EmployeeId))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                    _ = LoadEmployees();
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedEmployee();

            if (selected == null)
            {
                MessageBox.Show("Please select an employee first.");
                return;
            }

            using (var frm = new frmEmployeeDetails(selected.EmployeeId))
            {
                frm.ShowDialog();
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedEmployee();

            if (selected == null)
            {
                MessageBox.Show("Please select an employee first.");
                return;
            }

            var confirm = MessageBox.Show(
                $"Are you sure you want to delete employee {selected.FullName}?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes)
                return;

            btnDelete.Enabled = false;
            lblStatus.Text = "Deleting employee...";

            var result = await EmployeeServices.Delete(selected.EmployeeId);

            btnDelete.Enabled = true;

            if (!result.IsSuccess)
            {
                lblStatus.Text = "Failed to delete employee";
                MessageBox.Show(result.Title_Full, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            await LoadEmployees();
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            await LoadEmployees();
        }

       
    }
}

