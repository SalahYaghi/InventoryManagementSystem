using ContracOldCompatibile.Requests.Employees;
using Contract.Responses;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI.Forms.People;
using UI.Services;
using UI.Shared.Services;

namespace UI.Forms.Employees
{
    public partial class frmEmployeeEditor : Form
    {
        private readonly bool _isUpdateMode;
        private readonly Guid _employeeId;

        private EmployeeDto _employee;
        private Guid? _selectedPersonId;

        private Guid?  _selectedWarehouseId;
        public void DefineSelectedWarehouse(Guid? warehouseId) {

            this._selectedWarehouseId = warehouseId;
            if (warehouseId.HasValue)
                    cmbWarehouse.SelectedValue = warehouseId.Value;
        }


        public frmEmployeeEditor()
        {
            InitializeComponent();
            _isUpdateMode = false;
            SetupUI();
        }

        public frmEmployeeEditor(Guid personId)
        {
            InitializeComponent();
            _employeeId = personId;
            _isUpdateMode = true;
            SetupUI();
        }

        private async void frmEmployeeEditor_Load(object sender, EventArgs e)
        {
            await LoadWarehouses();

            if (_isUpdateMode)
                await LoadEmployee();

            if (_selectedWarehouseId.HasValue)
                DefineSelectedWarehouse(_selectedWarehouseId);
        }

        private void SetupUI()
        {

            this.btnShowPersonalData.Visible = false;
            this.btnShowPersonalData.Enabled = false;
            BackColor = Color.FromArgb(243, 246, 249);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            MaximizeBox = false;
            MinimizeBox = false;

            lblTitle.Text = _isUpdateMode ? "Edit Employee" : "Add Employee";
            lblSubtitle.Text = _isUpdateMode
                ? "Update employee job title, warehouse and hiring date."
                : "Create an employee record by selecting a person and warehouse.";

            StyleButton(btnShowPersonalData, Color.FromArgb(74, 112, 139), Color.White);

            StyleButton(btnSave, Color.FromArgb(74, 112, 139), Color.White);
            StyleButton(btnCancel, Color.FromArgb(243, 246, 249), Color.FromArgb(24, 33, 45));
            StyleSelectionButton(btnSelectPerson);

            StyleTextBox(txtPerson);
            StyleTextBox(txtJobTitle);

            txtPerson.ReadOnly = true;
            cmbWarehouse.DropDownStyle = ComboBoxStyle.DropDownList;

            dtpHiringDate.Format = DateTimePickerFormat.Short;
            dtpHiringDate.MaxDate = DateTime.Today;

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

        private void StyleSelectionButton(Button button)
        {
            button.Text = "...";
            button.BackColor = Color.FromArgb(248, 250, 252);
            button.ForeColor = Color.FromArgb(80, 95, 110);
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderColor = Color.FromArgb(220, 226, 232);
            button.FlatAppearance.BorderSize = 1;
            button.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            button.Cursor = Cursors.Hand;
        }

        private void StyleTextBox(TextBox textBox)
        {
            textBox.BackColor = Color.FromArgb(248, 250, 252);
            textBox.BorderStyle = BorderStyle.FixedSingle;
            textBox.Font = new Font("Segoe UI", 10F);
            textBox.ForeColor = Color.FromArgb(24, 33, 45);
        }

        private async Task LoadWarehouses()
        {
            lblStatus.Text = "Loading warehouses...";

            var result = await WarehousesServices.GetAll();

            if (!result.IsSuccess)
            {
                lblStatus.Text = "Failed to load warehouses";
                MessageBox.Show(result.Title_Full, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            cmbWarehouse.DataSource = result.Data ?? new List<WarehouseForListDto>();
            cmbWarehouse.DisplayMember = "Name";
            cmbWarehouse.ValueMember = "Id";

            if (cmbWarehouse.Items.Count > 0)
                cmbWarehouse.SelectedIndex = 0;

            lblStatus.Text = "Ready";
        }

        private async Task LoadEmployee()
        {
            lblStatus.Text = "Loading employee...";

            var result = await EmployeeServices.Get(_employeeId);

            if (!result.IsSuccess)
            {
                lblStatus.Text = "Failed to load employee";
                MessageBox.Show(result.Title_Full, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.btnShowPersonalData.Visible = true;
            this.btnShowPersonalData.Enabled = true;
            _employee = result.Data;
            BindEmployee();

            lblStatus.Text = "Ready";
        }

        private void BindEmployee()
        {
            if (_employee == null)
                return;

            _selectedPersonId = _employee.PersonId;
            txtPerson.Text = _employee.Person == null
                ? _employee.PersonId.ToString()
                : BuildPersonName(_employee.Person);

            txtJobTitle.Text = _employee.JobTitle;

            DateTimeOffset hiringDate = _employee.HiringDate;

            if (hiringDate < dtpHiringDate.MinDate)
                hiringDate = dtpHiringDate.MinDate;

            if (hiringDate > dtpHiringDate.MaxDate)
                hiringDate = dtpHiringDate.MaxDate;

            dtpHiringDate.Value = hiringDate.UtcDateTime;

            if (_employee.WarehouseId.HasValue)
                cmbWarehouse.SelectedValue = _employee.WarehouseId.Value;

            btnSelectPerson.Enabled = false;
        }

        private string BuildPersonName(PersonDto person)
        {
            return string.Join(" ", new[]
            {
                person.FirstName,
                person.SecondName,
                person.ThirdName,
                person.LastName
            }.Where(x => !string.IsNullOrWhiteSpace(x)));
        }

        private bool ValidateForm()
        {
            errorProvider.Clear();

            bool isValid = true;

            if (!_selectedPersonId.HasValue || _selectedPersonId.Value == Guid.Empty)
            {
                errorProvider.SetError(txtPerson, "Person is required.");
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(txtJobTitle.Text))
            {
                errorProvider.SetError(txtJobTitle, "Job title is required.");
                isValid = false;
            }

            if (cmbWarehouse.SelectedValue == null)
            {
                errorProvider.SetError(cmbWarehouse, "Warehouse is required.");
                isValid = false;
            }

            return isValid;
        }

        private CreateEmployeeWithPersonIdRequest BuildCreateRequest()
        {
            return new CreateEmployeeWithPersonIdRequest
            {
                jobTitle = txtJobTitle.Text.Trim(),
                personId = _selectedPersonId.Value,
                hiringDate = (dtpHiringDate.Value.Date),
                warehouseId = (Guid)cmbWarehouse.SelectedValue,
                
            };
        }

        private UpdateEmployeeRequest BuildUpdateRequest()
        {
            return new UpdateEmployeeRequest
            {
                jobTitle = txtJobTitle.Text.Trim(),
                employeeId = _selectedPersonId.Value,
                hiringDate = (dtpHiringDate.Value.Date),
                warehouseId = (Guid)cmbWarehouse.SelectedValue,

            };
        }

        private void btnSelectPerson_Click(object sender, EventArgs e)
        {
            using (var frm = new frmPersonSelector())
            {
                if (frm.ShowDialog() != DialogResult.OK)
                    return;

                _selectedPersonId = frm.SelectedPerson.Id;
                txtPerson.Text = frm.SelectedPerson.FullName;
            }
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateForm())
                return;

            btnSave.Enabled = false;
            lblStatus.Text = "Saving employee...";

            if (_isUpdateMode)
            {
                var result = await EmployeeServices.Update(_employeeId, BuildUpdateRequest());

                if (!result.IsSuccess)
                {
                    btnSave.Enabled = true;
                    lblStatus.Text = "Failed to save employee";
                    MessageBox.Show(result.Title_Full, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                var result = await EmployeeServices.Create(BuildCreateRequest());

                if (!result.IsSuccess)
                {
                    btnSave.Enabled = true;
                    lblStatus.Text = "Failed to save employee";
                    MessageBox.Show(result.Title_Full, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            lblStatus.Text = "Saved successfully";
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnShowPersonalData_Click(object sender, EventArgs e)
        {
            using (var frm = new frmPersonDetails(_employee.PersonId))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                    _ = LoadEmployee();
            }
        }
    }
}

