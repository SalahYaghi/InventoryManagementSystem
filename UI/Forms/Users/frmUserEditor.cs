using Contract.Features.User.Commands.CreateUser;
using Contract.Common;
using Contract.Requests.Users;
using Contract.Responses;
using OldContract.Features.User.Dtos;
using System;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using UI.Forms.Employees;
using UI.Forms.People;
using UI.Shared.Services;
 
namespace UI.Forms.Users
{
    public partial class frmUserEditor : Form
    {
        

        private readonly bool _isUpdateMode;
        private readonly Guid _userId;

        private Guid? _selectedEmployeeId;
        private UserDto _user;

        public frmUserEditor()
        {
            _isUpdateMode = false;
            InitializeComponent();
            SetupUI();
        }

        public frmUserEditor(Guid userId)
        {
            _userId = userId;
            _isUpdateMode = true;
            InitializeComponent();
            SetupUI();
        }

        private async void frmUserEditor_Load(object sender, EventArgs e)
        {
            if (_isUpdateMode)
                await LoadUser();
        }

 
        private void SetupUI()
        {
            BackColor = Color.FromArgb(243, 246, 249);

            lblTitle.Text = _isUpdateMode ? "Edit User" : "Add User";
            lblSubtitle.Text = _isUpdateMode
                ? "Update username, email, role and account status."
                : "Create a login account for an employee.";
          
            StyleButton(btnEditEmployeeInfo, Color.FromArgb(74, 112, 139), Color.White);
            StyleButton(btnSave, Color.FromArgb(74, 112, 139), Color.White);
            StyleButton(btnCancel, Color.FromArgb(243, 246, 249), Color.FromArgb(24, 33, 45));
            StyleSelectionButton(btnSelectEmployee);

            StyleTextBox(txtEmployee);
            StyleTextBox(txtUsername);
            StyleTextBox(txtEmail);
            StyleTextBox(txtPassword);

            cmbRole.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRole.DataSource = Enum.GetValues(typeof(Role));

            chkIsActive.Checked = true;

            if (_isUpdateMode)
            {
                txtPassword.Enabled = false;
                chkShowPassword.Enabled = false;
                btnSelectEmployee.Enabled = false;
                lblPassword.Text = "Password";
            }
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

        private async System.Threading.Tasks.Task LoadUser()
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

            Bind();

            lblStatus.Text = "Ready";
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

        private void Bind() {

            txtUsername.Text = _user.Username;
            txtEmail.Text = _user.Email;
            chkIsActive.Checked = _user.IsActive;
            _selectedEmployeeId = _user.EmployeeId;

            txtEmployee.Text = BuildPersonName(_user.Employee.Person);

            cmbRole.SelectedItem = _user.Role;

        }

        private bool ValidateForm()
        {
            errorProvider.Clear();

            bool valid = true;

            if (!_selectedEmployeeId.HasValue || _selectedEmployeeId.Value == Guid.Empty)
            {
                errorProvider.SetError(txtEmployee, "Employee is required.");
                valid = false;
            }

            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                errorProvider.SetError(txtUsername, "Username is required.");
                valid = false;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                errorProvider.SetError(txtEmail, "Email is required.");
                valid = false;
            }
            else if (!Regex.IsMatch(txtEmail.Text.Trim(), @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                errorProvider.SetError(txtEmail, "Email format is invalid.");
                valid = false;
            }

            if (!_isUpdateMode && string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                errorProvider.SetError(txtPassword, "Password is required.");
                valid = false;
            }

            if (cmbRole.SelectedItem == null)
            {
                errorProvider.SetError(cmbRole, "Role is required.");
                valid = false;
            }

            return valid;
        }

        private CreateUserRequest BuildCreateRequest()
        {
            return new CreateUserRequest
            {
                Username = txtUsername.Text.Trim(),
                Password = txtPassword.Text,
                Email = txtEmail.Text.Trim(),
                Role = (Role)cmbRole.SelectedItem,
                EmployeeId = _selectedEmployeeId.Value
            };
        }

        private UpdateUserRequest BuildUpdateRequest()
        {
            return new UpdateUserRequest() {
               id= _userId,
                username= txtUsername.Text.Trim(),
                email= txtEmail.Text.Trim(),
                isActive= chkIsActive.Checked,
                role=(Role)cmbRole.SelectedItem
            };
        }

        private void btnSelectEmployee_Click(object sender, EventArgs e)
        {
            using (var frm = new frmEmployeeSelector())
            {
                if (frm.ShowDialog() != DialogResult.OK)
                    return;

                _selectedEmployeeId = frm.SelectedEmployeeId;

                if (frm.SelectedEmployee != null )
                {
                    txtEmployee.Text = (frm.SelectedEmployee.FullName);
                }
                else
                {
                    txtEmployee.Text = _selectedEmployeeId.ToString();
                }
            }
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateForm())
                return;

            btnSave.Enabled = false;
            lblStatus.Text = "Saving user...";

            if (_isUpdateMode)
            {
                var result = await UserServices.Update(_userId, BuildUpdateRequest());

                if (!result.IsSuccess)
                {
                    btnSave.Enabled = true;
                    lblStatus.Text = "Failed to save user";
                    MessageBox.Show(result.Title_Full, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                var result = await UserServices.Create(BuildCreateRequest());

                if (!result.IsSuccess)
                {
                    btnSave.Enabled = true;
                    lblStatus.Text = "Failed to save user";
                    MessageBox.Show(result.Title_Full, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !chkShowPassword.Checked;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnEditPersonalInfo_Click(object sender, EventArgs e)
        {
            if (!_selectedEmployeeId.HasValue || _selectedEmployeeId.Value == Guid.Empty) return;
            using (var frm = new frmEmployeeEditor(_selectedEmployeeId.Value))
            {
                if (frm.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                }
            }
        }
}

