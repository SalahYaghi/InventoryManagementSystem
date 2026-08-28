using Contract.Requests.Adjustment;
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
        private bool _filtersInitialised;
        private bool _suppressFilterEvents;

        public frmShowAdjustments()
        {
            InitializeComponent();
            SetupUI();
        }

        #region Setup

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
            StyleButton(btnApprove, Color.FromArgb(39, 120, 97), Color.White);
            StyleButton(btnDelete, Color.FromArgb(220, 53, 69), Color.White);
            StyleButton(btnRefresh, Color.FromArgb(243, 246, 249), Color.FromArgb(24, 33, 45));

            StyleTextBox(txtSearch);

            cmbOrderBy.Title = "Order By";
            cmbAdjustmentStatus.Title = "Status";
            cmbAdjustmentReason.Title = "Reason";

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

        #endregion

        #region Loading

        private async void frmShowAdjustments_Load(object sender, EventArgs e)
        {
            dgvAdjustments.SubscribeToLoadData(LoadData);
            await dgvAdjustments.LoadDataGridViewData();
        }

        private async Task<ApiResult<PaginatedList>> LoadData(int pageNo, int pageSize)
        {
            var result = await AdjustmentsServices.GetAll(pageNo, pageSize);

            if (!result.IsSuccess || result.Data == null)
            {
                MessageBox.Show(result.Title_Full, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "Failed to load adjustments";
            }

            _allAdjustments = result.Data.Items ?? new List<AdjustmentForListDto>();

            dgvAdjustments.SetData(_allAdjustments);

            LoadFilterSources();
            ApplyCurrentView();

            return result.Data;
        }

        private void LoadFilterSources()
        {
            _suppressFilterEvents = true;

            try
            {
                cmbOrderBy.LoadData(dgvAdjustments.DgvCustom.GetColumnNamesExcept(new HashSet<string>
                {
                    "Id",
                    "WarehouseId"
                }));

                cmbAdjustmentStatus.LoadData<AdjustmentForListDto>(_allAdjustments, a => a.AdjustmentStatus);
                cmbAdjustmentReason.LoadData<AdjustmentForListDto>(_allAdjustments, a => a.AdjustmentReason);

                if (_filtersInitialised)
                    return;

                cmbAdjustmentStatus.IndexChanged += ApplyCurrentView;
                cmbAdjustmentReason.IndexChanged += ApplyCurrentView;
                cmbOrderBy.IndexChanged += ApplyCurrentView;

                _filtersInitialised = true;
            }
            finally
            {
                _suppressFilterEvents = false;
            }
        }

        #endregion

        #region Filtering

        private List<AdjustmentForListDto> ApplyLocalFilters()
        {
            IEnumerable<AdjustmentForListDto> query = _allAdjustments;

            string search = txtSearch.Text.Trim().ToLowerInvariant();

            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(a => MatchesSearch(a, search));

            query = ApplySorting(query);

            query = cmbAdjustmentStatus.FilterData<AdjustmentForListDto>(
                query,
                a => a.AdjustmentStatus == cmbAdjustmentStatus.GetSelectedItemName());

            query = cmbAdjustmentReason.FilterData<AdjustmentForListDto>(
                query,
                a => a.AdjustmentReason == cmbAdjustmentReason.GetSelectedItemName());

            return query.ToList();
        }

        private bool MatchesSearch(AdjustmentForListDto adjustment, string search)
        {
            return (adjustment.AsjustmentType ?? string.Empty).ToLowerInvariant().Contains(search) ||
                   (adjustment.AdjustmentStatus ?? string.Empty).ToLowerInvariant().Contains(search) ||
                   (adjustment.AdjustmentReason ?? string.Empty).ToLowerInvariant().Contains(search) ||
                   (adjustment.WarehouseName ?? string.Empty).ToLowerInvariant().Contains(search) ||
                   adjustment.CreatedAt.ToString().ToLowerInvariant().Contains(search) ||
                   (adjustment.AprovedAt.HasValue
                       ? adjustment.AprovedAt.Value.ToString().ToLowerInvariant().Contains(search)
                       : false);
        }

        private IEnumerable<AdjustmentForListDto> ApplySorting(IEnumerable<AdjustmentForListDto> query)
        {
            switch (cmbOrderBy.GetSelectedItemName())
            {
                case "AsjustmentType":
                    return cmbOrderBy.SortData<AdjustmentForListDto>(query, a => a.AsjustmentType);

                case "AdjustmentStatus":
                    return cmbOrderBy.SortData<AdjustmentForListDto>(query, a => a.AdjustmentStatus);

                case "AdjustmentReason":
                    return cmbOrderBy.SortData<AdjustmentForListDto>(query, a => a.AdjustmentReason);

                case "WarehouseName":
                    return cmbOrderBy.SortData<AdjustmentForListDto>(query, a => a.WarehouseName);

                case "AprovedAt":
                    return cmbOrderBy.SortData<AdjustmentForListDto>(query, a => a.AprovedAt ?? DateTime.MinValue);

                default:
                    return cmbOrderBy.SortData<AdjustmentForListDto>(query, a => a.CreatedAt);
            }
        }

        #endregion

        #region View

        private void ApplyCurrentView()
        {
            if (_suppressFilterEvents)
                return;

            dgvAdjustments.SetData(ApplyLocalFilters());

            var grid = dgvAdjustments.DgvCustom;

            grid.HideColumns("Id", "WarehouseId");

            grid.SetColumnHeaders(new Dictionary<string, string>
            {
                { "AsjustmentType", "Type" },
                { "AdjustmentStatus", "Status" },
                { "AdjustmentReason", "Reason" },
                { "WarehouseName", "Warehouse" },
                { "AprovedAt", "Approved At" },
                { "CreatedAt", "Created At" }
            });

            grid.FormatColumnsAsDateTime("AprovedAt", "CreatedAt");
        }

        private AdjustmentForListDto GetSelectedAdjustment()
        {
            return dgvAdjustments.DgvCustom.GetSelectedItem<AdjustmentForListDto>();
        }

        private AdjustmentForListDto RequireSelectedAdjustment()
        {
            var selected = GetSelectedAdjustment();

            if (selected == null)
                MessageBox.Show("Please select an adjustment first.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

            return selected;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            ApplyCurrentView();
        }

        #endregion

        #region Actions

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (var frm = new frmAdjustmentEditor())
            {
                if (frm.ShowDialog(this) == DialogResult.OK)
                    _ = dgvAdjustments.LoadDataGridViewData();
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            var selected = RequireSelectedAdjustment();

            if (selected == null)
                return;

            using (var frm = new frmAdjustmentDetails(selected.Id))
            {
                frm.ShowDialog(this);
            }

            _ = dgvAdjustments.LoadDataGridViewData();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var selected = RequireSelectedAdjustment();

            if (selected == null)
                return;

            if (selected.AdjustmentStatus != AdjustmentStatus.Draft.ToString())
            {
                MessageBox.Show("Only draft adjustments can be edited.", "Not Allowed",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (var frm = new frmAdjustmentEditor(selected.Id))
            {
                if (frm.ShowDialog(this) == DialogResult.OK)
                    _ = dgvAdjustments.LoadDataGridViewData();
            }
        }

        private async void btnApprove_Click(object sender, EventArgs e)
        {
            var selected = RequireSelectedAdjustment();

            if (selected == null)
                return;

            if (selected.AdjustmentStatus != AdjustmentStatus.Draft.ToString())
            {
                MessageBox.Show("Only draft adjustments can be approved.", "Not Allowed",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirm = MessageBox.Show(
                "Approving this adjustment will change warehouse stock. Continue?",
                "Confirm Approval",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes)
                return;

            await ChangeStatus(btnApprove, selected.Id, AdjustmentStatus.Approved);
        }

        private async void btnCancel_Click(object sender, EventArgs e)
        {
            var selected = RequireSelectedAdjustment();

            if (selected == null)
                return;

            if (selected.AdjustmentStatus == AdjustmentStatus.Cancelled.ToString())
            {
                MessageBox.Show("This adjustment is already cancelled.", "Not Allowed",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirm = MessageBox.Show(
                "Are you sure you want to cancel this adjustment?",
                "Confirm Cancellation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes)
                return;

            await ChangeStatus(btnCancel, selected.Id, AdjustmentStatus.Cancelled);
        }

        private async Task ChangeStatus(Button trigger, Guid adjustmentId, AdjustmentStatus status)
        {
            trigger.Enabled = false;

            var result = await AdjustmentsServices.UpdateStatus(adjustmentId, new UpdateAdjustmentStatusRequest
            {
                Id = adjustmentId,
                AdjustmentStatus = status
            });

            trigger.Enabled = true;

            if (!result.IsSuccess)
            {
                MessageBox.Show(result.Title_Full, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            await dgvAdjustments.LoadDataGridViewData();
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            var selected = RequireSelectedAdjustment();

            if (selected == null)
                return;

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

        #endregion
    }
}
