using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI.Shared.Controllers
{
    public partial class ctrlSortByCmb : UserControl
    {
        public event Action IndexChanged;

        public string Title
        {
            set => lblTitle.Text = value; get => lblTitle.Text;
              
        }

        public ctrlSortByCmb()
        {
            InitializeComponent();
        }

        public void LoadData<T>(IEnumerable<T> data , Func<T , object> selector) {

            
            var dataSource = 
                data.Select(selector)
                    .Where(d => d != null)
                    .Distinct().ToList();

            this.cmbData.Items.Clear();
            this.cmbData.Items.Add("All");
            foreach (var d in dataSource) { 
                cmbData.Items.Add(d.ToString());
            }
            this.cmbData.SelectedIndex = 0;
        }
        public string GetSelectedItemName() {

            if (cmbData.SelectedIndex < 0)
                return string.Empty;

            return cmbData.SelectedItem as string;
        }
        public IEnumerable<T> FilterData<T>(IEnumerable<T> data , Func<T , bool> prediator) {

            string SelectedItem = GetSelectedItemName();

            if(string.IsNullOrEmpty(SelectedItem) || 
                SelectedItem.ToLowerInvariant() == "all")
                return data; 

            return data.Where(prediator);
        }
     
        private void cmbData_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IndexChanged == null) return;
            IndexChanged?.Invoke();
        }


    }
}

