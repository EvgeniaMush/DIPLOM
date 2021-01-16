using Plugin.Connectivity;
using Newtonsoft.Json;
using System.Net.Http;
using Plugin.Connectivity.Abstractions;
using QR_Code_Scanner.Droid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Data;
using System.Net;
using System.Collections.Specialized;
using System.Configuration;
//using Tulpep.NotificationWindow;
using System.Threading;

namespace QR_Code_Scanner
{
  
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    [XamlCompilation(XamlCompilationOptions.Compile)]

    
    public partial class MainPage : ContentPage
    {
        SqlConnection sqlConnection;
        String ConnectionString;

      
        public Button btnInsert_Clicked;
       // public EditText date_p;
        SqlXml sqlXml;
        Button ScanButn;

        List<string> mas = new List<string> ();
       // int[] mas = new int[1000];
        
        public MainPage()
        {

            InitializeComponent();
            string a = "Истек срок поверки";
            ConnectionString = "Data Source=SQL5060.site4now.net;Initial Catalog=DB_A6A170_evgenia;User Id=DB_A6A170_evgenia_admin; Password = ITZI306a";

            // всплывающее окно       
            ToolbarItem tb = new ToolbarItem
            {
                Text = "Истек срок поверки у манометра №",
                Order = ToolbarItemOrder.Default,
                Priority = 0,
                Icon = new FileImageSource
                {
                    File = "iconTool.png"
                }
            };
            tb.Clicked += async (s, e) =>
            {
                //pb_ProgressBar.IsVisible = true;

                //pb_ProgressBar.Progress = 0.8;

                //await Task.Run(() =>
                // {
                //     for (int i = 0; i < 1000; i++)
                //     {
                //         Thread.Sleep(2);
                //         pb_ProgressBar.Progress = Convert.ToDouble("00." + i.ToString());
                //     }
                // });
          
                string data = get_man();
                if (data != null)
                {
                    await DisplayAlert(a, data, "Закрыть");
                }
                else await DisplayAlert("Все манометры", "Поверены!", "Закрыть");
                };

               
            ToolbarItems.Add(tb);
        }


        //private static SqlConnection OpenSqlConnection(string connectionString)
        //{
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        connection.Open();
        //        Console.WriteLine("ServerVersion: {0}", connection.ServerVersion);
        //        Console.WriteLine("State: {0}", connection.State);

        //        return connection;
        //    }
        //}

      
        
            

        
        //Получение списка неповеренных манометров
        public string get_man()
        {
            
            DateTime now = DateTime.Now;
            int y = Convert.ToInt32(now.ToString("yyyy"));
            int m = Convert.ToInt32(now.ToString("MM"));
            //  Console.WriteLine(y);
            //Теперь можно устанавливать соединение, вызывая метод Open объекта

            //создаем новый экземпляр SQLCommand
            //SqlCommand cmd = OpenSqlConnection(ConnectionString).CreateCommand();

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConnectionString;
            conn.Open();

            SqlCommand cmd = conn.CreateCommand();
            //определяем тип SQLCommand=StoredProcedure          
            cmd.CommandType = CommandType.StoredProcedure;
            //определяем имя вызываемой процедуры
            cmd.CommandText = "[pov_m]";
            //создаем параметр
            cmd.Parameters.Add("@m", SqlDbType.Int, 4);
            //задаем значение параметра
            cmd.Parameters["@m"].Value = m;
            //аналогично для всех параметров
            cmd.Parameters.Add("@y", SqlDbType.Int, 4);
            cmd.Parameters["@y"].Value = y;

           

            SqlDataAdapter ReportAdapter = new SqlDataAdapter();
            ReportAdapter.SelectCommand = cmd;           
            DataSet dsReport = new DataSet();           
            ReportAdapter.Fill(dsReport); //  , "Report"

            string result = "№    | № ТР    |   Инв. №   |  Давление  ";


            for (int i = 0; i < dsReport.Tables[0].Rows.Count; i++)
            {
                string inv_n = dsReport.Tables[0].Rows[i]["inv_n"].ToString();
                string davlen = dsReport.Tables[0].Rows[i]["davlen"].ToString();
                string id_tr = dsReport.Tables[0].Rows[i]["id_tr"].ToString();
                string ed_izm = dsReport.Tables[0].Rows[i]["ed_izm"].ToString();

                result += $"\n    { (i+1).ToString() }  |  {id_tr}         |   {inv_n}       |       {davlen} {ed_izm}";
            }

            return result;


            //Console.WriteLine(stringColumn);

        }
        int i = 0;
        private async void btnScan_Clicked(object sender, EventArgs e)
        {
            

            var scan = new ZXingScannerPage();
            await Navigation.PushAsync(scan);
            scan.OnScanResult += (result) =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    i++;
                    await Navigation.PopAsync();
                     mas.Add(result.Text);
                       mycode.Text = "";
                   
                    foreach (var item in mas)
                    {
                        mycode.Text += '\n' + item;
                    }

                });
            };
            btninsert.IsVisible = true;
            btnDel.IsVisible = true;
            clear.IsVisible = true;

        }
        public void insert_dat(int item)
        {
          

                SqlConnection conn = new SqlConnection(ConnectionString);
          //  conn.ConnectionString = ConnectionString;
            //Теперь можно устанавливать соединение, вызывая метод Open объекта
            conn.Open();
            string dat = DateTime.Now.Month.ToString() + "." + DateTime.Now.Year.ToString();
            DateTime data = DateTime.Now;
            //создаем новый экземпляр SQLCommand
            SqlCommand cmd = conn.CreateCommand();
            //определяем тип SQLCommand=StoredProcedure
            cmd.CommandType = CommandType.StoredProcedure;
            //определяем имя вызываемой процедуры
            cmd.CommandText = "[insert_data]";
            //создаем параметр
            cmd.Parameters.Add("@id_m", SqlDbType.Int, 50);
            //задаем значение параметра
            cmd.Parameters["@id_m"].Value = item;
            //аналогично для всех параметров
            cmd.Parameters.Add("@data", SqlDbType.NVarChar, 255);
            cmd.Parameters["@data"].Value = dat;
            

            cmd.ExecuteNonQuery();
            DisplayAlert("Уведомление", "Дата поверки, успешно изменена!", "ОK");


        }

        
        void BtnInsert_Clicked(object sender, EventArgs e)
        {
            foreach (var item in mas)
                insert_dat(int.Parse(item));

            /*DateTime data = DateTime.Now;
            string dat = data.Month.ToString() + "." + data.Year.ToString();

            WebClient client = new WebClient();
            Uri uri = new Uri("http://172.30.1.72/updateDat.php");
            NameValueCollection parameters = new NameValueCollection();

            foreach (var item in mas)
            {
                parameters.Add("id", item);
                parameters.Add("data", dat);

                client.UploadValuesCompleted += client_UploadValuesCompleted;
                client.UploadValuesAsync(uri, parameters);
            }*/

        }

        void client_UploadValuesCompleted(object sender, UploadValuesCompletedEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void Btnc_clear(object sender, EventArgs e)
        {
            mycode.Text = "";
            mas.Clear();

        }
        public void dell_m(int item)
        {


            SqlConnection conn = new SqlConnection(ConnectionString);
            //  conn.ConnectionString = ConnectionString;
            //Теперь можно устанавливать соединение, вызывая метод Open объекта
            conn.Open();
            DateTime data = DateTime.Now;
            //создаем новый экземпляр SQLCommand
            SqlCommand cmd = conn.CreateCommand();
            //определяем тип SQLCommand=StoredProcedure
            cmd.CommandType = CommandType.StoredProcedure;
            //определяем имя вызываемой процедуры
            cmd.CommandText = "[delle]";
            //создаем параметр
            cmd.Parameters.Add("@id_m", SqlDbType.Int, 50);
            //задаем значение параметра
            cmd.Parameters["@id_m"].Value = item;
            
            
            cmd.ExecuteNonQuery();
            DisplayAlert("Уведомление", "Списание прошло успешно!", "ОK");


        }
        void btnDel_Clicked(object sender, EventArgs e)
        {
            foreach (var item in mas)
                dell_m(int.Parse(item));
        }
        
    }
}
