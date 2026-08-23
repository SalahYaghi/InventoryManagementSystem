using HotelSystemUI.Login;
using InventorySystemUI.Main;
using System;
using System.ComponentModel;
using System.Windows.Forms;
using UI.Forms;

namespace UI
{
    internal static class Program
    {

        [STAThread]
        static void Main()
        {
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
            System.Windows.Forms.Application.Run(new frmLogin());
        }
    }
}

