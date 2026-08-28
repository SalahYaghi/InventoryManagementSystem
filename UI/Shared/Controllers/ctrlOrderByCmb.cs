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
    public partial class ctrlOrderByCmb : UserControl
    {
        public event Action IndexChanged;

        public string Title
        {
            set => lblTitle.Text = value; get => lblTitle.Text;
        }

        public ctrlOrderByCmb()
        {
            InitializeComponent();
            SetupUI();  

        }

        private void SetupUI() { 
        
            this.cmbData.Items.Clear();
            this.cmbSortDirection.Items.Clear();
            this.cmbSortDirection.Items.Add("ASC");
            this.cmbSortDirection.Items.Add("DESC");
            this.cmbSortDirection.SelectedIndex = 0;
        }

        public void LoadData(List<string> columns)
        {
            this.cmbData.DataSource = columns;
            if(columns.Count > 0 ) 
            this.cmbData.SelectedIndex = 0;
        }
        public string GetSelectedItemName()
        {

            if (cmbData.SelectedIndex < 0)
                return string.Empty;

            return cmbData.SelectedItem as string;
        }

        public bool IsAsc() {

            string dir = this.cmbSortDirection.Text.ToLowerInvariant();
            return dir == "asc";
        }
        
        public IEnumerable<T> SortData<T>(IEnumerable<T> data, Func<T, object> prediator)
        {

            string SelectedItem = GetSelectedItemName();

            if (string.IsNullOrEmpty(SelectedItem) ||
                SelectedItem.ToLowerInvariant() == "all")
                return data;

            return  IsAsc()  ?  data.OrderBy(prediator)
                : data.OrderByDescending(prediator);
        }


        private void cmbSortChaned_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IndexChanged == null) return;
            IndexChanged?.Invoke();

        }



    }
}

