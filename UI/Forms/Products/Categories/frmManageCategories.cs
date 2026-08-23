using Contract.Requests.Categories;
using Contract.Responses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI.Services;

namespace UI.Forms.Products.Categories
{
        public partial class frmManageCategories : Form
        {
            private List<CategoryDto> _categories = new List<CategoryDto>();

            public frmManageCategories()
            {
                InitializeComponent();
                SetupUI();
            }

            private async void frmManageCategories_Load(object sender, EventArgs e)
            {
                await LoadCategories();
            }

            private void SetupUI()
            {
                this.BackColor = Color.FromArgb(243, 246, 249);
                this.FormBorderStyle = FormBorderStyle.FixedDialog;
                this.StartPosition = FormStartPosition.CenterParent;
                this.MaximizeBox = false;
                this.MinimizeBox = false;

                StyleButton(btnAdd, Color.FromArgb(74, 112, 139), Color.White);
                StyleButton(btnUpdate, Color.FromArgb(243, 246, 249), Color.FromArgb(24, 33, 45));
                StyleButton(btnDelete, Color.FromArgb(220, 53, 69), Color.White);
                StyleButton(btnClear, Color.FromArgb(243, 246, 249), Color.FromArgb(24, 33, 45));
                StyleButton(btnRefresh, Color.FromArgb(243, 246, 249), Color.FromArgb(24, 33, 45));
                StyleButton(btnClose, Color.FromArgb(243, 246, 249), Color.FromArgb(24, 33, 45));

                lstCategories.DisplayMember = "Name";
                lblStatus.Text = "Ready";
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

            private async Task LoadCategories()
            {
                lblStatus.Text = "Loading categories...";

                var result = await CategoriesServices.GetAll();

                if (!result.IsSuccess)
                {
                    lblStatus.Text = "Failed to load categories";
                    MessageBox.Show(result.Title_Full, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                _categories = result.Data ?? new List<CategoryDto>();

                lstCategories.DataSource = null;
                lstCategories.DataSource = _categories;
                lstCategories.DisplayMember = "Name";

                lblStatus.Text = $"{_categories.Count} category(s) loaded";
            }

            private bool ValidateForm()
            {
                errorProvider.Clear();

                if (string.IsNullOrWhiteSpace(txtCategoryName.Text))
                {
                    errorProvider.SetError(txtCategoryName, "Category name is required.");
                    return false;
                }

                if (txtCategoryName.Text.Trim().Length > 50)
                {
                    errorProvider.SetError(txtCategoryName, "Category name must not exceed 50 characters.");
                    return false;
                }

                return true;
            }

            private CategoryDto SelectedCategory => lstCategories.SelectedItem as CategoryDto;

           
            private void ClearForm()
            {
                txtCategoryName.Clear();
                lstCategories.ClearSelected();
                errorProvider.Clear();
                lblStatus.Text = "Ready";
                txtCategoryName.Focus();
            }

            private async void btnAdd_Click(object sender, EventArgs e)
            {
                if (!ValidateForm())
                    return;

                btnAdd.Enabled = false;
                lblStatus.Text = "Creating category...";

                var request = new CreateCategoryRequest
                {
                    Name = txtCategoryName.Text.Trim()
                };

                var result = await CategoriesServices.Create(request);

                btnAdd.Enabled = true;

                if (!result.IsSuccess)
                {
                    lblStatus.Text = "Failed to create category";
                    MessageBox.Show(result.Title_Full, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                ClearForm();
                await LoadCategories();
            }

            private async void btnUpdate_Click(object sender, EventArgs e)
            {
                if (SelectedCategory == null)
                {
                    MessageBox.Show("Please select a category first.");
                    return;
                }

                if (!ValidateForm())
                    return;

                Guid categoryId = (SelectedCategory).Id;

                if (categoryId == Guid.Empty)
                {
                    MessageBox.Show("Selected category id is invalid.");
                    return;
                }

                btnUpdate.Enabled = false;
                lblStatus.Text = "Updating category...";

                var request = new UpdateCategoryRequest
                {
                    Name = txtCategoryName.Text.Trim()
                };

                var result = await CategoriesServices.Update(categoryId, request);

                btnUpdate.Enabled = true;

                if (!result.IsSuccess)
                {
                    lblStatus.Text = "Failed to update category";
                    MessageBox.Show(result.Title_Full, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                ClearForm();
                await LoadCategories();
            }

            private async void btnDelete_Click(object sender, EventArgs e)
            {
                if (SelectedCategory == null)
                {
                    MessageBox.Show("Please select a category first.");
                    return;
                }

                Guid categoryId = (SelectedCategory).Id;

                if (categoryId == Guid.Empty)
                {
                    MessageBox.Show("Selected category id is invalid.");
                    return;
                }

                var confirm = MessageBox.Show(
                    "Are you sure you want to delete this category?",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirm != DialogResult.Yes)
                    return;

                btnDelete.Enabled = false;
                lblStatus.Text = "Deleting category...";

                var result = await CategoriesServices.Delete(categoryId);

                btnDelete.Enabled = true;

                if (!result.IsSuccess)
                {
                    lblStatus.Text = "Failed to delete category";
                    MessageBox.Show(result.Title_Full, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                ClearForm();
                await LoadCategories();
            }

            private void lstCategories_SelectedIndexChanged(object sender, EventArgs e)
            {
                if (SelectedCategory == null)
                    return;

                txtCategoryName.Text = SelectedCategory.Name;
            }

            private void btnClear_Click(object sender, EventArgs e)
            {
                ClearForm();
            }

            private async void btnRefresh_Click(object sender, EventArgs e)
            {
                ClearForm();
                await LoadCategories();
            }

            private void btnClose_Click(object sender, EventArgs e)
            {
                Close();
            }
        }
    }

