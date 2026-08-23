using System;
 
using System.Windows.Forms;

namespace UI.Shared.Controllers
{
    public partial class ctrlClock : UserControl
    {
        
        private Label lblDateTime;
        private Timer timer = new Timer();

        public ctrlClock() {
            InitializeComponent();
         }
        private void ClockTime_Tick(object sender, EventArgs e) {

            UpdateDateTime();

        }
        private void UpdateDateTime() {
            lblDateTime.Text = DateTimeOffset.Now.ToString("dddd, dd MMMM yyyy   hh:mm:ss tt");
        }
        public void StartClock() {
            timer.Tick += ClockTime_Tick;
            timer.Interval = 1000;
            timer.Start();
            UpdateDateTime();
        }
        public void StopClock() { 
        
       
        
        } 
      }
}

