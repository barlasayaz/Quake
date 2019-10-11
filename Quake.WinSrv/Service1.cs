using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Quake.WinSrv
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            timer1.Start();
        }

        protected override void OnStop()
        {
            timer1.Stop();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            FileStream objFilestream = new FileStream("C://7tp_log.txt", FileMode.Append, FileAccess.Write);
            StreamWriter objStreamWriter = new StreamWriter((Stream)objFilestream);
            objStreamWriter.WriteLine("================================================================");
            objStreamWriter.WriteLine("******* ERROR *******");
            objStreamWriter.WriteLine("{0} ({1})", "Fatih", "Erdoğan");
            objStreamWriter.WriteLine("================================================================");
            objStreamWriter.Close();
            objFilestream.Close();


            //HttpClient client = new HttpClient();
            //client.BaseAddress = new Uri("http://127.0.0.1:39320/iotgateway/read?");
            //HttpResponseMessage response = client.GetAsync("http://127.0.0.1:39320/iotgateway/read?ids=RegionBursa1.Cinarcik_1.intensity&ids=RegionBursa2.Cinarcik2.intensity&ids=RegionBursa3.Mudanya1.intensity&ids=RegionBursa4.Mudanya2.intensity&ids=RegionIstanbul1.BuyukCekmece1.intensity&ids=RegionIstanbul2.BuyukCekmece2.intensity&ids=RegionIstanbul3.BuyukAda1.intensity&ids=RegionTekirdağ1.Liman1.intensity&ids=RegionTekirdağ2.Liman2.intensity&ids=RegionTekirdağ3.Sarkoy1.intensity&ids=.RegionTekirdağ4.Sarkoy2.intensity").Result;
            //if (response.IsSuccessStatusCode)
            //{
            //    var dto = response.Content.ReadAsStringAsync();
            //    var model = JsonConvert.DeserializeObject<RootObject>(dto.Result);
            //    foreach (var item in model.readResults)
            //    {
            //        if (!string.IsNullOrEmpty(item.v.ToString()))
            //            if (Convert.ToInt32(item.v) > 3)
            //                Logging.Save(item.id, item.v.ToString(), DateTime.Now);
            //    }
            //}
            //else
            //{
            //    FileStream objFilestream = new FileStream("C://7tp_log.txt", FileMode.Append, FileAccess.Write);
            //    StreamWriter objStreamWriter = new StreamWriter((Stream)objFilestream);
            //    objStreamWriter.WriteLine("================================================================");
            //    objStreamWriter.WriteLine("******* ERROR *******");
            //    objStreamWriter.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            //    objStreamWriter.WriteLine("================================================================");
            //    objStreamWriter.Close();
            //    objFilestream.Close();
            //}
        }

        private void GetQuakeStationData()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://127.0.0.1:39320/iotgateway/read?");
            HttpResponseMessage response = client.GetAsync("http://127.0.0.1:39320/iotgateway/read?ids=RegionBursa1.Cinarcik_1.intensity&ids=RegionBursa2.Cinarcik2.intensity&ids=RegionBursa3.Mudanya1.intensity&ids=RegionBursa4.Mudanya2.intensity&ids=RegionIstanbul1.BuyukCekmece1.intensity&ids=RegionIstanbul2.BuyukCekmece2.intensity&ids=RegionIstanbul3.BuyukAda1.intensity&ids=RegionTekirdağ1.Liman1.intensity&ids=RegionTekirdağ2.Liman2.intensity&ids=RegionTekirdağ3.Sarkoy1.intensity&ids=.RegionTekirdağ4.Sarkoy2.intensity").Result;
            if (response.IsSuccessStatusCode)
            {
                var dto = response.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<RootObject>(dto.Result);
                foreach (var item in model.readResults)
                {
                    Logging.Save(item.id, item.v.ToString(), DateTime.Now);
                }
            }
            else
            {
                FileStream objFilestream = new FileStream("C://7tp_log.txt", FileMode.Append, FileAccess.Write);
                StreamWriter objStreamWriter = new StreamWriter((Stream)objFilestream);
                objStreamWriter.WriteLine("================================================================");
                objStreamWriter.WriteLine("******* ERROR *******");
                objStreamWriter.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                objStreamWriter.WriteLine("================================================================");
                objStreamWriter.Close();
                objFilestream.Close();
            }
        }
    }

    public class ReadResult
    {
        public string id { get; set; }
        public bool s { get; set; }
        public string r { get; set; }
        public object v { get; set; }
        public object t { get; set; }
    }

    public class RootObject
    {
        public List<ReadResult> readResults { get; set; }
    }
}
