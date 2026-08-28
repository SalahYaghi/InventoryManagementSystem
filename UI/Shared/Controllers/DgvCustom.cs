using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI.Shared.Controllers
{
        public partial class DgvCustom : UserControl
        {
            private int _recordCount;

        public DataGridView dgv => this.dgvData;
            public DgvCustom()
            {
                InitializeComponent();
                SetupGrid();
            }

            [Browsable(false)]
            public DataGridView Grid => dgvData;

            [Browsable(false)]
            public int RecordCount
            {
                get => _recordCount;
                private set
                {
                    _recordCount = value;
                    lblRecordCount.Text = $"Records: {_recordCount}";
                }
            }

            private void SetupGrid()
            {
                dgvData.ReadOnly = true;
                dgvData.AllowUserToAddRows = false;
                dgvData.AllowUserToDeleteRows = false;
                dgvData.AllowUserToResizeRows = false;
                dgvData.AllowUserToResizeColumns = false;
                dgvData.MultiSelect = false;
                dgvData.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvData.RowHeadersVisible = false;
                dgvData.BorderStyle = BorderStyle.None;
                dgvData.BackgroundColor = Color.White;
                dgvData.GridColor = Color.FromArgb(229, 236, 242);

                dgvData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvData.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
                dgvData.RowTemplate.Height = 30;

                dgvData.EnableHeadersVisualStyles = false;

                dgvData.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                dgvData.ColumnHeadersHeight = 36;
                dgvData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

                dgvData.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(24, 33, 45);
                dgvData.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dgvData.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 8F, FontStyle.Bold);
                dgvData.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgvData.ColumnHeadersDefaultCellStyle.Padding = new Padding(10, 0, 0, 0);

                dgvData.DefaultCellStyle.BackColor = Color.White;
                dgvData.DefaultCellStyle.ForeColor = Color.FromArgb(31, 41, 55);
                dgvData.DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
                dgvData.DefaultCellStyle.SelectionBackColor = Color.FromArgb(219, 230, 241);
                dgvData.DefaultCellStyle.SelectionForeColor = Color.FromArgb(24, 33, 45);
                dgvData.DefaultCellStyle.Padding = new Padding(10, 0, 0, 0);

                dgvData.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
                dgvData.AlternatingRowsDefaultCellStyle.ForeColor = Color.FromArgb(31, 41, 55);
                dgvData.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(219, 230, 241);
                dgvData.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.FromArgb(24, 33, 45);

                dgvData.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                dgvData.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            dgvData.EnableHeadersVisualStyles = false;

            dgvData.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvData.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvData.ColumnHeadersDefaultCellStyle.SelectionBackColor = dgvData.ColumnHeadersDefaultCellStyle.BackColor;
        }

           

            public void SetData<T>(List<T> data)
            {
                dgvData.DataSource = null;

                if (data == null || data.Count == 0)
                {
                    RecordCount = 0;
                    return;
                }

                dgvData.DataSource = data;
                RecordCount = data.Count;
            }

            public void SetData<T>(IEnumerable<T> data)
            {
                SetData(data?.ToList());
            }

            public T GetSelectedItem<T>() where T : class
            {
                if (dgvData.SelectedRows.Count == 0)
                    return null;

                return dgvData.SelectedRows[0].DataBoundItem as T;
            }

        public int GetselectedItemPosition() { 

            return dgvData.SelectedRows.Count == 0 ? 0 : dgvData.SelectedRows[0].Index;
        }
        public void SetAsSelected(int index) {

            if (index < 0 || dgvData.Rows.Count - 1 < index) return;

            dgvData.Rows[index].Selected = true;

        }
            public DataGridViewRow GetSelectedItem()
            {
                if (dgvData.CurrentRow == null)
                    return null;

                return dgvData.CurrentRow;
            }

            public void Clear()
            {
                dgvData.DataSource = null;
                RecordCount = 0;
            }

            public void HideColumn(string columnName)
            {
                if (dgvData.Columns.Contains(columnName))
                    dgvData.Columns[columnName].Visible = false;
            }

            public void SetColumnHeader(string columnName, string headerText)
            {
                if (dgvData.Columns.Contains(columnName))
                    dgvData.Columns[columnName].HeaderText = headerText;
            }

            public void SetColumnWidth(string columnName, int width)
            {
                if (dgvData.Columns.Contains(columnName))
                {
                    dgvData.Columns[columnName].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    dgvData.Columns[columnName].Width = width;
                }
            }

            public void FormatColumnAsCurrency(string columnName)
            {
                if (dgvData.Columns.Contains(columnName))
                    dgvData.Columns[columnName].DefaultCellStyle.Format = "$#,##0.00";
            }

        public void SetDefaultValueForNulls(string columnName, object defaultValue)
        {
            if (!dgvData.Columns.Contains(columnName))
                return;

            foreach (DataGridViewRow row in dgvData.Rows)
            {
                if (row.IsNewRow)
                    continue;

                var cell = row.Cells[columnName];

                if (cell.Value == null || cell.Value == DBNull.Value)
                    cell.Value = defaultValue;
            }
        }

        public List<string> GetColumnNames()
        {
            return this.dgvData.Columns
                      .Cast<DataGridViewColumn>()
                      .Select(c => c.Name)
                      .ToList();
        }
        public List<string> GetColumnNamesExcept(HashSet<string> cols)
        {
            return this.dgvData.Columns
                      .Cast<DataGridViewColumn>()
                      .Select(c => c.Name)
                      .Where(c => !cols.Contains(c))
                      .ToList();
        }

        private void dgvData_DataBindingComplete_1(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvData.ClearSelection();

            foreach (DataGridViewColumn column in dgvData.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.Resizable = DataGridViewTriState.False;
            }
        }

    }
    }
