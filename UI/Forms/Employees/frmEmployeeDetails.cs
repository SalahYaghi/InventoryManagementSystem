using Contract.Responses;
using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI.Forms.People;
using UI.Services;
using UI.Shared.Services;

namespace UI.Forms.Employees
{
    public partial class frmEmployeeDetails : Form
    {
        private readonly Guid _employeeId;
        private EmployeeDto _employee;

        public frmEmployeeDetails(Guid personId)
        {
            InitializeComponent();
            _employeeId = personId;
            SetupUI();
        }

        private async void frmEmployeeDetails_Load(object sender, EventArgs e)
        {
            await LoadEmployee();
        }

        private void SetupUI()
        {
            BackColor = Color.FromArgb(243, 246, 249);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            MaximizeBox = false;
            MinimizeBox = false;
            StyleButton(btnShowPersonalData, Color.FromArgb(74, 112, 139), Color.White);

            StyleButton(btnEdit, Color.FromArgb(74, 112, 139), Color.White);
            StyleButton(btnClose, Color.FromArgb(243, 246, 249), Color.FromArgb(24, 33, 45));

            StyleValueLabel(lblPersonValue);
            StyleValueLabel(lblNationalNoValue);
            StyleValueLabel(lblJobTitleValue);
            StyleValueLabel(lblHiringDateValue);
            StyleValueLabel(lblWarehouseValue);
            StyleValueLabel(lblWarehouseCodeValue);

            lblStatus.Text = "Loading...";
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

        private async Task LoadEmployee()
        {
            lblStatus.Text = "Loading employee details...";

            var result = await EmployeeServices.Get(_employeeId);

            if (!result.IsSuccess)
            {
                lblStatus.Text = "Failed to load employee";
                MessageBox.Show(result.Title_Full, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _employee = result.Data;
            BindEmployee();

            lblStatus.Text = "Ready";
        }

        private void BindEmployee()
        {
            if (_employee == null)
                return;

            string fullName = _employee.Person == null
                ? _employee.PersonId.ToString()
                : BuildPersonName(_employee.Person);

            lblEmployeeName.Text = fullName;
            lblEmployeeSubTitle.Text = "Job Title: " + Safe(_employee.JobTitle);

            lblPersonValue.Text = fullName;
            lblNationalNoValue.Text = _employee.Person == null ? "-" : Safe(_employee.Person.NationalNo);
            lblJobTitleValue.Text = Safe(_employee.JobTitle);
            lblHiringDateValue.Text = _employee.HiringDate.ToString("dd MMM yyyy");
            lblWarehouseValue.Text = _employee.Warehouse == null ? "-" : Safe(_employee.Warehouse.Name);
            lblWarehouseCodeValue.Text = _employee.Warehouse == null ? "-" : Safe(_employee.Warehouse.Code);
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

        private string Safe(string value)
        {
            return string.IsNullOrWhiteSpace(value) ? "-" : value.Trim();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            using (var frm = new frmEmployeeEditor(_employeeId))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                    _ = LoadEmployee();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
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

