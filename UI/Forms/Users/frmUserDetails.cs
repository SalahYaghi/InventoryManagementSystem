using Contract.Responses;
using OldContract.Features.User.Dtos;
using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI.Forms.Employees;
using UI.Shared.Helpers.UI_Helpers;
using UI.Shared.Services;

namespace UI.Forms.Users
{
    public partial class frmUserDetails : Form
    {
        private readonly Guid _userId;
        private UserDto _user;
    
        public frmUserDetails(Guid userId)
        {
            _userId = userId;
            InitializeComponent();
            SetupUI();
        }

        private async void frmUserDetails_Load(object sender, EventArgs e)
        {
            await LoadUser();
        }
        private void SetupUI()
        {
            BackColor = Color.FromArgb(243, 246, 249);

            StyleButton(btnEmployeeData, Color.FromArgb(74, 112, 139), Color.White);
            StyleButton(btnEdit, Color.FromArgb(74, 112, 139), Color.White);
            StyleButton(btnResetPassword, Color.FromArgb(243, 246, 249), Color.FromArgb(24, 33, 45));
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

        private async Task LoadUser()
        {
            lblStatus.Text = "Loading user...";

            var result = await UserServices.Get(_userId);

            if (!result.IsSuccess)
            {
                lblStatus.Text = "Failed to load user";
                MessageBox.Show(result.Title_Full, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _user = result.Data;
            BindUser();

            lblStatus.Text = "Ready";
        }

        private void BindUser()
        {
            if (_user == null)
                return;

            lblUserName.Text = DisplayFormatter.Text(_user.Username);
            lblSubTitle.Text = DisplayFormatter.Text(_user.Email);

            lblUsernameValue.Text = DisplayFormatter.Text(_user.Username);
            lblEmailValue.Text = DisplayFormatter.Text(_user.Email);
            lblRoleValue.Text = _user.Role.ToString();
            lblLastLoginValue.Text = _user.LastLoginAt == default(DateTime)
                ? "Never signed in"
                : DisplayFormatter.DateTimeValue(_user.LastLoginAt);

            var employee = _user.Employee;

            lblJobTitleValue.Text = employee == null
                ? DisplayFormatter.NotSetPlaceholder
                : DisplayFormatter.Text(employee.JobTitle, DisplayFormatter.NotSetPlaceholder);

            lblWarehouseValue.Text = employee == null || employee.Warehouse == null
                ? "No warehouse assigned"
                : DisplayFormatter.Text(employee.Warehouse.Name, "No warehouse assigned");

            lblEmployeeValue.Text = employee == null || employee.Person == null
                ? DisplayFormatter.NotAvailablePlaceholder
                : DisplayFormatter.Text(BuildPersonName(employee.Person), DisplayFormatter.NotAvailablePlaceholder);

            if (_user.IsActive)
            {
                lblStatusBadge.Text = "Active";
                lblStatusBadge.BackColor = Color.FromArgb(219, 242, 230);
                lblStatusBadge.ForeColor = Color.FromArgb(22, 101, 52);
            }
            else
            {
                lblStatusBadge.Text = "Inactive";
                lblStatusBadge.BackColor = Color.FromArgb(243, 244, 246);
                lblStatusBadge.ForeColor = Color.FromArgb(107, 114, 128);
            }
        }

        private string BuildPersonName(PersonDto person)
        {
            if (person == null)
                return string.Empty;

            return TextFormattingHelper.BuildFullName(
                person.FirstName,
                person.SecondName,
                person.ThirdName,
                person.LastName);
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            using (var frm = new frmUserEditor(_userId))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                    _ = LoadUser();
            }
        }
        private void btnResetPassword_Click(object sender, EventArgs e)
        {
            if (_user == null)
                return;

            using (var frm = new frmResetUserPassword(_userId, _user.Username))
            {
                frm.ShowDialog();
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel; 
            Close();
        }

        private void btnEmployeeData_Click(object sender, EventArgs e)
        {
            if (_user == null || _user.EmployeeId == Guid.Empty)
            {
                MessageBox.Show("This user is not linked to an employee record.", "Not Available",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (var frm = new frmEmployeeDetails(_user.EmployeeId))
            {
                 if (
                frm.ShowDialog() == DialogResult.OK)
                    _ = LoadUser();
            }

        }


    }
}

