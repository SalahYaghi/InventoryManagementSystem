using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace UI.Shared.Controllers
{
    public partial class DgvCustom
    {
        public const string EmptyCellPlaceholder = "—";

        public void HideColumns(params string[] columnNames)
        {
            if (columnNames == null)
                return;

            foreach (string columnName in columnNames)
                HideColumn(columnName);
        }

        public void ShowColumn(string columnName)
        {
            if (!string.IsNullOrEmpty(columnName) && dgv.Columns.Contains(columnName))
                dgv.Columns[columnName].Visible = true;
        }

        public void SetColumnHeaders(Dictionary<string, string> headers)
        {
            if (headers == null)
                return;

            foreach (var header in headers)
                SetColumnHeader(header.Key, header.Value);
        }

        public void SetColumnOrder(string columnName, int displayIndex)
        {
            if (string.IsNullOrEmpty(columnName) || !dgv.Columns.Contains(columnName))
                return;

            if (displayIndex < 0 || displayIndex >= dgv.Columns.Count)
                return;

            dgv.Columns[columnName].DisplayIndex = displayIndex;
        }

        public int GetSelectedItemPosition()
        {
            return dgv.SelectedRows.Count == 0 ? -1 : dgv.SelectedRows[0].Index;
        }

        public void FormatColumnsAsCurrency(params string[] columnNames)
        {
            ApplyToEach(columnNames, FormatColumnAsCurrency);
        }

        public void FormatColumnAsQuantity(string columnName)
        {
            ApplyColumnFormat(columnName, "#,##0.##");
        }

        public void FormatColumnsAsQuantity(params string[] columnNames)
        {
            ApplyToEach(columnNames, FormatColumnAsQuantity);
        }

        public void FormatColumnAsDate(string columnName)
        {
            ApplyColumnFormat(columnName, "dd MMM yyyy");
        }

        public void FormatColumnsAsDate(params string[] columnNames)
        {
            ApplyToEach(columnNames, FormatColumnAsDate);
        }

        public void FormatColumnAsDateTime(string columnName)
        {
            ApplyColumnFormat(columnName, "dd MMM yyyy  HH:mm");
        }

        public void FormatColumnsAsDateTime(params string[] columnNames)
        {
            ApplyToEach(columnNames, FormatColumnAsDateTime);
        }

        public void SetNullPlaceholder(string columnName, string placeholder = EmptyCellPlaceholder)
        {
            if (string.IsNullOrEmpty(columnName) || !dgv.Columns.Contains(columnName))
                return;

            dgv.Columns[columnName].DefaultCellStyle.NullValue = placeholder;
        }

        private void ApplyColumnFormat(string columnName, string format)
        {
            if (string.IsNullOrEmpty(columnName) || !dgv.Columns.Contains(columnName))
                return;

            DataGridViewColumn column = dgv.Columns[columnName];

            column.DefaultCellStyle.Format = format;
            column.DefaultCellStyle.NullValue = EmptyCellPlaceholder;
        }

        private void ApplyToEach(string[] columnNames, Action<string> action)
        {
            if (columnNames == null)
                return;

            foreach (string columnName in columnNames)
                action(columnName);
        }
    }
}
