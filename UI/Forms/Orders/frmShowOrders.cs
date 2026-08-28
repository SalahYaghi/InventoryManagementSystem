using Contract.Requests.Orders;
using Contract.Responses;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI.HttpClient;
using UI.Services;

namespace UI.Forms.Orders
{
    public partial class frmShowOrders : Form
    {
        private readonly OrderType? _orderType;

        private List<OrderForListDto> _allOrders = new List<OrderForListDto>();
        private bool _filtersInitialised;
        private bool _suppressFilterEvents;

        public frmShowOrders(OrderType? orderType = null)
        {
            InitializeComponent();
            _orderType = orderType;
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
            StyleButton(btnDelete, Color.FromArgb(220, 53, 69), Color.White);
            StyleButton(btnRefresh, Color.FromArgb(243, 246, 249), Color.FromArgb(24, 33, 45));
            StyleButton(btnComplete, Color.FromArgb(243, 246, 249), Color.FromArgb(24, 33, 45));

            StyleTextBox(txtSearch);

            cmbOrderBy.Title = "Order By";
            cmbOrderStatus.Title = "Status";


            Text = BuildSectionTitle();
        }

        private string BuildSectionTitle()
        {
            if (_orderType == null)
                return "Transactions";

            switch (_orderType.Value)
            {
                case OrderType.Purchase:
                    return "Purchase Orders";
                case OrderType.Sale:
                    return "Sales Orders";
                case OrderType.Transfer:
                    return "Warehouse Transfers";
                case OrderType.ReturnIn:
                    return "Returns In";
                case OrderType.ReturnOut:
                    return "Returns Out";
                default:
                    return "Transactions";
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

        private void StyleTextBox(TextBox textBox)
        {
            textBox.BackColor = Color.FromArgb(248, 250, 252);
            textBox.BorderStyle = BorderStyle.FixedSingle;
            textBox.Font = new Font("Segoe UI", 10F);
            textBox.ForeColor = Color.FromArgb(24, 33, 45);
        }

        #endregion

        #region Loading

        private async void frmShowOrders_Load(object sender, EventArgs e)
        {
            dgvOrders.SubscribeToLoadData(LoadData);
            await dgvOrders.LoadDataGridViewData();
        }

        private async Task<ApiResult<PaginatedList>> LoadData(int pageNo, int pageSize)
        {
            var result = await OrdersServices.GetAll(pageNo, pageSize, _orderType);

            if (!result.IsSuccess || result.Data == null)
            {
                MessageBox.Show(result.Title_Full, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "Failed to load transactions";
            }

            _allOrders = result.Data.Items ?? new List<OrderForListDto>();

            dgvOrders.SetData(_allOrders);

            LoadFilterSources();
            ApplyCurrentView();

            return result.Data;
        }

        private void LoadFilterSources()
        {
            _suppressFilterEvents = true;

            try
            {
                cmbOrderBy.LoadData(dgvOrders.DgvCustom.GetColumnNamesExcept(BuildExcludedSortColumns()));
                cmbOrderStatus.LoadData<OrderForListDto>(_allOrders, o => o.OrderStatus);

                if (_filtersInitialised)
                    return;

                cmbOrderStatus.IndexChanged += ApplyCurrentView;
                cmbOrderBy.IndexChanged += ApplyCurrentView;

                _filtersInitialised = true;
            }
            finally
            {
                _suppressFilterEvents = false;
            }
        }

        private HashSet<string> BuildExcludedSortColumns()
        {
            var excluded = new HashSet<string>
            {
                "Id",
                "OrderType",
                "InvoiceId",
                "SourceWarehouseId",
                "DestinationWarehouseId",
                "CustomerId",
                "SupplierId"
            };

            if (_orderType != OrderType.Sale && _orderType != OrderType.ReturnIn)
                excluded.Add("CustomerName");

            if (_orderType != OrderType.Purchase && _orderType != OrderType.ReturnOut)
                excluded.Add("SupplierName");

            if (_orderType != OrderType.Transfer)
                excluded.Add("DestinationWarehouseName");

            if (_orderType == OrderType.Transfer)
            {
                excluded.Add("SubTotalAmount");
                excluded.Add("DiscountAmount");
                excluded.Add("NetAmount");
            }

            return excluded;
        }

        #endregion

        #region Filtering

        private List<OrderForListDto> ApplyLocalFilters()
        {
            IEnumerable<OrderForListDto> query = _allOrders;

            string search = txtSearch.Text.Trim().ToLowerInvariant();

            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(o => MatchesSearch(o, search));

            query = ApplySorting(query);

            query = cmbOrderStatus.FilterData<OrderForListDto>(
                query,
                o => o.OrderStatus == cmbOrderStatus.GetSelectedItemName());

            return query.ToList();
        }

        private bool MatchesSearch(OrderForListDto order, string search)
        {
            return (order.OrderType ?? string.Empty).ToLowerInvariant().Contains(search) ||
                   (order.OrderStatus ?? string.Empty).ToLowerInvariant().Contains(search) ||
                   (order.SupplierName ?? string.Empty).ToLowerInvariant().Contains(search) ||
                   (order.CustomerName ?? string.Empty).ToLowerInvariant().Contains(search) ||
                   (order.SourceWarehouseName ?? string.Empty).ToLowerInvariant().Contains(search) ||
                   (order.DestinationWarehouseName ?? string.Empty).ToLowerInvariant().Contains(search) ||
                   order.SubTotalAmount.ToString().Contains(search) ||
                   order.DiscountAmount.ToString().Contains(search) ||
                   order.NetAmount.ToString().Contains(search);
        }

        private IEnumerable<OrderForListDto> ApplySorting(IEnumerable<OrderForListDto> query)
        {
            switch (cmbOrderBy.GetSelectedItemName())
            {
                case "OrderType":
                    return cmbOrderBy.SortData<OrderForListDto>(query, o => o.OrderType);

                case "OrderStatus":
                    return cmbOrderBy.SortData<OrderForListDto>(query, o => o.OrderStatus);

                case "SupplierName":
                    return cmbOrderBy.SortData<OrderForListDto>(query, o => o.SupplierName ?? string.Empty);

                case "CustomerName":
                    return cmbOrderBy.SortData<OrderForListDto>(query, o => o.CustomerName ?? string.Empty);

                case "SourceWarehouseName":
                    return cmbOrderBy.SortData<OrderForListDto>(query, o => o.SourceWarehouseName ?? string.Empty);

                case "DestinationWarehouseName":
                    return cmbOrderBy.SortData<OrderForListDto>(query, o => o.DestinationWarehouseName ?? string.Empty);

                case "SubTotalAmount":
                    return cmbOrderBy.SortData<OrderForListDto>(query, o => o.SubTotalAmount);

                case "DiscountAmount":
                    return cmbOrderBy.SortData<OrderForListDto>(query, o => o.DiscountAmount);

                case "NetAmount":
                    return cmbOrderBy.SortData<OrderForListDto>(query, o => o.NetAmount);

                case "CreatedAt":
                    return cmbOrderBy.SortData<OrderForListDto>(query, o => o.CreatedAt);

                case "UpdatedAt":
                    return cmbOrderBy.SortData<OrderForListDto>(query, o => o.UpdatedAt);

                default:
                    return cmbOrderBy.SortData<OrderForListDto>(query, o => o.DueDate);
            }
        }

        #endregion

        #region View

        private void ApplyCurrentView()
        {
            if (_suppressFilterEvents)
                return;

            dgvOrders.SetData(ApplyLocalFilters());

            var grid = dgvOrders.DgvCustom;

            grid.HideColumns("Id", "SupplierId", "CustomerId", "InvoiceId",
                             "SourceWarehouseId", "DestinationWarehouseId");

            ApplyOrderTypeColumns(grid);

            grid.SetColumnHeaders(new Dictionary<string, string>
            {
                { "OrderType", "Type" },
                { "OrderStatus", "Status" },
                { "SupplierName", "Supplier" },
                { "CustomerName", "Customer" },
                { "SourceWarehouseName", "Source Warehouse" },
                { "DestinationWarehouseName", "Destination Warehouse" },
                { "SubTotalAmount", "Sub Total" },
                { "DiscountAmount", "Discount" },
                { "NetAmount", "Net" },
                { "DueDate", "Due Date" },
                { "CreatedAt", "Created" },
                { "UpdatedAt", "Updated" }
            });

            grid.FormatColumnsAsCurrency("SubTotalAmount", "DiscountAmount", "NetAmount");
            grid.FormatColumnsAsDateTime("DueDate", "CreatedAt", "UpdatedAt");
        }

        private void ApplyOrderTypeColumns(UI.Shared.Controllers.DgvCustom grid)
        {
            if (_orderType != null)
                grid.HideColumn("OrderType");

            if (_orderType == OrderType.Sale || _orderType == OrderType.ReturnIn)
            {
                grid.HideColumns("SupplierName", "DestinationWarehouseName");
                return;
            }

            if (_orderType == OrderType.Purchase || _orderType == OrderType.ReturnOut)
            {
                grid.HideColumns("CustomerName", "DestinationWarehouseName");
                return;
            }

            if (_orderType == OrderType.Transfer)
                grid.HideColumns("CustomerName", "SupplierName",
                                 "NetAmount", "DiscountAmount", "SubTotalAmount");
        }

        private OrderForListDto GetSelectedOrder()
        {
            return dgvOrders.DgvCustom.GetSelectedItem<OrderForListDto>();
        }

        private OrderForListDto RequireSelectedOrder()
        {
            var selected = GetSelectedOrder();

            if (selected == null)
                MessageBox.Show("Please select a transaction first.", "No Selection",
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
            using (var frm = new frmTransactionEditor())
            {
                if (_orderType != null)
                    frm.SetOrderType(_orderType.Value);

                if (frm.ShowDialog(this) == DialogResult.OK)
                    _ = dgvOrders.LoadDataGridViewData();
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            var selected = RequireSelectedOrder();

            if (selected == null)
                return;

            using (var frm = new frmTransactionDetails(selected.Id))
            {
                frm.ShowDialog(this);
            }

            _ = dgvOrders.LoadDataGridViewData();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var selected = RequireSelectedOrder();

            if (selected == null)
                return;

            using (var frm = new frmTransactionEditor(selected.Id))
            {
                if (frm.ShowDialog(this) == DialogResult.OK)
                    _ = dgvOrders.LoadDataGridViewData();
            }
        }

        private async void btnCancelOrder_Click(object sender, EventArgs e)
        {
            var selected = RequireSelectedOrder();

            if (selected == null)
                return;

            if (selected.OrderStatus == OrderStatus.Cancelled.ToString())
            {
                MessageBox.Show("This transaction is already cancelled.", "Not Allowed",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirm = MessageBox.Show(
                "Are you sure you want to cancel this transaction?",
                "Confirm Cancellation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes)
                return;

            await ChangeStatus(btnCancel, selected.Id, OrderStatus.Cancelled);
        }

        private async void btnCompleteOrder_Click(object sender, EventArgs e)
        {
            var selected = RequireSelectedOrder();

            if (selected == null)
                return;

            if (selected.OrderStatus == OrderStatus.Completed.ToString())
            {
                MessageBox.Show("This transaction is already completed.", "Not Allowed",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirm = MessageBox.Show(
                "Are you sure you want to complete this transaction?",
                "Confirm Completion",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes)
                return;

            await ChangeStatus(btnComplete, selected.Id, OrderStatus.Completed);
        }

        private async Task ChangeStatus(Button trigger, Guid orderId, OrderStatus status)
        {
            trigger.Enabled = false;

            var result = await OrdersServices.UpdateStatus(orderId, new UpdateOrderStatusRequest
            {
                Id = orderId,
                OrderStatus = status
            });

            trigger.Enabled = true;

            if (!result.IsSuccess)
            {
                MessageBox.Show(result.Title_Full, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            await dgvOrders.LoadDataGridViewData();
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            var selected = RequireSelectedOrder();

            if (selected == null)
                return;

            var confirm = MessageBox.Show(
                "Are you sure you want to delete this transaction?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes)
                return;

            btnDelete.Enabled = false;

            var result = await OrdersServices.Delete(selected.Id);

            btnDelete.Enabled = true;

            if (!result.IsSuccess)
            {
                MessageBox.Show(result.Title_Full, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            await dgvOrders.LoadDataGridViewData();
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            await dgvOrders.LoadDataGridViewData();
        }

        #endregion
    }
}
