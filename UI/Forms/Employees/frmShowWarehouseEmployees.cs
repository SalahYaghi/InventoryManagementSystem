using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Contract.Responses;
using global::UI.Shared.CurrentUser;
using UI.Shared.Services;

namespace UI.Forms.Employees
{
    public partial class frmShowWarehouseEmployees : Form
    {
        private Guid _warehouseId;
        private List<EmployeeDtoForList> _allEmployees = new List<EmployeeDtoForList>();
        private ToolTip _toolTip = new ToolTip();

        public string Title { set { 
                this.lblTitle.Text = value;
            } }

        public frmShowWarehouseEmployees()
        {
            InitializeComponent();

            var employee = CurrentUser.User == null ? null : CurrentUser.User.Employee;

            _warehouseId = employee == null || !employee.WarehouseId.HasValue
                ? Guid.Empty
                : employee.WarehouseId.Value;

            SetupUI();
        }

        public frmShowWarehouseEmployees(Guid warehouseId , string title)
        {
            InitializeComponent();
            _warehouseId = warehouseId;
            this.Title = title;
            SetupUI();
        }

        private async void frmShowWarehouseEmployees_Load(object sender, EventArgs e)
        {
            await LoadEmployees();
        }

        private void SetupUI()
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.FromArgb(243, 246, 249);
            this.StartPosition = FormStartPosition.CenterParent;

            flowEmployees.AutoScroll = true;
            flowEmployees.WrapContents = true;
            
            flowEmployees.Padding = new Padding(15, 15, 15, 30);

            cmbOrderBy.Items.AddRange(new object[]
            {
                "Full Name",
                "Job Title",
                "Email",
                "Phone"
            });

            cmbSortDirection.Items.AddRange(new object[]
            {
                "Ascending",
                "Descending"
            });

            cmbOrderBy.SelectedIndex = 0;
            cmbSortDirection.SelectedIndex = 0;

            StyleButton(btnRefresh, Color.FromArgb(74, 112, 139), Color.White);

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

        private async Task LoadEmployees()
        {
            lblStatus.Text = "Loading warehouse employees...";
            btnRefresh.Enabled = false;
            flowEmployees.Controls.Clear();

            var result = await EmployeeServices.GetAll(_warehouseId);

            btnRefresh.Enabled = true;

            if (!result.IsSuccess)
            {
                lblStatus.Text = "Failed to load employees";
                MessageBox.Show(result.Title_Full, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _allEmployees = result.Data ?? new List<EmployeeDtoForList>();

            lblTotalEmployees.Text = _allEmployees.Count.ToString();
            ApplyCurrentView();
           
        }

        private void ShowAddEmployeePanel() { 
        
            this.flowEmployees.Dock = DockStyle.None;
            this.flowEmployees.Visible = false;
            this.lblAddEmployee.Visible = true;
        }

        private void HideAddEmployeePanel()
        {

            this.flowEmployees.Dock = DockStyle.Fill;
            this.flowEmployees.Visible = true;
            this.lblAddEmployee.Visible = false ;
        }

        private void ApplyCurrentView()
        {
            IEnumerable<EmployeeDtoForList> query = _allEmployees;


            if (_allEmployees.Count == 0)
                ShowAddEmployeePanel();
            else
                HideAddEmployeePanel();

            string search = txtSearch.Text.Trim().ToLower();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(e =>
                    ValueOrDash(e.FullName).ToLower().Contains(search) ||
                    ValueOrDash(e.Email).ToLower().Contains(search) ||
                    ValueOrDash(e.PhoneNumber).ToLower().Contains(search) ||
                    ValueOrDash(e.JobTitle).ToLower().Contains(search)
                );
            }

            bool desc = cmbSortDirection.Text == "Descending";

            switch (cmbOrderBy.Text)
            {
                case "Email":
                    query = desc ? query.OrderByDescending(e => e.Email) : query.OrderBy(e => e.Email);
                    break;

                case "Phone":
                    query = desc ? query.OrderByDescending(e => e.PhoneNumber) : query.OrderBy(e => e.PhoneNumber);
                    break;

                case "Job Title":
                    query = desc ? query.OrderByDescending(e => e.JobTitle) : query.OrderBy(e => e.JobTitle);
                    break;

                default:
                    query = desc ? query.OrderByDescending(e => e.FullName) : query.OrderBy(e => e.FullName);
                    break;
            }

            RenderEmployeeCards(query.ToList());
        }

        private void RenderEmployeeCards(List<EmployeeDtoForList> employees)
        {
            flowEmployees.SuspendLayout();
            flowEmployees.Controls.Clear();

            if (employees.Count == 0)
            {
                ShowEmptyState();
                flowEmployees.ResumeLayout();
                lblStatus.Text = "No employees found";
                return;
            }

            foreach (var employee in employees)
                flowEmployees.Controls.Add(CreateEmployeeCard(employee));

          
            Panel bottomSpacer = new Panel();
            bottomSpacer.Width = flowEmployees.Width - flowEmployees.Padding.Left*3;
            bottomSpacer.Height = 50;
            bottomSpacer.Margin = new Padding(0);
            flowEmployees.Controls.Add(bottomSpacer);
            flowEmployees.ResumeLayout();

            lblStatus.Text = employees.Count + " employee(s) shown";
         }

        private Panel CreateEmployeeCard(EmployeeDtoForList employee)
        {
            Panel card = new Panel();
            card.Width = 345;
            card.Height = 225;
            card.BackColor = Color.White;
            card.Margin = new Padding(10);
            card.Padding = new Padding(16);
            card.Cursor = Cursors.Hand;
            card.Tag = employee;

            Label avatar = new Label();
            avatar.Size = new Size(60, 54);
            avatar.Location = new Point(16, 16);
            avatar.BackColor = Color.FromArgb(219, 230, 241);
            avatar.ForeColor = Color.FromArgb(74, 112, 139);
            avatar.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold);
            avatar.TextAlign = ContentAlignment.MiddleCenter;
            avatar.Text = GetEmployeeShortcut(employee.FullName);

            Label lblFullName = new Label();
            lblFullName.Location = new Point(84, 16);
            lblFullName.Size = new Size(230, 30);
            lblFullName.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            lblFullName.ForeColor = Color.FromArgb(24, 33, 45);
            lblFullName.Text = ValueOrDash(employee.FullName);

            Label lblJobTitle = new Label();
            lblJobTitle.Location = new Point(84, 47);
            lblJobTitle.Size = new Size(230, 24);
            lblJobTitle.Font = new Font("Segoe UI", 9.5F);
            lblJobTitle.ForeColor = Color.Gray;
            lblJobTitle.Text = ValueOrDash(employee.JobTitle);

            Label lblEmailTitle = CreateSmallTitle("Email", 18, 88);
            Label lblEmailValue = CreateSmallValue(ValueOrDash(employee.Email), 18, 110, 300);

            Label lblPhoneTitle = CreateSmallTitle("Phone", 18, 138);
            Label lblPhoneValue = CreateSmallValue(ValueOrDash(employee.PhoneNumber), 18, 160, 180);

            Label lblStatusBadge = new Label();
            lblStatusBadge.Location = new Point(220, 150);
            lblStatusBadge.Size = new Size(90, 28);
            lblStatusBadge.Font = new Font("Segoe UI Semibold", 8.5F, FontStyle.Bold);
            lblStatusBadge.TextAlign = ContentAlignment.MiddleCenter;
            lblStatusBadge.Text = "Active";
            lblStatusBadge.BackColor = Color.FromArgb(219, 242, 230);
            lblStatusBadge.ForeColor = Color.FromArgb(22, 101, 52);

            Panel actionsPanel = CreateActionsPanel(employee);
            actionsPanel.Location = new Point(16, 184);

            card.MouseEnter += (s, e) => card.BackColor = Color.FromArgb(248, 250, 252);
            card.MouseLeave += (s, e) => card.BackColor = Color.White;

            card.Controls.Add(avatar);
            card.Controls.Add(lblFullName);
            card.Controls.Add(lblJobTitle);
            card.Controls.Add(lblEmailTitle);
            card.Controls.Add(lblEmailValue);
            card.Controls.Add(lblPhoneTitle);
            card.Controls.Add(lblPhoneValue);
            card.Controls.Add(lblStatusBadge);
            card.Controls.Add(actionsPanel);

            return card;
        }

        private Panel CreateActionsPanel(EmployeeDtoForList employee)
        {
            Panel actionsPanel = new Panel();
            actionsPanel.Size = new Size(310, 36);
            actionsPanel.BackColor = Color.Transparent;

            Button btnView = CreateIconButton("👁", "View Details");
            Button btnEdit = CreateIconButton("✏", "Edit Employee");
            Button btnDelete = CreateIconButton("🗑", "Delete Employee");
            Button btnAdd = CreateIconButton("➕", "Add Employee");

            btnView.Location = new Point(0, 0);
            btnEdit.Location = new Point(58, 0);
            btnDelete.Location = new Point(116, 0);
            btnAdd.Location = new Point(174, 0);

            btnView.Click += (s, e) => ShowEmployee(employee);
            btnEdit.Click += async (s, e) => await EditEmployee(employee);
            btnDelete.Click += async (s, e) => await DeleteEmployee(employee);
            btnAdd.Click += async (s, e) => await AddEmployee();

            actionsPanel.Controls.Add(btnView);
            actionsPanel.Controls.Add(btnEdit);
            actionsPanel.Controls.Add(btnDelete);
            actionsPanel.Controls.Add(btnAdd);

            return actionsPanel;
        }

        private Button CreateIconButton(string icon, string tooltipText)
        {
            Button button = new Button();
            button.Size = new Size(34, 28);
            button.Text = icon;
            button.Font = new Font("Segoe UI Emoji", 8.5F, FontStyle.Regular);
            button.BackColor = Color.Transparent;
            button.ForeColor = Color.FromArgb(24, 33, 45);
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.Cursor = Cursors.Hand;
            button.TextAlign = ContentAlignment.MiddleCenter;

            button.MouseEnter += (s, e) =>
            {
                if (icon == "🗑")
                    button.BackColor = Color.FromArgb(254, 226, 226);
                else
                    button.BackColor = Color.FromArgb(230, 240, 250);
            };

            button.MouseLeave += (s, e) =>
            {
                button.BackColor =Color.Transparent ;
            };

            _toolTip.SetToolTip(button, tooltipText);

            return button;
        }

        private async Task  AddEmployee() {
            using (var frm = new frmEmployeeEditor())
            {
                frm.DefineSelectedWarehouse(_warehouseId);
                if (frm.ShowDialog() == DialogResult.OK)
                    await LoadEmployees();
            }
        }
        private void ShowEmployee(EmployeeDtoForList employee)
        {
            if (employee == null)
                return;

            using (var frm = new frmEmployeeDetails(employee.EmployeeId))
            {
                frm.ShowDialog();
            }
        }

        private async Task EditEmployee(EmployeeDtoForList employee)
        {
            if (employee == null)
                return;

            using (var frm = new frmEmployeeEditor(employee.EmployeeId))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                    await LoadEmployees();
            }
        }

        private async Task DeleteEmployee(EmployeeDtoForList employee)
        {
            if (employee == null)
                return;

            var confirm = MessageBox.Show(
                $"Are you sure you want to delete {employee.FullName}?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes)
                return;

            lblStatus.Text = "Deleting employee...";

            var result = await EmployeeServices.Delete(employee.EmployeeId);

            if (!result.IsSuccess)
            {
                lblStatus.Text = "Failed to delete employee";
                MessageBox.Show(result.Title_Full, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            await LoadEmployees();
        }

        private Label CreateSmallTitle(string text, int x, int y)
        {
            return new Label
            {
                Text = text,
                Location = new Point(x, y),
                Size = new Size(120, 20),
                Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold),
                ForeColor = Color.Gray
            };
        }

        private Label CreateSmallValue(string text, int x, int y, int width)
        {
            return new Label
            {
                Text = text,
                Location = new Point(x, y),
                Size = new Size(width, 24),
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.FromArgb(24, 33, 45)
            };
        }

        private void ShowEmptyState()
        {
            Label empty = new Label();
            empty.Dock = DockStyle.Fill;
            empty.TextAlign = ContentAlignment.MiddleCenter;
            empty.Font = new Font("Segoe UI Semibold", 14F, FontStyle.Bold);
            empty.ForeColor = Color.Gray;
            empty.Text = "No warehouse employees found.";
            flowEmployees.Controls.Add(empty);
        }

        private string GetEmployeeShortcut(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                return "?";

            string[] words = fullName.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (words.Length == 1)
                return words[0].Length >= 2 ? words[0].Substring(0, 2).ToUpper() : words[0].ToUpper();

            string result = "";

            foreach (string word in words)
            {
                if (!string.IsNullOrWhiteSpace(word))
                    result += word[0];
            }

            return result.Length >= 2 ? result.Substring(0, 2).ToUpper() : result.ToUpper();
        }

        private string ValueOrDash(string value)
        {
            return string.IsNullOrWhiteSpace(value) ? "-" : value;
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            await LoadEmployees();
        }

    
        private void txtSearch_TextChanged(object sender, EventArgs e)
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

        private async void lblAddEmployee_Click(object sender, EventArgs e)
        {
            await AddEmployee();    
        }
    }
}
