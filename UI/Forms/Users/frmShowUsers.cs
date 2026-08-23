using Contract.Features.User.Dtos;
using Contract.Responses;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI.Services;
using UI.Shared.Services;

namespace UI.Forms.Users
{
    public partial class frmShowUsers : Form
    {
      

        private List<UserForListDto> _users = new List<UserForListDto>();
        private bool _loadingFilters = false;

        public frmShowUsers()
        {
            InitializeComponent();
            SetupUI();
        }

        private async void frmShowUsers_Load(object sender, EventArgs e)
        {
            await LoadUsers();
        }

        private void SetupUI()
        {
            BackColor = Color.FromArgb(243, 246, 249);
            TopLevel = false;
            Dock = DockStyle.Fill;
             StyleButton(btnAdd, Color.FromArgb(74, 112, 139), Color.White);
            StyleButton(btnEdit, Color.FromArgb(243, 246, 249), Color.FromArgb(24, 33, 45));
            StyleButton(btnView, Color.FromArgb(243, 246, 249), Color.FromArgb(24, 33, 45));
            StyleButton(btnResetPassword, Color.FromArgb(243, 246, 249), Color.FromArgb(24, 33, 45));
            StyleButton(btnRefresh, Color.FromArgb(243, 246, 249), Color.FromArgb(24, 33, 45));
            StyleButton(btnDelete, Color.FromArgb(220, 53, 69), Color.White);

            StyleTextBox(txtSearch);
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

        
        private async Task LoadUsers()
        {
            lblStatus.Text = "Loading users...";

            var result = await UserServices.GetAll();

            if (!result.IsSuccess)
            {
                lblStatus.Text = "Failed to load users";
                MessageBox.Show(result.Title_Full, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _users = result.Data ?? new List<UserForListDto>();

            this.dgvUsers.SetData(this._users);

        cmbActive.LoadData<UserForListDto>(_users,
        p => p.IsActive);
            cmbActive.IndexChanged += ApplyCurrentView;

            var cols = dgvUsers.GetColumnNamesExcept(new HashSet<string>() {
                "Id","PersonId" , "EmployeeId" ,"IsActive"
            });
            cmbOrderBy.LoadData(cols);
            cmbOrderBy.IndexChanged += ApplyCurrentView;
 

             ApplyCurrentView();

            lblStatus.Text = $"{_users.Count} user(s) loaded";
        }

        
        private List<UserForListDto> ApplyLocalFilters()
        {
            IEnumerable<UserForListDto> query = _users;

            string search = txtSearch.Text.Trim().ToLower();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(u =>
                    (u.Username ?? "").ToLower().Contains(search) ||
                    (u.Email ?? "").ToLower().Contains(search) ||
                    (u.Role ?? "").ToLower().Contains(search) ||
                    (u.PersonName ?? "").ToLower().Contains(search) ||
                    (u.JobTitle ?? "").ToLower().Contains(search) ||
                    (u.WarehouseName ?? "").ToLower().Contains(search));
              
            }
      
            string st = cmbOrderBy.GetSelectedItemName();
            switch (st)
            {
                case "PersonName":
                    query = cmbOrderBy.SortData(query, c => c.PersonName);
                    break;
                case "Email":
                    query = cmbOrderBy.SortData(query, c => c.Email);
                    break;
                case "WarehouseName":
                    query = cmbOrderBy.SortData(query, c => c.WarehouseName);
                    break;
                case "Role":
                    query = cmbOrderBy.SortData(query, c => c.Role);
                    break;

                case "JobTitle":
                    query = cmbOrderBy.SortData(query, c => c.JobTitle);
                    break;

                case "Username":
                    query = cmbOrderBy.SortData(query, c => c.Username);

                    break;

                case "LastLoginAt":
                    query = cmbOrderBy.SortData(query, c => c.LastLoginAt);
                    break;
 
                default:
                    query = cmbOrderBy.SortData(query, c => c.Username);
                    break;
            }

            query = cmbActive.FilterData<UserForListDto>(query,
            p => p.IsActive.ToString() == cmbActive.GetSelectedItemName());
            return query.ToList();
        }

        private void ApplyCurrentView()
        {
            var users = ApplyLocalFilters();

            dgvUsers.SetData(users);

            dgvUsers.HideColumn("Id");
            dgvUsers.HideColumn("EmployeeId");
            dgvUsers.HideColumn("PersonId");
            
            dgvUsers.SetColumnHeader("IsActive", "Active");
            dgvUsers.SetColumnHeader("Username", "Username");
            dgvUsers.SetColumnHeader("Email", "Email");
            dgvUsers.SetColumnHeader("Role", "Role");
            dgvUsers.SetColumnHeader("IsActive", "Active");
            dgvUsers.SetColumnHeader("LastLoginAt", "Last Login");
            dgvUsers.SetColumnHeader("JobTitle", "Job Title");
            dgvUsers.SetColumnHeader("WarehouseName", "Warehouse");
            dgvUsers.SetColumnHeader("PersonName", "Employee");

            lblStatus.Text = $"Showing {users.Count} user(s)";
        }

        private UserForListDto GetSelectedUser()
        {
            return dgvUsers.GetSelectedItem<UserForListDto>();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (var frm = new frmUserEditor())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                    _ = LoadUsers();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedUser();

            if (selected == null)
            {
                MessageBox.Show("Please select a user first.");
                return;
            }

            using (var frm = new frmUserEditor(selected.Id))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                    _ = LoadUsers();
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedUser();

            if (selected == null)
            {
                MessageBox.Show("Please select a user first.");
                return;
            }

            using (var frm = new frmUserDetails(selected.Id))
            {
                frm.ShowDialog();
            }
        }

        private void btnResetPassword_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedUser();

            if (selected == null)
            {
                MessageBox.Show("Please select a user first.");
                return;
            }

            using (var frm = new frmResetUserPassword(selected.Id, selected.Username))
            {
                frm.ShowDialog();
            }
        }

        private async void btnRefresh1_Click(object sender, EventArgs e)
        {
            await LoadUsers();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            ApplyCurrentView();
        }

        private void cmbActive_Load(object sender, EventArgs e)
        {

        }

        private void cmbOrderBy_Load(object sender, EventArgs e)
        {

        }

        private void panelContent_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panelFilters_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblSearch_Click(object sender, EventArgs e)
        {

        }

        private void dgvUsers_Load(object sender, EventArgs e)
        {

        }

        private void panelActions_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panelFooter_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblStatus_Click(object sender, EventArgs e)
        {

        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            await LoadUsers();
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedUser(); // dg.DgvCustom.GetSelectedItem<WarehouseStockDtoForList>();

            if (selected == null)
            {
                MessageBox.Show("Please select a user first.");
                return;
            }



            var confirm = MessageBox.Show(
                $"Are you sure you want to delete {selected.Username}?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes)
                return;

            lblStatus.Text = "Deleting image...";
            btnDelete.Enabled = false;

            var result = await UserServices.Delete(selected.Id);

            btnDelete.Enabled = true;

            if (!result.IsSuccess)
            {
                lblSearch.Text = "Failed to delete user";
                MessageBox.Show(result.Title_Full, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            await LoadUsers();

        }
    }
}

