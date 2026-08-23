using Contract.Responses;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI.Services;
using UI.Shared.Services;

namespace UI.Forms.Employees
{
    public partial class frmEmployeeSelector : Form
    {
       
        private List<EmployeeDtoForList> _employees = new List<EmployeeDtoForList>();

        public EmployeeDtoForList SelectedEmployee { get; private set; }
        public Guid SelectedEmployeeId { get; private set; }

        public frmEmployeeSelector()
        {
            InitializeComponent();
            SetupUI();
        }

        private async void frmEmployeeSelector_Load(object sender, EventArgs e)
        {
            await LoadEmployees();
        }
        
        private void SetupUI()
        {
            BackColor = Color.FromArgb(243, 246, 249);
            this.dgvEmployees.dgv.DoubleClick += dgvEmployees_DoubleClick;
            StyleTextBox(txtSearch);

            StyleButton(btnSelect, Color.FromArgb(74, 112, 139), Color.White);
            StyleButton(btnRefresh, Color.FromArgb(243, 246, 249), Color.FromArgb(24, 33, 45));
            StyleButton(btnClose, Color.FromArgb(243, 246, 249), Color.FromArgb(24, 33, 45));
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

            ApplyCurrentView();

            lblStatus.Text = $"{_employees.Count} employee(s) loaded";
        }


        private void ApplyCurrentView()
        {
            string search = txtSearch.Text.Trim().ToLower();

            var query = _employees.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(e =>
                    (e.FullName ?? "").ToLower().Contains(search) ||
                    (e.NationalNo ?? "").ToLower().Contains(search) ||
                    (e.JobTitle ?? "").ToLower().Contains(search) ||
                    (e.WarehouseName ?? "").ToLower().Contains(search));
            }

            var data = query.OrderBy(e => e.FullName).ToList();

            dgvEmployees.SetData(data);

            dgvEmployees.HideColumn("EmployeeId");
            dgvEmployees.HideColumn("PersonId");
            dgvEmployees.HideColumn("WarehouseId");

            lblStatus.Text = $"Showing {data.Count} employee(s)";
        }

        private void SelectCurrentEmployee()
        {
            var selected = dgvEmployees.GetSelectedItem<EmployeeDtoForList>();

            if (selected == null)
            {
                MessageBox.Show("Please select an employee first.");
                return;
            }

            SelectedEmployeeId = selected.EmployeeId;
            SelectedEmployee = _employees.FirstOrDefault(e => e.PersonId == selected.PersonId);

            DialogResult = DialogResult.OK;
            Close();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            ApplyCurrentView();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            SelectCurrentEmployee();
        }

        private void dgvEmployees_DoubleClick(object sender, EventArgs e)
        {
            SelectCurrentEmployee();
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            await LoadEmployees();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

    }
}

