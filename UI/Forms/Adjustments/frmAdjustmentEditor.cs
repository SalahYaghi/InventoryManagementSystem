using Contract.Requests.Adjustment;
using Contract.Requests.Adjustments;
using Contract.Requests.Orders;
using Contract.Responses;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI.Forms.Products;
using UI.Forms.Warehouses;
using UI.Services;
using UI.Shared.CurrentUser;

namespace UI.Forms.Adjustments
{
    public partial class frmAdjustmentEditor : Form
    {
        private readonly bool _isUpdateMode;
        private readonly Guid _adjustmentId;

        private Guid? _warehouseId = Guid.Empty;
        private readonly List<AdjustmentDetailDto> _details = new List<AdjustmentDetailDto>();

        public frmAdjustmentEditor()
        {
            InitializeComponent();
            _isUpdateMode = false;
            SetupUI();
        }

        public frmAdjustmentEditor(Guid adjustmentId)
        {
            InitializeComponent();
            _adjustmentId = adjustmentId;
            _isUpdateMode = true;
            SetupUI();
        }

        private async void frmAdjustmentEditor_Load(object sender, EventArgs e)
        {
            if (_isUpdateMode)
                await LoadAdjustment();
        }

        private void SetupUI()
        {
            BackColor = Color.FromArgb(243, 246, 249);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            MaximizeBox = false;
            MinimizeBox = false;

            StyleButton(btnSelectWarehouse, Color.FromArgb(248, 250, 252), Color.FromArgb(74, 112, 139));
            StyleButton(btnAddDetail, Color.FromArgb(74, 112, 139), Color.White);
            StyleButton(btnRemoveDetail, Color.FromArgb(220, 53, 69), Color.White);
            StyleButton(btnUpdateQuantity, Color.FromArgb(74, 112, 139), Color.White);
            StyleButton(btnSave, Color.FromArgb(74, 112, 139), Color.White);
            StyleButton(btnCancel, Color.FromArgb(243, 246, 249), Color.FromArgb(24, 33, 45));

            StyleTextBox(txtWarehouse);
            StyleTextBox(txtQuantity);
            StyleTextBox(txtNotes);

            txtWarehouse.ReadOnly = true;

            cmbReason.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbReason.DataSource = Enum.GetValues(typeof(AdjustmentReason));

            cmbType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbType.DataSource = Enum.GetValues(typeof(AdjustmentType));

            lblTitle.Text = _isUpdateMode ? "Update Adjustment" : "Create Adjustment";
            lblSubtitle.Text = _isUpdateMode
                ? "Update adjustment notes and manage draft details."
                : "Create an inventory correction for your warehouse.";

            if (CurrentUser.User != null && CurrentUser.User.Employee != null && CurrentUser.User.Employee.WarehouseId.HasValue)
            {
                _warehouseId = CurrentUser.User.Employee.WarehouseId.Value;
                txtWarehouse.Text = CurrentUser.User.Employee.Warehouse == null ? "" : CurrentUser.User.Employee.Warehouse.Name;
            }

            btnUpdateQuantity.Visible = _isUpdateMode;
            btnUpdateQuantity.Enabled = _isUpdateMode;

            ApplyReasonTypeRules();
            BindDetailsGrid();
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

        private async Task LoadAdjustment()
        {
            lblStatus.Text = "Loading adjustment...";

            var result = await AdjustmentsServices.Get(_adjustmentId);

            if (!result.IsSuccess)
            {
                lblStatus.Text = "Failed to load adjustment";
                MessageBox.Show(result.Title_Full, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var adjustment = result.Data;

            _warehouseId = adjustment.WarehouseId;
            txtWarehouse.Text = adjustment.Warehouse == null ? "" : adjustment.Warehouse.Name;

            cmbReason.SelectedItem = adjustment.AdjustmentReason;
            cmbType.SelectedItem = adjustment.AdjustmentType;
            txtNotes.Text = adjustment.Notes ?? "";

            bool isDraft = adjustment.AdjustmentStatus == AdjustmentStatus.Draft;

            cmbReason.Enabled = false;
            cmbType.Enabled = false;
            btnSelectWarehouse.Enabled = false;

            btnAddDetail.Enabled = isDraft;
            btnRemoveDetail.Enabled = isDraft;
            btnUpdateQuantity.Enabled = isDraft;
            txtQuantity.Enabled = isDraft;
            txtNotes.Enabled = isDraft;

            _details.Clear();

            if (adjustment.AdjustmentDetailDtos != null)
                _details.AddRange(adjustment.AdjustmentDetailDtos);

            ApplyReasonTypeRules();
            BindDetailsGrid();

            lblStatus.Text = "Ready";
        }

        private AdjustmentReason SelectedReason
        {
            get { return (AdjustmentReason)cmbReason.SelectedItem; }
        }

        private AdjustmentType SelectedType
        {
            get { return (AdjustmentType)cmbType.SelectedItem; }
        }

        private decimal SelectedQuantity
        {
            get
            {
                if (decimal.TryParse(txtQuantity.Text, out decimal result))
                    return result;

                return 0;
            }
        }

        private void ApplyReasonTypeRules()
        {
            if (cmbReason.SelectedItem == null)
                return;

            var reason = SelectedReason;

            if (reason == AdjustmentReason.Damaged || reason == AdjustmentReason.Lost || reason == AdjustmentReason.Expired)
            {
                cmbType.SelectedItem = AdjustmentType.Decrease;
                cmbType.Enabled = false;
                lblHint.Text = "This reason automatically decreases warehouse stock.";
            }
            else if (reason == AdjustmentReason.ExtraFound)
            {
                cmbType.SelectedItem = AdjustmentType.Increase;
                cmbType.Enabled = false;
                lblHint.Text = "Extra found stock automatically increases warehouse stock.";
            }
            else
            {
                if(!_isUpdateMode)
                cmbType.Enabled = true;
                lblHint.Text = "Other reason requires you to choose increase or decrease.";
            }
        }

        private bool ValidateForm()
        {
            errorProvider.Clear();

            bool valid = true;

            if (_warehouseId == null || _warehouseId == Guid.Empty)
            {
                errorProvider.SetError(txtWarehouse, "Warehouse is required.");
                valid = false;
            }

            if (!_isUpdateMode && _details.Count == 0)
            {
                MessageBox.Show("Please add at least one adjustment detail.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                valid = false;
            }

            if (txtNotes.Text.Trim().Length > 500)
            {
                errorProvider.SetError(txtNotes, "Notes must not exceed 500 characters.");
                valid = false;
            }

            return valid;
        }

        private CreateAdjustmentRequest BuildCreateRequest()
        {
            return new CreateAdjustmentRequest
            {
                WarehouseId = _warehouseId.Value,
                AdjustmentReason = SelectedReason,
                AdjustmentType = SelectedType,
                Notes = txtNotes.Text.Trim(),
                AdjustmentDetails = _details.Select(d => new CreateAdjustmentDetailRequestInner
                {
                    ProductId = d.ProductId,
                    Quantity = d.Quantity,
                    RowVersion = d.RowVersion
                }).ToList()
            };
        }

        private UpdateAdjustmentRequest BuildUpdateRequest()
        {
            return new UpdateAdjustmentRequest
            {
                Id = _adjustmentId,
                Notes = txtNotes.Text.Trim()
            };
        }

        private void BindDetailsGrid()
        {
        
        
     
            dgvDetails.SetData(_details);
                      dgvDetails.HideColumn("RowVersion");
            dgvDetails.HideColumn("Id");
            dgvDetails.HideColumn("ProductId");
                        dgvDetails.HideColumn("AdjustmentId");
            dgvDetails.HideColumn("Product");

            }

        private AdjustmentDetailDto GetSelectedDetail()
        {
            return dgvDetails.GetSelectedItem<AdjustmentDetailDto>();
        }

        private List<Guid> SelectedProducts()
        {
            return _details.Select(x => x.ProductId).ToList();
        }

        private frmProductSelector MakeProductSelectorForm()
        {
            frmProductSelector frm = new frmProductSelector();

            frm.ExcludeProducts(SelectedProducts());

            if (_warehouseId == null || _warehouseId == Guid.Empty)
            {
                errorProvider.SetError(txtWarehouse, "Warehouse is required.");
                return null;
            }

            frm.FromWarehouse(_warehouseId);

            return frm;
        }

        private void cmbReason_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyReasonTypeRules();
        }

        private void btnSelectWarehouse_Click(object sender, EventArgs e)
        {
            using (var frm = new frmWarehouseSelector())
            {
                if (frm.ShowDialog() != DialogResult.OK)
                    return;

                _warehouseId = frm.SelectedWarehouse.Id;
                txtWarehouse.Text = frm.SelectedWarehouse.Name;
            }
        }

        private async void btnAddDetail_Click(object sender, EventArgs e)
        {
            var form = MakeProductSelectorForm();
            if (form == null) return;

            using (var frm = form)
            {
                if (frm.ShowDialog() != DialogResult.OK)
                    return;

                var product = frm.SelectedProduct;

                if (product == null)
                    return;

                decimal quantity = SelectedQuantity;

                if (quantity <= 0)
                {
                    MessageBox.Show("Invalid quantity defined.");
                    return;
                }

                var detail = new AdjustmentDetailDto
                {
                    RowVersion = product.RowVersion,
                    ProductId = product.Id,
                    Product = new ProductDto()
                    {
                        Id = product.Id,
                        ProductName = product.ProductName,
                        BarCode = product.BarCode,
                        IsActive = product.IsActive,
                        SKU = product.SKU,
                        SellingPrice = product.SellingPrice,
                        
                    },
                    Quantity = quantity , 
                    
                };

                if (_isUpdateMode)
                {
                    var result = await AdjustmentsServices.CreateAdjustmentDetail(new
                        CreateAdjustmentDetailRequest
                    {
                        AdjustmentId = _adjustmentId,
                        ProductId = product.Id,
                        Quantity = quantity,
                        RowVersion = product.RowVersion
                    });

                    if (!result.IsSuccess)
                    {
                        lblStatus.Text = "Failed to add detail";
                        MessageBox.Show(result.Title_Full, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    detail.Id = result.Data.Id;
                    detail.ProductId = result.Data.ProductId;
                    detail.Product = result.Data.Product;
                    detail.Quantity = result.Data.Quantity;
                    detail.RowVersion = result.Data.RowVersion;
                    
                }

                _details.Add(detail);
                BindDetailsGrid();
            }
        }

        private async void btnRemoveDetail_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedDetail();

            if (selected == null)
            {
                MessageBox.Show("Please select a detail first.");
                return;
            }

            if (_isUpdateMode)
            {
                var confirm = MessageBox.Show(
                    $"Are you sure you want to delete {selected.Product.ProductName}?",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirm != DialogResult.Yes)
                    return;

                btnRemoveDetail.Enabled = false;

                var result = await AdjustmentsServices.DeleteAdjustmentDetail(selected.Id);

                btnRemoveDetail.Enabled = true;

                if (!result.IsSuccess)
                {
                    MessageBox.Show(result.Title_Full, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            _details.Remove(selected);
            BindDetailsGrid();
        }

        private async void btnUpdateQuantity_Click(object sender, EventArgs e)
        {
            if (!_isUpdateMode)
                return;

            var selected = GetSelectedDetail();

            if (selected == null)
            {
                MessageBox.Show("Please select a detail first.");
                return;
            }

            decimal quantity = SelectedQuantity;

            if (quantity <= 0)
            {
                MessageBox.Show("Invalid quantity selected.");
                return;
            }

            var result = await AdjustmentsServices.UpdateAdjustmentDetailQuantity(
                selected.Id,
                new UpdateAdjustmentDetailQuantityRequest
                {
                    Quantity = quantity,
                    RowVersion = selected.RowVersion
                });

            if (!result.IsSuccess)
            {
                lblStatus.Text = "Failed to update quantity";
                MessageBox.Show(result.Title_Full, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            selected.Quantity = quantity;
            BindDetailsGrid();
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateForm())
                return;

            btnSave.Enabled = false;
            lblStatus.Text = "Saving adjustment...";

            if (_isUpdateMode)
            {
                var result = await AdjustmentsServices.Update(_adjustmentId, BuildUpdateRequest());

                btnSave.Enabled = true;

                if (!result.IsSuccess)
                {
                    lblStatus.Text = "Failed to update adjustment";
                    MessageBox.Show(result.Title_Full, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                var result = await AdjustmentsServices.Create(BuildCreateRequest());

                btnSave.Enabled = true;

                if (!result.IsSuccess)
                {
                    lblStatus.Text = "Failed to create adjustment";
                    MessageBox.Show(result.Title_Full, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            lblStatus.Text = "Saved successfully";
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            if (_isUpdateMode) return;
            var selected = GetSelectedDetail();
            var index = dgvDetails.GetselectedItemPosition();
            if (selected == null)
            {
                return;
            }

            selected.Quantity = SelectedQuantity;
            BindDetailsGrid();
            dgvDetails.SetAsSelected(index);
        }
    }
}

