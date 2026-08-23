using Contract.Requests.Adjustment;
using Contract.Requests.Adjustments;
using Contract.Responses;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI.HttpClient;
using UI.Services;

namespace UI.Forms.Adjustments
{
    public partial class frmShowAdjustments : Form
    {
        private List<AdjustmentForListDto> _allAdjustments = new List<AdjustmentForListDto>();
        private bool _isLoadingFilters = false;

        public frmShowAdjustments()
        {
            InitializeComponent();
            SetupUI();
        }

        private async void frmShowAdjustments_Load(object sender, EventArgs e)
        {
            dgvAdjustments.SubscribeToLoadData(LoadData);
            await dgvAdjustments.LoadDataGridViewData();
        }

        private void SetupUI()
        {
            FormBorderStyle = FormBorderStyle.None;
            TopLevel = false;
            Dock = DockStyle.Fill;
            BackColor = Color.FromArgb(243, 246, 249);

            StyleButton(btnAdd, Color.FromArgb(74, 112, 139), Color.White);
            StyleButton(btnView, Color.FromArgb(243, 246, 249), Color.FromArgb(24, 33, 45));
            StyleButton(btnEdit, Color.FromArgb(243, 246, 249), Color.FromArgb(24, 33, 45));
            StyleButton(btnCancel, Color.FromArgb(243, 246, 249), Color.FromArgb(24, 33, 45));
            StyleButton(btnApprove, Color.FromArgb(243, 246, 249), Color.FromArgb(24, 33, 45));
            StyleButton(btnDelete, Color.FromArgb(220, 53, 69), Color.White);
            StyleButton(btnRefresh, Color.FromArgb(243, 246, 249), Color.FromArgb(24, 33, 45));

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

        private async Task<ApiResult<PaginatedList>> LoadData(int pageNo, int pageSize)
        {
            var result = await AdjustmentsServices.GetAll(pageNo, pageSize);

            if (!result.IsSuccess)
            {
                MessageBox.Show(result.Title_Full, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "Failed to load adjustments";
            }

            var data = result.Data.Items;

            _allAdjustments = data == null
                ? new List<AdjustmentForListDto>()
                : data.Cast<AdjustmentForListDto>().ToList();

            dgvAdjustments.SetData<AdjustmentForListDto>(_allAdjustments);

            LoadFilterSources();
            ApplyCurrentView();

            return result.Data;
        }

        private void LoadFilterSources()
        {
            _isLoadingFilters = true;

            cmbOrderBy.LoadData(dgvAdjustments.DgvCustom.GetColumnNamesExcept(new HashSet<string>()
            {
                "Id",
                "WarehouseId"
            }));

            cmbAdjustmentStatus.LoadData<AdjustmentForListDto>(_allAdjustments, a => a.AdjustmentStatus);
            cmbAdjustmentReason.LoadData<AdjustmentForListDto>(_allAdjustments, a => a.AdjustmentReason);

            cmbAdjustmentStatus.IndexChanged += ApplyCurrentView;
            cmbAdjustmentReason.IndexChanged += ApplyCurrentView;
            cmbOrderBy.IndexChanged += ApplyCurrentView;

            _isLoadingFilters = false;
        }

        private List<AdjustmentForListDto> ApplyLocalFilters()
        {
            IEnumerable<AdjustmentForListDto> query = _allAdjustments;

            string search = txtSearch.Text.Trim().ToLower();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(a =>
                    (a.AsjustmentType ?? "").ToLower().Contains(search) ||
                    (a.AdjustmentStatus ?? "").ToLower().Contains(search) ||
                    (a.AdjustmentReason ?? "").ToLower().Contains(search) ||
                    (a.WarehouseName ?? "").ToLower().Contains(search) ||
                    (a.CreatedAt.ToString() ?? "").ToLower().Contains(search) ||
                    (//a.AprovedAt ?
                    a.AprovedAt.ToString() //: ""
                    ).ToLower().Contains(search));
            }

            switch (cmbOrderBy.GetSelectedItemName())
            {
                case "AsjustmentType":
                    query = cmbOrderBy.SortData<AdjustmentForListDto>(query, a => a.AsjustmentType);
                    break;

                case "AdjustmentStatus":
                    query = cmbOrderBy.SortData<AdjustmentForListDto>(query, a => a.AdjustmentStatus);
                    break;

                case "AdjustmentReason":
                    query = cmbOrderBy.SortData<AdjustmentForListDto>(query, a => a.AdjustmentReason);
                    break;

                case "WarehouseName":
                    query = cmbOrderBy.SortData<AdjustmentForListDto>(query, a => a.WarehouseName);
                    break;

                case "AprovedAt":
                    query = cmbOrderBy.SortData<AdjustmentForListDto>(query, a => a.AprovedAt);
                    break;

                default:
                    query = cmbOrderBy.SortData<AdjustmentForListDto>(query, a => a.CreatedAt);
                    break;
            }

            query = cmbAdjustmentStatus.FilterData<AdjustmentForListDto>(
                query,
                a => a.AdjustmentStatus == cmbAdjustmentStatus.GetSelectedItemName());

            query = cmbAdjustmentReason.FilterData<AdjustmentForListDto>(
                query,
                a => a.AdjustmentReason == cmbAdjustmentReason.GetSelectedItemName());

            return query.ToList();
        }

        private void ApplyCurrentView()
        {
            var adjustments = ApplyLocalFilters();

            dgvAdjustments.SetData(adjustments);

            dgvAdjustments.DgvCustom.HideColumn("Id");
            dgvAdjustments.DgvCustom.HideColumn("WarehouseId");

            dgvAdjustments.DgvCustom.SetColumnHeader("AsjustmentType", "Type");
            dgvAdjustments.DgvCustom.SetColumnHeader("AdjustmentStatus", "Status");
            dgvAdjustments.DgvCustom.SetColumnHeader("AdjustmentReason", "Reason");
            dgvAdjustments.DgvCustom.SetColumnHeader("WarehouseName", "Warehouse");
            dgvAdjustments.DgvCustom.SetColumnHeader("AprovedAt", "Approved At");
            dgvAdjustments.DgvCustom.SetColumnHeader("CreatedAt", "Created At");
        }

        private AdjustmentForListDto GetSelectedAdjustment()
        {
            return dgvAdjustments.DgvCustom.GetSelectedItem<AdjustmentForListDto>();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            ApplyCurrentView();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (var frm = new frmAdjustmentEditor())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                    _ = dgvAdjustments.LoadDataGridViewData();
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedAdjustment();

            if (selected == null)
            {
                MessageBox.Show("Please select an adjustment first.");
                return;
            }

            using (var frm = new frmAdjustmentDetails(selected.Id))
            {
                frm.ShowDialog();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedAdjustment();

            if (selected == null)
            {
                MessageBox.Show("Please select an adjustment first.");
                return;
            }

            using (var frm = new frmAdjustmentEditor(selected.Id))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                    _ = dgvAdjustments.LoadDataGridViewData();
            }
        }

        private async void btnApprove_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedAdjustment();

            if (selected == null)
            {
                MessageBox.Show("Please select an adjustment first.");
                return;
            }

            var confirm = MessageBox.Show(
                "Are you sure you want to approve this adjustment?",
                "Confirm Approval",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes)
                return;

            btnApprove.Enabled = false;

            var result = await AdjustmentsServices.UpdateStatus(selected.Id, new UpdateAdjustmentStatusRequest
            {
                Id = selected.Id,
                AdjustmentStatus = AdjustmentStatus.Approved
            });

            btnApprove.Enabled = true;

            if (!result.IsSuccess)
            {
                MessageBox.Show(result.Title_Full, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            await dgvAdjustments.LoadDataGridViewData();
        }

        private async void btnCancel_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedAdjustment();

            if (selected == null)
            {
                MessageBox.Show("Please select an adjustment first.");
                return;
            }

            var confirm = MessageBox.Show(
                "Are you sure you want to cancel this adjustment?",
                "Confirm Cancelation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes)
                return;

            btnCancel.Enabled = false;

            var result = await AdjustmentsServices.UpdateStatus(selected.Id, new UpdateAdjustmentStatusRequest
            {
                Id = selected.Id,
                AdjustmentStatus = AdjustmentStatus.Cancelled
            });

            btnCancel.Enabled = true;

            if (!result.IsSuccess)
            {
                MessageBox.Show(result.Title_Full, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            await dgvAdjustments.LoadDataGridViewData();
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedAdjustment();

            if (selected == null)
            {
                MessageBox.Show("Please select an adjustment first.");
                return;
            }

            var confirm = MessageBox.Show(
                "Are you sure you want to delete this adjustment?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes)
                return;

            btnDelete.Enabled = false;

            var result = await AdjustmentsServices.Delete(selected.Id);

            btnDelete.Enabled = true;

            if (!result.IsSuccess)
            {
                MessageBox.Show(result.Title_Full, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            await dgvAdjustments.LoadDataGridViewData();
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            await dgvAdjustments.LoadDataGridViewData();
        }
    }
}

