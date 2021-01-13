using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace QR_Code_Scanner.Droid
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class PageScan : ContentPage
    {
        List<string> mas = new List<string>();
        public PageScan()
        {
            InitializeComponent();
        }
        private async void btnScan_Clicked(object sender, EventArgs e)
        {

            var scan = new ZXingScannerPage();
            await Navigation.PushAsync(scan);
            scan.OnScanResult += (result) =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Navigation.PopAsync();
                    mas.Add(result.Text);
                    mycode.Text = "";
                    foreach (var item in mas)
                    {
                        mycode.Text += '\n' + item;
                    }

                });
            };

        }
        /*private void btnInsert_Clicked(object sender, EventArgs e)
        {
        }
        private void btnDel_Clicked(object sender, EventArgs e)
        {
        }*/
    }
}