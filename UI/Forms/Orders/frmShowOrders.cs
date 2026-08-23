using Contract.Responses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI.HttpClient;
using UI.Services;

namespace UI.Forms.Orders
{
    
        public partial class frmShowOrders : Form
        {
            private List<OrderForListDto> _allOrders = new List<OrderForListDto>();
            private bool _isLoadingFilters = false;

            private OrderType? orderType = null;
            public frmShowOrders(OrderType?  orderType = null)
            {
                InitializeComponent();
            this.orderType = orderType;
                SetupUI();
            }

            private async void frmShowOrders_Load(object sender, EventArgs e)
            {
                dgvOrders.SubscribeToLoadData(LoadData);
                await dgvOrders.LoadDataGridViewData();
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
                StyleButton(btnDelete, Color.FromArgb(220, 53, 69), Color.White);
                StyleButton(btnRefresh, Color.FromArgb(243, 246, 249), Color.FromArgb(24, 33, 45));
                StyleButton(btnCompeleted, Color.FromArgb(243, 246, 249), Color.FromArgb(24, 33, 45));
                StyleTextBox(txtSearch);
                //StyleComboBox(cmbOrderType);
                //StyleComboBox(cmbOrderStatus);
                //StyleComboBox(cmbOrderBy);

                //cmbOrderType.Items.Clear();
                //cmbOrderType.Items.AddRange(new object[]
                //{
                //"All",
                //"Purchase",
                //"Sale",
                //"Transfer"
                //});
                //cmbOrderType.SelectedIndex = 0;

                //cmbOrderStatus.Items.Clear();
                //cmbOrderStatus.Items.AddRange(new object[]
                //{
                //"All",
                //"Pending",
                //"Completed",
                //"Cancelled"
                //});
                //cmbOrderStatus.SelectedIndex = 0;

                //cmbOrderBy.Items.Clear();
                //cmbOrderBy.Items.AddRange(new object[]
                //{
                //"DueDate",
                //"OrderType",
                //"OrderStatus",
                //"SupplierName",
                //"CustomerName",
                //"SourceWarehouseName",
                //"DestinationWarehouseName",
                //"SubTotalAmount",
                //"DiscountAmount",
                //"NetAmount"
                //});
                //cmbOrderBy.SelectedIndex = 0;
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
                var result = await OrdersServices.GetAll(pageNo, pageSize ,orderType);

                if (!result.IsSuccess)
                {
                    MessageBox.Show(result.Title_Full, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return "Failed to load orders";
                }

                var data = result.Data;

                _allOrders = data == null
                    ? new List<OrderForListDto>()
                    : data.Items.Cast<OrderForListDto>().ToList();
            dgvOrders.SetData<OrderForListDto>(_allOrders);
                LoadFilterSources();
                ApplyCurrentView();

                return data;
            }

            private void LoadFilterSources()
            {

        string excludedDestinationWarehouseName = OrderType.Transfer != orderType ? "DestinationWarehouseName" : "" ;

            string excludedSupplierName = OrderType.Purchase != orderType ? "SupplierName" : "";

            string excludedCustomerName = OrderType.Sale != orderType ? "CustomerName" : "";


            cmbOrderBy.LoadData(dgvOrders.DgvCustom.GetColumnNamesExcept(new HashSet<string>() { 
                           "OrderType" ,"InvoiceId" , "SourceWarehouseId" ,"DestinationWarehouseId",
            "CustomerId"  , "SupplierId","Id" , excludedCustomerName   
            , excludedSupplierName , excludedDestinationWarehouseName ,
            }));


            cmbOrderStatus.LoadData<OrderForListDto>(_allOrders , o => o.OrderStatus);

            cmbOrderStatus.IndexChanged += ApplyCurrentView;
            cmbOrderBy.IndexChanged += ApplyCurrentView;

            _isLoadingFilters = false;
            }

            private List<OrderForListDto> ApplyLocalFilters()
            {
                IEnumerable<OrderForListDto> query = _allOrders;

                string search = txtSearch.Text.Trim().ToLower();

                if (!string.IsNullOrWhiteSpace(search))
                {
                query = query.Where(o =>
                    (o.SubTotalAmount.ToString() ?? "").ToLower().Contains(search) ||
                    (o.DiscountAmount.ToString() ?? "").ToLower().Contains(search) ||
                    (o.NetAmount.ToString() ?? "").ToLower().Contains(search) ||
                    (o.OrderType ?? "").ToLower().Contains(search) ||
                    (o.OrderStatus ?? "").ToLower().Contains(search) ||
                    (o.SupplierName ?? "").ToLower().Contains(search) ||
                    (o.CustomerName ?? "").ToLower().Contains(search) ||
                    (o.SourceWarehouseName ?? "").ToLower().Contains(search) ||
                    (o.DestinationWarehouseName ?? "").ToLower().Contains(search));
                }

        
            switch (cmbOrderBy.GetSelectedItemName())
            {
                case "OrderType":
                    query = cmbOrderBy.SortData<OrderForListDto>(query, o => o.OrderType);
                    break;

                case "OrderStatus":
                    query = cmbOrderBy.SortData<OrderForListDto>(query, o => o.OrderStatus);
                    break;

                case "SupplierName":
                    query = cmbOrderBy.SortData<OrderForListDto>(query, o => o.SupplierName);
                    break;

                case "CustomerName":
                    query = cmbOrderBy.SortData<OrderForListDto>(query, o => o.CustomerName);
                    break;

                case "SourceWarehouseName":
                    query = cmbOrderBy.SortData<OrderForListDto>(query, o => o.SourceWarehouseName);
                    break;

                case "DestinationWarehouseName":
                    query = cmbOrderBy.SortData<OrderForListDto>(query, o => o.DestinationWarehouseName);
                    break;

                case "SubTotalAmount":
                    query = cmbOrderBy.SortData<OrderForListDto>(query, o => o.SubTotalAmount);
                    break;

                case "DiscountAmount":
                    query = cmbOrderBy.SortData<OrderForListDto>(query, o => o.DiscountAmount);
                    break;

                case "NetAmount":
                    query = cmbOrderBy.SortData<OrderForListDto>(query, o => o.NetAmount);
                    break;

                default:
                    query = cmbOrderBy.SortData<OrderForListDto>(query, o => o.DueDate);
                    break;
            }


            query = cmbOrderStatus.FilterData<OrderForListDto>(query , o => o.OrderStatus == cmbOrderStatus.GetSelectedItemName());

                return query.ToList();
            }

            private void ApplyCurrentView()
            {
                var orders = ApplyLocalFilters();

                dgvOrders.SetData(orders);

                dgvOrders.DgvCustom.HideColumn("Id");
                dgvOrders.DgvCustom.HideColumn("SupplierId");
                dgvOrders.DgvCustom.HideColumn("CustomerId");
                dgvOrders.DgvCustom.HideColumn("InvoiceId");
                dgvOrders.DgvCustom.HideColumn("SourceWarehouseId");
                dgvOrders.DgvCustom.HideColumn("DestinationWarehouseId");
          
            if(orderType!= null)
            dgvOrders.DgvCustom.HideColumn("OrderType");

            if (orderType == OrderType.Sale || orderType == OrderType.ReturnIn)
            {
                dgvOrders.DgvCustom.HideColumn("SupplierName");
                dgvOrders.DgvCustom.HideColumn("DestinationWarehouseName");

            }
            else if(orderType == OrderType.Purchase || orderType == OrderType.ReturnOut){
                dgvOrders.DgvCustom.HideColumn("CustomerName");
                dgvOrders.DgvCustom.HideColumn("DestinationWarehouseName");

            }
            else if (orderType == OrderType.Transfer)
            {
                dgvOrders.DgvCustom.HideColumn("CustomerName");
                dgvOrders.DgvCustom.HideColumn("SupplierName");
                dgvOrders.DgvCustom.HideColumn("NetAmount");
                dgvOrders.DgvCustom.HideColumn("DiscountAmount");
                dgvOrders.DgvCustom.HideColumn("SubTotalAmount");


            }

            dgvOrders.DgvCustom.SetColumnHeader("OrderStatus", "Status");
                dgvOrders.DgvCustom.SetColumnHeader("SourceWarehouseName", "Source Warehouse");
                dgvOrders.DgvCustom.SetColumnHeader("SubTotalAmount", "Sub Total");
                dgvOrders.DgvCustom.SetColumnHeader("DiscountAmount", "Discount");
                dgvOrders.DgvCustom.SetColumnHeader("NetAmount", "Net");
                dgvOrders.DgvCustom.SetColumnHeader("DueDate", "Due Date");
            }

            private OrderForListDto GetSelectedOrder()
            {
                return dgvOrders.DgvCustom.GetSelectedItem<OrderForListDto>();
            }

            private void txtSearch_TextChanged(object sender, EventArgs e)
            {
                ApplyCurrentView();
            }

            private void cmbOrderType_SelectedIndexChanged(object sender, EventArgs e)
            {
                if (_isLoadingFilters) return;
                ApplyCurrentView();
            }

            private void cmbOrderStatus_SelectedIndexChanged(object sender, EventArgs e)
            {
                if (_isLoadingFilters) return;
                ApplyCurrentView();
            }

            private void cmbOrderBy_SelectedIndexChanged(object sender, EventArgs e)
            {
                ApplyCurrentView();
            }

            private void btnAdd_Click(object sender, EventArgs e)
            {
                using (var frm = new frmTransactionEditor())
                {
                if(orderType != null)
                frm.SetOrderType(orderType.Value);
                    if (frm.ShowDialog() == DialogResult.OK)
                        _ = dgvOrders.LoadDataGridViewData();
                }
            }

            private void btnView_Click(object sender, EventArgs e)
            {
                var selected = GetSelectedOrder();

                if (selected == null)
                {
                    MessageBox.Show("Please select an order first.");
                    return;
                }

            using (var frm = new frmTransactionDetails(selected.Id))
            {
                frm.ShowDialog();
            }
        }

            private void btnEdit_Click(object sender, EventArgs e)
            {
                var selected = GetSelectedOrder();

                if (selected == null)
                {
                    MessageBox.Show("Please select an order first.");
                    return;
                }

                using (var frm = new frmTransactionEditor(selected.Id))
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                        _ = dgvOrders.LoadDataGridViewData();
                }
            }

            private async void btnStatus_Click(object sender, EventArgs e)
            {
                var selected = GetSelectedOrder();

                if (selected == null)
                {
                    MessageBox.Show("Please select an order first.");
                    return;
                }
           

            var confirm = MessageBox.Show(
                   "Are you sure you want to cancel this order?",
                   "Confirm Cancelation",
                   MessageBoxButtons.YesNo,
                   MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes)
                return;

            btnDelete.Enabled = false;

            var result = await OrdersServices.UpdateStatus(selected.Id, new Contract.Requests.Orders.UpdateOrderStatusRequest()
            {
                Id = selected.Id,
                OrderStatus = OrderStatus.Cancelled
            });

            btnDelete.Enabled = true;

            if (!result.IsSuccess)
            {
                MessageBox.Show(result.Title_Full, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            await dgvOrders.LoadDataGridViewData();

        }

        private async void btnDelete_Click(object sender, EventArgs e)
            {
                var selected = GetSelectedOrder();

                if (selected == null)
                {
                    MessageBox.Show("Please select an order first.");
                    return;
                }

                var confirm = MessageBox.Show(
                    "Are you sure you want to delete this order?",
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

        private async void btnCompeleted_Click(object sender, EventArgs e)
        {

            var selected = GetSelectedOrder();

            if (selected == null)
            {
                MessageBox.Show("Please select an order first.");
                return;
            }


            var confirm = MessageBox.Show(
                   "Are you sure you want to compelete this order?",
                   "Confirm Compeletion",
                   MessageBoxButtons.YesNo,
                   MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes)
                return;

            btnDelete.Enabled = false;

            var result = await OrdersServices.UpdateStatus(selected.Id, new Contract.Requests.Orders.UpdateOrderStatusRequest()
            {
                Id = selected.Id,
                OrderStatus = OrderStatus.Completed
            });

            btnDelete.Enabled = true;

            if (!result.IsSuccess)
            {
                MessageBox.Show(result.Title_Full, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            await dgvOrders.LoadDataGridViewData();
        }
    }
    }

