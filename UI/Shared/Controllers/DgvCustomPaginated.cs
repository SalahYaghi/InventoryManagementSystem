using Contract.Responses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI.HttpClient;
using static HotelSystemUI.HttpClients.InventorySystemUI.HttpClients.Routes.Entities;

namespace UI.Shared.Controllers
{
    public partial class DgvCustomPaginated : UserControl
    {

        public string Status
        {
            set => this.lblStatus.Text = value;
        }


        public DgvCustom DgvCustom => dgvCustom;
        public event Func< int , int, Task<ApiResult<PaginatedList>>> LoadData;

        private int _totalPages = 0;
       
        private int _pageNumber = 1;
       
        private int _pageSize = 20;

        private int PageNumber { get {

                return _pageNumber;
            }
            set { 
            
                _pageNumber = value;
                lblPageInfo.Text = "Page " + _pageNumber.ToString();

            }
        }


        public DgvCustomPaginated()
        {
            InitializeComponent();
            SetupUI();
         }

        public void SetupUI() {

            dgvCustom.Grid.AutoGenerateColumns = true;

            cmbPageSize.Items.AddRange(new object[] { 10, 20, 50, 100 });
            cmbPageSize.SelectedItem = 20;

        }
        
        public void SetData<T>(IEnumerable<T> data) {

            dgvCustom.SetData<T>(data);
        }
        public void SubscribeToLoadData(Func<int, int, Task<ApiResult<PaginatedList>>> loadData) { 
        
            this.LoadData += loadData;
        }
        public async Task LoadDataGridViewData() {


            if (LoadData == null) return;


            lblStatus.Text = "Loading...";

            var result = await LoadData?.Invoke(_pageNumber, _pageSize);
            if (result.DataNotModified)
            {

                lblStatus.Text = "Data Not Modified";
            }
            else
            if (result.IsSuccess)
            {
                _totalPages = result.Data.TotalPages;

                lblStatus.Text = "Showing " + result.Data.PageSize + " product(s)";
            } else {
                lblStatus.Text = "Failed to show data";
            }


        }



        private  async void btnNext_Click(object sender, EventArgs e)
        {
            
            if (_totalPages == PageNumber )
            {
                return;
            }
            PageNumber++;
            await LoadDataGridViewData();
        }

        private async void btnPrevious_Click(object sender, EventArgs e)
        {
            if (PageNumber <= 1)
                return;

            PageNumber--;
            await LoadDataGridViewData();

        }



        private async void cmbPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPageSize.SelectedItem == null)
                return;

            _pageSize = Convert.ToInt32(cmbPageSize.SelectedItem);
            PageNumber = 1;
            await LoadDataGridViewData();
        }
    }
}

