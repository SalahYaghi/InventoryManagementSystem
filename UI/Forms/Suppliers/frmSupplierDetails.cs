using Contract.Responses;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI.Services;

namespace UI.Forms.Suppliers
{
    public partial class frmSupplierDetails : Form
    {
        private readonly Guid _supplierId;
        private SupplierDto _supplier;

        public frmSupplierDetails(Guid supplierId)
        {
            InitializeComponent();
            _supplierId = supplierId;
            SetupUI();
        }

        private async void frmSupplierDetails_Load(object sender, EventArgs e)
        {
            await LoadSupplier();
        }

        private void SetupUI()
        {
            BackColor = Color.FromArgb(243, 246, 249);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            MaximizeBox = false;
            MinimizeBox = false;

            StyleButton(btnEdit, Color.FromArgb(74, 112, 139), Color.White);
            StyleButton(btnClose, Color.FromArgb(243, 246, 249), Color.FromArgb(24, 33, 45));

            lblStatus.Text = "Loading...";
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

        private async Task LoadSupplier()
        {
            lblStatus.Text = "Loading supplier details...";

            var result = await SuppliersServices.Get(_supplierId);

            if (!result.IsSuccess)
            {
                lblStatus.Text = "Failed to load supplier";
                MessageBox.Show(result.Title_Full, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _supplier = result.Data;
            BindSupplier();

            lblStatus.Text = "Ready";
        }

        private void BindSupplier()
        {
            if (_supplier == null)
                return;

            lblSupplierName.Text = _supplier.SupplierName;
            lblSupplierCode.Text = "Code: " + _supplier.SupplierCode;

            txtNotes.Text = string.IsNullOrWhiteSpace(_supplier.Notes)
                ? "No notes provided."
                : _supplier.Notes;

            ctrlContactDetails1.LoadContact(_supplier.Contact);
            ctrlAddressDetails1.LoadAddress(_supplier.Address);

            if (_supplier.Status)
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

        private void btnEdit_Click(object sender, EventArgs e)
        {
            using (var frm = new frmSupplierEditor(_supplierId))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                    _ = LoadSupplier();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void panelBody_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

