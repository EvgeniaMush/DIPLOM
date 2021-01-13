using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace QR_Code_Scanner.Droid
{
   public class Manom
    {
        public static class Connection
        {

            public static string ConnectionString = "Data Source=SQL5060.site4now.net;Initial Catalog=DB_A6A170_evgenia;User Id=DB_A6A170_evgenia_admin; Password = ITZI306a";

        }
        public string Inv_n { get; set; }
        public string Diam { get; set; }
        public string Ed_izm { get; set; }
        public string Davlen { get; set; }
        public string Klass_t { get; set; }
        public string Data_p { get; set; }
        public int Id_tr { get; set; }
       
  
         public Manom(string inv_n, string diam, string ed_izm, string davlen, string klass_t, string data_p, int id_tr)
         {
                Inv_n = inv_n;
                Diam = diam;
                Ed_izm = ed_izm;
                Davlen = davlen;
                Klass_t = klass_t;
                Data_p = data_p;
                Id_tr = id_tr;
         }
        public override string ToString()
        {
            return Inv_n + "Диаметр: " + Diam + "Ед.изм: " + Ed_izm + "Давление: " + Davlen + "Класс точности: " + Klass_t + "Дата поверки: " + Data_p + "Тепловой район: " + Id_tr;
        }
       
    }
}