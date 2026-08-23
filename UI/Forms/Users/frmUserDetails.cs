using Contract.Responses;
using OldContract.Features.User.Dtos;
using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI.Forms.Employees;
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
            lblUserName.Text = _user.Username;
            lblSubTitle.Text = _user.Email;

            lblUsernameValue.Text = _user.Username;
            lblEmailValue.Text = _user.Email;
            lblRoleValue.Text = (_user.Role.ToString());
            lblLastLoginValue.Text = (_user.LastLoginAt.ToString("yyyy-MM-dd hh:mm:ss"));

            if (_user.Employee != null)
            {
                lblJobTitleValue.Text = (_user.Employee.JobTitle);

                if (_user.Employee.Warehouse != null)
                    lblWarehouseValue.Text = (_user.Employee.Warehouse.Name);

                if (_user.Employee.Person != null)
                    lblEmployeeValue.Text = BuildPersonName(_user.Employee.Person);
                else
                    lblEmployeeValue.Text = "Not Included";
            }

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
            return string.Join(" ", new[]
            {
                Convert.ToString(person.FirstName),
                Convert.ToString(person.SecondName),
                Convert.ToString(person.ThirdName),
                Convert.ToString(person.LastName)
            }.Where(x => !string.IsNullOrWhiteSpace(x)));
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
            using (var frm = new frmResetUserPassword(_userId, Convert.ToString(_user.Username)))
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
            using (var frm = new frmEmployeeDetails(_user.EmployeeId))
            {
                 if (
                frm.ShowDialog() == DialogResult.OK)
                    _ = LoadUser();
            }

        }


    }
}

