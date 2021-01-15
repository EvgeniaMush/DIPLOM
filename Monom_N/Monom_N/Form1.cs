using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using IronBarCode;
using QRCoder;
using MessagingToolkit.QRCode.Codec;
using MessagingToolkit.QRCode.Codec.Data;
using System.Drawing.Printing;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using Tulpep.NotificationWindow;
using System.Threading;
using System.Timers;
using System.Net;

namespace Monom_N
{
    public partial class Fnaim : Form
    {
        private PopupNotifier popup = null;
      
        SqlConnection sqlConnection;
        String ConnectionString;
        public Fnaim()
        {
            InitializeComponent();

            try
            {
                
                using (var client = new WebClient())
                using (client.OpenRead("http://google.com/generate_204")) ;


                //Создание конфиг. менеджера для работы с настройками подключения
                SqlConnectionStringBuilder csBuilder = new SqlConnectionStringBuilder(ConfigurationManager.ConnectionStrings["SQLServer"].ConnectionString);
                //имя сервера
                string ServerName = csBuilder.DataSource;
                //имя базы данных
                string DBName = csBuilder.InitialCatalog;
                string pass = "ITZI306a";
                string usi = "DB_A6A170_evgenia_admin";
                //строка подключения
                //  ConnectionString = "Data Source=" + ServerName + ";Initial Catalog=" + DBName + ";User Id="+usi+ "; Password=" + pass ;
                ConnectionString = "Data Source = SQL5060.site4now.net; Initial Catalog = DB_A6A170_evgenia; User ID = DB_A6A170_evgenia_admin; Password = ITZI306a";
                conn2(ConnectionString, tr1, comboBox1, "naim", "id_tr");
                conn3(ConnectionString, tr1, comboBox2, "naim", "id_tr");

                               
                comboBox4.Items.Add("0,5");
                comboBox4.Items.Add("0,6");
                comboBox4.Items.Add("1");
                comboBox4.Items.Add("1,5");
                comboBox4.Items.Add("1,6");
                comboBox4.Items.Add("2,5");

            }
            catch
            {


                MessageBox.Show("Error connection!");
                System.Environment.Exit(0);
            }
        }

        string tr1 = "Select * from list_tr";
        string tr2 = "Select * from list_m";

       
        public void conn2(string CS, string cmdT, ComboBox CB, string field1, string field2)
        {
            //создание экземпляра адаптера
            SqlDataAdapter Adapter = new SqlDataAdapter(cmdT, CS);
            //создание объекта DataSet (набор данных)
            DataSet ds = new DataSet();
            Adapter.Fill(ds, "Table");
            // привязка ComboBox к таблице БД
            CB.DataSource = ds.Tables["Table"];

            CB.DisplayMember = field1; //установка отображаемого в списке поля
            CB.ValueMember = field2; //установка ключевого поля

        }
        
        public void conn3(string CS, string cmdT, ComboBox CB, string field1, string field2)
        {
            //создание экземпляра адаптера
            SqlDataAdapter Adapter = new SqlDataAdapter(cmdT, CS);
            //создание объекта DataSet (набор данных)
            DataSet ds = new DataSet();
            Adapter.Fill(ds, "Table");
            // привязка ComboBox к таблице БД
            CB.DataSource = ds.Tables["Table"];

            CB.DisplayMember = field1; //установка отображаемого в списке поля
            CB.ValueMember = field2; //установка ключевого поля

        }

       


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                sqlConnection.Close();
        }

       public void conn(string CS, string cmdT, DataGridView dgv)
        {
            //создание экземпляра адаптера
            SqlDataAdapter Adapter = new SqlDataAdapter(cmdT, CS);
            //создание объекта DataSet (набор данных)
            DataSet ds = new DataSet();
            Adapter.Fill(ds, "Table");
            dgv.DataSource = ds.Tables["Table"].DefaultView;
        }
        public void conn4(string CS, string cmdT, DataGridView dgv)
        {
            //создание экземпляра адаптера
            SqlDataAdapter Adapter = new SqlDataAdapter(cmdT, CS);
            //создание объекта DataSet (набор данных)
            DataSet ds = new DataSet();
            Adapter.Fill(ds, "Table"); 
            dgv.DataSource = ds.Tables["Table"].DefaultView;
        }
        public void conn5(string CS, string cmdT)
        {
            //создание экземпляра адаптера
            SqlDataAdapter Adapter = new SqlDataAdapter(cmdT, CS);
            //создание объекта DataSet (набор данных)
            DataSet ds = new DataSet();
            Adapter.Fill(ds, "Table");
           // dgv.DataSource = ds.Tables["Table"].DefaultView;
        }


        private void fillByToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.list_trTableAdapter.FillBy(this.manomDataSet.list_tr);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

      

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string table = comboBox1.SelectedItem.ToString();
        }

        private void тепловые_районыToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.list_trTableAdapter.Тепловые_районы(this.manomDataSet.list_tr);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

           // string table = comboBox1.SelectedValue.ToString();
           // SqlDataReader sqlReader = null;
           // // SqlCommand command = new SqlCommand("SELECT * FROM tr4", sqlConnection);
           // string sql = "SELECT list_m.id_m AS ID, list_m.inv_n AS Номер, list_m.diam AS диаметр, list_m.ed_izm AS измерение, list_m.davlen AS давление, list_m.klass_t AS класс, list_m.data_p AS поверка " + 
           //  " FROM list_m INNER JOIN list_tr ON list_m.id_tr = list_tr.id_tr" + " WHERE list_m.id_tr= " + table;
           // //string sql = "SELECT list_m.id_m AS ID, list_m.inv_n AS Номер, list_m.diam AS диаметр, list_m.ed_izm AS измерение, list_m.davlen AS давление, list_m.klass_t AS класс, list_m.data_p AS поверка, list_m.id_tr" +
           //// " FROM list_m ORDER BY id_m ";
           // // MessageBox.Show(sql);
           // conn(ConnectionString, sql, dataGridView1);
           // int totalWidth = 0;

           
        }

       

        private void button2_Click(object sender, EventArgs e)
        {
            
            string tr = comboBox2.SelectedValue.ToString();
            string edIzm = comboBox3.Text;
            string data = dateTimePicker1.Value.ToString();
            SqlDataReader sqlReader = null;

            string sql = "SELECT list_m.id_m AS ID, list_m.inv_n AS Номер, list_m.diam AS диаметр, list_m.ed_izm AS измерение, list_m.davlen AS давление, list_m.klass_t AS класс, list_m.data_p AS поверка, list_m.id_tr, list_m.id_m " +
            " FROM list_m ";
            

                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = ConnectionString;
                //Теперь можно устанавливать соединение, вызывая метод Open объекта
                conn.Open();
                //создаем новый экземпляр SQLCommand
                SqlCommand cmd = conn.CreateCommand();
                //определяем тип SQLCommand=StoredProcedure
                cmd.CommandType = CommandType.StoredProcedure;
                //определяем имя вызываемой процедуры
                cmd.CommandText = "[ADD_MAN]";
                //создаем параметр
                cmd.Parameters.Add("@inv_n", SqlDbType.NVarChar, 50);
                //задаем значение параметра
                cmd.Parameters["@inv_n"].Value = textBox1.Text;
                //аналогично для всех параметров
                cmd.Parameters.Add("@diam", SqlDbType.NVarChar, 50);
                cmd.Parameters["@diam"].Value = textBox2.Text;

                cmd.Parameters.Add("@ed_izm", SqlDbType.NVarChar, 50);
                cmd.Parameters["@ed_izm"].Value = comboBox3.Text;

                cmd.Parameters.Add("@davlen", SqlDbType.NVarChar, 50);
                cmd.Parameters["@davlen"].Value = textBox3.Text;

                cmd.Parameters.Add("@klass_t", SqlDbType.Float);
                cmd.Parameters["@klass_t"].Value = comboBox4.Text;

                string dat = dateTimePicker1.Value.Month.ToString() + "." + dateTimePicker1.Value.Year.ToString();
                

                cmd.Parameters.Add("@data_p", SqlDbType.NVarChar, 20);
                cmd.Parameters["@data_p"].Value = dat;

                cmd.Parameters.Add("@kod_tr", SqlDbType.Int, 4);
                cmd.Parameters["@kod_tr"].Value = comboBox2.SelectedValue;

                cmd.Parameters.Add("@id_m", SqlDbType.Int, 4);
                cmd.Parameters["@id_m"].Direction = ParameterDirection.Output;

                cmd.Parameters.Add("@data_reg", SqlDbType.DateTime, 8);
                DateTime d= dateTimePicker1.Value;
                cmd.Parameters["@data_reg"].Value = d.Date;
           

                cmd.ExecuteScalar();

                //присовение переменной значения ID
                int id_m = Convert.ToInt32(cmd.Parameters["@id_m"].Value);
                  
                MessageBox.Show("Манометр добавлен!  ID= " + id_m.ToString(), "Добавление записей");
                textQRcode.Text = id_m.ToString();
                pic.Image = null;


            // toolStripProgressBar1.Value = 100;
            // toolStripStatusLabel1.Text = "Добавлено!";           

            // conn4(ConnectionString, sql, dataGridView2);
            

            conn.Close();

           
        }
        public string get_man()//поиск не поверенных манометров
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

            string result = "№ ТР   |   Инв. №   |  Давление";


            for (int i = 0; i < dsReport.Tables[0].Rows.Count; i++)
            {
                string inv_n = dsReport.Tables[0].Rows[i]["inv_n"].ToString();
                string davlen = dsReport.Tables[0].Rows[i]["davlen"].ToString();
                string id_tr = dsReport.Tables[0].Rows[i]["id_tr"].ToString();

                result += $"\n     {id_tr}      |   {inv_n}      |       {davlen} ";
            }

            return result;

            //Console.WriteLine(stringColumn);

        }
        int i = 0;
        private void Form1_Load(object sender, EventArgs e)
        {
            string table = comboBox1.SelectedValue.ToString();
            SqlDataReader sqlReader = null;
            // SqlCommand command = new SqlCommand("SELECT * FROM tr4", sqlConnection);
            //string sql1 = "SELECT list_m.id_m AS ID, list_m.inv_n AS Номер, list_m.diam AS диаметр, list_m.ed_izm AS измерение, list_m.davlen AS давление, list_m.klass_t AS класс, list_m.data_p AS поверка " +
            // " FROM list_m ";
            string sql = "SELECT list_m.id_m AS ID, list_m.inv_n AS Номер, list_m.diam AS Диаметр, list_m.ed_izm AS ЕдИзм, list_m.davlen AS Давление, list_m.klass_t AS класс, list_m.data_p AS Дата_поверки" +
            " FROM list_m ORDER BY id_m ";
            //  MessageBox.Show(sql);
            conn(ConnectionString, sql, dataGridView1);
            conn(ConnectionString, sql, dataGridView2);

            for (int i = 0; i < dataGridView2.ColumnCount - 1; i++)
            {
                dataGridView2.Columns[i].Width = 80;
            }

            int totalWidth = 0;


            //    Auto Resize the columns to fit the data
            foreach (DataGridViewColumn column in dataGridView2.Columns)
            {

                dataGridView2.Columns[column.Index].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                int widthCol = dataGridView2.Columns[column.Index].Width;
                dataGridView2.Columns[column.Index].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dataGridView2.Columns[column.Index].Width = widthCol;
                totalWidth = totalWidth + widthCol;
            }
            //
            dataGridView2.Width = totalWidth + 45;
            //    this.dataGridView2.Sort(this.dataGridView2.Columns["id_m"], ListSortDirection.Ascending);
            string data = get_man();



            //свойства для всплывающих уведомлений
            popup = new PopupNotifier();
            popup.Image = Properties.Resources.icon1;
            popup.ImageSize = new Size(96, 96);
            popup.Scroll = true;
            popup.Click += new EventHandler(button7_Click);

            popup.TitleText = "Ожидают поверки!!!";

            popup.ContentText = data;

            popup.TitleColor = Color.Red;
            //popup.HeaderColor

            ////вывод сообщения по времени
            //System.Timers.Timer tmr = new System.Timers.Timer(1000);
            //tmr.Elapsed += new ElapsedEventHandler(button3_Click);
            //tmr.Start();
            //MessageBox.Show("длжно сработать");

            //tmr.Stop();
            //tmr.Dispose();
            tabControl1.TabPages[2].Parent = null;

        }

       
      
        private void button3_Click(object sender, EventArgs e)
        {
            popup.Popup();
             
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            
        }

        private void btnGenerate_Click_1(object sender, EventArgs e)
        {
            {
                string qrtext = textQRcode.Text; //считываем текст из TextBox'a
                QRCodeEncoder encoder = new QRCodeEncoder(); //создаем объект класса QRCodeEncoder
                Bitmap qrcode = encoder.Encode(qrtext); // кодируем слово, полученное из TextBox'a (qrtext) в переменную qrcode. класса Bitmap(класс, который используется для работы с изображениями)
                pic.Image = qrcode as Image; // pictureBox выводит qrcode как изображение.

             }


            string querySelect = "SELECT id_m FROM list_m where id_m=" + textQRcode.Text;

            SqlConnection conn1 = new SqlConnection();
            conn1.ConnectionString = ConnectionString;
            //Теперь можно устанавливать соединение, вызывая метод Open объекта
            conn1.Open();

            SqlCommand commandSelect = new SqlCommand(querySelect, conn1);

            //получаем объект  для чтения табличного результата запроса SELECT
            SqlDataReader reader = commandSelect.ExecuteReader();

            //    int i = 0;
            while (reader.Read())
            {

                byte[] imageData = null;

                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = ConnectionString;
                //Теперь можно устанавливать соединение, вызывая метод Open объекта
                conn.Open();

                QRCodeEncoder encoder = new QRCodeEncoder(); //создаем объект класса QRCodeEncoder
                Bitmap qrcode = encoder.Encode(reader[0].ToString());
                MemoryStream stream = new MemoryStream();
                qrcode.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
                imageData = stream.ToArray();

                //текст запроса
                //   string commandText = "INSERT INTO report (screen, screen_format) VALUES(@screen, @screen_format)"
                string queryUpdate = "UPDATE list_m SET qr = @qr WHERE id_m =" + reader[0].ToString();

                SqlCommand commandUpdate = new SqlCommand(queryUpdate, conn);
                commandUpdate.Parameters.AddWithValue("@qr", (object)imageData);
                commandUpdate.ExecuteNonQuery();
            }
        }
        void printDoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            double cmToUnits = 100 / 2.54;
            e.Graphics.DrawImage(pic.Image, 0, 0, (float)(2 * cmToUnits), (float)(1.8 * cmToUnits)); 
        }

       
        private void button4_Click(object sender, EventArgs e)
        {
            // показываем окно настройки печати
            PrintDocument printDoc = new PrintDocument();
            PrintDialog dlg = new PrintDialog();
            dlg.Document = printDoc;
            if (dlg.ShowDialog() != DialogResult.Cancel)
            {
                printDoc.PrintPage += new PrintPageEventHandler(printDoc_PrintPage);
                printDoc.Print();
            }
        }

       
        public void btn_save_Click(object sender, EventArgs e)
        {


            string querySelect = "SELECT id_m FROM list_m where id_m="+ textQRcode.Text;

            SqlConnection conn1 = new SqlConnection();
            conn1.ConnectionString = ConnectionString;
            //Теперь можно устанавливать соединение, вызывая метод Open объекта
            conn1.Open();

            SqlCommand commandSelect = new SqlCommand(querySelect, conn1);

            //получаем объект  для чтения табличного результата запроса SELECT
            SqlDataReader reader = commandSelect.ExecuteReader();

            //    int i = 0;
            while (reader.Read())
            {

                byte[] imageData = null;

                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = ConnectionString;
                //Теперь можно устанавливать соединение, вызывая метод Open объекта
                conn.Open();

                QRCodeEncoder encoder = new QRCodeEncoder(); //создаем объект класса QRCodeEncoder
                Bitmap qrcode = encoder.Encode(reader[0].ToString());
                MemoryStream stream = new MemoryStream();
                qrcode.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
                imageData = stream.ToArray();

                //текст запроса
                //   string commandText = "INSERT INTO report (screen, screen_format) VALUES(@screen, @screen_format)"
                string queryUpdate = "UPDATE list_m SET qr = @qr WHERE id_m =" + reader[0].ToString();

                SqlCommand commandUpdate = new SqlCommand(queryUpdate, conn);
                commandUpdate.Parameters.AddWithValue("@qr", (object)imageData);
                commandUpdate.ExecuteNonQuery();
            }

         
        }

       

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            textQRcode.Text= dataGridView2[0, dataGridView2.CurrentRow.Index].Value.ToString();
            //выбор фото из базы

            string query_f = "select qr from list_m where qr is not null and id_m = " + dataGridView2[0, dataGridView2.CurrentRow.Index].Value;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConnectionString;
            conn.Open();
            SqlCommand commandSelect = new SqlCommand(query_f, conn);
            SqlDataReader reader = commandSelect.ExecuteReader();

           //  conn.Close();
            reader.Read();
            if (reader.HasRows)
            {
                try
                {
                    MemoryStream stream = new MemoryStream((byte[])reader[0]);
                    this.pic.Image = Image.FromStream(stream);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка!");
                }
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

      

       

        private void comboBox2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string table = comboBox2.SelectedValue.ToString();
            SqlDataReader sqlReader = null;
            // SqlCommand command = new SqlCommand("SELECT * FROM tr4", sqlConnection);
            string sql = "SELECT list_m.id_m AS ID, list_m.inv_n AS Номер, list_m.diam AS диаметр, list_m.ed_izm AS измерение, list_m.davlen AS давление, list_m.klass_t AS класс, list_m.data_p AS поверка, list_m.id_tr, list_m.id_m " +
             " FROM list_m INNER JOIN list_tr ON list_m.id_tr = list_tr.id_tr" + " WHERE list_m.id_tr= " + table;
            // MessageBox.Show(sql);
            conn(ConnectionString, sql, dataGridView2);
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            string table = comboBox2.SelectedValue.ToString();
            SqlDataReader sqlReader = null;
            // SqlCommand command = new SqlCommand("SELECT * FROM tr4", sqlConnection);
            string sql = "update list_m set data_reg = '2020/06/01'" ;
           // MessageBox.Show(sql);
            conn5(ConnectionString, sql);

        }

        private void button5_Click(object sender, EventArgs e)
        {
            //Открываем Excel
            Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
            ExcelApp.Application.Workbooks.Add(Type.Missing);
            ExcelApp.Columns.ColumnWidth = 15;

            ExcelApp.Cells[1, 1] = "Инвентарный номер";
            ExcelApp.Cells[1, 2] = "Диаметр";
            ExcelApp.Cells[1, 3] = "Ед.изм";
            ExcelApp.Cells[1, 4] = "Давление";
            ExcelApp.Cells[1, 5] = "Класс точности";
            ExcelApp.Cells[1, 6] = "Дата поверки";


            int r = dataGridView1.RowCount;
            
            int k = 0;
            progressBar1.Maximum = r;

            for (int i = 1; i <= r-1; i++)
              {
                
           //  Console.WriteLine("r = " + r);
                for (int j = 1; j < dataGridView1.ColumnCount; j++)
                {
                    if ( j != 6)
                    //Console.WriteLine("COlumn COunt: " + dataGridView1.ColumnCount);
                    ExcelApp.Cells[i+1,j].Value = dataGridView1[j, i-1].Value.ToString();
                    else ExcelApp.Cells[i + 1, j].Value = "'"+dataGridView1[j, i - 1].Value.ToString();
                
                }

            //      
            //for (int k = 0; k < 100; k++)
            //{
             progressBar1.Value = k;
            System.Threading.Thread.Sleep(100); k++;
            //}                
            }
            
            ExcelApp.Visible = true;
           
           // ExcelApp.Quit();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            tabPage3.Parent = tabControl1;
            tabControl1.SelectedTab = tabControl1.TabPages["TabPage3"];


            DateTime now = DateTime.Now;
          int y = Convert.ToInt32(now.ToString("yyyy"));
            int m = Convert.ToInt32(now.ToString("MM"));
          //  Console.WriteLine(y);

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConnectionString;
            //Теперь можно устанавливать соединение, вызывая метод Open объекта
            conn.Open();
            //создаем новый экземпляр SQLCommand
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
            //Console.WriteLine(y);
            SqlDataAdapter ReportAdapter = new SqlDataAdapter();
            ReportAdapter.SelectCommand = cmd;
            DataSet dsReport = new DataSet();
            ReportAdapter.Fill(dsReport, "Report");

            dataGridView3.DataSource = dsReport.Tables["Report"].DefaultView;
            dataGridView3.Columns[0].HeaderText = "ID";
            dataGridView3.Columns[1].HeaderText = "Инв.№";
            dataGridView3.Columns[2].HeaderText = "Диаметр";
            dataGridView3.Columns[3].HeaderText = "Ед.Изм";
            dataGridView3.Columns[4].HeaderText = "Давление";
            dataGridView3.Columns[5].HeaderText = "Класс точности";
            dataGridView3.Columns[6].HeaderText = "Дата Поверки";
            dataGridView3.Columns[7].HeaderText = "Тепловой район";
            dataGridView3.Columns[8].HeaderText = "QR код";
            dataGridView3.Columns[9].HeaderText = "Дата регистрации";
            Console.WriteLine(m);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
            popup.Popup(); 
            timer1.Start();
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            //string table = comboBox1.SelectedValue.ToString();
            //SqlDataReader sqlReader = null;
            //// SqlCommand command = new SqlCommand("SELECT * FROM tr4", sqlConnection);
            //string sql = "SELECT list_m.inv_n AS Номер, list_m.diam AS диаметр, list_m.ed_izm AS измерение, list_m.davlen AS давление, list_m.klass_t AS класс, list_m.data_p AS поверка, list_m.id_tr, list_m.id_m " +
            // " FROM list_m INNER JOIN list_tr ON list_m.id_tr = list_tr.id_tr" + " WHERE list_m.id_tr= " + table;
            //// MessageBox.Show(sql);
            //conn(ConnectionString, sql, dataGridView1);
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string table = comboBox1.SelectedValue.ToString();
            SqlDataReader sqlReader = null;
            // SqlCommand command = new SqlCommand("SELECT * FROM tr4", sqlConnection);
            string sql = "SELECT list_m.id_m AS ID, list_m.inv_n AS Номер, list_m.diam AS диаметр, list_m.ed_izm AS измерение, list_m.davlen AS давление, list_m.klass_t AS класс, list_m.data_p AS поверка " +
             " FROM list_m INNER JOIN list_tr ON list_m.id_tr = list_tr.id_tr" + " WHERE list_m.id_tr= " + table;
            //string sql = "SELECT list_m.id_m AS ID, list_m.inv_n AS Номер, list_m.diam AS диаметр, list_m.ed_izm AS измерение, list_m.davlen AS давление, list_m.klass_t AS класс, list_m.data_p AS поверка, list_m.id_tr" +
            // " FROM list_m ORDER BY id_m ";
            // MessageBox.Show(sql);
            conn(ConnectionString, sql, dataGridView1);
            int totalWidth = 0;
        }
    }
}

